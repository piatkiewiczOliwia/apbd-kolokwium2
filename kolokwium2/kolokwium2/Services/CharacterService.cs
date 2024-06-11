using kolokwium2.Contexts;
using kolokwium2.DTOs;
using kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2.Services;



public class CharacterService : ICharacterService
{
    private readonly DatabaseContext _context;

    public CharacterService(DatabaseContext context)
    {
        _context = context;
    }


    public async  Task<CharacterDto> GetCharacterWithData(int id)
    {
        bool characterExists = await _context.Characters.AnyAsync(c => c.Id == id);

        if (!characterExists)
        {
            throw new Exception("Character with given id does not exist");
        }

        var character = await _context.Characters
            .Select(c => c)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        var items = await _context
            .Items
            .Include(i => i.Backpacks)
            .Where(i => i.Backpacks.Any(b => b.CharacterId == id))
            .ToListAsync();
       
        var titles = await _context
            .Titles
            .Include(t => t.CharacterTitles)
            .Where(t => t.CharacterTitles.Any(ct => ct.CharacterId == id))
            .ToListAsync();

        
        List<ItemDto> itemDtos = new List<ItemDto>();
        ItemDto i;
        foreach (var item in items)
        {
            i = new ItemDto()
            {
                Name = item.Name,
                Amount = item.Backpacks.Where(i => i.ItemId == item.Id).Select(b => b.Amount).FirstOrDefault(),
                Weight = item.Weight
            };
            
            itemDtos.Add(i);
        }

        List<TitleDto> titleDtos = new List<TitleDto>();
        TitleDto t;
        foreach (var title in titles)
        {
            t = new TitleDto()
            {
                Name = title.Name,
                AcquiredAt = title.CharacterTitles.Where(t => t.TitleId == title.Id).Select(ct => ct.AcquiredAt).FirstOrDefault()
            };
            
            titleDtos.Add(t);
        }
        

        var res = new CharacterDto()
        {
            Id = character.Id,
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            ItemDtos = itemDtos,
            TitleDtos = titleDtos
        };

        return res;

    }
    

    public async Task<BackpackDto> AddItemsToBackpack(int[] ids, int characterId)
    {
        bool itemExists;

        foreach (var id in ids)
        {
            itemExists = await _context.Items.AnyAsync(i => i.Id == id);

            if (!itemExists)
            {
                throw new Exception("At least one item within given id's does not exists");
            }
        }

        bool characterExists = await _context.Characters.AnyAsync(c => c.Id == characterId);
        if (!characterExists)
        {
            throw new Exception("Character does not exist");
        }

        int maxWeight = await _context.Characters
            .Where(c => c.Id == characterId)
            .Select(c => c.MaxWeight).FirstOrDefaultAsync();

        int sum = 0;
        int weight;
        foreach (var id in ids)
        {
            weight = await _context.Items.Where(i => i.Id == id).Select(i => i.Weight).FirstOrDefaultAsync();
            sum += weight;
        }

        if (sum > maxWeight)
        {
            throw new Exception("The items are too heavy for character");
        }

        List<ItemToBackpackDto> resList = new List<ItemToBackpackDto>();

        foreach (var itemId in ids)
        {
            Backpack res = new Backpack()
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            };

            ItemToBackpackDto resItemToBackpackDto = new ItemToBackpackDto()
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            };
            
            await _context.AddAsync(res);
            await _context.SaveChangesAsync();
            
            resList.Add(resItemToBackpackDto);
        }

        var resBackpack = new BackpackDto()
        {
            BackpackItems = resList
        };

        return resBackpack;
    }
}