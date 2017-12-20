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
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return databases;
        }

        internal List<Picklist> GetValuesInCsvFile(string csvFile)
        {
            try
            {
                if (csvFile == string.Empty) throw new Exception("Csv file must all be provided");
                if (!File.Exists(csvFile)) throw new Exception("Csv file does not exist");
                List<Picklist> csvValues = new List<Picklist>();
                using (var reader = new StreamReader(csvFile))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        Picklist picklist = new Picklist(values[1], values[0]);
                        csvValues.Add(picklist);
                    }
                    csvValues.RemoveAt(0);
                }
                csvValues.RemoveAll(x => x.Value == "");
                csvValues = csvValues.Where(s => !string.IsNullOrWhiteSpace(s.Value)).ToList();
                csvValues = csvValues.Distinct(new PicklistComparer()).ToList();
                csvValues = csvValues.OrderBy(x => x.Name).ThenBy(x => x.Value).ToList();
                return csvValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal List<Picklist> GetValuesInDb(string serverInstance, string database)
        {
            List<Picklist> existingValuesInDb = new List<Picklist>();
            string sql = "SELECT a.PicklistName, b.PicklistValue FROM PnPPicklists a LEFT JOIN PnPPicklistValues b ON a.PnPID = b.PicklistId";
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
                                Picklist picklist = new Picklist(dr["PicklistName"].ToString(), dr["PicklistValue"].ToString());
                                existingValuesInDb.Add(picklist);
                            }
                        }
                    }
                    con.Close();
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
            List<Picklist> csvValues = GetValuesInCsvFile(csvFile);
            List<Picklist> existingValuesInDb = GetValuesInDb(serverInstance, database);

            List<Picklist> newCsvValues = csvValues.Except(existingValuesInDb, new PicklistComparer()).ToList<Picklist>();

            AddNewValuesToDb(serverInstance, database, newCsvValues);
        }

        internal void AddNewValuesToDb(string serverInstance, string database, List<Picklist> picklistsToBeInserted)
        {
            try
            {
                string sqlGetPnPID = "SELECT PnPID FROM PnPPicklists WHERE PicklistName = '{0}'";
                string sqlInsertPnPPicklists = "INSERT INTO PnPPicklists (DisplayName, PicklistName, PicklistType, PnPID) VALUES ('', '{0}', 'Regular', {1})";

                string sqlGetNextPnPID = "SELECT MAX(PnPID)+1 FROM PnPBase";
                string sqlInsertPnPBase = "INSERT INTO PnPBase (PnPClassName, PnPID) VALUES ('{0}', {1})";

                string sqlInsertPnPPicklistValues = "INSERT INTO PnPPicklistValues (PicklistId, PicklistValue, PnPID) VALUES ({0}, '{1}', {2})";

                string connectionString = GetConnectionString(serverInstance, database);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    foreach (Picklist picklist in picklistsToBeInserted)
                    {
                        int pnPID = 0;
                        int nextPnPID = 0;
                        
                        using (SqlCommand cmdGetPnPID = new SqlCommand(string.Format(sqlGetPnPID, picklist.Name), con))
                        {
                            object obj = cmdGetPnPID.ExecuteScalar();
                            if (obj != null)  //picklist.Name may not exist in PnPPicklists
                                pnPID = Convert.ToInt32(obj);
                        }

                        if (pnPID == 0) //picklist.Name doesn't exist in PnPPicklists, so add it
                        {
                            using (SqlCommand cmdGetNextPnPID = new SqlCommand(sqlGetNextPnPID, con))
                                nextPnPID = Convert.ToInt32(cmdGetNextPnPID.ExecuteScalar());

                            using (SqlCommand cmdInsertPnPBase = new SqlCommand(string.Format(sqlInsertPnPBase, "PnPPicklists", nextPnPID), con))
                                cmdInsertPnPBase.ExecuteNonQuery();

                            using (SqlCommand cmdInsertPnPPicklistValues = new SqlCommand(string.Format(sqlInsertPnPPicklists, picklist.Name, nextPnPID), con))
                                cmdInsertPnPPicklistValues.ExecuteNonQuery();

                           
                            pnPID = nextPnPID;
                        }

                        using (SqlCommand cmdGetNextPnPID = new SqlCommand(sqlGetNextPnPID, con))
                            nextPnPID = Convert.ToInt32(cmdGetNextPnPID.ExecuteScalar());

                        using (SqlCommand cmdInsertPnPBase = new SqlCommand(string.Format(sqlInsertPnPBase, "PnPPicklistValues", nextPnPID), con))
                            cmdInsertPnPBase.ExecuteNonQuery();

                        using (SqlCommand cmdInsertPnPPicklistValues = new SqlCommand(string.Format(sqlInsertPnPPicklistValues, pnPID, picklist.Value, nextPnPID), con))
                            cmdInsertPnPPicklistValues.ExecuteNonQuery();
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
