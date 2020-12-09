using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib.Day9 {

    public static class NoAdd {

        public static (int?, ImmutableQueue<int>) CheckNoAdd(int considerCount, ImmutableQueue<int> consideration, int n) {
            if (consideration.Count() == considerCount) {
                int? result;
                if (consideration.Any(n_ => consideration.Contains(n - n_))) {
                    result = null;
                } else {
                    result = n;
                }

                var popped = consideration.Dequeue();
                return (result, popped.Enqueue(n));
            }
            return (null, consideration.Enqueue(n));
        }


        public static int? FindNoAdd(this IEnumerable<int> @this, int considerationCount, ImmutableQueue<int> queue) {
            if (!@this.Skip(1).Any()) {
                return CheckNoAdd(considerationCount, queue, @this.First()).Item1;
            }

            return CheckNoAdd(considerationCount, queue, @this.First()) switch {
                (null, var queue_) => @this.Skip(1).FindNoAdd(considerationCount, queue_),
                (var val, _) => val
            };
        }
    }
}
