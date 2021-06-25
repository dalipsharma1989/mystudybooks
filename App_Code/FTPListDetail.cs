using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FTPListDetail
/// </summary>
public class FTPListDetail
{
    public bool IsDirectory
    {
        get
        {
            return !string.IsNullOrWhiteSpace(Dir) && Dir.ToLower().Equals("d");
        }
    }
    public string Dir { get; set; }
    public string Permission { get; set; }
    public string Filecode { get; set; }
    public string Owner { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }
    public string FullPath { get; set; }
}