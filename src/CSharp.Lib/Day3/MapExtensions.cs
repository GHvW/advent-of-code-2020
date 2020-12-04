using System.Collections.Generic;
using System.Linq;

namespace CSharp.Lib.Day3 {

    public static class MapExtensions {

        private class LocationFinder {

            private readonly int width;
            private readonly (int x, int y) slope;

            public LocationFinder(int width, (int, int) slope) {
                this.width = width;
                this.slope = slope;
            }

            public (int x, int y) NextLocation((int currentX, int currentY) location) {
                var wrap = (location.currentX + slope.x) - width;

                var newXPosition =
                    wrap switch {
                        < 0 => location.currentX + slope.x,
                        _ => wrap
                    };

                return (newXPosition, location.currentY + slope.y);
            }
        }

        public static int TreesEncountered(this char[][] @this, (int, int) slope) {

            var locationFinder = new LocationFinder(@this[0].Length, slope);

            (int currentX, int currentY) location = (0, 0);
            var count = 0;

            while (location.currentY <= (@this.Length - 1)) {

                var newCount =
                    @this[location.currentY][location.currentX] switch {
                        '#' => count + 1,
                        _ => count
                    };

                count = newCount;
                location = locationFinder.NextLocation(location);
            }

            return count;
        }

        public static IEnumerable<int> TreeCounts(this char[][] @this, List<(int, int)> slopes) =>
            slopes.Select(slope => @this.TreesEncountered(slope));
    }
}
