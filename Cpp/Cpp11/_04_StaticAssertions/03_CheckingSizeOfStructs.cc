#include "stdafx.h"

#define PACKED /* whatever */

PACKED struct Frame
{
  int a; int b; char c;
};

//static_assert(
//  sizeof(Frame) == sizeof(Frame::a) + sizeof(Frame::b) + sizeof(Frame::c), 
//  "Packing doesn't work!");
