using System.Numerics;
using System.Security.Cryptography;

namespace Janda.Runtime.Watchers
{
    internal class MethodParameter
    {
        public string Name { get; set; }
        public int Hash { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as MethodParameter;
            return Name == other.Name && Hash == other.Hash;
        }

        public override int GetHashCode()
        {
            return Hash;
        }


        private static int GetHashCode(object o)
        {
            if (o is byte[])
            {
                using (var md5 = MD5.Create())
                {
                    var b = o as byte[];
                    return new BigInteger(md5.ComputeHash(b)).GetHashCode();
                }
            }

            return o.GetHashCode();
        }

        public static MethodParameter Create(string name, object value)
        {
            return new MethodParameter()
            {
                Name = name,
                Hash = GetHashCode(value)
            };
        }
    }
}
