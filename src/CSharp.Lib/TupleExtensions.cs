using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib {

    public static class TupleExtensions {

        // ******************* Day 1 **************************
        public static int Product(this (int x, int y) @this) => 
            @this.x * @this.y;

        
        public static int Product(this (int x, int y, int z) @this) =>
            @this.x * @this.y * @this.z;


        // day 9
        public static long Sum(this (long x, long y) @this) => 
            @this.x + @this.y;
    }
}
