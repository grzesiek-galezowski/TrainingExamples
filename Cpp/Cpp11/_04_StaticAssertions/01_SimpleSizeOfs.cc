#include "stdafx.h"

#include<typeinfo>
#include<string>
#include<vector>

static_assert(sizeof(int) == 4, "sizeof(int) is not 4 bytes");
static_assert(sizeof(char) == 1, "sizeof(char) is not 1 byte");
//static_assert(sizeof(std::string) == sizeof(std::vector<char>),
//		"string and char vector are not the same!");
