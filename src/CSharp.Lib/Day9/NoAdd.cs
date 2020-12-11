using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib.Day9 {

    public static class NoAdd {

        public static (long?, ImmutableQueue<long>) CheckNoAdd(long considerCount, ImmutableQueue<long> consideration, long n) {
            if (consideration.Count() == considerCount) {
                long? result;
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


        public static long? FindNoAdd(this IEnumerable<long> @this, long considerationCount, ImmutableQueue<long> queue) {
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
