﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HutSoft.D3.CSVPoll
{
    public partial class CSVPollUI : Form
    {
        Settings _settings;
        CSVPollBO _csvPollBO;

        public CSVPollUI()
        {
            InitializeComponent();
            _csvPollBO = new CSVPollBO();
            _settings = new Settings();
        }

        private void CSVPoll_Load(object sender, EventArgs e)
        {
            PopulateFromSettings();
            if (txtServerInstance.Text == string.Empty)
            {
                txtServerInstance.Text = "SQL01\\BluePnid";
            }
            btnRefreshDatabases.Focus();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsEditor editor = new SettingsEditor(_settings);
            editor.ShowDialog();
            if (editor.IsDirty)
            {
                _settings = editor.Settings;
                _settings.Save();
                PopulateFromSettings();
            }
            editor.Dispose();
        }

        private void PopulateFromSettings()
        {
            if (_settings.CsvFile != string.Empty && _settings.CsvFile != null)
                txtCsvFile.Text = _settings.CsvFile;
            if (_settings.ServerInstance != string.Empty && _settings.ServerInstance != null)
                txtServerInstance.Text = _settings.ServerInstance;
            if (_settings.Database != string.Empty && _settings.Database != null)
            {
                cboAvailableDatabases.Items.Add(_settings.Database);
                cboAvailableDatabases.SelectedItem = cboAvailableDatabases.Items[0];
            }
        }

        private void btnBrowseCsvFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = FileUtility.GetAssemblyDirectory();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtCsvFile.Text = openFileDialog1.FileName;
        }

        private void btnRefreshDatabases_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefreshDatabases.Enabled = false;
                cboAvailableDatabases.Items.Clear();
                List<string> databases = _csvPollBO.GetAvailableDatabases(txtServerInstance.Text);
                foreach (string database in databases)
                    cboAvailableDatabases.Items.Add(database);
                if (cboAvailableDatabases.Items.Count > 0)
                    cboAvailableDatabases.SelectedItem = cboAvailableDatabases.Items[0];
                else
                    MessageBox.Show("No Databases found ending in '_PnId'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnRefreshDatabases.Enabled = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                lstExistingCsvEntries.Items.Clear();
                lstNewCsvEntries.Items.Clear();

                btnPreview.Enabled = false;
                List<Picklist> csvValues = new List<Picklist>();
                csvValues = _csvPollBO.GetValuesInCsvFile(txtCsvFile.Text);
                if (csvValues.Count > 0)
                {
                    List<Picklist> existingValuesInDb = _csvPollBO.GetValuesInDb(txtServerInstance.Text, cboAvailableDatabases.Text);

                    List<Picklist> existingCsvValues = csvValues.Intersect(existingValuesInDb, new PicklistComparer()).ToList<Picklist>();
                    List<Picklist> newCsvValues = csvValues.Except(existingValuesInDb, new PicklistComparer()).ToList<Picklist>();

                    foreach (Picklist picklist in existingCsvValues)
                        lstExistingCsvEntries.Items.AddRange(new object[] { string.Format("{0}, {1}", picklist.Name, picklist.Value) });

                    foreach (Picklist picklist in newCsvValues)
                        lstNewCsvEntries.Items.AddRange(new object[] { string.Format("{0}, {1}", picklist.Name, picklist.Value) });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnPreview.Enabled = true;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                btnImport.Enabled = false;
                _csvPollBO.ImportCsvValuesToDB(txtCsvFile.Text, txtServerInstance.Text, cboAvailableDatabases.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnImport.Enabled = true;
            }
        }
    }
}
