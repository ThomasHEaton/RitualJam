using System;

namespace Assets.HelperClasses
{
    public class Tuple
    {
        public int t1;
        public int t2;

        protected bool Equals(Tuple other)
        {
            return t1 == other.t1 && t2 == other.t2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (t1*397) ^ t2;
            }
        }

        public override string ToString()
        {
            return t1 + ", " + t2 + "\n";
        }

        public override bool Equals(object obj)
        {
            Tuple t;
            if (obj is Tuple)
            {
                t = obj as Tuple;
                if (t.t1 == this.t1 && t.t2 == this.t2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
