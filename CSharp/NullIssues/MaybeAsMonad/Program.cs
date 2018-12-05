using System;
using System.Collections.Generic;
using System.Linq;
using Functional.Maybe;

namespace MaybeAsMonad
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new Database();

            var userOfFirstContact = GetAddressBook()
                .Select(b => b.Contacts)
                .SelectMany(c => c.FirstOrDefault().ToMaybe())
                .Select(c => c.Name)
                .Select(name => database.LoadByName(name))
                .OrElse(() => new Person("Zenek"));
            Console.WriteLine(userOfFirstContact.Name);
        }

        private static Maybe<AddressBook> GetAddressBook()
        {
            return Maybe<AddressBook>.Nothing;
        }
    }

    public class AddressBook
    {
        public List<Contact> Contacts { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
    }

    public class Database
    {
        public Person LoadByName(string name)
        {
            return null;
        }
    }

    public class Person
    {
        public Person(string zenek)
        {
            Name = zenek;
        }

        public string Name { get; set; }
    }
}
