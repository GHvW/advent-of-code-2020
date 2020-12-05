using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp.Lib;
using CSharp.Lib.Day2;
using CSharp.Lib.Day3;
using CSharp.Lib.Day4;

Console.WriteLine("Hello Advent of Code 2020!");

var path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";
var path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt";
var path3 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-3-input.txt";
var path4 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-4-input.txt";

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
        .Where(Validators.ValidBasePassport(item => true))
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
        .Where(Validators.ValidBasePassport(item => true))
        .Count();

Console.WriteLine($"Day 4.2: {day4_2}");
