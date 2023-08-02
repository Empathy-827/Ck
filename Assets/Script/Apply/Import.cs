using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Import
{
    /**
        打开重叠对比文件夹
    */
    public static void OpenOverlapFolder(Material matOverlap, Material matContrast)
    {
        if(Variables.bOverlapNPC == true)
        {
            OpenNPCOverlapFolder(matOverlap,matContrast);
        }       
    }

    /**
        打开NPC重叠对比文件夹
    */
    public static void OpenNPCOverlapFolder(Material matOverlap,Material matContrast)
    {
        Variables.NPCOverlapPath = OpenFunc.Open("请选择需要重叠对比的NPC文件夹") + "/" + Variables.animName;
        Variables.NPCOverlapImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.NPCOverlapImages , Variables.force , Variables.NPCOverlapPath);
        if(Variables.NPCOverlapImages.Count != 0)
        {
            Variables.cutValueNPCOverlap = new Vector4((Variables.standardPixel.x - Variables.NPCOverlapImages[0].width) / 2 / Variables.standardPixel.x , (Variables.standardPixel.y - Variables.NPCOverlapImages[0].height) /2 / Variables.standardPixel.y , 0 , 0);
            matOverlap.SetVector("_CutValueNPC",Variables.cutValueNPCOverlap);
            matContrast.SetVector("_CutValueOverlapNPC",Variables.cutValueNPCOverlap);
        }
        if(Variables.NPCOverlapImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"该对比资产没有" + Variables.animName + "动作" , "确认" , 0);
        }
        GetImages.GetFilesAllImageNoForce(Variables.NPCOverlapImagesNoForce,Variables.NPCOverlapPath);
        Variables.overlapCount = Variables.NPCOverlapImagesNoForce.Count;
        Variables.overlapForceCount = Variables.NPCOverlapImages.Count;
        
        if(Variables.NPCOverlapImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认重叠对比NPC文件夹路径是否正确","确认",0);
            OpenNPCOverlapFolder(matOverlap, matContrast);
        }
    }

    

    /**
        打开角色对比文件夹
    */
    public static void OpenCharacterFolder(Material matCharacter)
    {
        if(Variables.bCharacterNPC == true)
        {
            OpenNPCCharacterFolder(matCharacter);
        }
        if(Variables.bCharacterNPC == false)
        {
            OpenBodyCharacterFolder(matCharacter);
            OpenBodyDepthCharacterFolder();
            OpenHeadCharacterFolder();
            OpenHeadDepthCharacterFolder();
            OpenWeaponCharacterFolder();
            OpenWeaponDepthCharacterFolder();
        }
        
    }

    /**
        打开角色对比NPC文件夹
    */
    public static void OpenNPCCharacterFolder(Material matCharacter)
    {
        Variables.NPCCharacterPath = OpenFunc.Open("请选择需要对比的标准NPC文件夹");
        Variables.NPCCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.NPCCharacterImages , Variables.force , Variables.NPCCharacterPath);

        string jsonPath = Variables.NPCCharacterPath.Remove(Variables.NPCCharacterPath.LastIndexOf(@"\")) + "/kpoint.json";
        bool bSafeJson = File.Exists(jsonPath);
        if(bSafeJson == true)
        {
            Data_Class dataClass = GetJsonFunc.GetJson(jsonPath);

            float width = float.Parse(dataClass.width);
            float height = float.Parse(dataClass.height);
            float kx = float.Parse(dataClass.kx);
            float ky = float.Parse(dataClass.ky);

            Variables.scaleNPCCharacter = new Vector4(width / Variables.standardPixel.x , height / Variables.standardPixel.y);
            Variables.cutValueNPCCharacter = new Vector4((Variables.standardPixel.x - width) / 2 / Variables.standardPixel.x , (Variables.standardPixel.y - height) /2 / Variables.standardPixel.y , 0 , 0);
        }
        if(bSafeJson == false)
        {
            Variables.scaleNPCCharacter = new Vector4(1,1,0,0);
            Variables.cutValueNPCCharacter = new Vector4(0,0,0,0);
        }
        
        matCharacter.SetVector("_CutValueNPC",Variables.cutValueNPCCharacter);
        matCharacter.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));
        matCharacter.SetVector("_ScaleNPC",new Vector4(1.0f,1.0f,0,0));

        if(Variables.NPCCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认对比的NPC文件夹路径是否正确","确认",0);
            OpenNPCCharacterFolder(matCharacter);
        }
    }

    /**
        打开角色对比主角身体文件夹
    */
    public static void OpenBodyCharacterFolder(Material matCharacter)
    {
        Variables.bodyCharacterPath = OpenFunc.Open("请选择需要对比的标准角色身体颜色文件夹");
        Variables.bodyCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.bodyCharacterImages , Variables.force , Variables.bodyCharacterPath);

        string jsonPath = Variables.bodyCharacterPath.Remove(Variables.bodyCharacterPath.LastIndexOf(@"\" + Variables.force)) + "/kpoint.json";
        bool bSafeJson = File.Exists(jsonPath);
        if(bSafeJson == true)
        {
            Data_Class dataClass = GetJsonFunc.GetJson(jsonPath);

            float width = float.Parse(dataClass.width);
            float height = float.Parse(dataClass.height);
            float kx = float.Parse(dataClass.kx);
            float ky = float.Parse(dataClass.ky);

            Variables.scaleCharacter = new Vector4(width / Variables.standardPixel.x , height / Variables.standardPixel.y,0,0);
            Variables.cutValueCharacter = new Vector4((Variables.standardPixel.x - width) / 2 / Variables.standardPixel.x , (Variables.standardPixel.y - height) /2 / Variables.standardPixel.y , 0 , 0);
        }
        if(bSafeJson == false)
        {
            Variables.scaleCharacter = new Vector4(1,1,0,0);
            Variables.cutValueCharacter = new Vector4(0,0,0,0);
        }
        
        matCharacter.SetVector("_CutValue",Variables.cutValueCharacter);
        matCharacter.SetVector("_KPointOffset",new Vector4(0,0,0,0));
        matCharacter.SetVector("_Scale",new Vector4(1,1,0,0));

        if(Variables.bodyCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色身体颜色文件夹路径是否正确","确认",0);
            OpenBodyCharacterFolder(matCharacter);
        }
    }

    /**
        打开角色对比主角身体深度文件夹
    */
    public static void OpenBodyDepthCharacterFolder()
    {   
        Variables.bodyDepthCharacterPath = OpenFunc.Open("请选择需要对比的标准角色身体深度文件夹");
        Variables.bodyDepthCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.bodyDepthCharacterImages , Variables.force , Variables.bodyDepthCharacterPath);
        if(Variables.bodyDepthCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色身体深度文件夹路径是否正确","确认",0);
            OpenBodyDepthCharacterFolder();
        }
    }

    /**
        打开角色对比主角头部文件夹
    */
    public static void OpenHeadCharacterFolder()
    {
        Variables.headCharacterPath = OpenFunc.Open("请选择需要对比的标准角色头部颜色文件夹");
        Variables.headCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.headCharacterImages , Variables.force , Variables.headCharacterPath);
        if(Variables.headCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色头部颜色文件夹路径是否正确","确认",0);
            OpenHeadCharacterFolder();
        }
    }

    /**
        打开角色对比主角头部深度文件夹
    */
    public static void OpenHeadDepthCharacterFolder()
    {
        Variables.headDepthCharacterPath = OpenFunc.Open("请选择需要对比的标准角色头部深度文件夹");
        Variables.headDepthCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.headDepthCharacterImages , Variables.force , Variables.headDepthCharacterPath);
        if(Variables.headDepthCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色头部深度文件夹路径是否正确","确认",0);
            OpenHeadDepthCharacterFolder();
        }
    }
    
    /**
        打开角色对比主角武器文件夹
    */
    public static void OpenWeaponCharacterFolder()
    {
        Variables.weaponCharacterPath = OpenFunc.Open("请选择需要对比的标准角色武器颜色文件夹") + "/" + Variables.animName;
        Variables.weaponCharacterImages.Clear();
        Resources.UnloadUnusedAssets();

        GetImages.GetFilesAllImage(Variables.weaponCharacterImages , Variables.force , Variables.weaponCharacterPath);
        if(Variables.weaponCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色武器颜色文件夹路径是否正确","确认",0);
            OpenWeaponCharacterFolder();
        }
    }

    /**
        打开角色对比主角武器深度文件夹
    */
    public static void OpenWeaponDepthCharacterFolder()
    {
        Variables.weaponDepthCharacterPath = OpenFunc.Open("请选择需要对比的标准角色武器深度文件夹");
        Variables.weaponDepthCharacterImages.Clear();
        Resources.UnloadUnusedAssets();
        GetImages.GetFilesAllImage(Variables.weaponDepthCharacterImages , Variables.force , Variables.weaponDepthCharacterPath);
        if(Variables.weaponDepthCharacterImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认标准角色武器深度文件夹路径是否正确","确认",0);
            OpenWeaponDepthCharacterFolder();
        }
    }



    /**
        添加主角输出资产部位
    */
    /*public static void AddBody(string bodyPath, string bodyDepthPath)
    {
        float count = Variables.count;
        int index = Variables.index;
        bool validBody = Variables.validBody;
        bool validBodyDepth = Variables.validBodyDepth;
        List<Texture2D> bodyImages = Variables.bodyImages;
        List<Texture2D> bodyDepthImages = Variables.bodyDepthImages;
        string force = Variables.force;

        Variables.validBody = AddPart(bodyPath, bodyImages, count, index, force);
        Variables.bodyImages = bodyImages;
        Variables.count = count;
        Variables.index = index;

        Variables.exportForceCount = bodyImages.Count;
        List<Texture2D> bodyImagesNoForce = new List<Texture2D>();
        GetImages.GetFilesAllImageNoForce(bodyImagesNoForce, bodyPath);
        Variables.bodyImagesNoForce = bodyImagesNoForce;
        Variables.exportCount = bodyImagesNoForce.Count;

        Variables.validBodyDepth = AddPart(bodyDepthPath, bodyDepthImages, count, index, force);
        Variables.bodyDepthImages = bodyDepthImages;
        Variables.count = count;
        Variables.index = index;

        if(bodyImages.Count != 0 || bodyDepthImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValue = new Vector4((standardPixel.x - bodyImages[0].width) / 2 / standardPixel.x , (standardPixel.y - bodyImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValue = cutValue;
            Control.mat.SetVector("_CutValue",cutValue);
            Control.matContrast.SetVector("_CutValue",cutValue);
        }  
    }

    public static void AddHead(string headPath, string headDepthPath)
    {
        float count = Variables.count;
        int index = Variables.index;
        bool validHead = Variables.validHead;
        bool validHeadDepth = Variables.validHeadDepth;
        List<Texture2D> headImages = Variables.headImages;
        List<Texture2D> headDepthImages = Variables.headDepthImages;
        string force = Variables.force;

        Variables.validHead = AddPart(headPath, headImages, count, index, force);
        Variables.headImages = headImages;
        Variables.count = count;
        Variables.index = index;

        Variables.validHeadDepth = AddPart(headDepthPath, headDepthImages, count, index, force);
        Variables.headDepthImages = headDepthImages;
        Variables.count = count;
        Variables.index = index;
    }

    public static void AddWeapon(string weaponPath, string weaponDepthPath)
    {
        float count = Variables.count;
        int index = Variables.index;
        bool validWeapon = Variables.validWeapon;
        bool validWeaponDepth = Variables.validWeaponDepth;
        List<Texture2D> weaponImages = Variables.weaponImages;
        List<Texture2D> weaponDepthImages = Variables.weaponDepthImages;
        string force = Variables.force;

        Variables.validWeapon = AddPart(weaponPath, weaponImages, count, index, force);
        Variables.weaponImages = weaponImages;
        Variables.count = count;
        Variables.index = index;

        Variables.validWeaponDepth = AddPart(weaponDepthPath, weaponDepthImages, count, index, force);
        Variables.weaponDepthImages = weaponDepthImages;
        Variables.count = count;
        Variables.index = index; 
    }

    public static void AddWeaponEffect(string weaponEffectPath)
    {
        float count = Variables.count;
        int index = Variables.index;
        List<Texture2D> weaponEffectImages = Variables.weaponEffectImages;
        string force = Variables.force;
        Debug.Log(Variables.validWeaponEffect);
        Variables.validWeaponEffect = AddPart(weaponEffectPath, weaponEffectImages, count, index, force);
        Debug.Log(weaponEffectImages.Count);
        Debug.Log(Variables.validWeaponEffect);

        Variables.weaponEffectImages = weaponEffectImages;
        Variables.count = count;
        Variables.index = index;
    }

    public static void AddGem(string gemPath)
    {
        float count = Variables.count;
        int index = Variables.index;
        bool validGem = Variables.validGem;
        List<Texture2D> gemImages = Variables.gemImages;
        string force = Variables.force;

        Variables.validGem = AddPart(gemPath, gemImages, count, index, force);
        Variables.gemImages = gemImages;
        Variables.count = count;
        Variables.index = index;
    }

    /**
        添加主角重叠对比资产部位
    */
    public static void AddBodyOverlap(string bodyOverlapPath, string bodyDepthOverlapPath)
    {
        float countOverlap = Variables.countOverlap;
        int indexOverlap = Variables.indexOverlap;
        bool validBodyOverlap = Variables.validBodyOverlap;
        bool validBodyDepthOverlap = Variables.validBodyDepthOverlap;
        List<Texture2D> bodyOverlapImages = Variables.bodyOverlapImages;
        List<Texture2D> bodyDepthOverlapImages = Variables.bodyDepthOverlapImages;
        string force = Variables.force;

        Variables.validBodyOverlap = AddPart(bodyOverlapPath, bodyOverlapImages, countOverlap, indexOverlap, force);
        Variables.bodyOverlapImages = bodyOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;

        Variables.overlapForceCount = bodyOverlapImages.Count;
        List<Texture2D> bodyOverlapImagesNoForce = new List<Texture2D>();
        GetImages.GetFilesAllImageNoForce(bodyOverlapImagesNoForce, bodyOverlapPath);
        Variables.bodyOverlapImagesNoForce = bodyOverlapImagesNoForce;
        Variables.overlapCount = bodyOverlapImagesNoForce.Count;

        Variables.validBodyDepthOverlap = AddPart(bodyDepthOverlapPath, bodyDepthOverlapImages, countOverlap, indexOverlap, force);
        Variables.bodyDepthOverlapImages = bodyDepthOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;

        if(bodyOverlapImages.Count != 0 || bodyDepthOverlapImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueOverlap = new Vector4((standardPixel.x - bodyOverlapImages[0].width) / 2 / standardPixel.x , (standardPixel.y - bodyOverlapImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueOverlap = cutValueOverlap;
            Control.matOverlap.SetVector("_CutValueOverlap",cutValueOverlap);
            Control.matContrast.SetVector("_CutValueOverlap",cutValueOverlap);
        }
    }

    public static void AddHeadOverlap(string headOverlapPath, string headDepthOverlapPath)
    {
        float countOverlap = Variables.countOverlap;
        int indexOverlap = Variables.indexOverlap;
        bool validHeadOverlap = Variables.validHeadOverlap;
        bool validHeadDepthOverlap = Variables.validHeadDepthOverlap;
        List<Texture2D> headOverlapImages = Variables.headOverlapImages;
        List<Texture2D> headDepthOverlapImages = Variables.headDepthOverlapImages;
        string force = Variables.force;

        Variables.validHeadOverlap = AddPart(headOverlapPath, headOverlapImages, countOverlap, indexOverlap, force);
        Variables.headOverlapImages = headOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;

        Variables.validHeadDepthOverlap = AddPart(headDepthOverlapPath, headDepthOverlapImages, countOverlap, indexOverlap, force);
        Variables.headDepthOverlapImages = headDepthOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap; 
    }

    public static void AddWeaponOverlap(string weaponOverlapPath, string weaponDepthOverlapPath)
    {
        float countOverlap = Variables.countOverlap;
        int indexOverlap = Variables.indexOverlap;
        bool validWeaponOverlap = Variables.validWeaponOverlap;
        bool validWeaponDepthOverlap = Variables.validWeaponDepthOverlap;
        List<Texture2D> weaponOverlapImages = Variables.weaponOverlapImages;
        List<Texture2D> weaponDepthOverlapImages = Variables.weaponDepthOverlapImages;
        string force = Variables.force;

        Variables.validWeaponOverlap = AddPart(weaponOverlapPath, weaponOverlapImages, countOverlap, indexOverlap, force);
        Variables.weaponOverlapImages = weaponOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;

        Variables.validWeaponDepthOverlap = AddPart(weaponDepthOverlapPath, weaponDepthOverlapImages, countOverlap, indexOverlap, force);
        Variables.weaponDepthOverlapImages = weaponDepthOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap; 
    }

    public static void AddWeaponEffectOverlap(string weaponEffectOverlapPath)
    {
        float countOverlap = Variables.countOverlap;
        int indexOverlap = Variables.indexOverlap;
        bool validWeaponEffectOverlap = Variables.validWeaponEffectOverlap;
        List<Texture2D> weaponEffectOverlapImages = Variables.weaponEffectOverlapImages;
        string force = Variables.force;

        Variables.validWeaponEffectOverlap = AddPart(weaponEffectOverlapPath, weaponEffectOverlapImages, countOverlap, indexOverlap, force);
        Variables.weaponEffectOverlapImages = weaponEffectOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;
    }

    public static void AddGemOverlap(string gemOverlapPath)
    {
        float countOverlap = Variables.countOverlap;
        int indexOverlap = Variables.indexOverlap;
        bool validGemOverlap = Variables.validGemOverlap;
        List<Texture2D> gemOverlapImages = Variables.gemOverlapImages;
        string force = Variables.force;

        Variables.validGemOverlap = AddPart(gemOverlapPath, gemOverlapImages, countOverlap, indexOverlap, force);
        Variables.gemOverlapImages = gemOverlapImages;
        Variables.countOverlap = countOverlap;
        Variables.indexOverlap = indexOverlap;
    }

    /**
        添加主角角色对比资产部位
    */
    public static void AddBodyCharacter(string bodyCharacterPath, string bodyDepthCharacterPath)
    {
        float countCharacter = Variables.countCharacter;
        int indexCharacter = Variables.indexCharacter;
        bool validBodyCharacter = Variables.validBodyCharacter;
        bool validBodyDepthCharacter = Variables.validBodyDepthCharacter;
        List<Texture2D> bodyCharacterImages = Variables.bodyCharacterImages;
        List<Texture2D> bodyDepthCharacterImages = Variables.bodyDepthCharacterImages;
        string force = Variables.force;

        Variables.validBodyCharacter = AddPart(bodyCharacterPath, bodyCharacterImages, countCharacter, indexCharacter, force);
        Variables.bodyCharacterImages = bodyCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;

        Variables.validBodyDepthCharacter = AddPart(bodyDepthCharacterPath, bodyDepthCharacterImages, countCharacter, indexCharacter, force);
        Variables.bodyDepthCharacterImages = bodyDepthCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;

        if(bodyCharacterImages.Count != 0 || bodyDepthCharacterImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueCharacter = new Vector4((standardPixel.x - bodyCharacterImages[0].width) / 2 / standardPixel.x , (standardPixel.y - bodyCharacterImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueCharacter = cutValueCharacter;
            Control.matCharacter.SetVector("_CutValue",cutValueCharacter);
        }
    }

    public static void AddHeadCharacter(string headCharacterPath, string headDepthCharacterPath)
    {
        float countCharacter = Variables.countCharacter;
        int indexCharacter = Variables.indexCharacter;
        bool validHeadCharacter = Variables.validHeadCharacter;
        bool validHeadDepthCharacter = Variables.validHeadDepthCharacter;
        List<Texture2D> headCharacterImages = Variables.headCharacterImages;
        List<Texture2D> headDepthCharacterImages = Variables.headDepthCharacterImages;
        string force = Variables.force;

        Variables.validHeadCharacter = AddPart(headCharacterPath, headCharacterImages, countCharacter, indexCharacter, force);
        Variables.headCharacterImages = headCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;

        Variables.validHeadDepthCharacter = AddPart(headDepthCharacterPath, headDepthCharacterImages, countCharacter, indexCharacter, force);
        Variables.headDepthCharacterImages = headDepthCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;
    }

    public static void AddWeaponCharacter(string weaponCharacterPath, string weaponDepthCharacterPath)
    {
        float countCharacter = Variables.countCharacter;
        int indexCharacter = Variables.indexCharacter;
        bool validWeaponCharacter = Variables.validWeaponCharacter;
        bool validWeaponDepthCharacter = Variables.validWeaponDepthCharacter;
        List<Texture2D> weaponCharacterImages = Variables.weaponCharacterImages;
        List<Texture2D> weaponDepthCharacterImages = Variables.weaponDepthCharacterImages;
        string force = Variables.force;

        Variables.validWeaponCharacter = AddPart(weaponCharacterPath, weaponCharacterImages, countCharacter, indexCharacter, force);
        Variables.weaponCharacterImages = weaponCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;

        Variables.validWeaponDepthCharacter = AddPart(weaponDepthCharacterPath, weaponDepthCharacterImages, countCharacter, indexCharacter, force);
        Variables.weaponDepthCharacterImages = weaponDepthCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;
    }

    public static void AddWeaponEffectCharacter(string weaponEffectCharacterPath)
    {
        float countCharacter = Variables.countCharacter;
        int indexCharacter = Variables.indexCharacter;
        bool validWeaponEffectCharacter = Variables.validWeaponEffectCharacter;
        List<Texture2D> weaponEffectCharacterImages = Variables.weaponEffectCharacterImages;
        string force = Variables.force;

        Variables.validWeaponEffectCharacter = AddPart(weaponEffectCharacterPath, weaponEffectCharacterImages, countCharacter, indexCharacter, force);
        Variables.weaponEffectCharacterImages = weaponEffectCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;
    }

    public static void AddGemCharacter(string gemCharacterPath)
    {
        float countCharacter = Variables.countCharacter;
        int indexCharacter = Variables.indexCharacter;
        bool validGemCharacter = Variables.validGemCharacter;
        List<Texture2D> gemCharacterImages = Variables.gemCharacterImages;
        string force = Variables.force;

        Variables.validGemCharacter = AddPart(gemCharacterPath, gemCharacterImages, countCharacter, indexCharacter, force);
        Variables.gemCharacterImages = gemCharacterImages;
        Variables.countCharacter = countCharacter;
        Variables.indexCharacter = indexCharacter;
    }

    /**
        添加NPC输出资产部位
    */
    public static void AddNPC(string NPCPath)
    {
        //
        float countNPC = Variables.countNPC;
        int indexNPC = Variables.indexNPC;
        bool validNPC = Variables.validNPC;

        //把文件夹下的图片写入列表
        List<Texture2D> NPCImages= Variables.NPCImages;
        
        string force = Variables.force;
        Variables.validNPC = AddPart(NPCPath, NPCImages, countNPC, indexNPC, force);
        Variables.NPCImages = NPCImages;
        Variables.countNPC = countNPC;
        Variables.indexNPC = indexNPC;
        Variables.exportForceCount = NPCImages.Count;

        //获得文件夹下所有tga，写入列表
        List<Texture2D> NPCImagesNoForce = new List<Texture2D>();
        GetImages.GetFilesAllImageNoForce(NPCImagesNoForce, NPCPath);
        Variables.NPCImagesNoForce = NPCImagesNoForce;
        Variables.exportCount = NPCImagesNoForce.Count;

        Debug.Log(NPCPath);
        //Debug.Log(NPCImagesNoForce.Count);

        //7.25
        Variables.NPCImagesForce1.Clear();
        Variables.NPCImagesForce2.Clear();
        Variables.NPCImagesForce3.Clear();
        //添加不同方向
        for (int i = 0; i < NPCImagesNoForce.Count; i++)
            {
                Texture2D image = NPCImagesNoForce[i];
                string imageName = image.name;
                if (imageName.StartsWith("10"))
                {
                    Variables.NPCImagesForce1.Add(image);
                }
                if (imageName.StartsWith("20"))
                {
                    Variables.NPCImagesForce2.Add(image);
                }
                if (imageName.StartsWith("30"))
                {
                    Variables.NPCImagesForce3.Add(image);
                }                
            }

        if(NPCImages.Count != 0)
        {
            
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPCImages[0].width) / 2 / standardPixel.x , (standardPixel.y - NPCImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC",cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC",cutValueNPC);
        }

    }

    public static void AddNPCAll(string NPCPath, List<Texture2D> NPCImages)
    {
        float countNPC = Variables.countNPC;
        int indexNPC = Variables.indexNPC;
        bool validNPC = Variables.validNPC;

        string force = Variables.force;

        Variables.validNPC = AddPart(NPCPath, NPCImages, countNPC, indexNPC, force);
        Variables.countNPC = countNPC;
        Variables.indexNPC = indexNPC;

        Debug.Log(NPCPath);

        if (NPCImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPCImages[0].width) / standardPixel.x, (standardPixel.y - NPCImages[0].height) / 2 / standardPixel.y, 0, 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC", cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC", cutValueNPC);
        }
    }

    public static void AddNPCAddon(string NPCPath)
    {
        float countNPCAddon = Variables.countNPCAddon;
        int indexNPCAddon = Variables.indexNPCAddon;
        bool validNPCAddon = Variables.validNPCAddon;
        List<Texture2D> NPCAddonImages= Variables.NPCAddonImages;
        string force = Variables.force;


        int index = NPCPath.LastIndexOf("/");
        string firstPart = NPCPath.Substring(0, index);
        string secondPart = NPCPath.Substring(index+1);
        string newPart = "addon\\01\\";

        String NPCAddonPath = firstPart + "/" + newPart + secondPart;

        //Debug.Log(NPCAddonPath);

        Variables.validNPCAddon = AddPart(NPCAddonPath, NPCAddonImages, countNPCAddon, indexNPCAddon, force);
        Variables.NPCAddonImages = NPCAddonImages;
        Variables.countNPCAddon = countNPCAddon;
        Variables.indexNPCAddon = indexNPCAddon;

        Variables.exportForceCount = NPCAddonImages.Count;
        List<Texture2D> NPCAddonImagesNoForce = new List<Texture2D>();
        GetImages.GetFilesAllImageNoForce(NPCAddonImagesNoForce, NPCAddonPath);
        Variables.NPCAddonImagesNoForce = NPCAddonImagesNoForce;
        Variables.exportCount = NPCAddonImagesNoForce.Count;

        Variables.NPCAddonImagesForce1.Clear();
        Variables.NPCAddonImagesForce2.Clear();
        Variables.NPCAddonImagesForce3.Clear();
        //添加不同方向
        for (int i = 0; i < NPCAddonImagesNoForce.Count; i++)
            {
                Texture2D image = NPCAddonImagesNoForce[i];
                string imageName = image.name;

                if (imageName.StartsWith("10"))
                {
                    Variables.NPCAddonImagesForce1.Add(image);
                    Debug.Log(imageName);
                    Debug.Log(Variables.NPCAddonImagesForce1.Count);
                }
                if (imageName.StartsWith("20"))
                {
                    Variables.NPCAddonImagesForce2.Add(image);
                }
                if (imageName.StartsWith("30"))
                {
                    Variables.NPCAddonImagesForce3.Add(image);
                }                
            }

        if(NPCAddonImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPCAddonImages[0].width) / 2 / standardPixel.x , (standardPixel.y - NPCAddonImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC",cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC",cutValueNPC);
        }
    }

    public static void AddNPCAddonAll(string NPCPath, List<Texture2D> NPCAddonImages)
    {
        float countNPCAddon = Variables.countNPCAddon;
        int indexNPCAddon = Variables.indexNPCAddon;
        bool validNPCAddon = Variables.validNPCAddon;

        string force = Variables.force;

        int index = NPCPath.LastIndexOf("/");
        string firstPart = NPCPath.Substring(0, index);
        string secondPart = NPCPath.Substring(index + 1);
        string newPart = "addon\\01\\";

        String NPCAddonPath = firstPart + "/" + newPart + secondPart;

        Variables.validNPCAddon = AddPart(NPCAddonPath, NPCAddonImages, countNPCAddon, indexNPCAddon, force);
        //Variables.NPCAddonImages = NPCAddonImages;
        Variables.countNPCAddon = countNPCAddon;
        Variables.indexNPCAddon = indexNPCAddon;

        Debug.Log(NPCPath);

        if(NPCAddonImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPCAddonImages[0].width) / 2 / standardPixel.x , (standardPixel.y - NPCAddonImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC",cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC",cutValueNPC);
        }
    }

    public static void AddNPC00(string NPCPath)
    {
        float countNPC00 = Variables.countNPC00;
        int indexNPC00 = Variables.indexNPC00;
        bool validNPC00 = Variables.validNPC00;
        List<Texture2D> NPC00Images= Variables.NPC00Images;
        string force = Variables.force;


        int index = NPCPath.LastIndexOf("/");
        string firstPart = NPCPath.Substring(0, index);
        string secondPart = NPCPath.Substring(index+1);
        string newPart = "00\\";

        String NPC00Path = firstPart + "/" + newPart + secondPart;

        Variables.validNPC00 = AddPart(NPC00Path, NPC00Images, countNPC00, indexNPC00, force);
        Variables.NPC00Images = NPC00Images;
        Variables.countNPC00 = countNPC00;
        Variables.indexNPC00 = indexNPC00;

        Variables.exportForceCount = NPC00Images.Count;
        List<Texture2D> NPC00ImagesNoForce = new List<Texture2D>();
        GetImages.GetFilesAllImageNoForce(NPC00ImagesNoForce, NPC00Path);
        Variables.NPC00ImagesNoForce = NPC00ImagesNoForce;
        Variables.exportCount = NPC00ImagesNoForce.Count;


        Variables.NPC00ImagesForce1.Clear();
        Variables.NPC00ImagesForce2.Clear();
        Variables.NPC00ImagesForce3.Clear();
        //添加不同方向
        for (int i = 0; i < NPC00ImagesNoForce.Count; i++)
            {
                Texture2D image = NPC00ImagesNoForce[i];
                string imageName = image.name;

                if (imageName.StartsWith("10"))
                {
                    Variables.NPC00ImagesForce1.Add(image);
        //            Debug.Log(imageName);
        //            Debug.Log(Variables.NPCAddonImagesForce1.Count);
                }
                if (imageName.StartsWith("20"))
                {
                    Variables.NPC00ImagesForce2.Add(image);
                }
                if (imageName.StartsWith("30"))
                {
                    Variables.NPC00ImagesForce3.Add(image);
                }                
            }

        if(NPC00Images.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPC00Images[0].width) / 2 / standardPixel.x , (standardPixel.y - NPC00Images[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC",cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC",cutValueNPC);
        }
    }

    public static void AddNPC00All(string NPCPath , List<Texture2D> NPC00Images)
    {
        float countNPC00 = Variables.countNPC00;
        int indexNPC00 = Variables.indexNPC00;
        bool validNPC00 = Variables.validNPC00;
        string force = Variables.force;


        int index = NPCPath.LastIndexOf("/");
        string firstPart = NPCPath.Substring(0, index);
        string secondPart = NPCPath.Substring(index +1);
        string newPart = "00\\";

        String NPC00Path = firstPart + "/" + newPart + secondPart;

        Variables.validNPC00 = AddPart(NPC00Path, NPC00Images, countNPC00, indexNPC00, force);
        Variables.countNPC00 = countNPC00;
        Variables.indexNPC00 = indexNPC00;

        if(NPC00Images.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPC = new Vector4((standardPixel.x - NPC00Images[0].width) / 2 / standardPixel.x , (standardPixel.y - NPC00Images[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPC = cutValueNPC;
            Control.mat.SetVector("_CutValueNPC",cutValueNPC);
            Control.matContrast.SetVector("_CutValueNPC",cutValueNPC);
        }
    }
    

    

    /**
        添加NPC重叠对比资产部位
    */
    public static void AddNPCOverlap(string NPCOverlapPath)
    {
        float countNPCOverlap = Variables.countNPCOverlap;
        int indexNPCOverlap = Variables.indexNPCOverlap;
        bool validNPCOverlap = Variables.validNPCOverlap;
        List<Texture2D> NPCOverlapImages = Variables.NPCOverlapImages;
        string force = Variables.force;

        Variables.validNPCOverlap = AddPart(NPCOverlapPath, NPCOverlapImages, countNPCOverlap, indexNPCOverlap, force);
        Variables.NPCOverlapImages = NPCOverlapImages;
        Variables.countNPCOverlap = countNPCOverlap;
        Variables.indexNPCOverlap = indexNPCOverlap;

        Variables.overlapForceCount = NPCOverlapImages.Count;
        List<Texture2D> NPCOverlapImagesNoForce = null;
        GetImages.GetFilesAllImageNoForce(NPCOverlapImagesNoForce, NPCOverlapPath);
        Variables.NPCOverlapImagesNoForce = NPCOverlapImagesNoForce;
        Variables.overlapCount = NPCOverlapImagesNoForce.Count;

        if(NPCOverlapImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPCOverlap = new Vector4((standardPixel.x - NPCOverlapImages[0].width) / 2 / standardPixel.x , (standardPixel.y - NPCOverlapImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPCOverlap = cutValueNPCOverlap;
            Control.matOverlap.SetVector("_CutValueNPCOverlap",cutValueNPCOverlap);
            Control.matContrast.SetVector("_CutValueNPCOverlap",cutValueNPCOverlap);
        }
    }

    /**
        添加NPC角色对比资产部位
    */
    public static void AddNPCCharacter(string NPCCharacterPath)
    {
        float countNPCCharacter = Variables.countNPCCharacter;
        int indexNPCCharacter = Variables.indexNPCCharacter;
        List<Texture2D> NPCCharacterImages= Variables.NPCCharacterImages;
        string force = Variables.force;

        Variables.validNPCCharacter = AddPart(NPCCharacterPath, NPCCharacterImages, countNPCCharacter, indexNPCCharacter, force);
        Variables.NPCCharacterImages = NPCCharacterImages;
        Variables.countNPCCharacter = countNPCCharacter;
        Variables.indexNPCCharacter = indexNPCCharacter;

        if(NPCCharacterImages.Count != 0)
        {
            Vector4 standardPixel = Variables.standardPixel;
            Vector4 cutValueNPCCharacter = new Vector4((standardPixel.x - NPCCharacterImages[0].width) / 2 / standardPixel.x , (standardPixel.y - NPCCharacterImages[0].height) /2 / standardPixel.y , 0 , 0);
            Variables.cutValueNPCCharacter = cutValueNPCCharacter;
            Control.matCharacter.SetVector("_CutValueNPCCharacter",cutValueNPCCharacter);
        }
    }

    /**
        添加部位方法
    */
    public static bool AddPart(string path, List<Texture2D> images, float countVar, int indexVar, string force)
    {
        bool validVar = false;

        //
        if(images.Count != 0)
        {
            countVar = 1;
            indexVar = 0;
            images.Clear();
            Resources.UnloadUnusedAssets();
        }

        //初始化列表
        try
        {
            GetImages.GetFilesAllImage(images , force , path);
            if(images.Count != 0)
            {
                validVar = true;
            }
            
        }
        catch (Exception)
        {
            int secondLastSlashIndex = path.LastIndexOf('/', path.LastIndexOf('/') - 1);
            int thirdLastSlashIndex = path.LastIndexOf('/', secondLastSlashIndex - 1);
            string partName = path.Substring(thirdLastSlashIndex + 1, secondLastSlashIndex - thirdLastSlashIndex - 1);
            Messagebox.MessageBox(IntPtr.Zero,"请确认" + partName + "是否正确","关闭",0);
            validVar = false;
        }

        return(validVar);

    }

}