using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonException = System.Text.Json.JsonException;

namespace LanguageExtExamples.Framework;

public class LanguageExtCollectionConverter : JsonConverterFactory
{
  private readonly Type _collectionOpenType;
  private readonly Type _builderFactoryOpenType;
  private readonly Type _converterType;

  public LanguageExtCollectionConverter(Type builderFactoryOpenType, Type collectionOpenType)
  {
    _builderFactoryOpenType = builderFactoryOpenType;
    _collectionOpenType = collectionOpenType;
    _converterType = typeof(LanguageExtCollectionConverter<,>);
  }

  public override bool CanConvert(Type typeToConvert)
  {
    if (!typeToConvert.IsGenericType)
    {
      return false;
    }

    return typeToConvert.GetGenericTypeDefinition() == _collectionOpenType;
  }

  public override JsonConverter CreateConverter(
    Type type,
    JsonSerializerOptions options)
  {
    var elementType = type.GetGenericArguments()[0];
    var builderType = _builderFactoryOpenType.MakeGenericType(elementType);

    var builder = Activator.CreateInstance(builderType);
    var converter = (JsonConverter)Activator.CreateInstance(
      _converterType.MakeGenericType(type, elementType),
      BindingFlags.Instance | BindingFlags.Public,
      binder: null,
      args: new[] { options, builder },
      culture: null)!;

    return converter;
  }
}

public class LanguageExtCollectionConverter<TCollection, TElement> : JsonConverter<TCollection>
  where TCollection : IEnumerable<TElement>
{
  private readonly ICollectionBuilderFactory<TCollection, TElement> _arrBuilderFactory;
  private readonly JsonConverter<TElement> _jsonConverter;

  public LanguageExtCollectionConverter(JsonSerializerOptions options, ICollectionBuilderFactory<TCollection, TElement> arrBuilderFactory)
  {
    _arrBuilderFactory = arrBuilderFactory;
    _jsonConverter = (JsonConverter<TElement>)options.GetConverter(typeof(TElement));
  }

  public override TCollection Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartArray)
    {
      throw new JsonException("Expected " + JsonTokenType.StartArray + " but got " + reader.TokenType);
    }

    var builder = _arrBuilderFactory.NewBuilder();

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndArray)
      {
        return builder.Build();
      }

      var value = _jsonConverter.Read(ref reader, typeof(TElement), options)!;

      builder.Add(value);
    }

    throw new JsonException();
  }

  public override void Write(Utf8JsonWriter writer, TCollection seq, JsonSerializerOptions options)
  {
    writer.WriteStartArray();

    foreach (var value in seq)
    {
      _jsonConverter.Write(writer, value, options);
    }

    writer.WriteEndArray();
  }
}