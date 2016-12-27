#include "stdafx.h"

#if 0
#include<iostream>
#include<algorithm>
#include<chrono>
using namespace std::chrono;
using namespace std;

namespace std
{
	typedef system_clock::time_point date;
}

#define NOW system_clock::now()

struct Task
{
	Task(const date& initStartDate
			, const date& initEndDate
			, const string initName)
	: startDate(initStartDate)
	, endDate(initEndDate)
	, name(initName) { }

	date startDate;
	date endDate;
	string name;
};


int main()
{
	std::vector<Task> tasks = {
			  Task(NOW - minutes(30), NOW - minutes(20), "Clean the toilet")
			, Task(NOW              , NOW + minutes(50), "Finish this presentation")
			, Task(NOW - minutes(99), NOW + minutes(99), "Dance")
	};

	auto printTaskName =
			[](const Task& t)
			{
				cout << t.name << std::endl;
			};

	///////// by start date
	sort(tasks.begin(), tasks.end(),
			[](const Task& t1, const Task& t2)
			{
				return t1.startDate < t2.startDate;
			}
	);

	for_each(tasks.begin(), tasks.end(), printTaskName);

	///////// by end date
	sort(tasks.begin(), tasks.end(),
			[](const Task& t1, const Task& t2)
			{
				return t1.endDate < t2.endDate;
			}
	);

	for_each(tasks.begin(), tasks.end(), printTaskName);

	///////// by name
	sort(tasks.begin(), tasks.end(),
			[](const Task& t1, const Task& t2)
			{
				return t1.name < t2.name;
			}
	);

	for_each(tasks.begin(), tasks.end(), printTaskName);



	return 0;
}
#endif
