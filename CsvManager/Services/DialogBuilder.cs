namespace CsvManager.Services;

public class DialogBuilder : ICommonDialogBuilder
{
    public ICommonDialogUnit GetDialog()
    {
        return new CommonDialogUnit();
    }
}