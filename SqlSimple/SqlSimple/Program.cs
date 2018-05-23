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
            var ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SqlSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var cnn = new SqlConnection(ConnectionString);

            var userStore = new UserStore(cnn);
            var houseStore = new HouseStore(cnn);

            var user = new User
            {
                FullName = "Nasut Evren Kayali",
                Email = "evren.kayali@readify.net",
            };

            var user2 = new User
            {
                FullName = "Haktan Enes Biçer",
                Email = "evren.kayali@readify.net",

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
                    FullName = "Haktan Enes Biçer",
                    Email = "evren.kayali@readify.net"
                }
        };

            userStore.Save(user);
            userStore.Save(user2);

            houseStore.Save(house);
            houseStore.Save(house2);

            var firstUser = userStore.GetById(1);
           
            Console.WriteLine(firstUser);
            Console.WriteLine("******************");
            var firstHouse = houseStore.getById(1);
            Console.WriteLine(firstHouse);
            Console.WriteLine("******************");

            var allUsers = userStore.GetAll();
            foreach (var u in allUsers) Console.WriteLine(u);

            var allHouses = houseStore.GetAll();
            foreach (var u in allHouses) Console.WriteLine(u);
        }
    }
}