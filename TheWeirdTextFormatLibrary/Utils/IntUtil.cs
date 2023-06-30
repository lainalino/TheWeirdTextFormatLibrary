namespace TheWeirdTextFormatLibrary.Utils
{
    public static class IntUtil
    {
        /// <summary>
        ///Convert Decimal to Binary (Base 10 to base 2)
        /// </summary>
        /// <example>[1, 1, 1] [2, 2, 2]
        /// <returns> [1, 2] [1, 2] [1, 2] </returns>
        public static async Task<string> ToConvertDecimalToBinary(this int decimalNumber)
        {
            int remainder;
            string result = string.Empty;

            while (decimalNumber > 1)
            {
                /// to retrieve the decimalNumber's remainder
                /// if the decimalNumberl number is even, then the result will be whole and will give remainder “0”
                /// If the given decimal number is odd, then the result will not be divided properly and will give the remainder “1”.
                remainder = decimalNumber % 2;

                /// add the remainder to result. 
                /// This way, the least significant bit  at the top and the most significant bit at the bottom
                result = remainder.ToString() + result;

                /// The integer quotient obtained is used as a dividend for the next step, until the quotient becomes 0
                decimalNumber /= 2;
            }

            var binaryText = Convert.ToString(decimalNumber) + result;

            /// Case the length of binary is less than 32, pad "0" to the left
            binaryText = binaryText.PadLeft(32, '0');

            return await Task.FromResult(binaryText);
        }
    }
}