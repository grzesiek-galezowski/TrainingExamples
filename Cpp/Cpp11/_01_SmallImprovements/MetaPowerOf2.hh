#pragma once

#include "MetaPow.hh"


struct MetaPowersOf2
{
  enum
  {
    first = MetaPow<2, 1>::value,
    second = MetaPow<2, 2>::value,
    third = MetaPow<2, 3>::value,
    fourth = MetaPow<2, 4>::value,
    fifth = MetaPow<2, 5>::value,
    sixth = MetaPow<2, 6>::value
  };
};
