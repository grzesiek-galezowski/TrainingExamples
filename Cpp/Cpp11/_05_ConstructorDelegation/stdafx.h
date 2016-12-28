// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

// Headers for CppUnitTest
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace Microsoft
{
  namespace VisualStudio
  {
    namespace CppUnitTestFramework
    {
      template<> static std::wstring ToString<long long>(const long long& t) {
        RETURN_WIDE_STRING(t);
      }
    }
  }
}