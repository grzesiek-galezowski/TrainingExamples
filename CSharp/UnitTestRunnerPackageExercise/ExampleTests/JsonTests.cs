using Newtonsoft.Json.Linq;
using UnitTestRunnerPackageExercise;

namespace ExampleTests;

public class JsonTests
{
  public void TestJsonObject()
  {
    var json = "{ 'key': 'value', 'nested': { 'key': 'nested value' } }";
    var jsonObject = JObject.Parse(json);

    JsonAssert.AssertHasKey("key", jsonObject);
    JsonAssert.AssertHasValue("key", "value", jsonObject);
    JsonAssert.AssertHasNestedValue(new[] { "nested", "key" }, "nested value", jsonObject);
    JsonAssert.AssertObjectCount(2, jsonObject);
  }

  public void TestJsonArray()
  {
    var json = "[1, 2, 3, 4, 5]";
    var jsonArray = JArray.Parse(json);

    JsonAssert.AssertArrayCount(5, jsonArray);
    JsonAssert.AssertArrayContainsValues("", new[] { "1", "2", "3", "4", "5" }, jsonArray);
  }

  public void TestJsonValidation()
  {
    var json = "{ 'name': 'John Smith', 'age': 30 }";
    var schema = "{ 'type': 'object', 'properties': { 'name': { 'type': 'string' }, 'age': { 'type': 'number' } }, 'required': ['name', 'age'] }";

    JsonAssert.AssertIsValid(json, schema);
  }
}