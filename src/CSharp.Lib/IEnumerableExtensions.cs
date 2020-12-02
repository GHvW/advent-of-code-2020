using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib {

    public static class IEnumerableExtensions {

        // ********************* Day 1 *********************************
        public static (int, int)? FindNums(this IEnumerable<int> @this, HashSet<int> set, int sum) {

            foreach (var n in @this) {
                var reqN = sum - n;

                if (set.Contains(reqN)) {
                    return (n, reqN);
                }

                set.Add(n);
            }

            return null;
        }

        public static (int, int, int)? FindTriple(this IEnumerable<int> @this, HashSet<int> set, int sum) {

            foreach (var n in @this) {
                var reqN = sum - n;

                var nextTwo = set.FindNums(set, reqN);

                if (nextTwo.HasValue) {
                    var (x, y) = nextTwo.Value;
                    return (x, y, n);
                }

                set.Add(n);
            }

            return null;
        }
    }
}
