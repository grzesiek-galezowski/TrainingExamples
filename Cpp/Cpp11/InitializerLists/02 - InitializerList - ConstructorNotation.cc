#include "stdafx.h"

#if 0

#include<iostream>
#include<initializer_list>

class Object
{
public:
	explicit Object(int a, double b, char c) {	}
};

int main()
{
	Object o1{1, 2.5, 'c'};
//	Object o2 = {1, 2.5, 'c'}; //comment out explicit for this to work
	return 0;
}
#endif
