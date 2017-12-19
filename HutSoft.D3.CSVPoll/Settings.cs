using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace HutSoft.D3.CSVPoll
{
    public class Settings
    {
        public Settings()
        {
            GetSettingsFromFile();
        }

        private void GetSettingsFromFile()
        {
            FilePath = string.Format("{0}\\Settings.xml", FileUtility.GetAssemblyDirectory());

            if (!File.Exists(FilePath))
                CreateSettingsFile();

            XmlReader reader = XmlReader.Create(FilePath);
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "CsvFile":
                            if (reader.Read())
                                CsvFile = reader.Value.Trim();
                            break;
                        case "ServerInstance":
                            if (reader.Read())
                                ServerInstance = reader.Value.Trim();
                            break;
                        case "Database":
                            if (reader.Read())
                                Database = reader.Value.Trim();
                            break;
                    }
                }
            }
            reader.Close();
            reader = null;
        }

        private void CreateSettingsFile()
        {
            Assembly assembly = GetType().Assembly;
            BinaryReader reader = new BinaryReader(assembly.GetManifestResourceStream("HutSoft.D3.CSVPoll.Settings.xml"));
            FileStream stream = new FileStream(FilePath, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            try
            {
                byte[] buffer = new byte[64 * 1024];
                int numread = reader.Read(buffer, 0, buffer.Length);

                while (numread > 0)
                {
                    writer.Write(buffer, 0, numread);
                    numread = reader.Read(buffer, 0, buffer.Length);
                }

                writer.Flush();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void Save()
        {
            SaveSettingsToFile();
        }

        private void SaveSettingsToFile()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };
            xmlWriterSettings.OmitXmlDeclaration = true;

            XmlWriter writer = XmlWriter.Create(FilePath, xmlWriterSettings);
            writer.WriteStartElement("Settings");

            writer.WriteElementString("CsvFile", CsvFile);
            writer.WriteElementString("ServerInstance", ServerInstance);
            writer.WriteElementString("Database", Database);

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
            writer = null;
        }

        internal string FilePath { get; set; }
        public string CsvFile { get; set; }
        public string ServerInstance { get; set; }
        public string Database { get; set; }
    }
}
