using System;

namespace Eldora.Packaging.API.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class PackageEntryAttribute : Attribute
{
	public PackageType PackageType;
}

public enum PackageType
{

}