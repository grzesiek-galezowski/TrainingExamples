#include "stdafx.h"

#include<string>
#include<vector>
#include<typeinfo>

template<typename T> constexpr T constexpr_pow(T num, int power)
{
  return (power > 0) ? num * constexpr_pow(num, power - 1) : 1;
}

static_assert(constexpr_pow(2, 3) == 2 * 2 * 2, "This power algorithm is a piece of...");
static_assert(constexpr_pow(5, 5) == 5 * 5 * 5 * 5 * 5, "This power algorithm is a piece of...");
