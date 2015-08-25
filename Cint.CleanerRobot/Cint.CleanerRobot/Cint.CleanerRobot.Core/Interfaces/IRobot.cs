using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cint.CleanerRobot.Core
{
    public interface IRobot
    {
        int ExecuteClean(CleanningSession session);
    }
}
