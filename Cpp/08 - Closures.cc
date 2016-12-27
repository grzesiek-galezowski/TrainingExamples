#include <functional>
#include <iostream>

std::function<int ()> sequencer(int start)
{
    return [=]() mutable
    {
        start++;
        return start;
    };
}

int main()
{
    auto s1 = sequencer(2);
    std::cout << s1();
    std::cout << s1();
    std::cout << s1();
    std::cout << s1();
    std::cout << s1();

    std::cout << std::endl;

    auto s2 = sequencer(2);
    std::cout << s2();
    std::cout << s2();
    std::cout << s2();
    std::cout << s2();
    std::cout << s2();

    return 0;
}
