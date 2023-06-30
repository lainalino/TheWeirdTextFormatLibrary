namespace TheWeirdTextFormatLibrary.Utils
{
    public static class CharUtil
    {
        /// <summary>
        /// Converting a char[row,column] to char[column, row]
        /// </summary>
        /// <example>[1, 1, 1] [2, 2, 2]
        /// <returns> [1, 2] [1, 2] [1, 2] </returns>
        public static async Task<char[,]> ToColumToRow(this char[,] data)
        {
            //get length of the row
            var row = data.GetLength(0);

            //get length of the column
            var column = data.GetLength(1);

            //create an array where the length of the row is the length of the column of the "data" parameter.
            //And the length of the column is the length of the row of the "data" parameter
            char[,] encodedArray = new char[column, row];

            //loop through the input array
            for (int i = 0; i < column; i++)
            {
                //loop through each row for each column
                for (int j = 0; j < row; j++)
                {
                    encodedArray[i, j] = data[j, i];
                }
            }

            return await Task.FromResult(encodedArray);
        }

        /// <summary>
        /// Converting a bidimensional array to string
        /// </summary>
        /// <example>[1, 1, 1] [2, 2, 2]
        /// <returns> 111222 </returns>
        public static async Task<string> ToConvertToString(this char[,] data)
        {
            var result = string.Empty;

            //get length of the row
            var row = data.GetLength(0);

            //get length of the column
            var column = data.GetLength(1);

            //loop through the input array
            for (var i = 0; i < row; i++)
            {
                //loop through each column for each row
                for (var j = 0; j < column; j++)
                {
                    result += data[i, j];
                }
            }

            return await Task.FromResult(result);
        }
    }
}
