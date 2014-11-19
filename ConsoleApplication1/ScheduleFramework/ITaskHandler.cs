using System;

namespace Suryani.LogAnalyzer.ScheduleFramework
{
    public interface ITaskHandler<T>
    {
        event Action<object, TaskHandlerStatus> OnStatusChanged;
        event Action<object, T> OnHandleStart;
        event Action<object, T> OnHandleComplete;
        event Action<object, string> OnMessageChange;

        TaskHandlerStatus Status { get; }

        string Name { get; set; }

        void Handle(T task);
        void Stop();

        void SetBusy();

        ITaskHandler<T> NextHandler { get; set; }
    }
}