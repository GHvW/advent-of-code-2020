
using System;

namespace CSharp.Lib.Day5 {

    public record SeatRange(double Min, double Max);

    public static class SeatRangeExtensions {

        public static SeatRange NextPosition(this SeatRange @this, char id) {
            var difference = @this.Max - @this.Min;
            if (id == 'B' || id == 'R') {
                return difference == 1
                    ? new SeatRange(@this.Max, @this.Max)
                    : new SeatRange(@this.Min + Math.Round(difference / 2), @this.Max);
            }

            //'F' | 'L' => (@this.Min, (@this.Max - @this.Min) / 2)
            return new SeatRange(@this.Min, @this.Min + Math.Truncate(difference / 2));
        }
    }
}
