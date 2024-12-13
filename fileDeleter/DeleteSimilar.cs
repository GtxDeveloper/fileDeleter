using System.Diagnostics;

namespace fileDeleter;

public static class DeleteSimilar
{
    public static void Delete(string path)
    {
        string directoryPath = path;
        double similarityRate = 85; 

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("404");
            return;
        }

        var sw = Stopwatch.StartNew();

        Dictionary<string, string> fileContents = new Dictionary<string, string>();
        List<string> duplicateFiles = new List<string>();

        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            string content = File.ReadAllText(filePath);

            bool isDuplicate = false;
            foreach (var pair in fileContents)
            {
                if (content.Length < pair.Value.Length)
                {
                    for (int i = content.Length; i >= pair.Value.Length;)
                    {
                        content += '0';
                    }
                }
                
                double similarity = 100 - (LevenshteinDistance.Calculate(content, pair.Value) * 100 / pair.Value.Length);
                Console.WriteLine($"{similarity}");

                if (similarity >= similarityRate)
                {
                    isDuplicate = true;
                    duplicateFiles.Add(filePath);
                    break;
                }
            }

            if (!isDuplicate)
            {
                fileContents.Add(filePath, content);
            }
        }

        foreach (string duplicateFile in duplicateFiles)
        {
            File.Delete(duplicateFile);
        }

        sw.Stop();

        Console.WriteLine($"Deleted dublicates: {duplicateFiles.Count}");
        Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds} sec");
    }
}