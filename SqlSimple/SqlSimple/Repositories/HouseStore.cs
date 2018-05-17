using SqlSimple.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSimple.Repositories
{
    public class HouseStore
    {
       

        private readonly SqlConnection _connection;

        public HouseStore(SqlConnection connection)
        {

            _connection = connection;
        }

        public void Save(House house)
        {
            var insertCommand = new SqlCommand(@"INSERT INTO 
                                                [House] (Address) 
                                                VALUES (@Address)", _connection);
            
            insertCommand.Parameters.AddWithValue("@Address", house.Address);
            

            _connection.Open();

            insertCommand.ExecuteNonQuery();

            _connection.Close();

        }
        public House getById(int id)
        {

            var house = new House();
            SqlCommand getByıDCommand = new SqlCommand(@"Select Address
                                                         FROM [House]
                                                         where Id = @Id",_connection);

            getByıDCommand.Parameters.AddWithValue("@Id", id);

            _connection.Open();

            

            using (var reader = getByıDCommand.ExecuteReader())
            {
                var addressOrdinal = reader.GetOrdinal("Address");
                reader.Read();
                house.Address = reader.GetString(addressOrdinal);

            }

            _connection.Close();
                
            
            return house;
        }

        public List<House> GetAll()
        {
            var house = new List<House>();
            SqlCommand getAllcommand = new SqlCommand(@"Select *
                                                       From [House]",_connection);
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
        

    }
}
