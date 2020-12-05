using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib.Day4 {

    public record HeightMeasure(string height, string measure);

    public static class HeightMeasureExtensions {

        public static bool ValidHeight(this HeightMeasure @this) =>
            @this switch {
                (var height, "cm") => true,
                (var height, "in") => false,
                _ => false
            };
    }
}
