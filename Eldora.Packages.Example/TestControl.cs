using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.Packaging.API.Attributes;

namespace Eldora.Packages.Example;

[PackagePage("Test")]
public partial class TestControl : UserControl
{
	public TestControl()
	{
		InitializeComponent();
	}
}
