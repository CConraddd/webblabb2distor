namespace webblabb2distor.Models.Auctions;

public class UserVm
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<AuctionVm> Auctions { get; set; }
}
