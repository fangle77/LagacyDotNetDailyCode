using System;
using System.Collections.Generic;
using System.Threading;

namespace Suryani.LogAnalyzer.ScheduleFramework
{
    public class TaskScheduler<T>
    {
        public event Action<object, T> OnHandleComplete;
        public event Action<object, T> OnHandleStart;
        public event Action<object, string> OnMessageChange;
        public event Action<object> OnAllTaskComplete;

        protected readonly Queue<T> TaskQueue = new Queue<T>();
        protected readonly List<ITaskHandler<T>> TaskHandlers = new List<ITaskHandler<T>>();
        protected int handlerCapacity = 4;
        protected TaskScheduleStatus scheduleStatus = TaskScheduleStatus.WaitForTask;

        public string Name { get; set; }

        public TaskScheduler() { }
        public TaskScheduler(int handlerCapacity) { this.handlerCapacity = handlerCapacity; }

        protected virtual void Init()
        {

        }

        public void Start()
        {
            Init();
            DispatchTask();
            NoMoreTask();
        }

        public void Stop()
        {
            TaskHandlers.ForEach(handler => handler.Stop());
        }

        public void NoMoreTask()
        {
            scheduleStatus = TaskScheduleStatus.NoMoreTask;
            CheckAllTaskComplte();
        }

        public virtual void AddHandler(ITaskHandler<T> handler)
        {
            if (handler == null) return;
            if (TaskHandlers.Count >= handlerCapacity)
            {
                throw new ArgumentOutOfRangeException("Task handler has attain to max Capacity: " + handlerCapacity);
            }
            if (!TaskHandlers.Contains(handler))
            {
                handler.OnStatusChanged += TaskHandlerStatusChanged;
                handler.OnHandleComplete += HandleComplete;
                handler.OnHandleStart += HandleStart;
                handler.OnMessageChange += MessageChange;
                TaskHandlers.Add(handler);
            }
        }

        public virtual void PushTask(T task)
        {
            scheduleStatus = TaskScheduleStatus.WaitForTask;
            lock (TaskQueue)
            {
                TaskQueue.Enqueue(task);
            }
            DispatchTask();
        }

        private void TaskHandlerStatusChanged(object sender, TaskHandlerStatus status)
        {
            if (status == TaskHandlerStatus.Free)
            {
                DispatchTask();
                CheckAllTaskComplte();
            }
        }
        private void HandleComplete(object sender, T task)
        {
            if (OnHandleComplete != null) OnHandleComplete(sender, task);
            if (NextScheduler != null) NextScheduler.PushTask(task);
        }
        private void HandleStart(object sender, T task)
        {
            if (OnHandleStart != null) OnHandleStart(sender, task);
        }
        protected void MessageChange(object sender, string message)
        {
            if (OnMessageChange != null) OnMessageChange(sender, message);
        }
        private void CheckAllTaskComplte()
        {
            if (scheduleStatus == TaskScheduleStatus.NoMoreTask)
            {
                if (TaskQueue.Count == 0 && !TaskHandlers.Exists(h => h.Status == TaskHandlerStatus.Busy))
                {
                    if (OnAllTaskComplete != null) OnAllTaskComplete(this);
                    if (NextScheduler != null) NextScheduler.NoMoreTask();
                }
            }
        }

        private void DispatchTask()
        {
            if (TaskIndicator != null && !TaskIndicator.CanContinue())
            {
                return;
            }

            lock (TaskQueue)
            {
                foreach (var handler in TaskHandlers)
                {
                    if (TaskQueue.Count == 0) break;
                    if (handler.Status == TaskHandlerStatus.Free)
                    {
                        var task = TaskQueue.Dequeue();
                        handler.SetBusy();
                        var obj = new TaskScheduleObject() { Task = task, TaskHandler = handler };
                        ThreadPool.QueueUserWorkItem(ThreadHandle, obj);
                    }
                }
            }
        }

        private void ThreadHandle(object taskHandler)
        {
            var taskHandlerObj = taskHandler as TaskScheduleObject;
            if (taskHandlerObj != null)
            {
                taskHandlerObj.TaskHandler.Handle(taskHandlerObj.Task);
            }
        }

        private class TaskScheduleObject
        {
            public ITaskHandler<T> TaskHandler { get; set; }
            public T Task { get; set; }
        }

        public TaskScheduler<T> NextScheduler { get; set; }

        private ITaskIndicator _TaskIndicator;
        public ITaskIndicator TaskIndicator
        {
            get { return _TaskIndicator; }
            set
            {
                _TaskIndicator = value;
                if (_TaskIndicator != null)
                {
                    _TaskIndicator.OnTaskCanContinue += TaskIndicator_OnTaskCanContinue;
                }
            }
        }

        void TaskIndicator_OnTaskCanContinue()
        {
            //MessageChange(this, "========Indicator change================can continue=" + _TaskIndicator.CanContinue());
            DispatchTask();
        }
    }
}