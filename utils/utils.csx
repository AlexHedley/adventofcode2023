using System.Collections.Generic;
using System.Runtime.Serialization;

public static class Utils
{
    public static string[] GetLines(string fileName)
    {
        return System.IO.File.ReadAllLines(fileName);
    }

    // Generate Matrix
    public static T[,] GenerateMatrix<T>(string[] lines, int rows, int cols)
    {
        T[,] matrix = new T[rows, cols];
        int rowLength = matrix.GetLength(0); // = rows;
        int colLength = matrix.GetLength(1); // = cols;

        var a = 0;
        for (var i = 0; i < rowLength; i++)
        {
            for (var j = 0; j < colLength; j++)
            {
                foreach (char c in lines[a].ToCharArray())
                {
                    if (typeof(T) == typeof(int))
                    {
                        // Console.WriteLine(c);
                        matrix[i, j] = (T)(object)int.Parse(c.ToString());
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        matrix[i, j] = (T)(object)c.ToString();
                    }

                    j++;
                }
            }
            a++;
        }

        return matrix;
    }

    public static T[,] GenerateMatrix<T>(char c, int rows, int cols)
    {
        T[,] matrix = new T[rows, cols];
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (var i = 0; i < rowLength; i++)
        {
            for (var j = 0; j < colLength; j++)
            {
                if (typeof(T) == typeof(int))
                {
                    // Console.WriteLine(c);
                    matrix[i, j] = (T)(object)0;
                }
                else if (typeof(T) == typeof(string))
                {
                    matrix[i, j] = (T)(object)c.ToString();
                }
            }
        }

        return matrix;
    }

    // Print Matrix
    public static void PrintMatrix<T>(T[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write($"{matrix[i, j]}");
            }
            Console.Write(Environment.NewLine);
        }
        Console.WriteLine();
    }

    // Loop Matrix
    public static void LoopMatrix<T>(T[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(matrix[i, j]);
            }
        }
    }

    public static void UpdatePosition(string[,] matrix, int rowIndex, int colIndex, string symbol)
    {
        matrix[rowIndex, colIndex] = symbol;
    }

    public static T[] GetColumn<T>(T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }

    // /// <summary>
    // /// Get Adjacents
    // ///   T R B L | TR TL BR BL
    // /// </summary>
    // /// <param name="matrix">the matrix containing your data.</param>
    // /// <param name="i">The row of the current cell you wish to check</param>
    // /// <param name="j">The col of the current cell you wish to check</param>
    // /// <example>
    // /// <code>
    // /// var adjacents = GetAdjacents(matrix, 1, 2);
    // /// </code>
    // /// </example>
    // /// <returns>A list of adjacent values</returns>
    // public static List<string> GetAdjacents<T>(T[,] matrix, int i, int j)
    // {
    //     int rowLength = matrix.GetLength(0);
    //     int colLength = matrix.GetLength(1);

    //     var adjecents = new List<string>();
    //     var top = "";
    //     var left = "";
    //     var bottom = "";
    //     var right = "";

    //     // U D L R
    //     if (i-1 > -1)
    //     {
    //         top = matrix[i-1, j];
    //         adjecents.Add(top);
    //     }
    //     if (j-1 > -1)
    //     {
    //         left = matrix[i, j-1];
    //         adjecents.Add(left);
    //     }
    //     if (i+1 < rowLength)
    //     {
    //         bottom = matrix[i+1, j];
    //         adjecents.Add(bottom); 
    //     }
    //     if (j+1 < colLength)
    //     {
    //         right = matrix[i, j+1];
    //         adjecents.Add(right);
    //     }

    //     Log($"T:{top} | L:{left} | B:{bottom} | R:{right}", true, true);

    //     var topLeft = "";
    //     var topRight = "";
    //     var bottomLeft = "";
    //     var bottomRight = "";

    //     // Diagonals
    //     if (i-1 > -1 && j-1 > -1)
    //     {
    //         topLeft = matrix[i-1, j-1];
    //         adjecents.Add(topLeft);
    //     }
    //     if (i+1 < rowLength && j-1 > -1)
    //     {
    //         bottomLeft = matrix[i+1, j-1];
    //         adjecents.Add(bottomLeft);
    //     }
    //     if (i-1 > -1 && j+1 < colLength)
    //     {
    //         topRight = matrix[i-1, j+1];
    //         adjecents.Add(topRight); 
    //     }
    //     if (i+1 < rowLength && j+1 < colLength)
    //     {
    //         bottomRight = matrix[i+1, j+1];
    //         adjecents.Add(bottomRight);
    //     }

    //     Log($"TR:{topRight} | TL:{topLeft} | BR:{bottomRight} | BL:{bottomLeft}", true, true);

    //     return adjecents;
    // }

    // https://stackoverflow.com/a/3261006
    public static Tuple<int, int> CoordinatesOf<T>(T[,] matrix, T value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }

    // https://codereview.stackexchange.com/a/44549
    public static int toNumber(String name)
    {
        int number = 0;
        for (int i = 0; i < name.Length; i++)
        {
            number = number * 26 + (name[i] - ('A' - 1));
        }
        return number;
    }

    public static String toName(int number)
    {
        StringBuilder sb = new StringBuilder();
        while (number-- > 0)
        {
            sb.Append((char)('A' + (number % 26)));
            number /= 26;
        }
        return new string(sb.ToString().Reverse().ToArray());
    }

    // 2021 - Day 5
    public static List<int> BoundsRange(int lower, int upper)
    {
        if (lower > upper)
            return Enumerable.Range(upper, lower-upper+1).ToList();
        return Enumerable.Range(lower, upper-lower+1).ToList();
    }

    public static List<long> CreateList(long lower, long numberOfItems)
    {
        var items = new List<long>();

        for (var i = 0; i < numberOfItems; i++)
        {
            items.Add(lower);
            lower++;
        }

        // items.ForEach(Console.WriteLine);
        
        return items;
    }

    public static List<int> BoundsRange(Bounds x)
    {
        return Enumerable.Range(x.Lower, x.Upper - x.Lower + 1).ToList();
    }

    public static void PrintRange(List<int> range)
    {
        string printOut = "";
        for (int i = range.Min(); i <= range.Max(); i++)
        {
            if (range.Contains(i))
            {
                printOut += i + " ";
            }
            else
            {
                printOut += ".";
            }
        }
        printOut += "  ";
        
        printOut += $"{range[0]}-{range[^1]}";
        
        Console.WriteLine(printOut);
    }

    public static void PrintDictionary<T, R>(Dictionary<T, R> dictionary, bool toConsole = false, bool toFile = false)
    {
        var items = dictionary.Select(i => $"{i.Key}: {i.Value}").ToList();
        if (toConsole) items.ForEach(Console.WriteLine);
        // if (toFile) File.AppendAllLines(@"debug.log", new[] { items });
    }

    // // Least Common Multiple
    // // https://stackoverflow.com/a/20824923
    static int gcf(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static int lcm(int a, int b)
    {
        return (a / gcf(a, b)) * b;
    }

    // https://www.w3resource.com/csharp-exercises/math/csharp-math-exercise-20.php
    // int[] nums1 = { 4, 6, 8 };                       // test(nums1) // 24
    // int[] nums2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // test(nums2) // 2520
    // int[] nums3 = { 48, 72, 108 };                   // test(nums3) // 432
    static int gcd(int n1, int n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return gcd(n2, n1 % n2);
        }
    }

    public static int test(int[] numbers)
    {
        return numbers.Aggregate((S, val) => S * val / gcd(S, val));
    }

    #region Logging

    public static void Log(string message, bool toConsole = false, bool toFile = false)
    {
        if (toConsole) Console.WriteLine(message);
        if (toFile) File.AppendAllLines(@"debug.log", new[] { message });
    }

    public static void Answer(string answer, bool toConsole = false, bool toFile = false)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        if (toConsole) Console.WriteLine(answer);
        Console.ResetColor();

        if (toFile) File.AppendAllLines(@"debug.log", new[] { $"ANSWER: {answer}" });
    }

    public static void Info(string message, bool toConsole = false, bool toFile = false)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        if (toConsole) Console.WriteLine(message);
        Console.ResetColor();

        if (toFile) File.AppendAllLines(@"debug.log", new[] { $"INFO: {message}" });
    }

    public static void Error(string message, bool toConsole = false, bool toFile = false)
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        if (toConsole) Console.WriteLine(message);
        Console.ResetColor();

        if (toFile) File.AppendAllLines(@"debug.log", new[] { message });
    }

    #endregion Logging
}

public struct Bounds
{
    public Bounds(int lower, int upper)
    {
        Lower = lower;
        Upper = upper;
    }

    public int Lower { get; }
    public int Upper { get; }

    public override string ToString() => $"{Lower}..{Upper}";
}

public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
{
    if (string.IsNullOrWhiteSpace(value))
        return default;

    return Enum.TryParse(value, true, out TEnum result) ? result : default;
}

public static T ToEnumFromValue<T>(this string str)
{
    var enumType = typeof(T);
    foreach (var name in Enum.GetNames(enumType))
    {
        var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
        if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
    }
    //throw exception or whatever handling you want or
    return default(T);
}

// https://blog.nimblepros.com/blogs/repeat-string-in-csharp/
public static string RepeatLinq(this string text, int n)
{
    return string.Concat(System.Linq.Enumerable.Repeat(text, n));
}

// // https://stackoverflow.com/a/2601501
// public static TValue GetValueOrDefault<TKey, TValue>(
//     this IDictionary<TKey, TValue> dictionary,
//     TKey key,
//     TValue defaultValue)
// {
//     return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
// }

// public static TValue GetValueOrDefault<TKey, TValue>(
//     this IDictionary<TKey, TValue> dictionary,
//     TKey key,
//     Func<TValue> defaultValueProvider)
// {
//     return dictionary.TryGetValue(key, out var value) ? value : defaultValueProvider();
// }

// https://stackoverflow.com/a/14591148
public static string RemoveWhitespace(this string input)
{
    return new string(input.ToCharArray()
        .Where(c => !Char.IsWhiteSpace(c))
        .ToArray());
}

// https://stackoverflow.com/a/2094316
// var lst = new List<int>() { 8, 3, 2, 4 };
// lst = lst.Swap(1, 2);
public static void Swap<T>(this IList<T> list, int indexA, int indexB)
{
    Utils.Log($"Swap", true, true);
    T tmp = list[indexA];
    list[indexA] = list[indexB];
    list[indexB] = tmp;
}

// public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
// {
//     T tmp = list[indexA];
//     list[indexA] = list[indexB];
//     list[indexB] = tmp;
//     return list;
// }