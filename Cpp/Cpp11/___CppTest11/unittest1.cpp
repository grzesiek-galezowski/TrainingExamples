#include "stdafx.h"
#include "CppUnitTest.h"
#include<iostream>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace CppTest11
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestMethod1)
		{
      Assert().AreEqual("1", "2");
			// TODO: Your test code here
		}

	};
}