#include "stdafx.h"
#include "CppUnitTest.h"

#include<vector>
#include<iostream>
#include "Pow.hh"
#include "PowerOf2.hh"
#include "constexpr.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace Microsoft
{
  namespace VisualStudio
  {
    namespace CppUnitTestFramework
    {
      template<> static std::wstring ToString<PowerOf2>(const PowerOf2& t) { 
        RETURN_WIDE_STRING(static_cast<int>(t)); }
    }
  }
}


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

int main()
{
  vector<PowerOf2> powers = {
    PowerOf2::first
    , PowerOf2::second
    , PowerOf2::third
    , PowerOf2::fourth
    , PowerOf2::fifth
  };

  print(powers);

  return 0;
}


	TEST_CLASS(UnitTest1)
	{
	public:
		
    TEST_METHOD(CorrectValuesOfConstexprPow)
    {
      Assert().AreEqual<PowerOf2>(PowerOf2::second, PowerOf2::second);
      Assert().AreNotEqual<PowerOf2>(PowerOf2::fifth, PowerOf2::first);

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


		}

	};
}