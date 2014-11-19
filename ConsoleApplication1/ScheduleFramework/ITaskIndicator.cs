using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suryani.LogAnalyzer.ScheduleFramework
{
    public interface ITaskIndicator
    {
        event Action OnTaskCanContinue;

        bool CanContinue();
    }
}
