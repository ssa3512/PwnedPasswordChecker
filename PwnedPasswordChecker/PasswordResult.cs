using System;
using System.Collections.Generic;
using System.Text;

namespace PwnedPasswordChecker
{
    public class PasswordResult
    {
        DateTimeOffset LastUpdateDate { get; set; }
        bool IsPwned { get; set; }
        int TimesPwned { get; set; }
    }
}
