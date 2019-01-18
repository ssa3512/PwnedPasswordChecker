using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PwnedPasswordChecker
{
    public class PwnedPassword : IEquatable<PwnedPassword>
    {
        private string _hashPrefix;
        private string _hashSuffix;

        public PwnedPassword(string hashPrefix, string apiResult)
        {
            int timesPwned = 0;
            if (apiResult?.Contains(":") == true)
            {
                var split = apiResult.Split(':');
                _hashSuffix = split[0];
                int.TryParse(split[1], out timesPwned);
            }

            _hashPrefix = hashPrefix;
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

    public class PwnedPasswordList : KeyedCollection<string, PwnedPassword>
    {
        public PwnedPasswordList()
        {

        }

        public PwnedPasswordList(IEnumerable<PwnedPassword> pwnedPasswords)
        {
            AddRange(pwnedPasswords);
        }

        public void AddRange(IEnumerable<PwnedPassword> pwnedPasswords)
        {
            foreach(var pwnedPassword in pwnedPasswords)
            {
                Add(pwnedPassword);
            }
        }

        protected override string GetKeyForItem(PwnedPassword item) => item.Hash;
    }

}
