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


enum class NestedConstexprs : int
{
  first = constexpr_pow(2, 2),
  second = constexpr_pow(first, 2),
  third = constexpr_pow(second, 2),
  fourth = constexpr_pow(third, 2),
  fifth = constexpr_pow(fourth, 2),
  sixth = constexpr_pow(constexpr_pow(fourth, 2), 2)

};

