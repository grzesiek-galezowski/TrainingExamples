#pragma once

#include "Pow.hh"

enum class PowersOf2 : int
{
  first = constexpr_pow(2, 1),
  second = constexpr_pow(2, 2),
  third = constexpr_pow(2, 3),
  fourth = constexpr_pow(2, 4),
  fifth = constexpr_pow(2, 5),
  sixth = constexpr_pow(2, 6)
};

