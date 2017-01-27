#include "stdafx.h"

#include<iostream>
#include<algorithm>
#include<functional>
#include<numeric>
#include<chrono>
#include<vector>
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace std::chrono;
using namespace std;

auto now()
{
  return system_clock::now();
}

/* //equivalent of:
auto now = []() { return system_clock::now(); };
*/

struct Task
{
	Task(const system_clock::time_point& initStartDate
			, const system_clock::time_point& initEndDate
			, const string& initName)
	: startDate(initStartDate)
	, endDate(initEndDate)
	, name(initName) { }

	system_clock::time_point startDate;
	system_clock::time_point endDate;
	string name;
};

//later
auto appendTaskNameTo(string& str)
{
  return [&str](const Task& t)
  {
    str.append(t.name);
  };
}


vector<Task> createTasks()
{
  return std::vector<Task>{
      Task(now() - 30min, now() - 20min, "Clean the toilet")
    , Task(now()        , now() + 50min, "Finish this presentation")
    , Task(now() - 99min, now() + 99min, "Dance")
    , Task(now() - minutes(1), now() + minutes(9), "Whatever")
  };
}


namespace _06_Lambdas
{
  TEST_CLASS(_06_Lambdas)
  {
  public:

    TEST_METHOD(SortingTasksFromEarliestToLatestStarted)
    {
      vector<Task> tasks = createTasks();

      sort(tasks.begin(), tasks.end(),
        [](const Task& t1, const Task& t2)
      {
        return t1.startDate < t2.startDate;
      }
      );

      Assert().AreEqual("Dance"s, tasks[0].name);
      Assert().AreEqual("Clean the toilet"s, tasks[1].name);
      Assert().AreEqual("Whatever"s, tasks[2].name);
      Assert().AreEqual("Finish this presentation"s, tasks[3].name);
    }

    TEST_METHOD(SortingTasksFromLatestToEarliestStarted)
    {
      vector<Task> tasks = createTasks();

      sort(tasks.rbegin(), tasks.rend(),
        [](const Task& t1, const Task& t2)
      {
        return t1.startDate < t2.startDate;
      }
      );

      Assert().AreEqual("Finish this presentation"s, tasks[0].name);
      Assert().AreEqual("Whatever"s, tasks[1].name);
      Assert().AreEqual("Clean the toilet"s, tasks[2].name);
      Assert().AreEqual("Dance"s, tasks[3].name);
    }

    TEST_METHOD(SortingTasksFromEarliestToLatestFinished)
    {
      vector<Task> tasks = createTasks();

      sort(tasks.begin(), tasks.end(),
        [](const Task& t1, const Task& t2)
      {
        return t1.endDate < t2.endDate;
      });

      Assert().AreEqual("Clean the toilet"s, tasks[0].name);
      Assert().AreEqual("Whatever"s, tasks[1].name);
      Assert().AreEqual("Finish this presentation"s, tasks[2].name);
      Assert().AreEqual("Dance"s, tasks[3].name);
    }



    TEST_METHOD(AddingADayToEachTaskStartAndEndDateUsingTransform)
    {
      vector<Task> tasks = createTasks();
      vector<Task> reschedulesTasks(tasks);

      transform(tasks.begin(), tasks.end(), reschedulesTasks.begin(),
        [](const Task& baseTask)
      {
        Task rescheduledTask = baseTask;
        rescheduledTask.startDate += 1_d;
        rescheduledTask.endDate += 1_d;
        return rescheduledTask;
      });

      Assert().IsTrue(
        reschedulesTasks[0].startDate - tasks[0].startDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[1].startDate - tasks[1].startDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[2].startDate - tasks[2].startDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[3].startDate - tasks[3].startDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[0].endDate - tasks[0].endDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[1].endDate - tasks[1].endDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[2].endDate - tasks[2].endDate == 1_d);
      Assert().IsTrue(
        reschedulesTasks[3].endDate - tasks[3].endDate == 1_d);
    }

    TEST_METHOD(AppendingTaskNamesUsingForeach)
    {
      vector<Task> tasks = createTasks();
      string str;
      for_each(tasks.begin(), tasks.end(), appendTaskNameTo(str));

      Assert().AreEqual(
        "Clean the toiletFinish this presentationDanceWhatever"s, str);
    }

    TEST_METHOD(AppendingTaskNamesUsingAccumulate)
    {
      vector<Task> tasks = createTasks();
      
      string str = std::accumulate(tasks.begin(), tasks.end(),
        ""s, [](string aggregatedValue, Task currentTask) 
      {
        return aggregatedValue + currentTask.name;
      });

      Assert().AreEqual(
        "Clean the toiletFinish this presentationDanceWhatever"s, str);
    }
  };
}


