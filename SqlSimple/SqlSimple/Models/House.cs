using System;

namespace SqlSimple.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }

        //TODO: Add User one to many house relation. Create the User just in here. 
        //TODO: Add database related relation without droping the db. you should alter table. Do this in a separate SQL file. yes, developer needs to rin this files sequentialy    
        //TODO: override ToString method to nicely show the house. Check User class for example
    }
}