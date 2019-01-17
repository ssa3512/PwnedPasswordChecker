using System;
using System.Collections.Generic;
using System.Text;

namespace PwnedPasswordChecker.Test
{
    class KnownPwnedPasswords
    {
        // Examples from https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/
        public static readonly Dictionary<string, PwnedPassword> PwnedPasswords = new Dictionary<string, PwnedPassword>
        {
            {"lauragpe", new PwnedPassword("21BD1", "0018A45C4D1DEF81644B54AB7F969B88D65", 1) },
            {"alexguo029", new PwnedPassword("21BD1", "00D4F6E8FA6EECAD2A3AA415EEC418D38EC", 2) },
            {"BDnd9102", new PwnedPassword("21BD1","011053FD0102E94D6AE2F8B83D76FAF94F6",1) },
            {"melobie", new PwnedPassword("21BD1","012A7CA357541F0AC487871FEEC1891C49C",2) },
            {"quvekyny", new PwnedPassword("21BD1","0136E006E24E7D152139815FB0FC6A50B15",2) }
        };

        public static string GetPwnedPasswordsInApiFormat()
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var pwnedPassword in PwnedPasswords.Values)
            {
                if (!first)
                {
                    sb.AppendLine();
                }
                sb.Append(pwnedPassword.Hash.Substring(5));
                sb.Append(":");
                sb.Append(pwnedPassword.TimesPwned);
                first = false;
            }

            return sb.ToString();
        }
    }
}
