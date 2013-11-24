using System.ComponentModel;

namespace CrystalLib.Toolset.Docking
{
	public abstract class ThemeBase : Component, ITheme
	{
	    public abstract void Apply(DockPanel dockPanel);
	}
}
