using System;
using System.IO;

namespace Eldora.Utils;

public sealed class TempFile : IDisposable
{
	private bool _isDisposed;

	public bool Keep { get; set; }
	public string Path { get; private set; }

	public TempFile() : this(false)
	{
	}

	public TempFile(bool shortLived)
	{
		Path = CreateTemporaryFile(shortLived);
	}

	~TempFile()
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

		if (!Keep)
		{
			TryDelete();
		}
	}

	private void TryDelete()
	{
		try
		{
			File.Delete(Path);
		}
		catch (IOException)
		{
		}
		catch (UnauthorizedAccessException)
		{
		}
	}

	private static string CreateTemporaryFile(bool shortLived)
	{
		var temporaryFile = System.IO.Path.GetTempFileName();

		if (shortLived)
		{
			// Set the temporary attribute, meaning the file will live 
			// in memory and will not be written to disk 
			//
			File.SetAttributes(temporaryFile, File.GetAttributes(temporaryFile) | FileAttributes.Temporary);
		}

		return temporaryFile;
	}
}