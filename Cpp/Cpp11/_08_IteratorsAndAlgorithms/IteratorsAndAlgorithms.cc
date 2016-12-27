#include "stdafx.h"
#include<iostream>
#include<iomanip>
#include<functional>
#include<algorithm>
#include<numeric>
#include<set>
#include<vector>
#include<string>
#include<iterator>

#define EXAMPLE_5

#ifdef EXAMPLE_1
int main()
{
  int array[] = { 1,9,2,8,3,7,4,6,5};

  std::set<int, std::less<int> > lessSet(array, array + sizeof(array)/sizeof(int));
  std::set<int, std::greater<int> > greaterSet(array, array + sizeof(array)/sizeof(int));


  std::copy(lessSet.begin(), lessSet.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  std::copy(greaterSet.begin(), greaterSet.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  return 0;
}
#endif

#if defined(EXAMPLE_2) || defined(EXAMPLE_3) || defined(EXAMPLE_4) || defined(EXAMPLE_5)

template<typename T> class IncrementalGenerator
{
public:
  IncrementalGenerator(T initStartValue, T initIncrement)
  {
    currentValue = initStartValue;
    increment = initIncrement;
  }

  T operator()()
  {
    T returnValue = currentValue;
    currentValue += increment;
    return returnValue;
  }

private:
  T increment;
  T currentValue;
};

#endif

#ifdef EXAMPLE_2

int main()
{
  //liczby całkowite
  std::cout << "Dodawanie liczb" << std::endl;
  std::vector<int> vec(10);
  std::generate(vec.begin(), vec.end(), IncrementalGenerator<int>(1, 2));
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  //napisy
  std::cout << "Dodawanie napisów" << std::endl;
  std::vector<std::string> strVec(10);
  std::generate(strVec.begin(), strVec.end(), IncrementalGenerator<std::string>("miau", " miau"));
  std::copy(strVec.begin(), strVec.end(), std::ostream_iterator<std::string>(std::cout, "\n"));
  std::cout << std::endl;

  //Przekazywanie obiektów funkcyjnych:
  IncrementalGenerator<int> gen(1, 2);

  //1. przekazanie przez wartość:
  std::cout << "Przekazanie przez wartość" << std::endl;

  std::generate(vec.begin(), vec.end(), gen);
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  std::generate(vec.begin(), vec.end(), gen);
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  //2. przekazanie przez referencję:
  std::cout << "Przekazanie przez referencję" << std::endl;

  std::generate<std::vector<int>::iterator, IncrementalGenerator<int>&> (vec.begin(), vec.end(), gen);
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  std::generate<std::vector<int>::iterator, IncrementalGenerator<int>&> (vec.begin(), vec.end(), gen);
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  return 0;
}

#endif

#ifdef EXAMPLE_3

class Nth
{
public:
  Nth(int n) : nth(n), count(0) { }

  bool operator()(int dummyArg) { return ++count == nth; }
private:
  int nth;
  int count;
};

int main()
{
  //Nth - bez stałej funkcji składowej
  std::vector<int> vec(8);
  std::generate(vec.begin(), vec.end(), IncrementalGenerator<int> (1, 1));
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  std::vector<int>::iterator remPosition;
  remPosition = std::remove_if(vec.begin(), vec.end(), Nth(3));
  vec.erase(remPosition, vec.end());
  std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
  std::cout << std::endl;

  //wniosek - predykat nie powinien zmieniać swojego stanu,
  //operator() zawsze powinien być skłądową stałą

  return 0;
}

#endif

#ifdef EXAMPLE_4 //predefiniowane obiekty funcyjne - jedno i dwuargumentowe

int main(void)
{
  { //obiekt funkcyjny dwuargumentowy
    std::cout << "--------------------------------------------------------" << std::endl;
    std::cout << "obiekt funkcyjny dwuargumentowy" << std::endl;
    std::cout << std::divides<double>()(1, 2) << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;
  }

  { //bind1st, bind2nd
    std::cout << "bind1st: 1/2" << std::endl;
    std::cout << std::bind1st(std::divides<double>(), 1)(2) << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "bind2nd: 2/1" << std::endl;
    std::cout << std::bind2nd(std::divides<double>(), 1)(2) << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;
  }

  {
    std::vector<int> vec(10);

    std::cout << "Ciag pierwotny:" << std::endl;
    std::generate(vec.begin(), vec.end(), IncrementalGenerator<int>(10, 3));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Po zanegowaniu:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::negate<int>());
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Po podzieleniu przez 5:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::divides<int>(), 5));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Po dodaniu 5:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::plus<int>(), 5));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Reszty z dzielenia elementów ciągu przez 2:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::modulus<int>(), 2));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Po pomnożeniu przez 16:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::multiplies<int>(), 16));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Po odjęciu 12:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::minus<int>(), 12));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "Wyniki sprawdzenia, czy element jest większy bądź równy 0:" << std::endl;
    std::transform(vec.begin(), vec.end(), vec.begin(), std::bind2nd(std::greater_equal<int>(), 0));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<bool>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;

    std::cout << "To samo co powyżej, tylko wypisane jako napisy, a nie cyfry:" << std::endl;
    std::cout << std::boolalpha;
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<bool>(std::cout, " "));
    std::cout << std::endl;
    std::cout << "--------------------------------------------------------" << std::endl;
  }

  return 0;
}

#endif

#ifdef EXAMPLE_5 //adaptatory funkcji i metod do obiektów funkcyjnych

bool isEven(int num)
{
  return 0 == (num % 2);
}

class HiddenOne
{
public:
  HiddenOne(bool initIsHidden) : hidden(initIsHidden) {}
  bool isHidden() { return hidden; }
  friend std::ostream& operator<<(std::ostream& out, const HiddenOne& hiddenOne);
private:
  bool hidden;
};

std::ostream& operator<<(std::ostream& out, const HiddenOne& hiddenOne)
{
  out << hiddenOne.hidden << " ";
  return out;
}

int main()
{
  {
    //fun_ptr:
    std::vector<int> vec(9);
    std::generate(vec.begin(), vec.end(), IncrementalGenerator<int>(11, 5));
    std::copy(vec.begin(), vec.end(), std::ostream_iterator<int>(std::cout, " "));
    std::cout << std::endl;
    std::vector<int>::reverse_iterator rit;
    rit = std::find_if(vec.rbegin(), vec.rend(), std::ptr_fun(isEven));
    std::cout << *rit << std::endl;
  }

  {
    //mem_fun_ref
    HiddenOne h1(false), h2(true), h3(true), h4(false), h5(true);
    std::vector<HiddenOne> hiddenArmy;
    hiddenArmy.push_back(h1);
    hiddenArmy.push_back(h2);
    hiddenArmy.push_back(h3);
    hiddenArmy.push_back(h4);
    hiddenArmy.push_back(h5);
    std::copy(hiddenArmy.begin(), hiddenArmy.end(), std::ostream_iterator<HiddenOne>(std::cout, " "));
    std::cout << std::endl;

    std::vector<HiddenOne>::iterator it =
      std::remove_if(hiddenArmy.begin(), hiddenArmy.end(), std::mem_fun_ref(&HiddenOne::isHidden));
    hiddenArmy.erase(it, hiddenArmy.end());

    std::cout << std::boolalpha;
    std::copy(hiddenArmy.begin(), hiddenArmy.end(), std::ostream_iterator<HiddenOne>(std::cout, " "));
    std::cout << std::endl;
  }

  {
    //mem_fun
    HiddenOne h1(false), h2(true), h3(true), h4(false), h5(true);
    std::vector<HiddenOne*> hiddenArmy;
    hiddenArmy.push_back(&h1);
    hiddenArmy.push_back(&h2);
    hiddenArmy.push_back(&h3);
    hiddenArmy.push_back(&h4);
    hiddenArmy.push_back(&h5);
    std::vector<HiddenOne*>::iterator it =
      std::remove_if(hiddenArmy.begin(), hiddenArmy.end(), std::mem_fun(&HiddenOne::isHidden));
    hiddenArmy.erase(it, hiddenArmy.end());

    for(unsigned int i = 0 ; i < hiddenArmy.size() ; ++i)
    {
      std::cout << *hiddenArmy[i] << " ";
    }
    std::cout << std::endl;

  }
  return 0;
}
#endif
