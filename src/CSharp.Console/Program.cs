using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using CSharp.Lib;
using CSharp.Lib.Day2;
using CSharp.Lib.Day3;
using CSharp.Lib.Day4;
using CSharp.Lib.Day5;
using CSharp.Lib.Day9;

Console.WriteLine("Hello Advent of Code 2020!");

var path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";
var path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt";
var path3 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-3-input.txt";
var path4 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-4-input.txt";
var path5 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-5-input.txt";
var path9 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-9-input.txt";

// ************ Day 1 ***************
var day1_1 =
    File.ReadLines(path)
        .Select(Int32.Parse)
        .FindNums(new HashSet<int>(), 2020)
        ?.Product();

Console.WriteLine($"Day 1.1: {day1_1}");

var day1_2 =
    File.ReadLines(path)
        .Select(Int32.Parse)
        .FindTriple(new HashSet<int>(), 2020)
        ?.Product();

Console.WriteLine($"Day 1.2: {day1_2}");

// ************ Day 2 ****************
var day2_1 =
    File.ReadLines(path2)
        .Select(Util.ParseLine)
        .Where(data => (data != null) && Util.IsWrongValidPassword(data))
        .Count();

Console.WriteLine($"Day 2.1: {day2_1}");

var day2_2 =
    File.ReadLines(path2)
        .Select(Util.ParseLine)
        .Where(data => (data != null) && Util.IsValidPassword(data))
        .Count();

Console.WriteLine($"Day 2.2: {day2_2}");

// ***************** Day 3 **************************
var day3_1 =
    File.ReadLines(path3)
        .Select(line => line.ToCharArray())
        .ToArray()
        .TreesEncountered((3, 1));

Console.WriteLine($"Day 3.1: {day3_1}");

var day3_2 =
    File.ReadLines(path3)
        .Select(line => line.ToCharArray())
        .ToArray()
        .TreeCounts(new() { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) })
        .Aggregate(1UL, (total, next) => total * (UInt64) next);

Console.WriteLine($"Day 3.2: {day3_2}");


// ********************* Day 4 ********************************
var day4_1 =
    File.ReadLines(path4)
        .Aggregate(new List<List<string>>() { new() }, (result, line) => {
            if (line == "") {
                result.Add(new());
                return result;
            } else {
                result[result.Count - 1].Add(line);
                return result;
            }
        })
        .Select(list => {
            return list
                .SelectMany(line => {
                    return line
                        .Trim()
                        .Split(" ")
                        .Select(item => {
                            var arr = item.Split(":");
                            return (arr[0], arr[1]);
                        });
                })
                .ToList();
        })
        .Where(Validators.ValidPassport(item => true))
        .Count();

Console.WriteLine($"Day 4.1: {day4_1}");

var day4_2 =
    File.ReadLines(path4)
        .Aggregate(new List<List<string>>() { new() }, (result, line) => {
            if (line == "") {
                result.Add(new());
                return result;
            } else {
                result[result.Count - 1].Add(line);
                return result;
            }
        })
        .Select(list => {
            return list
                .SelectMany(line => {
                    return line
                        .Trim()
                        .Split(" ")
                        .Select(item => {
                            var arr = item.Split(":");
                            return (arr[0], arr[1]);
                        });
                })
                .ToList();
        })
        .Where(Validators.ValidPassport(
            Validators.PassportItem(
                eyr: Validators.ExpirationYear(),
                byr: Validators.BirthYear(),
                iyr: Validators.IssueYear(),
                PID: Validators.PID,
                hgt: Validators.SplitHeight().Compose(Validators.Height),
                hcl: Validators.HairColor,
                ecl: Validators.EyeColor)
        ))
        .Count();

Console.WriteLine($"Day 4.2: {day4_2}");


// ************ Day 5 ******************
var day5_1 =
    File.ReadLines(path5)
        .Select(Seat.CalculateSeatId)
        .Max();

Console.WriteLine($"Day 5.1: {day5_1}");

// ugly, just dont look, skip to day 6+. hopefully it will be better. I didn't feel like refactoring
var (list, ids) =
    File.ReadLines(path5)
        .Aggregate((new HashSet<double>[128], new HashSet<double>()), (result, next) => { // 128 total rows = row # 0 - 127
            var (list, ids) = result;
            var row = Convert.ToInt32(Seat.Row(next[..7]));
            var column = Convert.ToInt32(Seat.Column(next[7..]));

            if (list[row] == null) {
                list[row] = new() { column };
                ids.Add(row * 8 + column);
                return (list, ids);
            }

            list[row].Add(column);
            ids.Add(row * 8 + column);

            return (list, ids);
        });

Func<HashSet<double>[], HashSet<double>, double> findSeat = (seatlist, seatids) =>
    seatlist
        .Zip(Enumerable.Range(0, 127))
        .Where(x => x.First.Count != 8)
        .Select(x => {
            var (row, rowNumber) = x;
            return ValidSeat(row, seatids, rowNumber);

            static double ValidSeat(HashSet<double> row, HashSet<double> seatids_, int rowNumber) {
                var seatColumn =
                    new HashSet<double>() { 0, 1, 2, 3, 4, 5, 6, 7 }
                        .Except(row)
                        .First();

                var seatId = rowNumber * 8 + seatColumn;
                if (seatids_.Contains(seatId + 1) && seatids_.Contains(seatId - 1)) {
                    return seatId;
                }

                return 0;
            };
        })
        .Where(x => x != 0)
        .First();

var day5_2 = findSeat(list, ids);

Console.WriteLine($"Day 5.2: {day5_2}");

// ******************* Day 9 *****************
var day9_1 =
    File.ReadAllLines(path9)
        .Select(long.Parse)
        .FindNoAdd(25, ImmutableQueue<long>.Empty);

Console.WriteLine($"Day 9.1: {day9_1}");

var sequence =
    File.ReadAllLines(path9)
        .Select(long.Parse);

var num =
    sequence
        .FindNoAdd(25, ImmutableQueue<long>.Empty);

var day9_2 =
        !num.HasValue
            ? null
            : sequence
                .FindContiguousSumFor(num.Value)
                ?.Aggregate((0L, 0L), (result, n) => {
                    var (min, max) = result;
                    if (min == 0 && max == 0) {
                        return (n, n);
                    } else if (n < min) {
                        return (n, max);
                    } else if (n > max) {
                        return (min, n);
                    }

                    return result;
                })
                .Sum();


Console.WriteLine($"Day 9.2: {day9_2}");

Console.WriteLine("End");
