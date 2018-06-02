using System;

namespace SqlSimple.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public User Owner { get; set; }
        public int UserId { get; set; }

        public override string ToString()
        {
            return $" Id = {Id} || Address = {Address} || User = {Owner} || UserId = {UserId}";
        }               
    }
}