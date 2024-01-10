namespace CsvManager.Core
{
    class CsvMerger
    {
        public static void MergeCsvFiles(IEnumerable<string> filePaths, string outputPath)
        {
            using var writer = new StreamWriter(outputPath);
            bool headerWritten = false;
            foreach (var path in filePaths)
            {
                using var reader = new StreamReader(path);
                // Check if it's the first file, then write the header with new column
                if (!headerWritten)
                {
                    writer.WriteLine("Datetime," + reader.ReadLine());
                    headerWritten = true;
                }
                else
                {
                    // Skip header line
                    reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string timeValue = line.Split(',')[1]; // Assuming comma as delimiter and second column contains the time.

                    writer.WriteLine($"{GetDateTimeFromTime(timeValue)},{line}");
                }
            }
        }

        // Method to get the datetime based on the provided time
        private static string GetDateTimeFromTime(string timeValue)
        {
            // Use your logic to calculate the datetime value based on the provided time.
            // As an example, if the timeValue format is "HH:mm:ss":
            DateTime resultDateTime = DateTime.Today.Add(TimeSpan.Parse(timeValue));
            return resultDateTime.ToString("o"); // "o" is the round-trip format specifier.
        }
    }
}