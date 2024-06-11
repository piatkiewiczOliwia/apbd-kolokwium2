using kolokwium2.DTOs;

namespace kolokwium2.Services;

public interface ICharacterService
{
    public Task<CharacterDto> GetCharacterWithData(int id);

    public Task<BackpackDto> AddItemsToBackpack(int[] ids, int characterId);
}