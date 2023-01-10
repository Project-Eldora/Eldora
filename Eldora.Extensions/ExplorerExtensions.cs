using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.Extensions;
public static class ExplorerExtensions
{

	public static void OpenExplorer(string path)
	{
		if (!Directory.Exists(path)) { return; }

		Process.Start("explorer.exe", path);
	}

	[DllImport("shell32.dll", SetLastError = true)]
	private static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

	[DllImport("shell32.dll", SetLastError = true)]
	private static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

	public static void OpenExplorerAndSelectItem(string folderPath, string file)
	{
		SHParseDisplayName(folderPath, IntPtr.Zero, out IntPtr nativeFolder, 0, out uint psfgaoOut);

		if (nativeFolder == IntPtr.Zero)
		{
			// Log error, can't find folder
			return;
		}

		SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out IntPtr nativeFile, 0, out psfgaoOut);

		IntPtr[] fileArray;
		if (nativeFile == IntPtr.Zero)
		{
			// Open the folder without the file selected if we can't find the file
			fileArray = Array.Empty<IntPtr>();
		}
		else
		{
			fileArray = new IntPtr[] { nativeFile };
		}

		_ = SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

		Marshal.FreeCoTaskMem(nativeFolder);
		if (nativeFile != IntPtr.Zero)
		{
			Marshal.FreeCoTaskMem(nativeFile);
		}
	}

}
