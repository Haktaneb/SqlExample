using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlSimple
{
    public class Program
    {

        public static void Main()
        {
            var ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SqlSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var cnn = new SqlConnection(ConnectionString);

            var userStore = new UserStore(cnn);

            var user = new User
            {
                FullName = "Nasut Evren Kayali",
                Email = "evren.kayali@readify.net"
            };

            var user2 = new User
            {
                FullName = "Erhan Cakirman",
                Email = "evren.kayali@readify.net"
            };

            userStore.Save(user);

            userStore.Save(user2);

            var firstUser = userStore.GetById(1);

            Console.WriteLine(firstUser);
            Console.WriteLine("******************");

            var allUsers = userStore.GetAll();

            foreach (var u in allUsers) Console.WriteLine(u);
        }
    }
}