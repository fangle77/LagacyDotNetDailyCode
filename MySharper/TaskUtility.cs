using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySharper
{
    class TaskUtility
    {
        public static void ParallelTask(List<Action> actionList)
        {
            ParallelTask(actionList, Environment.ProcessorCount);
        }

        public static void ParallelTask(List<Action> actionList, int taskCountPerTime)
        {
            if (actionList == null || actionList.Count == 0) return;
            if (taskCountPerTime <= 1 || actionList.Count == 1)
            {
                SerialTask(actionList);
                return;
            }

            var enumer = actionList.GetEnumerator();

            int taskCount = Math.Min(actionList.Count, taskCountPerTime);

            Task[] tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                enumer.MoveNext();
                tasks[i] = Task.Factory.StartNew(enumer.Current);
            }

            while (enumer.MoveNext())
            {
                int idx = Task.WaitAny(tasks);
                tasks[idx] = Task.Factory.StartNew(enumer.Current);
            }
            Task.WaitAll(tasks);
        }

        public static void SerialTask(List<Action> actionList)
        {
            foreach (var action in actionList)
            {
                action();
            }
        }
    }
}
