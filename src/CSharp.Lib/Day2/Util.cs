using System;
using System.Linq;

namespace CSharp.Lib.Day2 {

    public static class Util {

        public static PasswordData? ParseLine(string data) {
            try {
                var result = data.Split(" ");
                var bounds = result[0].Split("-");
                var character = result[1].ToCharArray();
                var password = result[2];

                return new PasswordData(
                    Int32.Parse(bounds[0]),
                    Int32.Parse(bounds[1]),
                    character[0],
                    password);

            } catch (Exception) {
                return null;
            }
        }

        public static bool IsWrongValidPassword(PasswordData data) {
            var count =
                data.Password
                    .Where(c => c == data.Character)
                    .Count();

            return (count >= data.First) && (count <= data.Second);
        }


        public static bool IsValidPassword(PasswordData data) {
            try {
                var f = data.First - 1;
                var s = data.Second - 1;
                var firstEq = data.Password[f] == data.Character;
                var secondEq = data.Password[s] == data.Character;

                return (firstEq || secondEq) && !(firstEq && secondEq);
            } catch (Exception) {
                return false;
            }
        }
    }


}
