using System.Windows;
using System.Windows.Controls;

namespace CodeWanda.UI.Welcome
{
	class WelcomeButton : Button
	{
		static WelcomeButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WelcomeButton), new FrameworkPropertyMetadata(typeof(WelcomeButton)));
		}

		public static readonly DependencyProperty TitleProperty
			= DependencyProperty.Register(nameof(Title), typeof(string), typeof(WelcomeButton));

		public static readonly DependencyProperty DescriptionProperty
			= DependencyProperty.Register(nameof(Description), typeof(string), typeof(WelcomeButton));

		public string Title
		{
			get => GetValue(TitleProperty) as string;
			set => SetValue(TitleProperty, value);
		}

		public string Description
		{
			get => GetValue(DescriptionProperty) as string;
			set => SetValue(DescriptionProperty, value);
		}
	}
}
