using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace HutSoft.D3.CSVPoll
{
    class CSVPollBO
    {
        public CSVPollBO() { }

        internal List<string> GetAvailableDatabases(string serverInstance)
        {
            List<string> databases = new List<string>();
            try
            {
                string connectionString = GetConnectionString(serverInstance, "master");
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases WHERE name like '%_PnId'", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                databases.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return databases;
        }

        internal List<string> GetValuesInCsvFile(string csvFile)
        {
            try
            {
                if (csvFile == string.Empty) throw new Exception("Csv file must all be provided");
                if (!File.Exists(csvFile)) throw new Exception("Csv file does not exist");
                List<string> csvValues = new List<string>();
                using (var reader = new StreamReader(csvFile))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        csvValues.Add(values[0]);
                    }
                    csvValues.RemoveAt(0);
                }
                for (int i = 0; i < csvValues.Count; i++)
                {
                    csvValues[i] = csvValues[i].Replace("\"", "");
                }
                csvValues.RemoveAll(x => x == "");
                csvValues = csvValues.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                csvValues.Sort();
                return csvValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal List<string> GetValuesInDb(string serverInstance, string database)
        {
            List<string> existingValuesInDb = new List<string>();
            string sql = "SELECT b.PicklistValue FROM PnPPicklists a LEFT JOIN PnPPicklistValues b ON a.PnPID = b.PicklistId WHERE a.PicklistName = 'Part_Number'";
            try
            {
                string connectionString = GetConnectionString(serverInstance, database);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                existingValuesInDb.Add(dr[0].ToString());
                            }
                        }
                    }
                }
                return existingValuesInDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        internal void ImportCsvValuesToDB(string csvFile, string serverInstance, string database)
        {
            List<string> csvValues = GetValuesInCsvFile(csvFile);
            List<string> existingValuesInDb = GetValuesInDb(serverInstance, database);
            List<string> newCsvValues = csvValues.Except(existingValuesInDb, StringComparer.OrdinalIgnoreCase).ToList<string>();
            AddNewValuesToDb(serverInstance, database, newCsvValues);
        }

        internal void AddNewValuesToDb(string serverInstance, string database, List<string> valuesToBeInserted)
        {
            try
            {
                string connectionString = GetConnectionString(serverInstance, database);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    int pnPID;

                    string sqlGetPnPID = "SELECT PnPID FROM PnPPicklists WHERE PicklistName = 'Part_Number'";
                    using (SqlCommand cmdGetPnPID = new SqlCommand(sqlGetPnPID, con))
                    {
                        pnPID = Convert.ToInt32(cmdGetPnPID.ExecuteScalar());
                    }

                    foreach (string value in valuesToBeInserted)
                    {
                        int nextPnPID;

                        string sqlGetNextPnPID = "SELECT MAX(PnPID)+1 FROM PnPBase";
                        using (SqlCommand cmdGetNextPnPID = new SqlCommand(sqlGetNextPnPID, con))
                        {
                            nextPnPID = Convert.ToInt32(cmdGetNextPnPID.ExecuteScalar());
                        }

                        string sqlInsertPnPPicklistValues =
                            string.Format(
                                "INSERT INTO PnPPicklistValues (PicklistId, PicklistValue, PnPID) VALUES ({0}, '{1}', {2})",
                                pnPID,
                                value,
                                nextPnPID);
                        using (SqlCommand cmdInsertPnPPicklistValues = new SqlCommand(sqlInsertPnPPicklistValues, con))
                        {
                            cmdInsertPnPPicklistValues.ExecuteNonQuery();
                        }

                        string sqlInsertPnPBase =
                            string.Format(
                                "INSERT INTO PnPBase (PnPClassName, PnPID) VALUES ('{0}', {1})",
                                "PnPPicklistValues",
                                nextPnPID);
                        using (SqlCommand cmdInsertPnPBase = new SqlCommand(sqlInsertPnPBase, con))
                        {
                            cmdInsertPnPBase.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetConnectionString(string serverInstance, string database)
        {
            return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", serverInstance, database);
        }
    }
}
