using System;
using System.Collections.Generic;
using System.Text;

namespace PwnedPasswordChecker
{
    public class PasswordResult
    {
        public string PasswordHash { get; set; }
        public bool IsPwned { get; set; }
        public int TimesPwned { get; set; }
        public PwnedLevel PwnedLevel { get; set; }
    }
}
