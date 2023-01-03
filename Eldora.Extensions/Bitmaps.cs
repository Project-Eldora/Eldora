using System.Drawing;
using Svg;

namespace Eldora.Extensions;

public static class Bitmaps
{
	public static Bitmap LoadBitmapFromSvg(byte[] data, int width = 24, int height = 24)
	{
		//var byteArray = Encoding.UTF8.GetBytes(File.ReadAllText(Resources.GetObject(key)));
		using var stream = new MemoryStream(data);
		//if stream is null return new empty Bitmap
		if (stream == null) return new Bitmap(1, 1);
		var svgDocument = SvgDocument.Open<SvgDocument>(stream);
		var bitmap = svgDocument.Draw(width, height);
		return bitmap;
	}
}
