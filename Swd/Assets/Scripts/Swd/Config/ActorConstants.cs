using System.Collections.Generic;
using swd.character;

namespace swd.Config
{
    public class ActionConstants
    {
        public static Dictionary<EAction, string> ActionNameMap = new()
        {
            { EAction.Stand,"c01"},
            { EAction.Walk,"c02"},
            { EAction.Run,"c03"},
            { EAction.BackStep,"c15"},
        };
    }
}