#include "stdafx.h"

#if 0

#include <future>
#include <iostream>
#include <cstdio>
#include <vector>

using namespace std;

int main()
{
	std::vector<int> v = {1, 2, 3, 4, 5, 6};

	auto additionLambda1 =
	[&]()
	{
		return v[0] + v[1] + v[2];
	};

	auto additionLambda2 =
	[&]()
	{
		return v[3] + v[4] + v[5];
	};


	auto activeObject1 = std::async(additionLambda1);
	auto activeObject2 = std::async(additionLambda2);

	std::cout << activeObject1.get() + activeObject2.get() << std::endl;

	return 0;
}
#endif
