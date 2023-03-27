using StoreApp.View.Enums;
using System.Text.RegularExpressions;

namespace StoreApp.View.Helpers
{
    public static class CheckPassword
    {
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            var hasNumber = new Regex(@"[0-9]+");
            var hasLetter = new Regex(@"\p{L}");

            if (hasNumber.IsMatch(password))
            {
                score++;
            }
            if (hasLetter.IsMatch(password))
            {
                score += 2;
            }

            return (PasswordScore)score;
        }
    }
}
