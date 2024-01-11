
using System.Reactive.Disposables;
using CsvManager.ViewModels;
using ReactiveUI;

namespace CsvManager.Views.Ucs
{
    /// <summary>
    /// Interaction logic for MergeUc.xaml
    /// </summary>
    public sealed partial class MergeUc : RBaseUc<MergeCsvVm>
    {
        public MergeUc()
        {
            InitializeComponent();
            this.Setup();
        }

        protected override void SetupElements(CompositeDisposable d)
        {
           
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            this.BindCommand(ViewModel, x => x.MergeCmd, v => v.MergeBtn).DisposeWith(d);
        }
    }
}
