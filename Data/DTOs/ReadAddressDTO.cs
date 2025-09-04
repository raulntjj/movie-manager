namespace movies_api.Data.DTOs;

public class ReadAddressDTO
{
	public int Id { get; set; }
	public required string Street { get; set; }
	public required string Number { get; set; }
	public DateTime QueryTimestamp { get; set; } = DateTime.Now;
}