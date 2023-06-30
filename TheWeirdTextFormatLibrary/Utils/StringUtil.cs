using TheWeirdTextFormatLibrary.Global;

namespace TheWeirdTextFormatLibrary.Utils
{
    public static class StringUtil
    {
        /// <summary>
        ///Convert the text to Binary
        /// </summary>
        /// <example> ocat
        /// <returns> 01101111011000110110000101110100 </returns>
        public static async Task<string> ToBinary32(this string data, int sCharacterSize, int sBinarySize)
        {
            int maxSize = sCharacterSize * sBinarySize;
            char[] buffer = new char[maxSize];
            int index = 0;

            // if the text length is smaller than 4, complement the array with 0
            if (data.Length < sCharacterSize)
            {
                // to know how many "0" is necessary to pad the array. 
                index = (sCharacterSize - data.Length) * sBinarySize;
                buffer = await ToCompleteWith0(buffer, index);
            }

            for (int i = 0; i < data.Length; i++)
            {
                //storage for the single byte. Padding with 0 on the left, case the size is smaller the 8
                string binary = Convert.ToString(data[i], 2).PadLeft(sBinarySize, '0');
                for (int j = 0; j < sBinarySize; j++)
                {
                    //adding the value to the array, from the size already filled in previously
                    buffer[index] = binary[j];
                    index++;
                }
            }

            return await Task.FromResult(new string(buffer));
        }

        /// <summary>
        /// Convert the text to Two Dimensional Array
        /// </summary>
        /// <example> data: 01101111 01100011 011000010 1110100, row: 4 column: 8
        /// <returns>   [0 1 1 0 1 1 1 1]
        ///             [0 1 1 0 0 0 1 1]
        ///             [0 1 1 0 0 0 0 1]
        ///             [0 1 1 1 0 1 0 0]
        /// </returns>
        public static async Task<char[,]> ToTwoDimensionalArray(this string data, int row, int column)
        {
            // Convert the string to one-dimensional array
            var dataArray = data.ToArray();
            char[,] data2DArray = new char[row, column];

            // starting from 0 to the length of the data
            int count = 0;

            //loop through the input array
            for (int i = 0; i < row; i++)
            {
                //loop through each row for each column
                for (int j = 0; j < column; j++)
                {
                    data2DArray[i, j] = dataArray[count++];
                }
            }

            return await Task.FromResult(data2DArray);
        }

        /// <summary>
        /// Convert the binary to decimal (Base 2 to base 10)
        /// </summary>
        /// <example> 00001111111100011000100111001110
        /// <returns> 267487694 </returns>
        public static async Task<string> BinaryToDecimal(this string binary)
        {
            uint resultDecimal = 0u;
            for (int i = 0; i < binary.Length; i++)
            {
                // make room for the next bit by shifting what is there already
                resultDecimal <<= 1;
                char characterBinary = binary[i];

                // case the characterBinary is not 0, gives 1 
                if (characterBinary == '1')
                {
                    resultDecimal |= 1u;
                }
            }
            
            return await Task.FromResult(resultDecimal.ToString());
        }

        /// <summary>
        /// Convert the binary to text (Base 2 to base 10)
        /// </summary>
        /// <example> 00000000011101000110000101100011
        /// <returns> tac </returns>
        public static async Task<string> BinaryToText(this string binary, int sCharacterSize, int sBinarySize)
        {
            string builder = string.Empty;
            int maxSize = sCharacterSize * sBinarySize;
            // if the length of the binary isn't multiple of 8, padding with "0" to the left
            if (binary.Length % 8 != 0)
            {
                binary = binary.PadLeft(maxSize, '0');
            }

            for (int i = 0; i < binary.Length; i += sBinarySize)
            {
                //get the binary
                string section = binary.Substring(i, sBinarySize);
                int ascii = 0;

                // converting the binary to int
                ascii = Convert.ToInt32(section, 2);

                // get the ascii value
                builder += (char)ascii;
            }

            // removing the "\0"(it means null in ASCII)
            return await Task.FromResult(builder.ToString().Replace("\0", ""));

        }

        /// <summary>
        /// Complete the array with 0
        /// </summary>
        /// <example> 
        /// <returns>  </returns>
        private static async Task<char[]> ToCompleteWith0(char[] buffer, int size)
        {
            int index = 0;

            //add '0' to buffer array.
            for (int j = 0; j < size; j++)
            {
                buffer[index] = '0';
                index++;
            }

            return await Task.FromResult(buffer);
        }
    }
}
