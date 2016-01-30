using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.HelperClasses
{
    public class Income
    {
        public int Soul;

        public int People;
        public int Money;
        public int Not;
        public int Inf;

        public static Income operator +(Income i1, Income i2)
        {
            var finalIncome = new Income
            {
                Soul = i1.Soul + i2.Soul,

                People = i1.People + i2.People,
                Money = i1.Money + i2.Money,
                Not = i1.Not + i2.Not,
                Inf = i1.Inf + i2.Inf
            };

            return finalIncome;
        }

        public static Income operator -(Income i1, Income i2)
        {
            var finalIncome = new Income
            {
                Soul = i1.Soul - i2.Soul,

                People = i1.People - i2.People,
                Money = i1.Money - i2.Money,
                Not = i1.Not - i2.Not,
                Inf = i1.Inf - i2.Inf
            };

            return finalIncome;
        }
    }


}
