using System;
using Newtonsoft.Json;

namespace NUnitTestProject1.Json
{
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
            var value = serializer.Deserialize<U>(reader);

            try
            {
                return _wrapper(value);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to parse property " 
                                    + reader.Path 
                                    + " with value " 
                                    + (value?.ToString() ?? "null") 
                                    + " to type " + objectType, e);

            }
        }
    }
}