using System.Collections.Generic;

namespace PrivateMethodsAsEventHandlers
{
  public class BiDirectionalDictionary
  {
    public BiDirectionalDictionary(ExpiryPolicy expiryPolicy)
    {
      expiryPolicy.ItemNotNeededAnymore += this.OnItemNotNeededAnymore;
    }

    private readonly Dictionary<string, string> _oneWay = new Dictionary<string, string>();
    private readonly Dictionary<string, string> _otherWay = new Dictionary<string, string>();
    
    public void Add(string key, string value)
    {
      _oneWay[key] = value;
      _otherWay[value] = key;
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