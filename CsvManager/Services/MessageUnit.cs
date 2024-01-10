using System;
using System.Windows;

namespace CsvManager.Services;

public class MessageUnit : IMessageUnit
{
      
       
    public MessageUnit()
    {

            
    }
    private void ShowMessage(MessageType type, string message)
    {

        MessageBox.Show(message, type.ToString());

    }
    public void SetParentObject(object mainVm)
    {
           
    }
    public void ErrorMessage(string message)
    {
        ShowMessage(MessageType.Error, message);
    }

    public void ErrorMessage(Exception error)
    {
            
    }

    public void WarningMessage(string message)
    {
        ShowMessage(MessageType.Warning, message);
    }
    public void InformationMessage(string message)
    {
        ShowMessage(MessageType.Information, message);
    }
}