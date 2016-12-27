#pragma once

template <unsigned TNum, unsigned TPow>
struct MetaPow
{
  enum
  {
    value = MetaPow<TNum, TPow - 1>::value * TNum
  };
};

template <unsigned TNum> struct MetaPow<TNum, 0>
{
  enum
  {
    value = 1
  };
};

template <unsigned TNum>
struct MetaPow<TNum, 1>
{
  enum
  {
    value = TNum
  };
};
