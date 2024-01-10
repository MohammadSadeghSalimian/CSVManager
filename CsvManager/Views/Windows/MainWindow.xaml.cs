using System;
using System.Reactive.Disposables;
using CsvManager.ViewModels;
using ReactiveUI;
using Splat;

namespace CsvManager.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : RBaseWindow<MainVm>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ViewModel = Locator.Current.GetService<MainVm>() ??
                             throw new ApplicationException("Main view model cannot be null");

            this.Setup();
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            
        }

        protected override void SetupElements(CompositeDisposable d)
        {
            this.OneWayBind(this.ViewModel, x => x.MergeVm, v => v.CsvMergerUc.ViewModel).DisposeWith(d);

        }
    }
}
