using MySql.Data.MySqlClient;

namespace Barkacs.Properties
{


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {

        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        
        
        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}

namespace Barkacs
{
    public class Database
    {
        public static MySqlConnection kapcsolat;
        public static MySqlCommand parancs;
        public static MySqlDataReader eredmeny;
        public static MySqlDataAdapter adapter;

        public static void kapcsol()
        { 
            string connStr = "server=localhost; database=barkacsbolt;" + 
                             "uid=root; character set=utf8";
            kapcsolat = new MySqlConnection(connStr);
            kapcsolat.Open();
            parancs = kapcsolat.CreateCommand();
        }

        public static void kapcsolatBont()
        {
            kapcsolat.Close();
        }
    }
}
