#include "stdafx.h"
#include <iostream>
#include <string>
#include <random>
#include <ctime>
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

mt19937 random(time(0));

class ObjectWithTwoConstructors
{
public:
	ObjectWithTwoConstructors(const int& initialValue) 
    : a(initialValue), random2(random()) {}

  ObjectWithTwoConstructors() : ObjectWithTwoConstructors(2) {} // legal in c++ 11!!
	
  const int a;
  
  //field initializer:
  const int random1 = random();
  const int random2;
};

TEST_CLASS(ConstructorDelegation)
{
public:

  TEST_METHOD(DelegatedConstructors)
  {
    ObjectWithTwoConstructors o1(11);
    Assert().AreEqual(11, o1.a);

    ObjectWithTwoConstructors o2;
    Assert().AreEqual(2, o2.a);
  }

  TEST_METHOD(FieldInitializers)
  {
    ObjectWithTwoConstructors o1;
    ObjectWithTwoConstructors o2;

    Assert().AreNotEqual(o1.random1, o2.random1);
    Assert().AreNotEqual(o1.random2, o2.random2);
  }

};


