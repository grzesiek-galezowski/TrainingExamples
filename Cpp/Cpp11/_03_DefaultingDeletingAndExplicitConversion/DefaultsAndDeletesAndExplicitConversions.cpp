#include "stdafx.h"
#include "CppUnitTest.h"

#include<iostream>
#include<string>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

class ConversibleValue //deletes and explicit operators!
{
public:
  ConversibleValue(const ConversibleValue& other) = delete;
  explicit ConversibleValue() = default;
  ConversibleValue& operator=(const ConversibleValue& other) = default;

  //implicit:
  operator int() 
  {
    return 5;
  }

  //explicit:
  explicit operator std::string()
  {
    return "Piejo kury piejo nie majo koguta!";
  }
};


namespace _03_DefaultingDeletingAndExplicitConversion
{		
	TEST_CLASS(DefaultsAndDeletesAndExplicitConversions)
	{
	public:
		
    TEST_METHOD(DefaultAndDeletedMembers)
    {
      ConversibleValue value1; //explicit constructor is default
      //ConversibleValue value2(value1); // error - copy constructor deleted
      ConversibleValue value3; //explicit constructor is default
      value3 = value1; //assignment operator is default
    }

    TEST_METHOD(ImplicitAndExplicitConversions)
    {
      ConversibleValue value1;
      int convertedToInt = value1;
      //string convertedToString = value1; //compile error - explicit required
      string convertedToString1 = (string)value1; //OK, explicit cast
      string convertedToString2 = static_cast<string>(value1); //OK, better explicit cast
      
      //!! note the 's' suffix:
      Assert().AreEqual(
        "Piejo kury piejo nie majo koguta!"s, convertedToString1);
      Assert().AreEqual(
        "Piejo kury piejo nie majo koguta!"s, convertedToString2);

      Assert().AreEqual(5, convertedToInt);
      //!!note the to_string() usage:
      Assert().AreEqual("5"s, std::to_string(convertedToInt));
      //!!note the stoi usage
      Assert().AreEqual(std::stoi("5"s), convertedToInt);

      //stoi throws exceptions
      Assert().ExpectException<std::invalid_argument>([]() 
      { 
        std::stoi("la bamba"); 
      });
    }

	};
}