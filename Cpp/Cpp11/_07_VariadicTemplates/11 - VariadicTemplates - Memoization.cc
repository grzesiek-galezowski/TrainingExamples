#include "stdafx.h"

#if 0
#include<functional>
#include<map>
#include<iostream>
#include<iomanip>

#define W(obj) std::setw(3) << obj

#define __GXX_EXPERIMENTAL_CXX0X__

template <typename ReturnType, typename... Args>
std::function<ReturnType (Args...)>
memoize(std::function<ReturnType (Args...)> func)
{
    std::map<std::tuple<Args...>, ReturnType> cache;
    return (
		[=](Args... args) mutable
		{
			std::tuple<Args...> t(args...);
			if (cache.find(t) == cache.end())
			{
				cache[t] = func(args...);
			}
			return cache[t];
		}
    );
}

int foo(int x, int y, int z)
{
	std::cout << "Args: " << W(x) << W(y) << W(z) << std::endl;
	return x + y + z;
}

int main()
{
	auto memoizedFoo = memoize( std::function<int(int, int, int)>(foo));

	std::cout << memoizedFoo(1, 2, 3) << std::endl;
	std::cout << memoizedFoo(1, 2, 3) << std::endl;
	std::cout << memoizedFoo(1, 2, 3) << std::endl;
	std::cout << memoizedFoo(1, 2, 3) << std::endl;

	std::cout << memoizedFoo(2, 2, 3) << std::endl;
	std::cout << memoizedFoo(2, 2, 3) << std::endl;
	std::cout << memoizedFoo(2, 2, 3) << std::endl;

}


#endif
