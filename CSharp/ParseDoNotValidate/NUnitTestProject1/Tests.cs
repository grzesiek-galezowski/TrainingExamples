using System;
using System.Configuration;
using FluentAssertions;
using Newtonsoft.Json;
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
        DateTime.Parse("1990-01-01")
      );

      var str = JsonConvert.SerializeObject(userInfo, new JsonSerializerSettings()
      {
        Converters = new JsonConverter[]
        {
          new ValueConverter<Name, string>(Name.Value, name => name.ToString()),
          new ValueConverter<Surname, string>(Surname.Value, surname => surname.ToString()),
          new ValueConverter<Age, int>(Age.Of, age => age.Value),
        }
      });

      str.Should().Be("{\"Name\":\"Zenek\",\"Surname\":\"Lolek\",\"Age\":12,\"Birthday\":\"1990-01-01T00:00:00\"}");
    }
  }

  public class ValueConverter<T, U> : JsonConverter<T> where T : class
  {
    private readonly Func<U, T> _wrapper;
    private readonly Func<T, U> _unwrapper;

    public ValueConverter(Func<U, T> wrapper, Func<T, U> unwrapper)
    {
      _wrapper = wrapper;
      _unwrapper = unwrapper;
    }

    public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    {
      if (value != null)
      {
        serializer.Serialize(writer, _unwrapper(value));
      }
      else
      {
        serializer.Serialize(writer, null);
      }
    }

    public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue,
      JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return null;
      }

      var value = serializer.Deserialize<U>(reader);
      return _wrapper(value);
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
      Surname = surname ?? throw new ArgumentNullException(nameof(surname));
      Age = age ?? throw new ArgumentNullException(nameof(age));
      Birthday = birthday;
    }
  }
}