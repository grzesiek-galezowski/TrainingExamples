#if 0
#include<vector>
#include<iostream>

template<typename T> constexpr T constexpr_pow(T num, int power)
{
	return (power > 0) ? num * constexpr_pow(num, power - 1) : 1;
}

enum class PowerOf2 : int
{
	first = constexpr_pow(2, 1),
	second = constexpr_pow(2, 2),
	third = constexpr_pow(2, 3),
	fourth = constexpr_pow(2, 4),
	fifth = constexpr_pow(2, 5),
	sixth = constexpr_pow(2, 6)
};

int main()
{
	std::vector<PowerOf2> powers = {
			PowerOf2::first
			, PowerOf2::second
			, PowerOf2::third
			, PowerOf2::fourth
			, PowerOf2::fifth
	};

	for (PowerOf2& value : powers)
	{
		std::cout << u8"Następna potęga dwójki: " << std::endl;

		// std::cout << value << std::endl; //=> error!

		auto printablePower = static_cast<int>(value);
		std::cout << printablePower << std::endl;
	}

	return 0;
}
#endif
