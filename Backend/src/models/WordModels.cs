using Microsoft.EntityFrameworkCore;

namespace models.WordModels;

public class WordEntry
{
    public int Id { get; set; }

    public required string Word { get; set; }
    public long Count { get; set; }
}

[Keyless]
public class WordFives
{
    public string Word { get; set; } = "";
    public int Count { get; set; }
}