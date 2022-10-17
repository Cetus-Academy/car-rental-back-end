namespace CarRental.Application.Dto;

public class ReservatedCarDto
{
    public int Id { get; set; }
    public int KmToDrive { get; set; }
    public DateTimeOffset RentFrom { get; set; }
    public DateTimeOffset RentTo { get; set; }
}