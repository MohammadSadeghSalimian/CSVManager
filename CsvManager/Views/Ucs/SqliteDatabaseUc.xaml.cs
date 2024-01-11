using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvManager.ViewModels;
using ReactiveUI;

namespace CsvManager.Views.Ucs
{
    /// <summary>
    /// Interaction logic for SqLiteDatabaseUc.xaml
    /// </summary>
    public sealed partial class SqLiteDatabaseUc : RBaseUc<SqLiteDatabaseVm>
    {
        public SqLiteDatabaseUc()
        {
            InitializeComponent();
            this.Setup();
        }

        protected override void SetupElements(CompositeDisposable d)
        {
            this.Bind(ViewModel, x => x.IgnoredColumns, v => v.IgnoredColumnsTxt.Text).DisposeWith(d);
            this.Bind(ViewModel, x => x.PickedDateTime, v => v.ReferencedDate.Value).DisposeWith(d);
            this.Bind(ViewModel, x => x.TableName, v => v.TableNameTxt.Text).DisposeWith(d);
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            this.BindCommand(ViewModel, x => x.ConvertToDatabaseCmd, v => v.ConvertBtn).DisposeWith(d);
        }
    }
}
