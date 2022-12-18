namespace Eldora.Utils;

using System;
using System.IO;

public sealed class TempFolder : IDisposable
{
	private bool _isDisposed;

	public bool Keep { get; set; }
	public string Path { get; private set; }

	public TempFolder() : this(false)
	{
	}

	public TempFolder(bool shortLived)
	{
		this.Path = CreateTemporaryFolder();
	}

	~TempFolder()
	{
		Dispose(false);
	}

	public void Dispose()
	{
		Dispose(false);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (_isDisposed) return;

		_isDisposed = true;

		if (!this.Keep)
		{
			TryDelete();
		}
	}

	private void TryDelete()
	{
		try
		{
			Directory.Delete(this.Path);
		}
		catch (IOException)
		{
		}
		catch (UnauthorizedAccessException)
		{
		}
	}

	public static string CreateTemporaryFolder()
	{
		var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
		Directory.CreateDirectory(path);
		return path;
	}
}