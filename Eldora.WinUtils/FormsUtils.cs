using System.Drawing;
using System.Linq;
using System.Reflection;

namespace Eldora.WinUtils;

public static class FormsUtils
{
	public static Image ReadEmbeddedRessourceImage(Assembly assembly, string searchPattern)
	{
		var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(searchPattern));
		using var stream = assembly.GetManifestResourceStream(resourceName);
		return stream != null ? Image.FromStream(stream) : null;
	}
	
}