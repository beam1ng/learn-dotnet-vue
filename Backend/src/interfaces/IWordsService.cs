namespace interfaces;

using dtos.words;

public interface IWordsService
{
    public Task<WordDto> GetRandomWord(GetRandomWordDto getWordDto);
}