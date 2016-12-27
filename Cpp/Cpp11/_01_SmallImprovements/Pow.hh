#ifndef POW_HH
#define POW_HH

template<typename T> constexpr T constexpr_pow(T num, int power)
{
  return (power > 0) ? num * constexpr_pow(num, power - 1) : 1;
}

#endif // !POW_HH


