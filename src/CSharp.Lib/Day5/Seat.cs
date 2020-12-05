using System.Linq;

namespace CSharp.Lib.Day5 {

    public static class Seat {

        public static double Row(string rowPartition) =>
            rowPartition
                .Aggregate(new SeatRange(0, 127), (result, c) => result.NextPosition(c))
                .Min;


        public static double Column(string columnPartition) =>
            columnPartition
                .Aggregate(new SeatRange(0, 7), (result, c) => result.NextPosition(c))
                .Min;


        public static double CalculateSeatId(string spacePartition) {
            var row = Row(spacePartition[..7]);

            var column = Column(spacePartition[7..]);

            return (row * 8) + column;
        }
    }
}
