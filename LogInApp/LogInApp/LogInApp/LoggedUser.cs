using System;
using System.Collections.Generic;
using System.Text;

namespace LogInApp
{
    public class LoggedUser
    {
        public Guid guid { get; set; }
        private LoggedUser() { }

        public static LoggedUser instance;
        static readonly object PadLock = new object();
        public static LoggedUser Instance
        {
            get
            {
                lock (PadLock)
                {
                    if (instance == null)
                    {
                        instance = new LoggedUser();
                    }

                    return instance;
                }
            }
        }
    }
}
