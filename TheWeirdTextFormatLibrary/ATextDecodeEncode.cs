using TheWeirdTextFormatLibrary.Utils;

namespace TheWeirdTextFormatLibrary
{
    public class ATextDecodeEncode
    {
        /// <summary> 
        /// The code creates, through the text sent by the user, an array[x,y] and then transforms it into array[y, x]. 
        ///  By getting a string from this new array configuration, it will be possible to know the encoded text.
        /// <example>For example: FRED
        /// results in <c>arrayEncoded</c> having the value [251792692].
        /// </example>
        /// </summary>
        public static async Task<string> Encode(string textToEncode, int sCharacterSize = Global.Global.sCharacterSize, int sBinarySize= Global.Global.sBinarySize)
        {
            /// if the text size is not a multiple of Global.sCharacterSize, the array will have a text with "0" padding to form a Global.sCharacterSize-character text.
            int sizeArray = textToEncode.Length % sCharacterSize == 0 ? textToEncode.Length / sCharacterSize : textToEncode.Length / sCharacterSize + 1;
            string[] arrayEncoded = new string[sizeArray];

            /// this variable will start the substring textToEncode, getting the text with Global.sCharacterSize characters
            int position = 0;
            string returnBinary, textEncoded, textEncodedDecimal;
            char[,] textArray2D = new char[sCharacterSize, sBinarySize];
            char[,] invertArray2D = new char[sBinarySize, sCharacterSize];

            // starting encode, converting the string to an array[Global.sCharacterSize,Global.sBinarySize],
            // then encode. And at the end convert the binary to decimal
            for (int i = 0; i < sizeArray; i++)
            {
                // get the substring and reverse it 
                // example: abcdefg
                // when the Global.sCharacterSize is 4 and position = 0
                // result: dcba
                // when position = Global.sCharacterSize
                // result: gfe
                if (position + sCharacterSize >= textToEncode.Length)
                {
                    returnBinary = new string(textToEncode.Substring(position)?.Reverse().ToArray());
                }
                else
                {
                    returnBinary = new string(textToEncode.Substring(position, sCharacterSize)?.Reverse().ToArray());
                }

                // Convert the text to binary
                returnBinary = await returnBinary.ToBinary32(sCharacterSize, sBinarySize);

                // Convert the text to array[Global.sCharacterSize, Global.sBinarySize]
                textArray2D = await returnBinary.ToTwoDimensionalArray(sCharacterSize, sBinarySize);

                // Convert a char[row,column] to char[column, row]   
                invertArray2D = await textArray2D.ToColumToRow();

                //Convert the array to string
                textEncoded = await invertArray2D.ToConvertToString();

                // Convert the string to decimal
                textEncodedDecimal = await textEncoded.BinaryToDecimal();

                // Add the decimal to the array
                arrayEncoded[i] = textEncodedDecimal;

                // Increment position by value of Global.sCharacterSize 
                position += sCharacterSize;
            }

            return await Task.FromResult($"{String.Join(",", arrayEncoded.Select(p => p.ToString()).ToArray())}");
        }

        /// <summary> 
        /// The code decodes a text, transforming the received value into binary, then transforms it into an array[x,y].
        /// After converting the array[x,y] to arra[y,x]
        /// By getting a string from this new array configuration, it will be possible to know the decoded text.
        /// <example>For example: [251792692]
        /// results in <c>textDecodec</c> having the value FRED.
        /// </example>
        /// </summary>  
        public static async Task<string> Decode(string textToDecode, int sCharacterSize = Global.Global.sCharacterSize, int sBinarySize = Global.Global.sBinarySize)
        {
            string textDecode = string.Empty, binary;
            string[] arrayTextToDecode = textToDecode.Split(',');
            char[,] arrayBinary  = new char[sBinarySize, sCharacterSize];
            char[,] invertArray2D = new char[sCharacterSize, sBinarySize];

            for (int i = 0; i < arrayTextToDecode.Length; i++)
            {
                // Convert the Decimal to binary
                binary = await int.Parse(arrayTextToDecode[i]).ToConvertDecimalToBinary();

                // Convert binary to array[Global.sBinarySize, Global.sCharacterSize]
                arrayBinary = await binary.ToTwoDimensionalArray(sBinarySize, sCharacterSize);

                // Convert a char[row,column] to char[column, row]   
                invertArray2D = await arrayBinary.ToColumToRow();

                //Convert the array to string
                binary = await invertArray2D.ToConvertToString();

                //Append the string to textDecoded
                textDecode += new string((await binary.BinaryToText(sCharacterSize, sBinarySize)).Reverse().ToArray());               
            }

            return await Task.FromResult(textDecode);
        }

    }
}
