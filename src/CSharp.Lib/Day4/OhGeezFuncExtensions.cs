using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib.Day4 {

    public static class OhGeezFuncExtensions {

        public static Func<A, C> Compose<A, B, C>(this Func<A, B> @this, Func<B, C> next) => (a) => next(@this(a));
    }
}
