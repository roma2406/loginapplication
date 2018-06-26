using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogInApp.Models
{
    public class User
    {
        [PrimaryKey]
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Guid RoleId { get; set; }

    }
}