using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace LogInApp.Models
{
    public class Role
    {
        [PrimaryKey]
        public Guid id { get; set; }
        public string name { get; set; }
        public bool administration { get; set; }
    }
}