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

            var result = GetById(user.Id);

            _connection.Open();          
            if (result == null)
            {
                var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [User] (FullName, Email,Phone,TC) 
                                                VALUES (@FullName, @Email, @Phone, @TC)", _connection);

                insertCommand.Parameters.AddWithValue("@FullName", user.FullName);
                insertCommand.Parameters.AddWithValue("@Email", user.Email);
                insertCommand.Parameters.AddWithValue("@Phone", user.Phone);
                insertCommand.Parameters.AddWithValue("@TC", user.TC);
                insertCommand.ExecuteNonQuery();

            }
            else
            {
                var updateCommand = new SqlCommand(@"UPDATE [User] 
                                                    SET (FullName, Email,Phone,TC)
                                                    VALUES (@FullName, @Email, @Phone, @TC) 
                                                    Where Id = @Id", _connection);

                updateCommand.Parameters.AddWithValue("@FullName", user.FullName);
                updateCommand.Parameters.AddWithValue("@Email", user.Email);
                updateCommand.Parameters.AddWithValue("@Phone", user.Phone);
                updateCommand.Parameters.AddWithValue("@TC", user.TC);
                updateCommand.Parameters.AddWithValue("@Id", user.Id);
                updateCommand.ExecuteNonQuery();
            }
           _connection.Close();
        }
        public User GetById(int id)
        {
            
            var user = new User();

            SqlCommand getByIdCommand = new SqlCommand(@"Select 
                                                  Id as Id,
                                                  FullName as Name,
                                                  Email as Email,
                                                  Phone as Phone,
                                                  TC as Tc      
                                                  FROM [User] 
                                                  where Id = @Id",
                                                  _connection);

            getByIdCommand.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            using (var sqlReader = getByIdCommand.ExecuteReader())
            {
                if (sqlReader.HasRows)
                {
                    var IdOrdinal = sqlReader.GetOrdinal("Id");
                    var nameOrdinal = sqlReader.GetOrdinal("Name");
                    var emailOrdinal = sqlReader.GetOrdinal("Email");
                    var phoneOrdinal = sqlReader.GetOrdinal("Phone");
                    var tcOrdinal = sqlReader.GetOrdinal("Tc");
                    sqlReader.Read();

                    user.Id = sqlReader.GetInt32(IdOrdinal);
                    user.FullName = sqlReader.GetString(nameOrdinal);
                    user.Email = sqlReader.GetString(emailOrdinal);
                    user.Phone = sqlReader.GetString(phoneOrdinal);
                    user.TC = sqlReader.GetInt32(tcOrdinal);
                    _connection.Close();
                    return user;
                }
                
                _connection.Close();
                return null;
            }
           
        }
        public User GetByTc(int Tc)
        {

            var user = new User();

            SqlCommand getByIdCommand = new SqlCommand(@"Select 
                                                  Id as Id,
                                                  FullName as Name,
                                                  Email as Email,
                                                  Phone as Phone,
                                                  TC as Tc      
                                                  FROM [User] 
                                                  where TC = @Tc",
                                                  _connection);

            getByIdCommand.Parameters.AddWithValue("@Tc", Tc);
            _connection.Open();
            using (var sqlReader = getByIdCommand.ExecuteReader())
            {
                if (sqlReader.HasRows)
                {
                    var IdOrdinal = sqlReader.GetOrdinal("Id");
                    var nameOrdinal = sqlReader.GetOrdinal("Name");
                    var emailOrdinal = sqlReader.GetOrdinal("Email");
                    var phoneOrdinal = sqlReader.GetOrdinal("Phone");
                    var tcOrdinal = sqlReader.GetOrdinal("Tc");
                    sqlReader.Read();

                    user.Id = sqlReader.GetInt32(IdOrdinal);
                    user.FullName = sqlReader.GetString(nameOrdinal);
                    user.Email = sqlReader.GetString(emailOrdinal);
                    user.Phone = sqlReader.GetString(phoneOrdinal);
                    user.TC = sqlReader.GetInt32(tcOrdinal);
                    _connection.Close();
                    return user;
                }

                _connection.Close();
                return null;
            }

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
