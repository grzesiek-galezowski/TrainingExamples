#include "stdafx.h"
#include "CppUnitTest.h"
#include<iostream>
#include<initializer_list>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

class Object
{
public:
  Object(const std::initializer_list<int>& args)
  {
    //try converting to auto:
    for(std::initializer_list<int>::iterator it = args.begin(); it != args.end() ; ++it)
    {
      std::cout << *it;
    }
    std::cout << "\n";
  }
};

TEST_CLASS(_02_SequenceConstructorNotation)
{
public:

  TEST_METHOD(SequenceConstructor)
  {
    Object o0({ 1, 2, 3, 4, 5, 7, 45, 3, 5 });
    Object o1{ 1, 2, 3, 4, 5, 7, 45, 3, 5 };
    Object o2 = { 1, 2, 3, 4, 5, 7, 45, 3, 5 };
  }

  TEST_METHOD(InitializerListType)
  {
    //It's just an object:
    auto list2 = { 2.3, 4.5, 5.6, 6.7 };
    Assert().AreEqual(
      "class std::initializer_list<double>", typeid(list2).name());

    std::initializer_list<double> list1{ 2.3, 4.5, 5.6, 6.7 };
    Assert().AreEqual(2.3, item(list1, 0));
    Assert().AreEqual(4.5, item(list1, 1));
    Assert().AreEqual(5.6, item(list1, 2));
    Assert().AreEqual(6.7, item(list1, 3));
    Assert().AreEqual(*(list1.end() - 1), item(list1, 3));
    Assert().AreEqual<unsigned>(4u, list1.size());

  }

  template<typename T> auto item(T collection, int place)
  {
    return *(collection.begin() + place);
  }

};
