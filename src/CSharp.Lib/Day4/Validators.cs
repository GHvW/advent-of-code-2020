using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Lib.Day4 {

    public class ItemValidator<A> where A : IComparable<A> {

        private readonly (A min, A max) range;

        public ItemValidator((A, A) range) {
            this.range = range;
        }

        public bool Validate(A item) =>
            item.CompareTo(this.range.min) >= 0 && item.CompareTo(this.range.max) <= 0;
    }


    public static class Validators {

        public static Func<List<(string, string)>, bool> ValidPassport(Func<(string, string), bool> validator) =>
            (data) =>
                data.Count switch {
                    8 => data.All(validator),
                    7 => data.All(item => item.Item1 != "cid" && validator(item)),
                    _ => false
                };


        private static HashSet<string> eyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };


        public static Func<char, bool> Numeric() =>
            new ItemValidator<char>(('0', '9')).Validate;


        public static bool EyeColor(string item) =>
            eyeColors.Contains(item);


        public static Func<int, bool> BirthYear() =>
            new ItemValidator<int>((1920, 2002)).Validate;


        public static bool PID(string pid) =>
            (pid.Length != 9)
                ? false
                : pid.All(Numeric());


        public static Func<int, bool> IssueYear() =>
            new ItemValidator<int>((2010, 2020)).Validate;


        public static Func<int, bool> ExpirationYear() =>
            new ItemValidator<int>((2020, 2030)).Validate;


        public static bool HairColor(string data) =>
            data.First() switch {
                '#' => data.Skip(1).All(c => {
                    var validaf = new ItemValidator<char>(('a', 'f'));
                    var numeric = Numeric();
                    return numeric(c) || validaf.Validate(c);
                }),
                _ => false
            };


        public static Func<string, (string, string)> SplitHeight() => (data) => {
            try {
                var indexToSplitAt =
                    data.IndexOfAny(new char[] { 'i', 'c' });

                return (data[..indexToSplitAt], data[indexToSplitAt..]);
            } catch (Exception) {
                return ("", "");
            }
        };


        public static bool Height((string, string) data) =>
            data switch {
                (var height, "in") => new ItemValidator<int>((59, 76)).Validate(Int32.Parse(height)),
                (var height, "cm") => new ItemValidator<int>((150, 193)).Validate(Int32.Parse(height)),
                _ => false
            };



        public static Func<(string, string), bool> PassportItem(
            Func<int, bool> eyr,
            Func<int, bool> byr,
            Func<int, bool> iyr,
            Func<string, bool> PID,
            Func<string, bool> hgt,
            Func<string, bool> hcl,
            Func<string, bool> ecl
            ) => ((string, string) item) =>
                item switch {
                    ("cid", _) => true,
                    ("pid", var pid) => PID(pid),
                    ("byr", var birthYear) => byr(Int32.Parse(birthYear)),
                    ("iyr", var issueYear) => iyr(Int32.Parse(issueYear)),
                    ("eyr", var expirationYear) => eyr(Int32.Parse(expirationYear)),
                    ("hgt", var height) => hgt(height),
                    ("hcl", var hairColor) => hcl(hairColor),
                    ("ecl", var eyeColor) => ecl(eyeColor),
                    _ => false
                };
    }
}
