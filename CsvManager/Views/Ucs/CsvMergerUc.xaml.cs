
using System.Reactive.Disposables;

using CsvManager.ViewModels;
using ReactiveUI;

namespace CsvManager.Views.Ucs
{
    /// <summary>
    /// Interaction logic for CsvMergerUc.xaml
    /// </summary>
    public sealed partial class CsvMergerUc : RBaseUc<MergeVm>
    {
        public CsvMergerUc()
        {
            InitializeComponent();
            Setup();
        }

        protected override void SetupElements(CompositeDisposable d)
        {
            this.OneWayBind(ViewModel, x => x.Progress, v => v.ProgressBar.Value).DisposeWith(d);
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            this.BindCommand(ViewModel, x => x.MergeCmd, v => v.MergeBtn).DisposeWith(d);
        }
    }
}
