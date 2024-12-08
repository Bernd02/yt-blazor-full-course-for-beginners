using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameStore.UI.Converters;

/// <summary>
/// Read - Defines how specific json data-types converts to a string.<br/>
/// Write - Defines how a string converts to json data-types.
/// </summary>
internal class StringConverter : JsonConverter<string?>
{
	public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// if type is number ? convert to int32 first : just read as string
		if (reader.TokenType == JsonTokenType.Number)
		{
			return reader.GetInt32().ToString();
		}
		return reader.GetString();
	}

	public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
	{
		// just write the value
		writer.WriteStringValue(value);
	}
}
