namespace AngryWasp.Random
{
    public static class RandomString
    {
        private static XoShiRo256PlusPlus rng = new XoShiRo256PlusPlus();

        public static string AlphaNumeric(int length)
        {
            char[] chars = ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ").ToCharArray();

            string str = string.Empty;

            for (int i = 0; i < length; i++)
                str += chars[rng.Next(0, chars.Length)];

            return str;
        }

        public static string Hex(int length, bool upperCase = false)
        {
            char[] chars = $"0123456789{(upperCase ? "ABCDEF" : "abcdef")}".ToCharArray();

            string str = string.Empty;

            for (int i = 0; i < length; i++)
                str += chars[rng.Next(0, chars.Length)];

            return str;
        }
    }
}