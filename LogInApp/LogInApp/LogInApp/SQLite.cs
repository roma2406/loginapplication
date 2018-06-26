using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LogInApp.Interfaces;

namespace LogInApp
{
    public class SQLite : ISQLite
    {
        public SQLite() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = documentsPath + sqliteFilename;
            return path;
        }
    }
}
