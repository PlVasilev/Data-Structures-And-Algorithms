﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[TestFixture]
public class Perf7
{

    [TestCase]
    public void ChangePriority_ThreadExecutor_ShouldWorkFast()
    {

        //Arange
        const int items = 100_000;
        IScheduler executor = new ThreadExecutor();

        Stopwatch watch = new Stopwatch();
        Dictionary<Priority, List<Task>> dict = new Dictionary<Priority, List<Task>>();
        dict.Add(Priority.LOW, new List<Task>());
        dict.Add(Priority.MEDIUM, new List<Task>());
        dict.Add(Priority.HIGH, new List<Task>());
        dict.Add(Priority.EXTREME, new List<Task>());

        Priority[] priorities = new Priority[] { Priority.LOW, Priority.MEDIUM, Priority.HIGH, Priority.EXTREME };
        Random rand = new Random();

        //Act
        for (int i = 0; i < items; i++)
        {
            Task task = new Task(i, rand.Next(0, 10000), priorities[rand.Next(0, 4)]);
            dict[task.TaskPriority].Add(task);
            executor.Execute(task);
        }

        dict[Priority.LOW] = dict[Priority.LOW].OrderByDescending(x => x.Id).ToList();
        dict[Priority.MEDIUM] = dict[Priority.MEDIUM].OrderByDescending(x => x.Id).ToList();
        dict[Priority.HIGH] = dict[Priority.HIGH].OrderByDescending(x => x.Id).ToList();
        dict[Priority.EXTREME] = dict[Priority.EXTREME].OrderByDescending(x => x.Id).ToList();
        //Act

        Priority p1 = priorities[rand.Next(0, 4)];
        Priority p2 = priorities[rand.Next(0, 4)];

        int randomCount = rand.Next(5000, 6000);
        List<Task> p2Tasks = dict[p2].Skip(randomCount - (randomCount / 2)).Take(randomCount).ToList();
        List<Task> p1Tasks = dict[p1].Skip(randomCount - (randomCount / 2)).Take(randomCount).ToList();

        watch.Start();
        int min = Math.Min(p1Tasks.Count, p2Tasks.Count);
        for (int i = 0; i < min; i++)
        {
            executor.ChangePriority(p1Tasks[i].Id, p2);
            executor.ChangePriority(p2Tasks[i].Id, p1);
        }
    
        watch.Stop();

        dict[p1].RemoveRange(randomCount - (randomCount / 2), randomCount);
        dict[p2].RemoveRange(randomCount - (randomCount / 2), randomCount);
        dict[p1].AddRange(p2Tasks);
        dict[p2].AddRange(p1Tasks);

        dict[p1] = dict[p1].OrderByDescending(x => x.Id).ToList();
        dict[p2] = dict[p2].OrderByDescending(x => x.Id).ToList();

        CollectionAssert.AreEqual(dict[p1], executor.GetByPriority(p1));
        CollectionAssert.AreEqual(dict[p2], executor.GetByPriority(p2));

        //Assert

        Assert.Less(watch.ElapsedMilliseconds, 200);

    }

}
