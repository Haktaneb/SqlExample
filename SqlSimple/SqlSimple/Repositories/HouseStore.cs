using SqlSimple.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlSimple.Repositories
{
    public class HouseStore
    {
         private readonly SqlConnection _connection;
        static SqlConnection UserStoreConnection;
        public HouseStore(SqlConnection connection)
        {
            _connection = connection;
            UserStoreConnection = connection;
        }
        

        public void Save(House house)
        {

            if (house.Owner != null)
            {

                UserStore userStore = new UserStore(UserStoreConnection);
                var userExists = userStore.GetByTc(house.Owner.TC);
                var result = getById(house.Id);
                //var insertCommand = new SqlCommand(@"INSERT INTO 
                //                                [House] (Address,UserId) 
                //                                VALUES (@Address,@UserId)", _connection);

                _connection.Open();
                if (result == null)
                {
                    if (userExists == null)
                    {


                        userStore.Save(house.Owner);

                        var savedUser = userStore.GetByTc(house.Owner.TC);

                        var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [House] (Address,UserId) 
                                                VALUES (@Address,@UserId)", _connection);

                        insertCommand.Parameters.AddWithValue("@Address", house.Address);
                        insertCommand.Parameters.AddWithValue("@UserId", savedUser.Id);
                        insertCommand.ExecuteNonQuery();

                    }
                    else
                    {
                        var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [House] (Address,UserId) 
                                                VALUES (@Address,@UserId)", _connection);

                        insertCommand.Parameters.AddWithValue("@Address", house.Address);
                        insertCommand.Parameters.AddWithValue("@UserId", userExists.Id);
                        insertCommand.ExecuteNonQuery();
                    }

                }
                else
                {
                    if (userExists == null)
                    {

                        userStore.Save(house.Owner);

                        var savedUser = userStore.GetByTc(house.Owner.TC);


                        var updateCommand = new SqlCommand(@"UPDATE [House] 
                                                    SET Address = @Address ,UserId = @UserId
                                                    Where Id = @Id", _connection);

                        updateCommand.Parameters.AddWithValue("@Address", house.Address);
                        updateCommand.Parameters.AddWithValue("@UserId", savedUser.Id);
                        updateCommand.Parameters.AddWithValue("@Id", house.Id);
                        updateCommand.ExecuteNonQuery();

                    }
                    else
                    {
                        var updateCommand = new SqlCommand(@"UPDATE [House] 
                                                    SET Address = @Address ,UserId = @UserId
                                                    Where Id = @Id", _connection);

                        updateCommand.Parameters.AddWithValue("@Address", house.Address);
                        updateCommand.Parameters.AddWithValue("@UserId", userExists.Id);
                        updateCommand.Parameters.AddWithValue("@Id", house.Id);
                        updateCommand.ExecuteNonQuery();
                    }                 
                }
            }
           
            else
            {
                _connection.Close();
                var result = getById(house.Id);
                if (result == null)
                {
                    
                    var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [House] (Address) 
                                                VALUES (@Address)", _connection);

                    insertCommand.Parameters.AddWithValue("@Address", house.Address);
                   
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    var updateCommand = new SqlCommand(@"UPDATE [House] 
                                                    SET Address = @Address 
                                                    Where Id = @Id", _connection);
                    updateCommand.Parameters.AddWithValue("@Address", house.Address);
                    updateCommand.Parameters.AddWithValue("@Id", house.Id);
                }

            }
          
            _connection.Close();
        }

        public House getById(int id)
        {
            var house = new House();
            SqlCommand getByıDCommand = new SqlCommand(@"Select *
                                                         FROM [House]  h                                         
                                                         where h.Id = @Id", _connection);
            getByıDCommand.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            using (var reader = getByıDCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    var addressOrdinal = reader.GetOrdinal("Address");
                    var UserIdOrdinal = reader.GetOrdinal("UserId");
                    var IdOrdinal = reader.GetOrdinal("Id");
                    
                    reader.Read();
                    int controlUserId = reader.GetInt32(UserIdOrdinal);
                    if (controlUserId == 0)
                    {
                        house.Id = reader.GetInt32(IdOrdinal);
                        house.Address = reader.GetString(addressOrdinal);
                        house.UserId = controlUserId;
                        _connection.Close();
                        return house;

                    }
                    else
                    {
                        _connection.Close();
                        SqlCommand getUserCommand = new SqlCommand(@"Select *
                                                         FROM [User]  u 
                                                         INNER JOIN [House] h    
                                                         ON h.UserId = u.Id
                                                         where h.Id = @Id", _connection);

                        getUserCommand.Parameters.AddWithValue("@Id", id);
                        _connection.Open();
                        using (var readerUsers = getUserCommand.ExecuteReader())
                        {
                             addressOrdinal = readerUsers.GetOrdinal("Address");
                             UserIdOrdinal = readerUsers.GetOrdinal("UserId");
                             IdOrdinal = readerUsers.GetOrdinal("Id");
                            var nameOrdinal = readerUsers.GetOrdinal("FullName");
                            var emailOrdinal = readerUsers.GetOrdinal("Email");
                            var phoneOrdinal = readerUsers.GetOrdinal("Phone");
                            var tcOrdinal = readerUsers.GetOrdinal("Tc");
                            readerUsers.Read();
                            
                            house.Id = readerUsers.GetInt32(IdOrdinal);
                           house.Address = readerUsers.GetString(addressOrdinal);
                            house.Owner = new User
                            {
                                Id= readerUsers.GetInt32(IdOrdinal),
                                FullName = readerUsers.GetString(nameOrdinal),
                                Email = readerUsers.GetString(emailOrdinal),
                                Phone = readerUsers.GetString(phoneOrdinal),
                                TC = readerUsers.GetInt32(tcOrdinal)
                            };
                          
                           house.UserId = controlUserId;
                            _connection.Close();
                            return house;
                        }
                    }                  
                }
                _connection.Close();
                return null;
            }
        }
        public List<House> GetAll()
        {
            var house = new List<House>();
            SqlCommand getAllcommand = new SqlCommand(@"Select *
                                                       From [House] h 
                                                       where h.UserId IS NULL", _connection);
            _connection.Open();
            using (var reader = getAllcommand.ExecuteReader())
            {
                var addressOrdinal = reader.GetOrdinal("Address");
                var IdOrdinal = reader.GetOrdinal("Id");
                while (reader.Read())
                {
                    var houses = new House();
                    houses.Id = reader.GetInt32(IdOrdinal);
                    houses.Address = reader.GetString(addressOrdinal);
                    house.Add(houses);
                }
            }
            _connection.Close();
            return house;
        }
        public List<House> GetAllOwnedHouses()
        {
            
            var house = new List<House>();
            SqlCommand getAllUserCommand = new SqlCommand(@"Select *
                                                         FROM [User]  u 
                                                         INNER JOIN [House] h    
                                                         ON h.UserId = u.Id" ,_connection);
            
            _connection.Open();
            using (var reader = getAllUserCommand.ExecuteReader())
            {
                var addressOrdinal = reader.GetOrdinal("Address");
                var IdOrdinal = reader.GetOrdinal("Id");
                var nameOrdinal = reader.GetOrdinal("FullName");
                var emailOrdinal = reader.GetOrdinal("Email");
                var phoneOrdinal = reader.GetOrdinal("Phone");
                var tcOrdinal = reader.GetOrdinal("Tc");
                while (reader.Read())
                {
                    var houses = new House();
                    houses.Id = reader.GetInt32(IdOrdinal);
                    houses.Address = reader.GetString(addressOrdinal);
                    houses.Owner = new User
                    {
                        Id = reader.GetInt32(IdOrdinal),
                        FullName = reader.GetString(nameOrdinal),
                        Email = reader.GetString(emailOrdinal),
                        Phone = reader.GetString(phoneOrdinal),
                        TC = reader.GetInt32(tcOrdinal)
                    };
                    house.Add(houses);
                }
            }
            _connection.Close();
            return house;
        }

    }
}