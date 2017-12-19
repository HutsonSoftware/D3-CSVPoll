using System;
using System.Windows.Forms;

namespace HutSoft.D3.CSVPoll
{
    internal partial class SettingsEditor : Form
    {
        public Settings Settings { get; set; }
        public bool IsDirty { get; set; }

        public SettingsEditor(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
        }

        private void SettingsEditor_Load(object sender, EventArgs e)
        {
            txtCsvFile.Text = Settings.CsvFile;
            txtServerInstance.Text = Settings.ServerInstance;
            txtDatabase.Text = Settings.Database;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()
        {
            if (Settings.CsvFile != txtCsvFile.Text)
            {
                Settings.CsvFile = txtCsvFile.Text;
                IsDirty = true;
            }
            if (Settings.ServerInstance != txtServerInstance.Text)
            {
                Settings.ServerInstance = txtServerInstance.Text;
                IsDirty = true;
            }
            if (Settings.Database != txtDatabase.Text)
            {
                Settings.Database = txtDatabase.Text;
                IsDirty = true;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
