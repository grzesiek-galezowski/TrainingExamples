#include "stdafx.h"

#include<string>
#include<vector>
#include<typeinfo>

enum class Enumeration : long long
{
	A, B, C, D = 0xffffffffffff
};

static_assert(sizeof(Enumeration) == sizeof(Enumeration::A), "Hey Ya!");
static_assert(sizeof(Enumeration) == sizeof(Enumeration::B), "Hey Ya!");
static_assert(sizeof(Enumeration) == sizeof(Enumeration::C), "Hey Ya!");
static_assert(sizeof(Enumeration) == sizeof(Enumeration::D), "Hey Ya!");
static_assert(sizeof(Enumeration) == sizeof(long long), "Hey Ya!");

