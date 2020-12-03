using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp.Lib;
using CSharp.Lib.Day2;

Console.WriteLine("Hello Advent of Code 2020!");

var path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";
var path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt";

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
