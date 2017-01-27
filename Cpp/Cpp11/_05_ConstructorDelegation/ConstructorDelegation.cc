#include "stdafx.h"
#include <iostream>
#include <string>
#include <random>
#include <chrono>
#include <ctime>
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;
using namespace std::chrono;

auto seed = std::chrono::high_resolution_clock::now().time_since_epoch().count();
std::mt19937 random(seed); //functor!!

class ObjectWithTwoConstructors
{
public:
  const int a;

  //field initializer:
  const int randomValue1 = random();
  const int randomValue2;


	ObjectWithTwoConstructors(const int& initialValue) 
    : a(initialValue), randomValue2(random()) {}

  ObjectWithTwoConstructors() : ObjectWithTwoConstructors(2) {} // legal in c++ 11!!
	
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

    Assert().AreNotEqual(o1.randomValue1, o2.randomValue1);
    Assert().AreNotEqual(o1.randomValue2, o2.randomValue2);
  }

};


