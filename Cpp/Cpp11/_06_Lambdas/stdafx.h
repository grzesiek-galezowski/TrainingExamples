// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

// Headers for CppUnitTest
#include "CppUnitTest.h"
#include<chrono>


inline constexpr std::chrono::hours operator "" _d(unsigned long long value)
{	// return integral hours
  return (std::chrono::hours(value * 24));
}

// TODO: reference additional headers your program requires here
