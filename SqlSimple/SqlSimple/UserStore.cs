using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSimple
{
    public class UserStore
    {
        private readonly SqlConnection _connection;

        public UserStore(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Save(House house)
        {
            var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [User] (FullName, Email) 
                                                VALUES (@FullName, @Email);

                                                INSERT INTO [House] (UserId,Address)
                                                VALUES ((Select Id From [User] where FullName = @FullName),@Address) ", _connection);

            insertCommand.Parameters.AddWithValue("@FullName", house.User.FullName);
            insertCommand.Parameters.AddWithValue("@Email", house.User.Email);
            insertCommand.Parameters.AddWithValue("@Address", house.Address);

            _connection.Open();

            insertCommand.ExecuteNonQuery();

            _connection.Close();
        }
        public User GetById(int id)
        {
            var user = new User();
           // var house = new House();

            SqlCommand getByIdCommand = new SqlCommand(@"Select 
                                                  U.Id as Id,
                                                  U.FullName as Name,
                                                  U.Email as Email , 
                                                  H.Address as Address,
                                                  H.UserId as UserId
                                                  FROM [User] U , [House] H
                                                  where U.Id = @Id 
                                                  AND 
                                                  U.Id =H.UserId ",
                                                  _connection);

            getByIdCommand.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            using (var sqlReader = getByIdCommand.ExecuteReader())
            {
                var IdOrdinal = sqlReader.GetOrdinal("Id");
                var nameOrdinal = sqlReader.GetOrdinal("Name");
                var emailOrdinal = sqlReader.GetOrdinal("Email");
                var addressOrdinal = sqlReader.GetOrdinal("Address");
                var userIdOrdinal = sqlReader.GetOrdinal("UserId");

                sqlReader.Read();
        
                user.Id = sqlReader.GetInt32(IdOrdinal);
                user.FullName = sqlReader.GetString(nameOrdinal);
                user.Email = sqlReader.GetString(emailOrdinal);
                //house.Address= sqlReader.GetString(addressOrdinal);
                //house.Id = sqlReader.GetInt32(userIdOrdinal);

                //house.User = user;
                user.Houses.Add(new House { Id = sqlReader.GetInt32(userIdOrdinal), Address = sqlReader.GetString(addressOrdinal) , User = user });
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

                while(sqlReader.Read())
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
