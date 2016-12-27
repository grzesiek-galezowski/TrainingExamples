#if 0
#include<iostream>
#include<string>

class TrendyObject
{
public:
	TrendyObject(const TrendyObject& other) = delete;
	explicit TrendyObject() = default;
	TrendyObject& operator=(const TrendyObject& other) = default;

	operator int()
	{
		return 5;
	}

	explicit operator std::string()
	{
		return "Piejo kury piejo na mego koguta!";
	}
};


int main()
{
	TrendyObject object1;
//	TrendyObject object2(object1); // compile error
	TrendyObject object3;
	object3 = object1;



	int trendyAsInt = object1;
//	std::string trendyAsString = object1; //compile error
	std::string trendyAsString = (std::string)object1;

	std::cout << trendyAsInt << std::endl;
	std::cout << trendyAsString << std::endl;

	return 0;
}

#endif


