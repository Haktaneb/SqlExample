using SqlSimple.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSimple.Repositories
{
    public class UserStore
    {
        private readonly SqlConnection _connection;

        public UserStore(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Save(User user)
        {
            var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [User] (FullName, Email) 
                                                VALUES (@FullName, @Email)", _connection);

            insertCommand.Parameters.AddWithValue("@FullName", user.FullName);
            insertCommand.Parameters.AddWithValue("@Email", user.Email);

            _connection.Open();

            insertCommand.ExecuteNonQuery();

            _connection.Close();
        }
        public User GetById(int id)
        {
            var user = new User();

            SqlCommand getByIdCommand = new SqlCommand(@"Select 
                                                  Id as Id,
                                                  FullName as Name,
                                                  Email as Email 
                                                  FROM [User] 
                                                  where Id = @Id",
                                                  _connection);

            getByIdCommand.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            using (var sqlReader = getByIdCommand.ExecuteReader())
            {
                var IdOrdinal = sqlReader.GetOrdinal("Id");
                var nameOrdinal = sqlReader.GetOrdinal("Name");
                var emailOrdinal = sqlReader.GetOrdinal("Email");

                sqlReader.Read();

                user.Id = sqlReader.GetInt32(IdOrdinal);
                user.FullName = sqlReader.GetString(nameOrdinal);
                user.Email = sqlReader.GetString(emailOrdinal);
            }

            _connection.Close();

            return user;
        }
        public List<User> GetAll()
        {
            var users = new List<User>();

            SqlCommand cmd = new SqlCommand(@"Select 
                                                  Id as Id,
                                                  FullName as Name,
                                                  Email as Email 
                                                  FROM [User]",
                                                  _connection);
            _connection.Open();

            using (var sqlReader = cmd.ExecuteReader())
            {
                var IdOrdinal = sqlReader.GetOrdinal("Id");
                var nameOrdinal = sqlReader.GetOrdinal("Name");
                var emailOrdinal = sqlReader.GetOrdinal("Email");

                while (sqlReader.Read())
                {
                    var user = new User();

                    user.Id = sqlReader.GetInt32(IdOrdinal);
                    user.FullName = sqlReader.GetString(nameOrdinal);
                    user.Email = sqlReader.GetString(emailOrdinal);

                    users.Add(user);
                }
            }

            _connection.Close();
            return users;
        }
    }
}
