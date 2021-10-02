using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.GameLoop
{
    public interface ICoroutineManager
    {
        ICoroutine StartCoroutine(Action<float> action, float stopAfterTime);
        ICoroutine StartCoroutine(Action<float> action);
        ICoroutine ExecuteAfterDelay(Action action, float delay);
    }
}
