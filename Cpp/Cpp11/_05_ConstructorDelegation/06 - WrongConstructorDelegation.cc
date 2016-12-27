#include "stdafx.h"

#if 0
#include<iostream>

class A
{
public:
	A(int a) { _a = a;}
// which one is correct?
//	A() : this(2) {}
//	A() : *this(2) {}
//	A() { A(2); }
//	A() { new (this) A(2); } // not so fast!
//	A() : A(2) {} // future legal
	int _a;
private:
};

int main()
{
	A a;
	std::cout << a._a << std::endl;
	return 0;
}

#endif
