using System;
using System.Collections.Generic;

namespace Eldora.PluginApi.Attributes;

/// <summary>
/// This attribute can be used on any Control to be added to the sidebar nav
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ToolsPageAttribute : Attribute
{
	public string PageTitle { get; }
	public string[] PagePath { get; }
	public string[] PagePathWithTitle { get; }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="title">The Title of the page, which will be shown as the Clickable Node</param>
	/// <param name="pagePath">The Path of the Page in the sidebar</param>
	public ToolsPageAttribute(string title, string pagePath = "")
	{
		PageTitle = title;
		PagePath = pagePath.Split('/');
		if (string.IsNullOrEmpty(pagePath)) PagePath = Array.Empty<string>();

		var list = new List<string>(PagePath)
		{
				PageTitle
		};
		PagePathWithTitle = list.ToArray();
		//PagePathTitles = PagePath.ToList().Select(p => p.FirstCharToUpper()).ToArray();
	}
}