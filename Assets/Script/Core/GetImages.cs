using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GetImages
{
    public static void GetFilesAllImage(List<Texture2D> images , string num , string path_)
    {
        List<string> filePaths = new List<string>();
        List<string> filePathsTGA = new List<string>();

        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG|*.TGA";
        string[] ImageType = imgtype.Split('|');

        for (int i = 0; i < ImageType.Length; i++)
        {
            if(ImageType[i] != "*.TGA")
            {
                if(Directory.Exists(path_) == true)
                {
                    string[] dirs = Directory.GetFiles((path_), ImageType[i]);
                    for (int j = 0; j < dirs.Length; j++)
                    {
                        filePaths.Add(dirs[j]);
                    }
                }
            }
            else
            {
                if(Directory.Exists(path_) == true)
                {
                    string[] dirsTGA = Directory.GetFiles((path_), ImageType[i]);
                    for (int k = 0; k < dirsTGA.Length; k++)
                    {
                        filePathsTGA.Add(dirsTGA[k]);
                    }
                }
            } 
            
        }
        
        if(filePaths.Count > 0)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                string forceNum = filePaths[i].Substring(filePaths[i].Length - 8).Remove(2);
                if(forceNum == num)
                {
                    Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                    FileInfo fileInfo = new FileInfo(filePaths[i]);
                    fileInfo.IsReadOnly = false;
                    tx.LoadImage(GetImageByte(filePaths[i]));
                    tx.Apply();
                    tx.filterMode = FilterMode.Point;
                    tx.wrapMode = TextureWrapMode.Clamp;
                    //转化成Texture2D添加到列表使用
                    images.Add(tx);
                }

            }
        }
        if(filePathsTGA.Count > 0)
        {
            for (int i = 0; i < filePathsTGA.Count; i++)
            {
                string forceNum = filePathsTGA[i].Substring(filePathsTGA[i].Length - 8).Remove(2);
                if(forceNum == num)
                {
                    Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                    FileInfo fileInfo = new FileInfo(filePathsTGA[i]);
                    fileInfo.IsReadOnly = false;
                    tx = TGALoader.LoadTGA(filePathsTGA[i]);
                    tx.filterMode = FilterMode.Point;
                    tx.wrapMode = TextureWrapMode.Clamp;
                    images.Add(tx);
                }
            }
        }      
    }

    
    //获取路径文件下所有指定方向图片

    public static void GetFilesAllImageNoForce(List<Texture2D> images , string path_)
    {
        List<string> filePaths = new List<string>();
        List<string> filePathsTGA = new List<string>();

        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG|*.TGA";
        string[] ImageType = imgtype.Split('|');

        for (int i = 0; i < ImageType.Length; i++)
        {
           //获取Application.dataPath文件夹下所有的图片路径
            if(ImageType[i] != "*.TGA")
            {
                if(Directory.Exists(path_) == true)
                {
                    string[] dirs = Directory.GetFiles((path_), ImageType[i]);

                    for (int j = 0; j < dirs.Length; j++)
                    {
                        filePaths.Add(dirs[j]);
                    }
                }
                
            }
            else
            {
                if(Directory.Exists(path_) == true)
                {
                    string[] dirsTGA = Directory.GetFiles((path_), ImageType[i]);
                    for (int k = 0; k < dirsTGA.Length; k++)
                    {
                        filePathsTGA.Add(dirsTGA[k]);
                    }
                }
                
            }
        }
        
        if(filePaths.Count > 0)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                FileInfo fileInfo = new FileInfo(filePaths[i]);
                fileInfo.IsReadOnly = false;
                tx.LoadImage(GetImageByte(filePaths[i]));
                tx.Apply();
                tx.filterMode = FilterMode.Point;
                tx.wrapMode = TextureWrapMode.Clamp;
                //转化成Texture2D添加到列表使用
                images.Add(tx);
            }
        }
        if(filePathsTGA.Count > 0)
        {
            for(int i = 0; i < filePathsTGA.Count; i++)
            {
                Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                FileInfo fileInfo = new FileInfo(filePathsTGA[i]);
                fileInfo.IsReadOnly = false;
                tx = TGALoader.LoadTGA(filePathsTGA[i]);
                tx.filterMode = FilterMode.Point;
                tx.wrapMode = TextureWrapMode.Clamp;
                images.Add(tx);
            }
        }
        
    }
    //获取路径文件下所有图片

    public static void GetFilesAllImageReSampling(List<Texture2D> images , string path_)
    {
        List<string> filePaths = new List<string>();
        List<string> filePathsTGA = new List<string>();

        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG|*.TGA";
        string[] ImageType = imgtype.Split('|');

        for (int i = 0; i < ImageType.Length; i++)
        {
           //获取Application.dataPath文件夹下所有的图片路径
            if(ImageType[i] != "*.TGA")
            {
                string[] dirs = Directory.GetFiles((path_), ImageType[i]);

                for (int j = 0; j < dirs.Length; j++)
                {
                    filePaths.Add(dirs[j]);
                }
            }
            else
            {
                string[] dirsTGA = Directory.GetFiles((path_), ImageType[i]);
                for (int k = 0; k < dirsTGA.Length; k++)
                {
                    filePathsTGA.Add(dirsTGA[k]);
                }
            }
        }
        
        if(filePaths.Count > 0)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                FileInfo fileInfo = new FileInfo(filePaths[i]);
                fileInfo.IsReadOnly = false;
                tx.LoadImage(GetImageByte(filePaths[i]));
                tx.Apply();
                // tx.filterMode = FilterMode.Point;
                // tx.wrapMode = TextureWrapMode.Clamp;
                //转化成Texture2D添加到列表使用
                images.Add(tx);
            }
        }
        if(filePathsTGA.Count > 0)
        {
            for(int i = 0; i < filePathsTGA.Count; i++)
            {
                Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
                FileInfo fileInfo = new FileInfo(filePathsTGA[i]);
                fileInfo.IsReadOnly = false;
                tx = TGALoader.LoadTGA(filePathsTGA[i]);
                // tx.filterMode = FilterMode.Point;
                // tx.wrapMode = TextureWrapMode.Clamp;
                images.Add(tx);
            }
        }
        
    }
    //获取路径文件下所有图片(重设分辨率)

    public static byte[] GetImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }
    //将IMG转为Byte
    private Sprite ChangeToSprite(Texture2D tex)
    {
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
    //将tex转为sprite
}
