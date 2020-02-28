using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ParseNotValidate.Json;
using ParseNotValidate.Values;
using static TddXt.AnyRoot.Root;

namespace ParseNotValidate
{
  public class Tests
  {
    [Test]
    public void ShouldSerializeAndDeserializeIntoOriginalValue()
    {
      var dtoBeforeSerialization = new UserDto(
        Name.Value("Zenek"),
        Surname.Value("Lolek"),
        Age.Of(12),
        DateTime.Parse("1990-01-01")
      );

      var str = JsonConvert.SerializeObject(dtoBeforeSerialization, MyAppSerializationSettings.Get());

      str.Should().Be(CreatePersonjSON("Zenek", "Lolek", 12));

      var dtoAfterDeserialization = JsonConvert.DeserializeObject<UserDto>(str, MyAppSerializationSettings.Get());

      dtoAfterDeserialization.Should().BeEquivalentTo(
          dtoBeforeSerialization, 
          options => options.IncludingNestedObjects());
    }

    [Test]
    public void ShouldThrowExceptionOnNullInvalidValues()
    {
        CreatePersonjSON(null, "Lolek", 12)
            .Invoking(s => JsonConvert.DeserializeObject<UserDto>(s, MyAppSerializationSettings.Get()))
            .Should().ThrowExactly<Exception>()
            .WithMessage("Failed to parse property Name with value null to type ParseNotValidate.Values.Name");
        
        CreatePersonjSON(string.Empty, "Lolek", 12)
            .Invoking(s => JsonConvert.DeserializeObject<UserDto>(s, MyAppSerializationSettings.Get()))
            .Should().ThrowExactly<Exception>()
            .WithMessage("Failed to parse property Name with value  to type ParseNotValidate.Values.Name");

        CreatePersonjSON("Zenek", null, 12)
            .Invoking(s => JsonConvert.DeserializeObject<UserDto>(s, MyAppSerializationSettings.Get()))
            .Should().ThrowExactly<Exception>()
            .WithMessage("Failed to parse property Surname with value null to type ParseNotValidate.Values.Surname");

        CreatePersonjSON("Zenek", string.Empty, 12)
            .Invoking(s => JsonConvert.DeserializeObject<UserDto>(s, MyAppSerializationSettings.Get()))
            .Should().ThrowExactly<Exception>()
            .WithMessage("Failed to parse property Surname with value  to type ParseNotValidate.Values.Surname");

        CreatePersonjSON("Zenek", "Lolek", -1)
            .Invoking(s => JsonConvert.DeserializeObject<UserDto>(s, MyAppSerializationSettings.Get()))
            .Should().ThrowExactly<Exception>()
            .WithMessage("Failed to parse property Age with value -1 to type ParseNotValidate.Values.Age");

    }

    private static string CreatePersonjSON(string name, string surname, int age)
    {
        return "{" +
               $"\"Name\":{name.QuotedOrNull()}," +
               $"\"Surname\":{surname.QuotedOrNull()}," +
               $"\"Age\":{age}," +
               "\"Birthday\":\"1990-01-01T00:00:00\"" +
               "}";
    }

    [Test]
    public void ShouldBeAbleToCreateAnyInstances()
    {
      var age = Any.Instance<Age>();
      var name = Any.Instance<Name>();
      var surname = Any.Instance<Surname>();
    }
  }
}