using System;

namespace Suryani.LogAnalyzer.ScheduleFramework
{
    public abstract class BaseTaskHandler<T> : ITaskHandler<T>
    {
        public event Action<object, TaskHandlerStatus> OnStatusChanged;

        public event Action<object, T> OnHandleStart;

        public event Action<object, T> OnHandleComplete;
        public event Action<object, string> OnMessageChange;

        private TaskHandlerStatus status;
        public TaskHandlerStatus Status
        {
            get { return status; }
            private set
            {
                var old = status;
                status = value;
                if (old != value && OnStatusChanged != null)
                {
                    OnStatusChanged(this, status);
                }
            }
        }

        public abstract string Name { get; set; }

        protected abstract void HandleTask(T task);

        protected void MessageChange(object sender, string message)
        {
            if (OnMessageChange != null) OnMessageChange(sender, message);
        }

        public void Handle(T task)
        {
            if (OnHandleStart != null) OnHandleStart(this, task);
            Status = TaskHandlerStatus.Busy;

            HandleTask(task);

            if (OnHandleComplete != null) OnHandleComplete(this, task);
            if (NextHandler != null) NextHandler.Handle(task);
            Status = TaskHandlerStatus.Free;
        }

        public void Stop()
        {
            Status = TaskHandlerStatus.Free;
            if (NextHandler != null) NextHandler.Stop();
        }

        public void SetBusy()
        {
            Status = TaskHandlerStatus.Busy;
        }

        public ITaskHandler<T> NextHandler { get; set; }

        public override string ToString()
        {
            return this.Name ?? base.ToString();
        }
    }
}