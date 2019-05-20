using System;

namespace CommandComparisonDispatcher
{
  public class User
  {
    private readonly string _name;
    private readonly string _surname;

    public User(string name, string surname)
    {
      _name = name;
      _surname = surname;
    }

    public void AssertIsValid()
    {
      if (_name.Length < 10)
      {
        throw new Exception();
      }
    }

    public void AddTo(Repository repository, ResultInProgress resultInProgress)
    {
      repository.SaveUser(_name, _surname);
      resultInProgress.SuccessfullyAdded(_name, _surname);
    }
  }
}