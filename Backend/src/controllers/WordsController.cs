using dtos.words;
using interfaces;
using Microsoft.AspNetCore.Mvc;

namespace controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly IWordsService _service;

    public WordsController(IWordsService service)
    {
        _service = service;
    }

    [HttpPost("random")]
    public async Task<ActionResult<WordDto>> GetRandomWord([FromBody] GetRandomWordDto getWordDto)
    {
        var wordDto = await _service.GetRandomWord(getWordDto);
        return wordDto is null
            ? NoContent()
            : Ok(wordDto);
    }

}