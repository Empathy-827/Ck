using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//[StructLayout(LayoutKind.Sequential)]
public class OpenFunc
{
    public static string Open(string diaTitle)
    {
        OpenDialogDir d = new OpenDialogDir();
        d.lpszTitle = diaTitle;
        IntPtr i = DllOpenFileDialog.SHBrowseForFolder(d);
        char[] c = new char[256];
        DllOpenFileDialog.SHGetPathFromIDList(i, c);
        string finalPath = new string(c);
        finalPath = finalPath.Substring(0, finalPath.IndexOf('\0'));
        return finalPath;
    }

    public static string OpenFile(string diaTitle , params string[] ext)
    {
        OpenDialogFile i = new OpenDialogFile(ext);
        i.title = diaTitle;
        DllOpenFileDialog.GetOpenFileName(i);
        return i.file;
    }
}

public class OpenDialogFile
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
    public OpenDialogFile(params string[] ext)
        {
            structSize = Marshal.SizeOf(this);
            defExt = ext[0];
            string n = null;
            string e = null;
            foreach (string _e in ext)
            {
                if (_e == "*")
                {
                    n += "All Files";
                    e += "*.*;";
                }
                else
                {
                    string _n = "." + _e + ";";
                    n += _n;
                    e += "*" + _n;
                }
            }
            n = n.Substring(0, n.Length - 1);
            filter = n + "\0" + e + "\0";
            file = new string(new char[256]);
            maxFile = file.Length;
            fileTitle = new string(new char[64]);
            maxFileTitle = fileTitle.Length;
            //flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            initialDir = Application.dataPath;
        }
}
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenDialogDir
{
    public IntPtr hwndOwner = IntPtr.Zero;
    public IntPtr pidlRoot = IntPtr.Zero;
    public String pszDisplayName = null;
    public String lpszTitle = null;
    public UInt32 ulFlags = 0;
    public IntPtr lpfn = IntPtr.Zero;
    public IntPtr lParam = IntPtr.Zero;
    public int iImage = 0;
    public OpenDialogDir()
    {
        pszDisplayName = new string(new char[256]);
        ulFlags = 0x00000040 | 0x00000010; //BIF_NEWDIALOGSTYLE | BIF_EDITBOX;
        lpszTitle = "打开目录";
    }
}
public class DllOpenFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In,Out] OpenDialogFile ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In,Out] OpenDialogFile ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern IntPtr SHBrowseForFolder([In,Out] OpenDialogDir ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In,Out] char[] fileName);
}