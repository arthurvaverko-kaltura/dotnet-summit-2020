using ApiApp.Models;
using KLogMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace ApiApp.DAL
{
    public class UsersManager
    {
        private static readonly KLogger _Logger = new KLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        private static IEnumerable<User> _UserStore = new List<User>
        {
            new User(1, "Scott","Lang"),
            new User(2, "Oliver","Queen"),
            new User(3, "Natasha","Romanova"),
            new User(4, "Bruce","Wayne"),
            new User(5, "Katherine","Kane"),
            new User(6, "Barbara","Gordon"),
            new User(7, "Clark","Kent"),
            new User(8, "Cara","Denvers"),
            new User(9, "Arthur","Curry"),
            new User(10, "Peter","Parker"),
        };

        public static IEnumerable<User> GetUsers()
        {
            _Logger.Info("Please wait fetching users...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return _UserStore;
        }
    }
}