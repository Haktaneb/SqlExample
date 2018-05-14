using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlSimple
{
    class Program
    {
        static string ConnectionString = "Data Source=HAKTANENESB8201\\SQLEXPRESS;Initial Catalog=LocalDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static SqlConnection cnn = new SqlConnection(ConnectionString);
        static SqlDataReader SqlReader = null;
        static SqlCommand cmd;
        static User Users = new User();
        static List<User> UserList = new List<User>();


        static void Main(string[] args)
        {
            int choice ;
            
            Console.WriteLine("Which function would you call\n");
            Console.WriteLine("Add User -----> 1\n");
            Console.WriteLine("Get User İnformation by Id -----> 2 \n");
            Console.WriteLine("Get All User İnformation -----> 3 \n");
            choice = Int32.Parse(Console.ReadLine());
            Console.WriteLine("\n\n");
            while (choice != 0)
            {
               

                switch (choice)
                {
                    case 1:

                       Console.WriteLine("Id\n");
                        Users.Id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Full Name \n ");
                        Users.FullName = Console.ReadLine();
                        Console.WriteLine("Email \n ");
                        Users.Email = Console.ReadLine();

                        AddUser(Users.Id, Users.FullName, Users.Email);
                        break;
                    case 2:
                        Console.WriteLine("Id\n ");
                        Users.Id = Int32.Parse(Console.ReadLine());
                        Users = GetById(Users.Id);
                        Console.WriteLine("Id = {0}  Full Name = {1}   Email  =  {2} \n", Users.Id, Users.FullName, Users.Email);
                        break;

                    case 3:

                        GetAll();
                        foreach (var user in UserList)
                        {
                            Console.WriteLine("Id = {0}  Full Name = {1}   Email  =  {2} \n", user.Id, user.FullName, user.Email);
                        }
                       
                        break;

                    default:
                        Console.WriteLine("Wrong Choice \n");
                        break;
                }
                Console.WriteLine("Which function would you call\n");
                Console.WriteLine("Add User -----> 1\n");
                Console.WriteLine("Get User İnformation by Id -----> 2 \n");
                Console.WriteLine("Get All User İnformation -----> 3 \n");
                choice = Int32.Parse(Console.ReadLine());
            }
            
           

        }
            

           

            



        

       static public void AddUser(int Id,string FullName,string Email)

        {
            
            try
            {
               
                
                cmd = new SqlCommand("Select * from UserState where Id='"+Id+"'", cnn);
                cnn.Open();
               
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    cmd = new SqlCommand("INSERT INTO UserState (Id,FullName,Email) VALUES (@Id,@FullName,@Email)", cnn);
                   
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
                else
                {
                  cmd = new SqlCommand("UPDATE kisiler SET Id=@Id WHERE Id=@Id", cnn);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
               
            }
            catch (Exception)
            {
                throw;
            }

        }

        static public User GetById(int id)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("Select * from UserState where Id='" + id + "'", cnn);
                cnn.Open();
                SqlReader = cmd.ExecuteReader();
                SqlReader.Read();
                Users.Id = Int32.Parse(SqlReader[0].ToString());
                Users.FullName = SqlReader[1].ToString();
                Users.Email = SqlReader[2].ToString();
                cnn.Close();
                return Users;
            }
            catch (Exception )
            {

                throw;
            }
           

        }

        static public List<User> GetAll()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("Select * from UserState", cnn);
                cnn.Open();
                SqlReader = cmd.ExecuteReader();
                while (SqlReader.Read())
                {
                    User Users = new User();
                    Users.Id = Int32.Parse(SqlReader[0].ToString());
                    Users.FullName = SqlReader[1].ToString();
                    Users.Email = SqlReader[2].ToString();
                    UserList.Add(Users);


                }

                cnn.Close();
                return UserList;
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
