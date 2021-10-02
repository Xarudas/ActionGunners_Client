using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.Relays
{
    public class Triggers3DMonoRelay : MonoRelay
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            RaiseOnTriggerEnterEvt(other);
        }
        private void OnTriggerStay(UnityEngine.Collider other)
        {
            RaiseOnTriggerStayEvt(other);
        }
        private void OnTriggerExit(UnityEngine.Collider other)
        {
            RaiseOnTriggerExitEvt(other);
        }
    }
}
