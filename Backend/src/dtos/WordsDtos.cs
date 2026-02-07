using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace dtos.words;

public class GetRandomWordDto
{
    public int length {get;set;} = 5;
    public int frequencyRankTop {get;set;} = 0;
    public int frequencyRankBottom {get;set;} = 500;
}

public class WordDto
{
    public required string word{get;set;}
}