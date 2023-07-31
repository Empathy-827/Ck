using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GetFiles
{
    public static List<string> GetAllFiles(string mainFolderPath)
    {
        List<String> folderPath = new List<String>();
        if(Directory.Exists(mainFolderPath) == false)
        {
            return new List<string>();
        }
        DirectoryInfo direction = new DirectoryInfo(mainFolderPath);
        DirectoryInfo[] allDirs = direction.GetDirectories("*");
        for(int i = 0; i < allDirs.Length; i++)
        {
            folderPath.Add(allDirs[i].Name);
        }
        return folderPath;
    }
    //获取路径下所有文件夹
}
