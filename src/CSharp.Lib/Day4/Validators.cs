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

        // part 1
        public static Func<List<(string, string)>, bool> ValidBasePassport(Func<(string, string), bool> validator) => 
            (data) =>
                data.Count switch {
                    8 => data.All(validator),
                    7 => data.All(item => item.Item1 != "cid" && validator(item)),
                    _ => false
                };

        // part 2
        private static HashSet<string> eyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }; 

        public static bool Numeric(string item) {
            var validator = new ItemValidator<char>(('0', '9'));

            return item.All(validator.Validate);
        }

        
        public static bool EyeColor(string item) => 
            eyeColors.Contains(item);


        //public static bool BirthYear(int year) => new ItemValidator<int>((1920, 2002)).Validate(year);
    }
}
