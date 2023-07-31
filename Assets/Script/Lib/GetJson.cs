using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GetJsonFunc
{
    /**
        获取Json文件
    */
    public static Data_Class GetJson(string path)
    {
        string readData = "";
        StreamReader str = File.OpenText(path);
        readData = str.ReadToEnd();
        str.Close();
        Data_Class dataClass = JsonUtility.FromJson<Data_Class>(readData);
        return dataClass;
    }
}

[System.Serializable]
public class Data_Class
{
    public string version;
    public string name;
    public string width;
    public string height;
    public string kx;
    public string ky;
    public string frames;
}
