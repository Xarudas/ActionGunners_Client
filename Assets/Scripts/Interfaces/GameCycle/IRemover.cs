using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IRemover<T>
    {
        void Remove(T element);
    }
}
