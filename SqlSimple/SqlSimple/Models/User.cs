using System;

namespace SqlSimple.Models
{
    public class User
    {
        public int Id { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        
        public override string ToString()
        {
            return $" Id = {Id} || Name = {FullName} || Email = {Email}";
        }
    }
}