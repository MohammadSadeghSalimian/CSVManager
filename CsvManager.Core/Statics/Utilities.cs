using System.Text.RegularExpressions;
using CsvManager.Core.Models;

namespace CsvManager.Core.Statics;

public static class Utilities
{
    public static void MergeCsvFiles(IReadOnlyList<string> inputs,string output,IProgress<double> progress)
    {
        using var writer = new StreamWriter(output);
        var headerWritten = false;
        var n = 0;
        var totalFiles =(double) inputs.Count;
        for (var i = 0; i < inputs.Count; i++)
        {
            var path = inputs[i];
            using var reader = new StreamReader(path);
            // Check if it's the first file, then write the header with new column
            if (!headerWritten)
            {
                var headers = reader.ReadLine();
                if (headers != null) n = headers.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
                writer.WriteLine(headers);
                headerWritten = true;
            }
            else
            {
                // Skip header line
                reader.ReadLine();
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var nColumns = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
                if (nColumns < n)
                {
                    continue;
                }

                writer.WriteLine(line);
            }

            progress.Report(100*(i+1)/totalFiles);
        }
    }

   

    private static bool ExtractTimeFromCsvName(string name,out DateTime dateTime)
    {
        var m = Regex.Match(name, @"\w+_(\d{4})-(\d{2})-(\d{2})-(\d{2})-(\d{2})-(\d{2})");
        if (m.Success)
        {
            dateTime = new DateTime(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value),
                int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value), int.Parse(m.Groups[5].Value),
                int.Parse(m.Groups[5].Value));

            return true;
        }
        dateTime=DateTime.Today;
        return false;
    }

    public static IReadOnlyList<CsvFile> SortCsvFiles(IReadOnlyList<string> files)
    {
        var ll = new List<CsvFile>(files.Count);
        foreach (var file in files)
        {
            var res = ExtractTimeFromCsvName(Path.GetFileNameWithoutExtension(file), out var date);
            if (!res)
            {
                continue;
            }

            var t = new CsvFile()
            {
                FileInfo = new FileInfo(file),
                DateTime = date
            };
            ll.Add(t);
        }
        ll.Sort((x,y)=>DateTime.Compare(x.DateTime,y.DateTime));
        return ll;
    }


   
}