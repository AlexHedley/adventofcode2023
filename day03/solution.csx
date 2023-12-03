#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day3
{
    bool logToConsole = false;
    bool logToFile = false;

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix);

        // Find all numbers and sum
        var numbers = FindNumbersInLines(lines);
        var total = numbers.Sum();
        Utils.Log($"Total: '{total}'", logToConsole, logToFile);

        var validNumbers = CheckAdjecents(matrix);
        // validNumbers.ForEach(Console.WriteLine);
        var sum = validNumbers.Sum();
        
        Utils.Log($"{sum} ({total}) {total-sum}", logToConsole, logToFile);
        Utils.Answer($"{sum}");
    }

    public List<long> FindNumbersInLines(string[] lines)
    {
        var numbers = new List<long>();

        foreach (var line in lines)
        {
            numbers.AddRange(FindNumbersInLine(line));
        }
        
        return numbers;
    }

    public List<long> FindNumbersInLine(string line)
    {
        var numbers = new List<long>();

        var pattern = @"\d+";
        Regex reg = new Regex(pattern);
        MatchCollection matchedCommands = reg.Matches(line);
        for (int count = 0; count < matchedCommands.Count; count++)
        {
            Utils.Log($"{matchedCommands[count].Value}", logToConsole, logToFile);
            numbers.Add(long.Parse(matchedCommands[count].Value));
        }

        return numbers;
    }

    // public bool IsSymbol(char c)
    // {
    //     if (c == '.') return false;
    //     if (c == '#') return true;
    //     if (c == '%') return true;
    //     if (c == '&') return true;
    //     if (c == '@') return true;
    //     if (c == '_') return true;
    //     if (c == '-') return true;
    //     if (c == '*') return true;
    //     if (c == '/') return true;
        
    //     if (Char.IsSymbol(c)) return true;

    //     return false;
    // }

    public bool IsSymbol(char c)
    {
        if (c == '.') return false;
        return !Char.IsLetterOrDigit(c);
    }

    public List<long> CheckAdjecents(string[,] matrix)
    {
        var validNumbers = new List<long>();

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            // Get the row and find the numbers in the that row.
            var row = Utils.GetRow(matrix, i);
            var line = string.Join("", row);
            Utils.Log($"{i}: {line}", logToConsole, logToFile);

            // CheckForDuplicates(line, i);

            var numbers = new List<long>();
            var currentNumber = 0L;
            var added = false;

            for (int j = 0; j < colLength; j++)
            {
                var currentValue = matrix[i, j];
                Utils.Log($"{currentValue} | {i}:{j}", logToConsole, logToFile);

                // Don't need to check "."
                if (currentValue == ".")
                {
                    currentNumber = 0; // Reset for Duplicates L: 124 (916)
                    added = false;
                    continue;
                }
                // Don't need to check "symbols"
                // if (!Char.IsLetterOrDigit(currentValue.ToCharArray()[0])) continue;

                var adjecents = GetAdjacents(matrix, i, j);
                
                if (!adjecents.Where(a => IsSymbol(a.ToCharArray()[0])).Any())
                {
                    Utils.Log("NO SYMBOL ADJ", logToConsole, logToFile);
                    // var currentString = line.Substring(j+1);
                    // Utils.Log($"{line} : {currentString}", logToConsole, logToFile);
                    
                    if (IsSymbol(currentValue.ToCharArray()[0]))
                    {
                        currentNumber = 0; // Reset for Duplicates L: 124 (916)
                        added = false;
                        continue;
                    }
                    
                    Utils.Log($"NSA - {currentNumber} | {added}", logToConsole, logToFile);
                    
                    var number = FindNumber(line, j);
                    if (currentNumber != number) added = false;
                    currentNumber = number;

                    Utils.Log($"NSA - {number} | {currentNumber} | {added}", logToConsole, logToFile);

                    Utils.Log($"Numbers #: {numbers.Count}", logToConsole, logToFile);
                }
                else
                {
                    Utils.Log("SYMBOL ADJ", logToConsole, logToFile);
                    var number = FindNumber(line, j);
                    if (currentNumber != number) added = false;
                    currentNumber = number;

                    Utils.Log($"SA - {number} | {currentNumber} | {added}", logToConsole, logToFile);

                    if (!added)
                    {
                        Utils.Log($"Add Number: {number}", logToConsole, logToFile);                    
                    
                        numbers.Add(number);
                        added = true;
                    }
                    
                    Utils.Log($"Numbers #: {numbers.Count}", logToConsole, logToFile);
                }
            }

            validNumbers.AddRange(numbers);
        }

        return validNumbers;
    }

    public List<string> GetAdjacents(string[,] matrix, int i, int j)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var adjecents = new List<string>();
        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        // U D L R
        if (i-1 > -1)
        {
            top = matrix[i-1, j];
            adjecents.Add(top);
        }
        if (j-1 > -1)
        {
            left = matrix[i, j-1];
            adjecents.Add(left);
        }
        if (i+1 < rowLength)
        {
            bottom = matrix[i+1, j];
            adjecents.Add(bottom); 
        }
        if (j+1 < colLength)
        {
            right = matrix[i, j+1];
            adjecents.Add(right);
        }

        Utils.Log($"T:{top} | L:{left} | B:{bottom} | R:{right}", logToConsole, logToFile);

        var topLeft = "";
        var topRight = "";
        var bottomLeft = "";
        var bottomRight = "";

        // Diagonals
        if (i-1 > -1 && j-1 > -1)
        {
            topLeft = matrix[i-1, j-1];
            adjecents.Add(topLeft);
        }
        if (i+1 < rowLength && j-1 > -1)
        {
            bottomLeft = matrix[i+1, j-1];
            adjecents.Add(bottomLeft);
        }
        if (i-1 > -1 && j+1 < colLength)
        {
            topRight = matrix[i-1, j+1];
            adjecents.Add(topRight); 
        }
        if (i+1 < rowLength && j+1 < colLength)
        {
            bottomRight = matrix[i+1, j+1];
            adjecents.Add(bottomRight);
        }

        Utils.Log($"TR:{topRight} | TL:{topLeft} | BR:{bottomRight} | BL:{bottomLeft}", logToConsole, logToFile);

        return adjecents;
    }

    // void CheckForDuplicates(string line, int lineNumber)
    // {
    //     var numbersInLine = FindNumbersInLine(line);
    //     // bool hasAnyDuplicate = numbersInLine.Count > numbersInLine.Distinct().Count;
    //     var duplicates = numbersInLine.GroupBy(x => x).Where(g => g.Count() > 1);
    //     if (duplicates.Any())
    //     {
    //         Utils.Answer($"{lineNumber} DUPLICATES");
    //         foreach (var group in duplicates)
    //         {
    //             Console.Write($"{group.Key}: [{string.Join(",", group)}]");
    //         }
    //     }
    //     else
    //     {
    //         Utils.Info($"");
    //     }
    // }

    public long FindNumber(string line, int currentPos)
    {
        // 0123456789
        // ..35..633.
        //    .

        StringBuilder sb = new StringBuilder();
        sb.Insert(0, line[currentPos]); // 5

        var length = line.Length - 1;
        
        var position = currentPos;
        // Check Left (..3)
        while (position > 0)
        {
            position--;
            var characterToCheck = line[position].ToString();
            var isNumeric = long.TryParse(characterToCheck, out _);
            if (isNumeric)
            {
                sb.Insert(0, characterToCheck);
            }
            else
            {
                break;
            }
        }

        position = currentPos;
        // Check Right (..633.)
        while (position < length)
        {
            position++;
            var characterToCheck = line[position].ToString();
            var isNumeric = long.TryParse(characterToCheck, out _);
            if (isNumeric)
            {
                sb.Append(characterToCheck);
            }
            else
            {
                break;
            }
        }

        Utils.Log($"FindNumber: '{sb.ToString()}'", logToConsole, logToFile);

        return long.Parse(sb.ToString());
    }

    public void Part2(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix);

        var totals = CheckGears(matrix);
        var sum = totals.Sum();
        
        Utils.Log($"{sum}", logToConsole, logToFile);
        Utils.Answer($"{sum}");
    }

    public List<long> CheckGears(string[,] matrix)
    {
        var gearsTotal = new List<long>();

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                var currentValue = matrix[i, j];
                Utils.Log($"{currentValue} | {i}:{j}", logToConsole, logToFile);

                if (currentValue == "*")
                {
                    var total = 1L;
                    var numbers = new HashSet<long>();
                    // var adjecents = GetAdjacents(matrix, i, j);
                    var adjecentPositions = GetAdjacentPositions(matrix, i, j);
                    
                    foreach (var adjecentPosition in adjecentPositions)
                    {
                        Console.WriteLine($"{adjecentPosition.Item1}, {adjecentPosition.Item2}");
                        
                        var row = Utils.GetRow(matrix, adjecentPosition.Item1);
                        var line = string.Join("", row);
                        Utils.Log($"{i}: {line}", logToConsole, logToFile);
                        var number = FindNumber(line, adjecentPosition.Item2);
                        numbers.Add(number);
                    }

                    Utils.Log($"# {numbers.Count()}", logToConsole, logToFile);

                    if (numbers.Count() > 1)
                    {
                        foreach(var number in numbers)
                        {
                            Utils.Log($"{number}", logToConsole, logToFile);
                            total *= number;
                        }
                        Utils.Info($"{total}");

                        gearsTotal.Add(total);
                    }
                }
            }
        }

        return gearsTotal;
    }

    public List<(int, int)> GetAdjacentPositions(string[,] matrix, int i, int j)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        var adjecents = new List<(int, int)>();
        var top = "";
        var left = "";
        var bottom = "";
        var right = "";

        // U D L R
        if (i-1 > -1)
        {
            top = matrix[i-1, j];
            var isNumeric = long.TryParse(top, out _);
            if (isNumeric)
                adjecents.Add((i-1, j));
        }
        if (j-1 > -1)
        {
            left = matrix[i, j-1];
            var isNumeric = long.TryParse(left, out _);
            if (isNumeric)
                adjecents.Add((i, j-1));
        }
        if (i+1 < rowLength)
        {
            bottom = matrix[i+1, j];
            var isNumeric = long.TryParse(bottom, out _);
            if (isNumeric)
                adjecents.Add((i+1, j)); 
        }
        if (j+1 < colLength)
        {
            right = matrix[i, j+1];
            var isNumeric = long.TryParse(right, out _);
            if (isNumeric)
                adjecents.Add((i, j+1));
        }

        Utils.Log($"T:{top} | L:{left} | B:{bottom} | R:{right}", logToConsole, logToFile);

        var topLeft = "";
        var topRight = "";
        var bottomLeft = "";
        var bottomRight = "";

        // Diagonals
        if (i-1 > -1 && j-1 > -1)
        {
            topLeft = matrix[i-1, j-1];
            var isNumeric = long.TryParse(topLeft, out _);
            if (isNumeric)
                adjecents.Add((i-1, j-1));
        }
        if (i+1 < rowLength && j-1 > -1)
        {
            bottomLeft = matrix[i+1, j-1];
            var isNumeric = long.TryParse(bottomLeft, out _);
            if (isNumeric)
                adjecents.Add((i+1, j-1));
        }
        if (i-1 > -1 && j+1 < colLength)
        {
            topRight = matrix[i-1, j+1];
            var isNumeric = long.TryParse(topRight, out _);
            if (isNumeric)
                adjecents.Add((i-1, j+1)); 
        }
        if (i+1 < rowLength && j+1 < colLength)
        {
            bottomRight = matrix[i+1, j+1];
            var isNumeric = long.TryParse(bottomRight, out _);
            if (isNumeric)
                adjecents.Add((i+1, j+1));
        }

        Utils.Log($"TR:{topRight} | TL:{topLeft} | BR:{bottomRight} | BL:{bottomLeft}", logToConsole, logToFile);

        return adjecents;
    }
}

Utils.Log("-- Day 3 --", true, true);
Utils.Log("-----------", true, true);

var day3 = new Day3();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day3.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day3.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();