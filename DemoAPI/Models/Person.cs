﻿using System;
using MongoDAL;
using System.Linq;

namespace DemoAPI.Models

{
    [MongoIgnoreExtras]
    public class Person : MongoEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string[] PhoneNumbers { get; set; }
        public DateTime? RetirementDate { get; set; }

        [MongoIgnore]
        public Address[] Addresses { get; set; }

        public void Save()
        {
            DB.Save<Person>(this);
        }

        public Person FindLast()
        {
            return (from p in DB.Collection<Person>()
                    select p).LastOrDefault();
        }
    }
}