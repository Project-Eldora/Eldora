using System;

namespace Eldora.Packaging.API.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OnPackageLoadedAttribute : Attribute
{

}
[AttributeUsage(AttributeTargets.Method)]
public class OnPackageUnloadedAttribute : Attribute
{

}