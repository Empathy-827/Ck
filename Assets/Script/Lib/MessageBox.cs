using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Messagebox
{
    [DllImport("User32.dll",SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr handle, String message, String title, int type);
}


