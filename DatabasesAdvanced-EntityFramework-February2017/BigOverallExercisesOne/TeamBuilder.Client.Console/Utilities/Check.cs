namespace TeamBuilder.Client.Console.Utilities
{
    using System;

    public class Check
    {
        public static void CheckLength(int excpectedLength, string[] array)
        {
            if (excpectedLength != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}
