using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.Extensions;

/// <summary>
/// A custom Resource Manager that provides cached instances of objects.
/// This differs from the stock ResourceManager class which always
/// deserializes and creates new instances of every object.
/// After the first time an object is requested, it will be cached
/// for all future requests.
/// </summary>
public class CachedResourceManager : System.Resources.ResourceManager
{
	/// <summary>
	/// A hashtable is used to store the objects.
	/// </summary>
	private readonly Hashtable _objectCache = new();

	public CachedResourceManager(Type resourceSource) : base(resourceSource)
	{
	}

	public CachedResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
	{
	}

	public CachedResourceManager(string baseName, Assembly assembly, Type usingResourceSet) : base(baseName, assembly, usingResourceSet)
	{
	}

	public CachedResourceManager() : base()
	{
	}

	/// <summary>
	/// Returns a cached instance of the specified resource.
	/// </summary>
	public override object? GetObject(string name)
	{
		return GetObject(name, null);
	}

	/// <summary>
	/// Returns a cached instance of the specified resource.
	/// </summary>
	public override object? GetObject(string name, CultureInfo? culture)
	{
		// Try to get the specified object from the cache.
		var obj = _objectCache[name];

		// If the object has not been cached, add it
		// and return a cached instance.
		if (obj == null)
		{
			_objectCache[name] = base.GetObject(name, culture);
			obj = _objectCache[name];
		}

		return obj;
	}
}
