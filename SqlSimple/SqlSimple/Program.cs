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
            var ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SqlSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var cnn = new SqlConnection(ConnectionString);

            var userStore = new UserStore(cnn);

            //TODO: buralari toplarsin. Bir anda cok is bitirmeye calisiyosun yapma. 
            //TODO: hizli ilerlemenin kimseye bir faydasi yok. Bu gercek bir proje yada odev degil. sikildigin zaman birakabilirsin. 
            //TODO: Adim adim tasklari takipi et simdilik 
            

            //var user = new User
            //{
            //    Houses = new List<House>()

            //};

            //var house = new House
            //{
            //    Address = "asfasf",

            //    User = new User
            //    {

            //        FullName = "Nasut Evren Kayali",
            //        Email = "evren.kayali@readify.net",


            //    }

            //};
            //var house1 = new House
            //{
            //    Address = "cccc",

            //    User = new User
            //    {

            //        FullName = "Haktan Enes Biçer",
            //        Email = "evren.kayali@readify.net",


            //    }

            //};

            //user.Houses.Add(house);
            //user.Houses.Add(house1);
            //userStore.Save(house);

            //userStore.Save(house1);

            //var firstUser = userStore.GetById(9);

            //Console.WriteLine(firstUser);
            //Console.WriteLine("******************");

            //var allUsers = userStore.GetAll();

            //foreach (var u in allUsers) Console.WriteLine(u);
        }
    }
}