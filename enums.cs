using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEonAX.Shared
{
    public enum eAction
    {
        Message,
        Error,
        State,
        Notification,
        RestError,
        WS_Error,
        Cache_Error,
    }
    public enum RetCode
    {
        Maybe = -1,
        No,
        Yes,
        False,
        True,
        Unsuccessful,
        Successful
    }
}
