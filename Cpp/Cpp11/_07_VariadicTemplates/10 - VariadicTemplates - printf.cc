#include "stdafx.h"

//#if 0
#include<iostream>
#include<exception>
#include<stdexcept>
#include<cstdio>

void tsprintf(const char* s)
{
	while (*s)
	{
		if (*s == '%' && *++s != '%')
		{
			throw std::runtime_error("invalid format string: missing arguments");
		}
		std::cout << *s++ << std::flush;
	}
}

template<typename T, typename ...Args>
void tsprintf(const char* s, const T& value, const Args&... args)
{
	while (*s)
	{
		if (*s == '%' && *++s != '%')
		{
			std::cout << value;
			return tsprintf(s, args...);
		}
		std::cout << *s++;
	}
	throw std::runtime_error("extra arguments provided to printf");
}


int main()
{
	tsprintf("The following values have been supplied: %, %, %\n", "lalamido", 123, "aa");
	std::printf("The following values have been supplied: %d, %d, %s\n", "lalamido", 123, "aa");
	return 0;
}

//#endif


