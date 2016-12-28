#include "stdafx.h"
#include<string>
#include<vector>
#include<typeinfo>

template<typename T> int foo(T var)
{
//  	static_assert(sizeof(T) < 1000,
//  			"Object too big to fit into the internal cache.");
  return 0;
}

class TooBigObject
{
public:
  int arr[1000000];
};

/////
int main()
{
  foo(123);
  foo(TooBigObject()); //compile error - static assertion fails

  return 0;
}
