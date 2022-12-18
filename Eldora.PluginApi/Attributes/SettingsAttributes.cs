using System;
using System.Runtime.CompilerServices;

namespace Eldora.PluginApi.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SettingsAttribute : Attribute
{
	public string Name { get; }

	public SettingsAttribute([CallerMemberName] string name = null)
	{
		Name = name;
	}
}

public class BooleanSettingsAttribute : SettingsAttribute
{
}

public class IntegerSettingsAttribute : SettingsAttribute
{
	public IntegerDisplayType DisplayType { get; } = IntegerDisplayType.NumericUpDown;
}

public enum IntegerDisplayType
{
	NumericUpDown,
	Slider
}

public class StringSettingsAttribute : SettingsAttribute
{
}

public class PasswordSettingsAttribute : SettingsAttribute
{
	public char PasswordChar { get; } = '*';
}

public class FileChooseSettingsAttribute : SettingsAttribute
{
}