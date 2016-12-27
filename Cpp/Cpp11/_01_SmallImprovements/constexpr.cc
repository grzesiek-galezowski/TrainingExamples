#include "stdafx.h"
#include "CppUnitTest.h"

#include<vector>
#include<iostream>
#include "Pow.hh"
#include "PowerOf2.hh"
#include "constexpr.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace _01_SmallImprovements
{
  void print(vector<PowerOf2>& powers)
  {
    for (PowerOf2& value : powers)
    {
      cout << u8"Nastêpna potêga dwójki: " << endl;

      // std::cout << value << std::endl; //=> error!

      auto printablePower = static_cast<int>(value);
      cout << printablePower << endl;
    }
  }

  TEST_CLASS(UnitTest1)
  {
  public:

    TEST_METHOD(CorrectValuesOfConstexprPow)
    {
      Assert().AreEqual<PowerOf2>(PowerOf2::second, PowerOf2::second);
      Assert().AreNotEqual<PowerOf2>(PowerOf2::third, PowerOf2::first);

      Assert().AreEqual(2, static_cast<int>(PowerOf2::first));
      Assert().AreEqual(4, static_cast<int>(PowerOf2::second));
      Assert().AreEqual(8, static_cast<int>(PowerOf2::third));
      Assert().AreEqual(16, static_cast<int>(PowerOf2::fourth));
      Assert().AreEqual(32, static_cast<int>(PowerOf2::fifth));
    }

    TEST_METHOD(InitializerLists)
    {
      vector<PowerOf2> constexpr_powers = {
        PowerOf2::first
        , PowerOf2::second
        , PowerOf2::third
        , PowerOf2::fourth
        , PowerOf2::fifth
      };
      
      print(constexpr_powers);

      Assert().AreEqual(PowerOf2::first, constexpr_powers[0]);
      Assert().AreEqual(PowerOf2::second, constexpr_powers[1]);
      Assert().AreEqual(PowerOf2::third, constexpr_powers[2]);
      Assert().AreEqual(PowerOf2::fourth, constexpr_powers[3]);
      Assert().AreEqual(PowerOf2::fifth, constexpr_powers[4]);
    }

  };
}