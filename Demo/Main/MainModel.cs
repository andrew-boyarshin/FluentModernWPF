#define READONLY

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using CodeWanda.UI.Common;
using CodeWanda.UI.Main.Pages.ContextTree;
using MahApps.Metro.Controls;
using ModernWpf.Controls;

namespace CodeWanda.UI.Main
{
    public sealed class MainModel : DependencyObject, INotifyPropertyChanged
    {
        public MainModel()
        {
            ContextTreeDetails.CollectionChanged += ContextTreeDetailsOnCollectionChanged;
        }

        private void ContextTreeDetailsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var oldContexts = (e.OldItems ?? Array.Empty<AnalysisContext>()).OfType<AnalysisContext>();
            var newContexts = (e.NewItems ?? Array.Empty<AnalysisContext>()).OfType<AnalysisContext>();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    DoAdd();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    DoRemove();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    DoRemove();
                    DoAdd();
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ContextTreeDetailsMenuItems.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            void DoAdd()
            {
                HamburgerMenuIconItem Creator(AnalysisContext x)
                {
                    return new HamburgerMenuIconItem
                    {
                        Tag = new ContextTreePage(x),
                        Label = $"Context #{x.GetHashCode()}",
                        Icon = new SymbolIcon
                        {
                            Symbol = Symbol.Favorite
                        }
                    };
                }

                ContextTreeDetailsMenuItems.AddRange(newContexts.Select(Creator));
            }

            void DoRemove()
            {
                foreach (var context in oldContexts)
                    ContextTreeDetailsMenuItems.RemoveAll(x => x.Tag == context);
            }
        }

        public ObservableCollection<AnalysisContext> ContextTreeDetails { get; } =
            new ObservableCollection<AnalysisContext>();

        public ObservableCollection<HamburgerMenuItemBase> ContextTreeDetailsMenuItems { get; } = new ObservableCollection<HamburgerMenuItemBase>();

#if READONLY
        private static readonly DependencyPropertyKey WindowTitlePropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(WindowTitle), typeof(string), typeof(MainModel),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty WindowTitleProperty = WindowTitlePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ProjectHeaderTooltipPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(ProjectHeaderTooltip), typeof(string), typeof(MainModel),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty ProjectHeaderTooltipProperty =
            ProjectHeaderTooltipPropertyKey.DependencyProperty;
#else
		private static readonly DependencyProperty WindowTitleProperty
			= DependencyProperty.Register(nameof(WindowTitle), typeof(string), typeof(MainModel),
				new FrameworkPropertyMetadata());

		private static readonly DependencyProperty ProjectHeaderTooltipProperty
			= DependencyProperty.Register(nameof(ProjectHeaderTooltip), typeof(string), typeof(MainModel),
				new FrameworkPropertyMetadata());

		private static readonly DependencyProperty ProjectNameProperty
			= DependencyProperty.Register(nameof(ProjectName), typeof(string), typeof(MainModel),
				new FrameworkPropertyMetadata
				{
					PropertyChangedCallback = ProjectNamePropertyChanged
				});
#endif

        private static readonly DependencyProperty ProjectNameProperty
            = DependencyProperty.Register(nameof(ProjectName), typeof(string), typeof(MainModel),
                new FrameworkPropertyMetadata
                {
                    PropertyChangedCallback = ProjectNamePropertyChanged,
                    BindsTwoWayByDefault = true
                });

        /// <summary>Identifies the <see cref="ProjectNameEditValue"/> dependency property.</summary>
        public static readonly DependencyProperty ProjectNameEditValueProperty
            = DependencyProperty.Register(nameof(ProjectNameEditValue), typeof(string), typeof(MainModel),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true
                });

        private static void ProjectNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is MainModel model))
                throw new InvalidOperationException();

            model.UpdateWindowTitle();
            model.UpdateProjectHeaderTooltip();
        }

        public void UpdateWindowTitle()
        {
            var name = ProjectName;
            WindowTitle = name != null ? $"{name} - CodeWanda" : "CodeWanda";
        }

        public void UpdateProjectHeaderTooltip()
        {
            var name = ProjectName ?? "<Unnamed>";
            ProjectHeaderTooltip = $"Analysis project {name}, last saved <?> minutes ago.";
        }

        public string WindowTitle
        {
            get => GetValue(WindowTitleProperty) as string;
            private set
            {
#if READONLY
                SetValue(WindowTitlePropertyKey, value);
#else
				SetValue(WindowTitleProperty, value);
#endif
                OnPropertyChanged();
            }
        }

        public string ProjectHeaderTooltip
        {
            get => GetValue(ProjectHeaderTooltipProperty) as string;
            private set
            {
#if READONLY
                SetValue(ProjectHeaderTooltipPropertyKey, value);
#else
				SetValue(ProjectHeaderTooltipProperty, value);
#endif
                OnPropertyChanged();
            }
        }

        public string ProjectName
        {
            get => GetValue(ProjectNameProperty) as string;
            set
            {
                SetValue(ProjectNameProperty, value);
                OnPropertyChanged();
            }
        }

        public string ProjectNameEditValue
        {
            get => GetValue(ProjectNameEditValueProperty) as string;
            set
            {
                SetValue(ProjectNameEditValueProperty, value);
                OnPropertyChanged();
            }
        }

        private static CancellationTokenSource _rebuildGraphCancellationTokenSource;
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}