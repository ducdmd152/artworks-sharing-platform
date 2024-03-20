namespace ArtHubBO.DTO;

public class AccountUpdateTypeDto
{
    public int TypeUpdate { get; set; }
    public string NameTypeUpdate { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Avatar { get; set; }
}
