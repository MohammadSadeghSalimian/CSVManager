
using System.Reactive.Disposables;

using CsvManager.ViewModels;
using ReactiveUI;

namespace CsvManager.Views.Ucs
{
    /// <summary>
    /// Interaction logic for DataCreatorUc.xaml
    /// </summary>
    public sealed partial class DataCreatorUc : RBaseUc<DataCreatorVm>
    {
        public DataCreatorUc()
        {
            InitializeComponent();
            Setup();
        }

        protected override void SetupElements(CompositeDisposable d)
        {
            this.OneWayBind(ViewModel, x => x.Progress, v => v.ProgressBar.Value).DisposeWith(d);
            this.OneWayBind(ViewModel, x => x.MergeVm, v => v.MergeCsvUc.ViewModel).DisposeWith(d);
            this.OneWayBind(ViewModel, x => x.SqLiteDatabaseVm, v => v.DatabaseUc.ViewModel).DisposeWith(d);
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
          
        }
    }
}
