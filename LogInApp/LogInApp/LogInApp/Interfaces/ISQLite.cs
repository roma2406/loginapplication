using System;
using System.Collections.Generic;
using System.Text;

namespace LogInApp.Interfaces
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
