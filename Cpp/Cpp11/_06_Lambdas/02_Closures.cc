#include "stdafx.h"
#include <functional>
#include <iostream>
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

std::function<int ()> sequencer(int start)
{
    return [=]() mutable //return [&start]()
    {
        start++;
        return start;
    };
}

namespace _06_Lambdas
{
  TEST_CLASS(_06_Closures)
  {
  public:

    TEST_METHOD(SequencerStartingWith2)
    {
      auto s1 = sequencer(2);
      Assert().AreEqual(3, s1());
      Assert().AreEqual(4, s1());
      Assert().AreEqual(5, s1());
      Assert().AreEqual(6, s1());
      Assert().AreEqual(7, s1());
    }

    TEST_METHOD(SequencerStartingWith3)
    {
      auto s1 = sequencer(3);
      Assert().AreEqual(4, s1());
      Assert().AreEqual(5, s1());
      Assert().AreEqual(6, s1());
      Assert().AreEqual(7, s1());
      Assert().AreEqual(8, s1());
    }
  };
}
