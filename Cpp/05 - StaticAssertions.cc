#if 0
#include<string>
#include<vector>
#include<typeinfo>

/////////////////////// #1 simple sizeofs
static_assert(sizeof(int) == 4, "sizeof(int) is not 4 bytes");
static_assert(sizeof(char) == 1, "sizeof(char) is not 1 byte");
//static_assert(sizeof(std::string) == sizeof(std::vector<char>),
//		"string and char vector are not the same!");

/////////////////////// #2 checking template arguments
template<typename T> int foo(T var)
{
//	static_assert(sizeof(T) < 1000,
//			"Object too big to fit into the internal cache.");
	return 0;
}

class TooBigObject
{
public:
	int arr[1000000];
};

/////////////////////// #3
#define PACKED

PACKED struct Frame
{
	int a; int b; char c;
};

//static_assert(sizeof(Frame) == sizeof(Frame::a) + sizeof(Frame::b) + sizeof(Frame::c), "Packing doesn't work!");

///////////////////// #4

template<typename T> constexpr T constexpr_pow(T num, int power)
{
	return (power > 0) ? num * constexpr_pow(num, power - 1) : 1;
}

static_assert( constexpr_pow(2, 3) == 2*2*2, "This power algorithm is a piece of..." );
static_assert( constexpr_pow(5, 5) == 5*5*5*5*5, "This power algorithm is a piece of..." );

////////////////////// #5
enum class Enumeration : long long
{
	A, B, C, D = 0xffffffffffff
};

static_assert(sizeof(Enumeration) == sizeof(Enumeration::D), "Hey Ya!");

/////
int main()
{
	foo(123);
	foo(TooBigObject()); //compile error - static assertion fails

	return 0;
}

#endif
