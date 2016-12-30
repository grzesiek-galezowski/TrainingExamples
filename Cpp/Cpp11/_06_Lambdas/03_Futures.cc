#include "stdafx.h"

#include <future>
#include <iostream>
#include <vector>
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace _06_Lambdas
{
  TEST_CLASS(_06_Futures)
  {
  public:

    TEST_METHOD(SequencerStartingWith2)
    {
      std::vector<int> v = { 1, 2, 3, 4, 5, 6 };

      auto additionLambda1 =
        [&]()
      {
        this_thread::sleep_for(chrono::milliseconds(100));
        return v[0] + v[1] + v[2];
      };

      auto additionLambda2 =
        [&]()
      {
        this_thread::sleep_for(chrono::milliseconds(20));
        return v[3] + v[4] + v[5];
      };

      auto task1 = std::async(additionLambda1);
      auto task2 = std::async(additionLambda2);

      auto result = 
        task1.get() + 
        task2.get();

      Assert().AreEqual(21, result);
    }
  };
}
