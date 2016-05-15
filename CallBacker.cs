using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEonAX.Shared
{
    public class CallBacker
    {
        public CallBacker(Action<eAction, object, object, object> CallBackFunction)
        {
            callbackFn = CallBackFunction;
        }

        public Action<eAction, object, object, object> callbackFn;
        public void callback(eAction action, object obj1 = null, object obj2 = null, object obj3 = null)
        {
            if (callbackFn == null) throw new NullReferenceException();
            callbackFn(action, obj1, obj2, obj3);
        }

    }
}
