public class WordDataImporter
{
    private const int TargetFiveLetterCount = 25000;
    private List<WordEntry> wordFrequencyData = new();

    private readonly  AppDbContext _db;
    public WordDataImporter(AppDbContext db) => _db = db;


    public async Task ImportAsync(string path, CancellationToken ct = default)
    {
        using (var csv = File.OpenText(path))
        {
            string? newLine = csv.ReadLine();
            for (int i = 0; i < TargetFiveLetterCount && newLine != null; i++)
            {
                newLine = csv.ReadLine();

                if (newLine == string.Empty || newLine == null)
                    continue;

                var values = newLine.Split(',');
                string word = values[0];
                long frequencyRank = long.Parse(values[1],System.Globalization.NumberStyles.Integer);

                wordFrequencyData.Add(new WordEntry { Word = word, Count = frequencyRank });
            }
        }

        _db.ChangeTracker.AutoDetectChangesEnabled = false;
        _db.Words.AddRange(wordFrequencyData);
        await _db.SaveChangesAsync(ct);

        _db.ChangeTracker.AutoDetectChangesEnabled =true;
    }
}
