using System;
using System.Collections.Generic;
using System.Text;

namespace BringIt.Orders.Core.Enums
{
    public enum OrderState
    {
        added = 0,
        pending = 10,
        confirmed = 20,
        completed = 30
    }
}
