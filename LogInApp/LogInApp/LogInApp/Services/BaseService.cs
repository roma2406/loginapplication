using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogInApp.Models;
using LogInApp;

namespace LogInApp.Services
{
    public class BaseService
    {
        public SQLiteConnection database;
        public BaseService()
        {
            var sa = new SQLite();
            var path = sa.GetDatabasePath(Config.DB_CONECTION_STRING);
            database = new SQLiteConnection(path);
            database.CreateTable<Role>();
            database.CreateTable<User>();
            //var admin_role = new Role { id = Guid.NewGuid(), name = "Admin", administration  = true};
            //var user_role = new Role { id = Guid.NewGuid(), name = "User", administration = false};
            //database.InsertOrReplace(admin_role);
            //database.InsertOrReplace(user_role);

            //var admin = new User { RoleId = GetRoleGuid("Admin"), name = "admin", surname = "admin", login = "admin", password = "admin", id = Guid.NewGuid() };
            //var user = new User { RoleId = GetRoleGuid("User"), name = "user", surname = "user", login = "user", password = "user", id = Guid.NewGuid() };
            //database.InsertOrReplace(admin);
            //database.InsertOrReplace(user);
        }

        #region User
        public IList<User> GetItems()
        {
            //return (from i in database.Table<User>() select i).ToList();

            return database.Table<User>().ToList();
        }
        public IEnumerable<string> GetUserNames()
        {
            return database.Table<User>().ToList().Select(u => u.name);
        }
        public string[] GetUserInfo(Guid UserID)
        {
            var user = GetUserByID(UserID);
            var UserInfo = new string[4];
            UserInfo[0] = Config.NAME_STRING + user.name;
            UserInfo[1] = Config.SURNAME_STRING + user.surname;
            UserInfo[2] = Config.LOGIN_STRING + user.login;
            UserInfo[3] = Config.ROLE_STRING + GetRoleInfo(user.RoleId);


            //Result = database.Table<User>().//.ToArray();
            return UserInfo;
        }
        public Guid GetUserGuid(string login, string password)
        {
            // var res = GetItems();

            var user = database.Table<User>().FirstOrDefault(er => er.login == login && er.password == password);
            return user != null ? user.id : Guid.Empty;
        }
        public User GetUserByID(Guid id)
        {
            return database.Table<User>().FirstOrDefault(er => er.id == id);//Get<User>(id);
        }
        public void DeleteUser(Guid id)
        {
            database.Delete<User>(id);
            return;
        }
        public Guid GetUserLogin(string login)
        {
            // var res = GetItems();

            var user = database.Table<User>().FirstOrDefault(er => er.login == login);
            return user != null ? user.id : Guid.Empty;
        }
        #endregion

        #region Role
        public IEnumerable<Role> GetRoleItems()
        {
            //return (from i in database.Table<Role>() select i).ToList();

            return database.Table<Role>().ToList();
        }

        public IEnumerable<string> GetRoleNames()
        {
            return database.Table<Role>().ToList().Select(u => u.name);
        }
        public Guid GetRoleGuid(string name)
        {
            var user = database.Table<Role>().FirstOrDefault(er => er.name == name);
            return user != null ? user.id : Guid.Empty;
        }
        public string GetRoleInfo(Guid RoleID)
        {
            var user = GetUserByID(RoleID);
            var roleInfo = new string(GetRoleObject(RoleID).name);
            return roleInfo;
        }
        public Role GetRoleObject(Guid id)
        {
            return database.Table<Role>().FirstOrDefault(er => er.id == id);
        }
        public void DeleteRole(Guid Id)
        {
            database.Delete<Role>(Id);
        }
        #endregion
    }
}