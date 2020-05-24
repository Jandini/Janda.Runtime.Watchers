using System;
using System.Collections.Generic;

namespace Janda.Runtime.Watchers
{
    internal class MethodParameters : List<MethodParameter>
    {
        public override bool Equals(object obj)
        {
            var other = obj as MethodParameters;

            int count = other.Count;

            if (count != Count)
                return false;

            for (int i = 0; i < count; i++)
                if (other[i].Name != this[i].Name || other[i].Hash != this[i].Hash)
                    return false;

            return true;
        }


        public override int GetHashCode()
        {
            int result = 0;

            for (int i = 0, c = Count; i < c; i++)
                result = 31 * result + this[i].Hash;

            return result;
        }
    }
}
