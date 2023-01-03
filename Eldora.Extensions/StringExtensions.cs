using System.Linq;
using System.Text.RegularExpressions;

namespace Eldora.Extensions;

public static class StringExtensions
{

	/// <summary>
	/// Splits a Camel Case string
	/// </summary>
	/// <param name="input"></param>
	/// <param name="delimiter"></param>
	/// <returns></returns>
	public static string SplitCamelCase(this string input, string delimiter = " ")
	{
		return input.Any(char.IsUpper) ? string.Join(delimiter, Regex.Split(input, "(?<!^)(?=[A-Z])")) : input;
	}

	/// <summary>
	/// Returns a string to TitleCase
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string FirstCharToUpper(this string input) =>
	input switch
	{
		null => "",
		"" => "",
		_ => string.Concat(input[0].ToString().ToUpper(), input.Substring(1))
	};
}
