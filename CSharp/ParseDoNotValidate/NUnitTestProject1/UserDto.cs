using System;

namespace NUnitTestProject1
{
    public class UserDto
    {
        public Name Name { get; }
        public Surname Surname { get; }
        public Age Age { get; }
        public DateTime Birthday { get; }

        public UserDto(Name name, Surname surname, Age age, in DateTime birthday)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Age = age ?? throw new ArgumentNullException(nameof(age));
            Birthday = birthday;
        }
    }
}