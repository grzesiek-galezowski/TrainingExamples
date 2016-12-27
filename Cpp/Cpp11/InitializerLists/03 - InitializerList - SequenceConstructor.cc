#include "stdafx.h"

#if 0

#include<iostream>
#include<initializer_list>

class Object
{
public:
	Object(const std::initializer_list<int>& args)
	{
		for(std::initializer_list<int>::iterator it = args.begin(); it != args.end() ; ++it)
		{
			std::cout << *it;
		}
		std::cout << "\n";
	}
};

int main()
{
	Object o0({1, 2, 3, 4, 5, 7, 45, 3, 5});

	Object o1{1, 2, 3, 4, 5, 7, 45, 3, 5};

	Object o2 = {1, 2, 3, 4, 5, 7, 45, 3, 5};

	//It's just an object:
	std::initializer_list<double> il{2.3, 4.5, 5.6, 6.7};

	return 0;
}
#endif
