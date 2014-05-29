using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebeclick.WebClient.Utilities
{
    public static class RandomExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing(new Random());
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random random)
        {
            if (random == null)
                throw new ArgumentNullException("random");

            int index = random.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}
