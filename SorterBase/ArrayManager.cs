using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sorter.Base
{
    public class ArrayManager
    {
        /// <summary>
        /// Returns a string array that contains the words from file.   
        /// </summary>
        /// <param name="path"> The file path </param>
        /// <returns> String array of words from file </returns>
        /// <exception cref="FileNotFoundException"> </exception>
        static public string[][] GetStringArrayFromFile(string path)
        {            
            string[] lines = null;
            string[][] array = new string[0][];
            try
            {
                StreamReader streamReader = new StreamReader(path);
                lines = streamReader.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                streamReader.Close();
                streamReader.Dispose();
                array = new string[lines.Length][];
                for (int i = 0; i < lines.Length; i++)
                {
                    array[i] = lines[i].Split(new char[] { ' ', ',', ':', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch (Exception e)
            {
                switch (e.GetType().Name)
                {
                    case("FileNotFoundException"):
                        throw new FileNotFoundException("File " + path + " is not found.\a");
                }
                Console.ReadKey();
            }
            return array;
        }

        /// <summary>
        /// Returns a int array that contains only integers from file.   
        /// </summary>
        /// <param name="path"> The file path </param>
        /// <returns> Array of integers from file </returns>
        /// <exception cref="FileNotFoundException"> </exception>
        static public int[][] GetIntArrayFromFile(string path)
        {
            int rows = 0;
            string[][] stringArray;
            stringArray = GetStringArrayFromFile(path); 
            int[][] array = new int[stringArray.Length][];
            for (int i = 0; i < stringArray.Length; i++)
            {
                array[rows] = new int[stringArray[i].Length];
                int k = 0;
                for (int j = 0; j < stringArray[i].Length; j++)
                {
                    if (int.TryParse(stringArray[i][j], out array[rows][k])) k++;                                    
                }
                array[rows] = array[rows].Take(k).ToArray();
                if (k > 0) rows++;
            }
            array = array.Take(rows).ToArray();
            return array;
        }

        /// <summary>
        /// Returns a int array that contains all numbers from file.   
        /// </summary>
        /// <param name="path"> The file path </param>
        /// <returns> Array of numbers from file </returns>
        /// <exception cref="FileNotFoundException"> </exception>
        static public float[][] GetFloatArrayFromFile(string path)
        {
            int rows = 0;
            string[][] stringArray = GetStringArrayFromFile(path);
            float[][] array = new float[stringArray.Length][];
            for (int i = 0; i < stringArray.Length; i++)
            {
                array[rows] = new float[stringArray[i].Length];
                int k = 0;
                for (int j = 0; j < stringArray[i].Length; j++)
                {
                    if (float.TryParse(stringArray[i][j], out array[rows][k])) k++;
                }
                array[rows] = array[rows].Take(k).ToArray();
                if (k > 0) rows++;
            }
            array = array.Take(rows).ToArray();
            return array;
        }

    }
}
