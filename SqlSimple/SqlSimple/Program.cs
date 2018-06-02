using SqlSimple.Models;
using SqlSimple.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlSimple
{
    public class Program
    {

        public static void Main()
        {
            var ConnectionString = @"Data Source=HAKTANENESB8201\SQLEXPRESS;Initial Catalog=SqlSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConnectionString);

            var cnn = new SqlConnection(builder.ConnectionString);

            var userStore = new UserStore(cnn);
            var houseStore = new HouseStore(cnn);

            var user = new User
            {
                FullName = "Nasut Evren Kayali",
                Email = "evren.kayali@readify.net",
                Phone = "3234",
                TC = 1
            };

            var user2 = new User
            {
                FullName = "Haktan Enes Biçer",
                Email = "evren.kayali@readify.net",
                Phone = "CXZ",
                TC = 2

            };
            var user3 = new User
            {
                FullName = "asfasf",
                Email = "easfasfnet",
                Phone = "3234",
                TC = 3
            };

            var house = new House
            {
                Address = "AAAAAAAA",
                Owner = new User
                {
                    FullName = "Nasut Evren Kayali",
                    Email = "evren.kayali@readify.net",
                }
            };

            var house2 = new House
            {
                Address = "BBBBBB",
                Owner= new User
                {
                    FullName = "Deneme",
                    Email = "Deneme@readify.net",
                    TC = 3
                }
        };

           //  userStore.Save(user);
            // userStore.Save(user2);
           //userStore.Save(user3);

            //houseStore.Save(house);
          //  houseStore.Save(house2);

          //  var firstUser = userStore.GetByTc(1);
            
           
          //  Console.WriteLine(firstUser);
            Console.WriteLine("******************");
            var firstHouse = houseStore.getById(3);
            Console.WriteLine(firstHouse);
            Console.WriteLine("******************");

            //var allUsers = userStore.GetAll();
            //foreach (var u in allUsers) Console.WriteLine(u);

            var allHousesNotOwnedHouses = houseStore.GetAll();
            foreach (var u in allHousesNotOwnedHouses) Console.WriteLine(u);

            var allHousesOwnedHouses = houseStore.GetAllOwnedHouses();
            foreach (var u in allHousesOwnedHouses) Console.WriteLine(u);

        }
    }
}