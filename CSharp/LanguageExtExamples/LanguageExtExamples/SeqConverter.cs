using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using LanguageExt;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonException = System.Text.Json.JsonException;

namespace LanguageExtExamples;

public class SeqConverter : JsonConverterFactory
{
    private readonly Type type = typeof(Seq<>);

    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        return typeToConvert.GetGenericTypeDefinition() == type;
    }

    public override JsonConverter CreateConverter(
        Type type,
        JsonSerializerOptions options)
    {
        var valueType = type.GetGenericArguments()[0];

        var converter = (JsonConverter)Activator.CreateInstance(
            typeof(SeqConverter<>).MakeGenericType(valueType),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: new object[] { options },
            culture: null)!;

        return converter;
    }
}

public class SeqConverter<T> : JsonConverter<Seq<T>>
{
    private readonly JsonConverter<T> _jsonConverter;

    public SeqConverter(JsonSerializerOptions options)
    {
        _jsonConverter = (JsonConverter<T>)options.GetConverter(typeof(T));
    }

    public override Seq<T> Read(
                    ref Utf8JsonReader reader,
                    Type typeToConvert,
                    JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected " + JsonTokenType.StartArray + " but got " + reader.TokenType);
        }

        var defaultValue = new Seq<T>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return defaultValue;
            }

            var value = _jsonConverter.Read(ref reader, typeof(T), options)!;

            // Add to seq.
            defaultValue = defaultValue.Add(value);
        }

        throw new JsonException();
    }

    public override void Write(
    Utf8JsonWriter writer,
        Seq<T> seq,
        JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var value in seq)
        {
            _jsonConverter.Write(writer, value, options);
        }

        writer.WriteEndArray();
    }
}

