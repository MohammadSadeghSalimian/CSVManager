using System.Reactive.Disposables;
using ReactiveUI;
using Splat;

namespace CsvManager.Views.Ucs;

public abstract class RBaseUc<T> : ReactiveUserControl<T> where T : class
{
    protected RBaseUc()
    {

    }

    protected virtual void Setup()
    {
        this.WhenActivated(d =>
        {
            SetupElements(d);
            SetupCommands(d);
        });
        //this.DataContext = this;
    }
    protected abstract void SetupElements(CompositeDisposable d);
    protected abstract void SetupCommands(CompositeDisposable d);
    protected void GetViewModel()
    {
        ViewModel = Locator.Current.GetService<T>();
    }
}