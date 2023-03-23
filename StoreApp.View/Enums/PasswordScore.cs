using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.View.Enums
{
    public enum PasswordScore
    {
        NoNumberAndChar = 0,
        NoChar = 1,
        NoNumber = 2,
        Strong = 3
    }
}
