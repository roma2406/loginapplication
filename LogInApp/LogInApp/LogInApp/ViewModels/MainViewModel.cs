using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogInApp.Models;
using LogInApp.Services;

namespace LogInApp.ViewModels
{
    public class MainViewModel
    {
        public static MainViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainViewModel();
                }

                return _instance;
            }
        }
        private static MainViewModel _instance;

        public List<User> Users { get; private set; }

        public List<Role> Roles { get; private set; }


        protected MainViewModel()
        {
            Users = new List<User>();

            Roles = new List<Role>();
        }

        public void Init()
        {
            Users.Clear();
            var users = DatabaseService.Instance.Login.GetItems();
            if (users != null)
            {
                Users.AddRange(users);
            }

            Roles.Clear();
            var roles = DatabaseService.Instance.Login.GetRoleItems();
            if (roles != null)
            {
                Roles.AddRange(roles);
            }
        }
        #region ADD

        public void AddNewuser(User user)
        {
            DatabaseService.Instance.Login.database.Insert(user);
            Users.Add(user);
        }
        public void AddNewrole(Role role)
        {
            DatabaseService.Instance.Login.database.Insert(role);
            Roles.Add(role);
        }
        #endregion
        #region DELETE

        public void DeleteUser(User user)
        {
            DatabaseService.Instance.Login.database.Delete(user);
            var deletedUser = Users.FirstOrDefault(u => u.id == user.id);
            if (deletedUser != null)
                Users.Remove(deletedUser);
        }

        public void DeleteRole(Role role)
        {
            DatabaseService.Instance.Login.database.Delete(role);
            var deletedRole = Roles.FirstOrDefault(u => u.id == role.id);
            Roles.Remove(deletedRole);
        }
        #endregion
        #region CHANGE
        public void ChangeRole(Role role)
        {
            var deletedRole = Roles.FirstOrDefault(u => u.id == role.id);
            if (deletedRole != null)
                Roles.Remove(deletedRole);
            DatabaseService.Instance.Login.database.Update(role);
            Roles.Add(role);
        }
        public void ChangeUser(User user)
        {
            var deletedUser = Users.FirstOrDefault(u => u.id == user.id);
            if (deletedUser != null)
                Users.Remove(deletedUser);
            DatabaseService.Instance.Login.database.Update(user);
            Users.Add(user);
        }

        #endregion
        #region CHECK
        public Guid CheckUser(string login, string password)
        {
            //TODO перемутити без бд
            var result = DatabaseService.Instance.Login.GetUserGuid(login, password);
            if (result != Guid.Empty)
                LoggedUser.Instance.guid = result;
            return result;
        }

        public bool CheckRole()
        {
            //var user = DatabaseService.Instance.Login.GetUserByID(LoggedUser.instance.guid);
            var user = Users.FirstOrDefault(u => u.id == LoggedUser.instance.guid);
            var role = Roles.FirstOrDefault(u => u.id == user.RoleId);
            return role.administration;
        }

        #endregion

        //    AppDatabase.Instance.Login.GetUserLogin(NewUser.login)//guid
        //AppDatabase.Instance.Login.GetUserByID(Guid.Parse(extras))//user
        //AppDatabase.Instance.Login.GetRoleInfo(usertochange.RoleId);//string name

        public Guid GetRoleGuid(string name)
        {
            return DatabaseService.Instance.Login.GetRoleGuid(name); //roleid
        }
        public Guid GetUserGuid(string login)
        {
            return DatabaseService.Instance.Login.GetUserLogin(login);
        }
        public User GetUserById(string extras)
        {
            return DatabaseService.Instance.Login.GetUserByID(Guid.Parse(extras));
        }

        public string GetRoleName(Guid id)
        {
            return DatabaseService.Instance.Login.GetRoleInfo(id);
        }


    }
}