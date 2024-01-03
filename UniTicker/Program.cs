using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\Zaid\Desktop\UniTicker\logfile.log";
        Dictionary<string, int> operationCounts = new Dictionary<string, int>();
        HashSet<string> uniquePairs = new HashSet<string>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 3)
                    {
                        string operation = parts[2];
                        string user = parts[1];
                        string pair = $"{user}-{operation}";

                        if (uniquePairs.Add(pair))
                        {
                            if (operationCounts.ContainsKey(operation))
                            {
                                operationCounts[operation]++;
                            }
                            else
                            {
                                operationCounts[operation] = 1;
                            }
                        }
                    }
                }
            }

            var topTwoOperations = operationCounts.OrderByDescending(kv => kv.Value).Take(2);
            int distinctUserCount = uniquePairs.Select(pair => pair.Split('-')[0]).Distinct().Count();

            Console.WriteLine($"distinctUsersCount: {distinctUserCount}");

            foreach (var item in topTwoOperations)
            {
                var percentage = Math.Round(((float)item.Value / (float)distinctUserCount) * 100, 2);
                Console.WriteLine($"{item.Key}: {item.Value} times; percentage of users using it: {percentage}%");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.ReadLine();
    }
}
