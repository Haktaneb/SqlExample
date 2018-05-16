using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSimple
{
    public class User
    {
        public int Id { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public List<House> Houses { get; set; }

        public override string ToString()
        {
            return $"Name = {FullName} || Email = {Email}";
        }
    }

    public class House
    {
        public int Id { get; set; }
        public String Address { get; set; }
        public User User { get; set; }

       
    }
   
}
