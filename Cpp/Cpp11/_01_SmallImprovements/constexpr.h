#pragma once

#include "stdafx.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std;

namespace Microsoft
{
  namespace VisualStudio
  {
    namespace CppUnitTestFramework
    {
      template<> static std::wstring ToString<PowerOf2>(const PowerOf2& t) {
        RETURN_WIDE_STRING(static_cast<int>(t));
      }
    }
  }
}


int main();
