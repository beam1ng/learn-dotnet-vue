using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace dtos.words;

public class GetRandomWordDto
{
    public int length = 5;
    public int frequencyRankTop = 0;
    public int frequencyRankBottom = 500;
}

public class WordDto
{
    public required string word{get;set;}
}