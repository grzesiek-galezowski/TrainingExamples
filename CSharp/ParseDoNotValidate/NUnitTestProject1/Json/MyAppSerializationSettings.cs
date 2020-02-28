using Newtonsoft.Json;
using ParseNotValidate.Values;

namespace ParseNotValidate.Json
{
    static internal class MyAppSerializationSettings
    {
        public static JsonSerializerSettings Get()
        {
            return new JsonSerializerSettings()
            {
                Converters = new JsonConverter[]
                {
                    new ValueConverter<Name, string>(Name.Value, name => name.ToString()),
                    new ValueConverter<Surname, string>(Surname.Value, surname => surname.ToString()),
                    new ValueConverter<Age, int>(Age.Of, age => age.Value),
                }
            };
        }
    }
}