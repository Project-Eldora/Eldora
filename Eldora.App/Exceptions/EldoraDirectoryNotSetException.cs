using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.App.Exceptions;

internal class EldoraDirectoryNotSetException : Exception
{
	public string Directory { get; }

	public EldoraDirectoryNotSetException(string directory)
	{
		Directory = directory;
	}
}
