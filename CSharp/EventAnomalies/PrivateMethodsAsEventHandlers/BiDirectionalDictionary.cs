using System.Collections.Generic;

namespace PrivateMethodsAsEventHandlers
{
  public class BiDirectionalDictionary
  {
    public BiDirectionalDictionary(ExpiryPolicy expiryPolicy)
    {
      expiryPolicy.ItemNotNeededAnymore += this.OnItemNotNeededAnymore;
    }

    private readonly Dictionary<string, string> _valuesByKey = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _keysByValue = new Dictionary<string, string>();
    
    public void Add(string key, string value)
    {
      SetValueForKey(key, value);
      SetKeyForValue(key, value);
    }

    private void SetValueForKey(string key, string value)
    {
      _valuesByKey[key] = value;
    }

    private void SetKeyForValue(string key, string value)
    {
      _keysByValue[value] = key;
    }


    public string GetValueByKey(string key) => _valuesByKey[key];
    public string GetKeyByValue(string value) => _keysByValue[value];

    private void OnItemNotNeededAnymore(object sender, RemoveEventArgs eventArgs)
    {
      var value = _valuesByKey[eventArgs.Key];
      _valuesByKey.Remove(eventArgs.Key);
      _keysByValue.Remove(value);
    }

  }
}