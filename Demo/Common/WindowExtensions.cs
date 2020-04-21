using System;
using System.Windows;
using System.Windows.Interop;

namespace CodeWanda.UI.Common
{
	public static class WindowExtensions
	{
		public static void SetPlacement(this Window window, string placementXml)
		{
			WindowPlacement.SetPlacement(new WindowInteropHelper(window).Handle, placementXml);
		}

		public static string GetPlacement(this Window window)
		{
			return WindowPlacement.GetPlacement(new WindowInteropHelper(window).Handle);
		}

		public static Rect GetAbsolutePlacement(this FrameworkElement element, Window window)
		{
			if (element == null) throw new ArgumentNullException(nameof(element));
			if (window == null) throw new ArgumentNullException(nameof(window));

			var absolutePos = element.PointToScreen(new Point(0, 0));
			var posMW = window.PointToScreen(new Point(0, 0));
			absolutePos = new Point(absolutePos.X - posMW.X, absolutePos.Y - posMW.Y);
			return new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
		}
	}
}