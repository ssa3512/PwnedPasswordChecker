﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PwnedPasswordChecker
{
    public class PwnedPassword : IEquatable<PwnedPassword>
    {
        private string _hashPrefix;
        private string _hashSuffix;

        public PwnedPassword(string hashPrefix, string apiResult)
        {
            var split = apiResult.Split(':');
            int timesPwned = 0;
            int.TryParse(split[1], out timesPwned);

            _hashPrefix = hashPrefix;
            _hashSuffix = split[0];
            TimesPwned = timesPwned;
        }

        public PwnedPassword(string hashPrefix, string hashSuffix, int timesPwned)
        {
            _hashPrefix = hashPrefix;
            _hashSuffix = hashSuffix;
            TimesPwned = timesPwned;
        }

        public string Hash => _hashPrefix + _hashSuffix;

        public int TimesPwned { get; private set; }

        public bool Equals(PwnedPassword other)
        {
            if (other == null)
                return false;

            return other.Hash == Hash
                && other.TimesPwned == TimesPwned;
        }
    }
}