using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CsvManager.Services;

public class CommonDialogUnit : ICommonDialogUnit
{
    private IList<string> _filter;
    private IList<string> _extensions;
    public string Title { get; set; }
    public string DefaultExtension { get; set; }
    public void SetFilters(IList<string> filters)
    {
        _filter = filters;
    }
    public void SetFilters(params string[] filters)
    {
        _filter = filters;
    }
    private static IEnumerable<CommonFileDialogFilter> Convert(ICollection<string> strFilter, ICollection<string> extensions)
    {
        var aa = new List<CommonFileDialogFilter>(strFilter.Count);
        foreach (var item in strFilter)
        {
            var parts = item.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new ApplicationException("The filter is not set correctly!");
            }
            try
            {
                aa.Add(new CommonFileDialogFilter(parts[0].Trim(), parts[1].Trim()));
                extensions.Add(parts[1].Trim());
            }
            catch (Exception)
            {
                throw new ApplicationException("The filter is not set correctly!");
            }
        }
        return aa;
    }
    public bool OpenFileDialog(out string fileName)
    {
        _extensions = new List<string>();
        using var op = new CommonOpenFileDialog()
        {
            Title = Title,
            DefaultExtension = DefaultExtension,
        };
        var filters = Convert(_filter, _extensions);
        foreach (var item in filters)
        {
            op.Filters.Add(item);
        }
        if (op.ShowDialog() != CommonFileDialogResult.Ok)
        {
            fileName = null;
            return false;
        }
        if (Path.HasExtension(op.FileName))
        {
            fileName = op.FileName;
        }
        else
        {
            fileName = op.FileName + "." + _extensions[op.SelectedFileTypeIndex - 1];
        }
        return true;
    }
    public bool OpenManyFileDialog(out IList<string> fileNames)
    {
        _extensions = new List<string>();
        using var op = new CommonOpenFileDialog()
        {
            Title = Title,
            DefaultExtension = DefaultExtension,
            Multiselect = true,
        };
        var filters = Convert(_filter, _extensions);
        foreach (var item in filters)
        {
            op.Filters.Add(item);
        }
        if (op.ShowDialog() != CommonFileDialogResult.Ok)
        {
            fileNames = null;
            return false;
        }
        if (op.FileNames.All(Path.HasExtension))
        {
            fileNames = op.FileNames.ToArray();
        }
        else
        {
            var n = op.FileNames.Count();
            fileNames = new List<string>(n);
            foreach (var item in op.FileNames)
            {
                fileNames.Add(item + "." + _extensions[op.SelectedFileTypeIndex - 1]);
            }
        }
        return true;
    }

    public bool OpenManyFolderDialog(out IList<string> fileNames)
    {
        _extensions = new List<string>();
        using var op = new CommonOpenFileDialog()
        {
            Title = Title,
            Multiselect = true,
            IsFolderPicker = true,
        };


        if (op.ShowDialog() != CommonFileDialogResult.Ok)
        {
            fileNames = null;
            return false;
        }
        fileNames = op.FileNames.ToArray();

        return true;
    }

    public bool SaveFileDialog(out string fileName)
    {
        _extensions = new List<string>();
        using var sp = new CommonSaveFileDialog()
        {
            Title = Title,
            DefaultExtension = DefaultExtension,
        };
        var filters = Convert(_filter, _extensions);
        foreach (var item in filters)
        {
            sp.Filters.Add(item);
        }
        if (sp.ShowDialog() != CommonFileDialogResult.Ok)
        {
            fileName = null;
            return false;
        }
        if (Path.HasExtension(sp.FileName))
        {
            fileName = sp.FileName;
        }
        else
        {
            fileName = sp.FileName + "." + _extensions[sp.SelectedFileTypeIndex - 1];
        }
        return true;
    }
    public bool OpenFolderDialog(out string fileName)
    {
        using var op = new CommonOpenFileDialog()
        {
            Title = Title,
            IsFolderPicker = true,
        };
        //var filters=Convert(_filter);
        //foreach (var item in filters)
        //{
        //    op.Filters.Add(item);
        //}
        if (op.ShowDialog() != CommonFileDialogResult.Ok)
        {
            fileName = null;
            return false;
        }
        else
        {
            fileName = op.FileName;
            return true;
        }
    }
}