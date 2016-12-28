#include "stdafx.h"

#include<iostream>
#include<algorithm>
#include<chrono>
#include<vector>
using namespace std::chrono;
using namespace std;

namespace std
{
	typedef system_clock::time_point date;
}

#define NOW system_clock::now()

struct Task
{
	Task(const system_clock::time_point& initStartDate
			, const system_clock::time_point& initEndDate
			, const string& initName)
	: startDate(initStartDate)
	, endDate(initEndDate)
	, name(initName) { }

	date startDate;
	date endDate;
	string name;
};


int main789()
{
	std::vector<Task> tasks = {
			  Task(NOW - 30min, NOW - 20min, "Clean the toilet")
			, Task(NOW        , NOW + 50min, "Finish this presentation")
			, Task(NOW - 99min, NOW + 99min, "Dance")
      , Task(NOW - minutes(1), NOW + minutes(9), "Whatever")
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
