using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp.Lib;

Console.WriteLine("Hello Advent of Code 2020!");

var path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";
var path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";

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
