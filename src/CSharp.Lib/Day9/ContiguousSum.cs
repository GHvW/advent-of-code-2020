using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CSharp.Lib.Day9 {

    public static class ContiguousSum {

        public static ImmutableQueue<long>? FindContiguousSumFor(this IEnumerable<long> @this, long n) {
            return Loop(@this, n, ImmutableQueue<long>.Empty);
        }

        private static ImmutableQueue<long>? Loop(IEnumerable<long> ns, long n, ImmutableQueue<long> list) {
            var total = list.Sum();

            if (!ns.Any()) {
                return null;
            } else if (total == n) {
                return list;
            } else if (total > n) {
                return Loop(ns, n, list.Dequeue());
            }

            return Loop(ns.Skip(1), n, list.Enqueue(ns.First()));
        }
    }


}
