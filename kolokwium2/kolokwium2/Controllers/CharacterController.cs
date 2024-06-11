using kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCharacterWithData(int id)
    {
        var res = await _characterService.GetCharacterWithData(id);
        return Ok(res);
    }

    [HttpPost("/characters/{id:int}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack(int[] ids, int characterId)
    {
        var res = await _characterService.AddItemsToBackpack(ids, characterId);
        return Ok(res);
    }
}