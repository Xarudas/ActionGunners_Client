using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.Relays
{
    public class Full3DMonoRelay : Triggers3DMonoRelay
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            RaiseOnCollisionEnterEvt(collision);
        }
        private void OnCollisionStay(UnityEngine.Collision collision)
        {
            RaiseOnCollisionStayEvt(collision);
        }
        private void OnCollisionExit(UnityEngine.Collision collision)
        {
            RaiseOnCollisionExitEvt(collision);
        }
    }
}
