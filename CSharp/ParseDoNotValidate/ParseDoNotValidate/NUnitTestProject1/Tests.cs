using System;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var userInfo = new UserDto(
                Name.Value("Zenek"),
                Surname.Value("Lolek"),
                Age.Of(12),
                DateTime.Now
            );
        }
    }

    public class UserDto
    {
        public Name Name { get; }
        public Surname Surname { get; }
        public Age Age { get; }
        public DateTime Birthday { get; }

        public UserDto(Name name, Surname surname, Age age, in DateTime birthday)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname  ?? throw new ArgumentNullException(nameof(surname));
            Age = age ?? throw new ArgumentNullException(nameof(age));
            Birthday = birthday;
        }
    }
}