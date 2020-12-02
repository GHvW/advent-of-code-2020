using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib {

    public static class TupleExtensions {

        public static int? Product(this (int x, int y)? @this) {
            if (@this.HasValue) {
                var (x, y) = @this.Value;
                return x * y;
            }

            return null;
        }

        public static int? Product(this (int x, int y, int z)? @this) {
            if (@this.HasValue) {
                var (x, y, z) = @this.Value;
                return x * y * z;
            }

            return null;
        }
    }
}
