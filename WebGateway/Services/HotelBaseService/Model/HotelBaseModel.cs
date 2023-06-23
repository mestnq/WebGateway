namespace Gateway.Services.HotelBaseService.Model;

public record HotelBaseModel
{
    public long Id { get; set; }
    public double Rating { get; set; }
    public Coordinates Coordinates { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string[] Photos { get; set; } = null!;

    public int RoomsAvailable { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal MinPrice { get; set; }
}