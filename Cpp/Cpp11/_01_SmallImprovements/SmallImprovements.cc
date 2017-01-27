#include "stdafx.h"
#include "CppUnitTest.h"

#include<vector>
#include<iostream>
#include "Pow.hh"
#include "PowersOf2.hh"
#include "constexpr.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace _01_SmallImprovements
{
  void print(vector<PowersOf2>& powers)
  {
    for (PowersOf2& value : powers)
    {
      cout << u8"Następna potęga dwójki: " << endl;
      auto printablePower = static_cast<int>(value);
      cout << printablePower << endl;
    }
  }

  TEST_CLASS(_01_SmallImprovements)
  {
  public:

    TEST_METHOD(ClassEnums)
    {
      Assert().AreEqual<PowersOf2>(PowersOf2::second, PowersOf2::second);
      Assert().AreNotEqual<PowersOf2>(PowersOf2::third, PowersOf2::first);

      Assert().AreEqual(2, static_cast<int>(PowersOf2::first));
      Assert().AreEqual(4, static_cast<int>(PowersOf2::second));
      Assert().AreEqual(8, static_cast<int>(PowersOf2::third));
      Assert().AreEqual(16, static_cast<int>(PowersOf2::fourth));
      Assert().AreEqual(32, static_cast<int>(PowersOf2::fifth));
    }

    TEST_METHOD(InitializerLists)
    {
      vector<PowersOf2> constexpr_powers = {
        PowersOf2::first
        , PowersOf2::second
        , PowersOf2::third
        , PowersOf2::fourth
        , PowersOf2::fifth
      };
      
      print(constexpr_powers);

      Assert().AreEqual(PowersOf2::first, constexpr_powers[0]);
      Assert().AreEqual(PowersOf2::second, constexpr_powers[1]);
      Assert().AreEqual(PowersOf2::third, constexpr_powers[2]);
      Assert().AreEqual(PowersOf2::fourth, constexpr_powers[3]);
      Assert().AreEqual(PowersOf2::fifth, constexpr_powers[4]);
    }

  };
}