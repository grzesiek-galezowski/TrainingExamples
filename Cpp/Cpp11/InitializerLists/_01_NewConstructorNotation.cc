#include "stdafx.h"
#include "CppUnitTest.h"
#include<iostream>
#include<initializer_list>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

class Object
{
public:
  const int a;
  const double b;
  const char c;

	explicit Object(int a, double b, char c) : a(a), b(b), c(c) {	}
};

TEST_CLASS(_02_ConstructorNotation)
{
public:

  TEST_METHOD(CustomConstructorInvocation)
  {
    Object o1{ 1, 2.5, 'c' };
    //Object o2 = {1, 2.5, 'c'}; //comment out explicit for this to work

    Assert().AreEqual(1, o1.a);
    Assert().AreEqual(2.5, o1.b);
    Assert().AreEqual('c', o1.c);
  }

};


