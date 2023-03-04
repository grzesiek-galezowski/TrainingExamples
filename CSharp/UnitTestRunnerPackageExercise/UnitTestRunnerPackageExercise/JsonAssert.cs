using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace UnitTestRunnerPackageExercise;

public static class JsonAssert
{
  public static void AssertHasKey(string key, JObject jsonObject)
  {
    Assert.IsTrue(jsonObject.ContainsKey(key));
  }

  public static void AssertHasValue(string key, string value, JObject jsonObject)
  {
    Assert.AreEqual(value, jsonObject[key].ToString());
  }

  public static void AssertHasNestedValue(string[] keys, string value, JObject jsonObject)
  {
    var token = keys.Aggregate((JToken)jsonObject, (current, key) => current[key]);
    Assert.AreEqual(value, token.ToString());
  }

  public static void AssertArrayContainsValues(string key, string[] values, JArray jsonObject)
  {
    Assert.AreEquivalent(values, jsonObject[key].Select(x => x.Value<string>()).ToArray());
  }

  public static void AssertObjectCount(int count, JObject jsonObject)
  {
    Assert.AreEqual(count, jsonObject.Count);
  }

  public static void AssertArrayCount(int count, JArray jsonArray)
  {
    Assert.AreEqual(count, jsonArray.Count);
  }

  public static void AssertIsValid(string json, string schema)
  {
    var jsonSchema = JSchema.Parse(schema);
    var jsonObject = JObject.Parse(json);
    Assert.IsTrue(jsonObject.IsValid(jsonSchema));
  }
}