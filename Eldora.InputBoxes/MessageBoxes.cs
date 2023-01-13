using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Eldora.Extensions;

public delegate void ConfirmDelegate();
public delegate void CancelDelegate();

public static class MessageBoxes
{
	public static void RequestOkCancelConfirmation(string content, string title, MessageBoxIcon icon, ConfirmDelegate? confirmationCallback = null, CancelDelegate? cancelationCallback = null)
	{
		var result = MessageBox.Show(content, title, MessageBoxButtons.OKCancel, icon);
		if (result != DialogResult.OK)
		{
			cancelationCallback?.Invoke();
			return;
		}

		confirmationCallback?.Invoke();
	}

	public static void RequestYesNoConfirmation(string content, string title, MessageBoxIcon icon, ConfirmDelegate? confirmationCallback = null, CancelDelegate? cancelationCallback = null)
	{
		var result = MessageBox.Show(content, title, MessageBoxButtons.YesNo, icon);
		if (result != DialogResult.Yes)
		{
			cancelationCallback?.Invoke();
			return;
		}

		confirmationCallback?.Invoke();
	}
}
