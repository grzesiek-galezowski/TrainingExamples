using System.Collections.Generic;

namespace PrivateMethodsAsEventHandlersRegisteredInMethod
{
  public class BiDirectionalDictionary
  {
    public BiDirectionalDictionary()
    {
    }

    private readonly Dictionary<string, string> _oneWay = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _otherWay = new Dictionary<string, string>();
    
    public void Add(string key, string value, ValueExpiryPolicy valueExpiryPolicy)
    {
      _oneWay[key] = value;
      _otherWay[value] = key;
      valueExpiryPolicy.ItemNotNeededAnymore += OnItemNotNeededAnymore;
    }

    public string GetValueByKey(string key) => _oneWay[key];
    public string GetKeyByValue(string value) => _otherWay[value];

    private void OnItemNotNeededAnymore(object sender, RemoveEventArgs eventArgs)
    {
      var value = _oneWay[eventArgs.Key];
      _oneWay.Remove(eventArgs.Key);
      _otherWay.Remove(value);
    }

  }
}