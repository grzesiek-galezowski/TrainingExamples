namespace unit_tests_csharp.P03Any
{
  public class MyClass<T>
  {
    private readonly T instance;

    public MyClass(T instance)
    {
      this.instance = instance;
    }

    public T Instance
    {
      get { return instance; }
    }
  }
}