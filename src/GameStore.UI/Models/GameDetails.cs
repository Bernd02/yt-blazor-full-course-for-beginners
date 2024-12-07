using System.ComponentModel.DataAnnotations;

namespace GameStore.UI.Models;

public class GameDetails
{
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string? Name { get; set; }

	[Required(ErrorMessage = "The field Genre is required!")]
	public int? GenreId { get; set; }

	[Range(1, 100)]
	public decimal Price { get; set; }

	public DateOnly ReleaseDate { get; set; }
}
