using System;
using System.Collections.Generic;
using Core.Maybe;

namespace MaybeAsMonad;

//NRT vs Nullable<T>
//NRT vs Maybe<T>
// - Maybe is part of type system
// - Maybe has some monadic behaviors
// - NRTs are just "annotations" on types, accessible to compiler and reflection
// - cannot to overloads on NRTs
// - slightly more confusing generic programming scenarios (?)
class Program
{
    static void Main(string[] args)
    {
        var database = new Database();

        //1. Compare to null propagation
        //2. Execute it!
        var userOfFirstContact = GetAddressBook()
            .Select(b => b.Contacts)
            .SelectMany(c => c.FirstMaybe())
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