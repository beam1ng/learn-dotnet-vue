using System.Text;
using dtos.words;
using interfaces;
using other;

namespace services;

public class WordsService : IWordsService
{
    private AppDbContext _context;

    public WordsService(AppDbContext context) => _context = context;

    public async Task<string> GetRandomWordInRange(int frequencyRankTop = 0, int frequencyRankBottom = 500)
    {
        var recordsCount = _context.Fives.Count();

        var wordEntry = _context.Fives
            .OrderByDescending(w=>w.Count)
            .Skip(Random.Shared.Next(
                frequencyRankTop,
                (int)MathF.Min(recordsCount ,frequencyRankBottom)))
            .FirstOrDefault();

        if(wordEntry is null)
        {
            StringBuilder sb = new();
            sb.Append($"{nameof(AppDbContext)} {nameof(GetRandomWordInRange)} could not satisfy the query conditions:\n ");
            sb.Append($"{nameof(frequencyRankTop)}: {frequencyRankTop}\n");
            sb.Append($"{nameof(frequencyRankBottom)}: {frequencyRankBottom}\n");
            throw new Exception(sb.ToString());
        }

        if(wordEntry.Word is null || wordEntry.Word == string.Empty)
        {
            throw new Exception($"{nameof(wordEntry.Word)} was empty.");
        }

        return wordEntry.Word;
    }

    public async Task<WordDto> GetRandomWord(GetRandomWordDto getWordDto)
    {

        string randomWord = await GetRandomWordInRange(
            frequencyRankTop: getWordDto.frequencyRankTop,
            frequencyRankBottom: getWordDto.frequencyRankBottom);

        var wordDto = new WordDto()
        {
            word = randomWord
        };

        return wordDto;
    }
}
