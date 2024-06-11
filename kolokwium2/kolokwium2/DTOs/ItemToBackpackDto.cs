namespace kolokwium2.DTOs;

public class ItemToBackpackDto
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
}

public class BackpackDto
{
    public ICollection<ItemToBackpackDto> BackpackItems { get; set; } = null!;
}