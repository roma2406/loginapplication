using System;
using System.Collections.Generic;
using System.Text;

namespace LogInApp.Services
{
    class DatabaseService
    {
        public BaseService Login = new BaseService();
        private DatabaseService() { }

        static DatabaseService instance;
        static readonly object PadLock = new object();
        public static DatabaseService Instance
        {
            get
            {
                lock (PadLock)
                {
                    if (instance == null)
                    {
                        instance = new DatabaseService();
                    }

                    return instance;
                }
            }
        }
    }
}
