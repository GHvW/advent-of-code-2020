using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp.Lib;

Console.WriteLine("Hello Advent of Code 2020!");

var path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt";

// ************ Day 1 ***************
//var result =
//    File.ReadLines(path)
//        .Select(Int32.Parse)
//        .FindNums(new HashSet<int>(), 2020)
//        .Product();

var result =
    File.ReadLines(path)
        .Select(Int32.Parse)
        .FindTriple(new HashSet<int>(), 2020)
        .Product();

Console.WriteLine($"result is {result}");
