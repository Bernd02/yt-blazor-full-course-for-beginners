using GameStore.UI.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameStore.UI.Models;

public class GameDetails
{
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string? Name { get; set; }

	[Required(ErrorMessage = "The field Genre is required!")]
	// specify conversion from json number to c# string
	// ... you could also just use a int? and not deal with a specific converter.
	[JsonConverter(typeof(StringConverter))]
	public string? GenreId { get; set; }

	[Range(1, 100)]
	public decimal Price { get; set; }

	public DateOnly ReleaseDate { get; set; }
}
