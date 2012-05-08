using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zkdao.Domain {
    public enum eAct {
        Normal = 1,
        Delete = 2,
        Freeze = 3,
        unApproved = 4,
        Reported = 5
    }
}
