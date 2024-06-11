using kolokwium2.Models;

namespace kolokwium2.DTOs;

public class CharacterDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<ItemDto> ItemDtos { get; set; } = new HashSet<ItemDto>();
    public ICollection<TitleDto> TitleDtos { get; set; } = new HashSet<TitleDto>();
}

public class ItemDto
{
    public string Name { get; set; }

    public int Weight { get; set; }
    public int Amount { get; set; }
}

public class TitleDto
{
    public string Name { get; set; }
    public DateTime AcquiredAt { get; set; }
}