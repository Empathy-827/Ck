using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Export
{
    private static void SaveImage(FileStream file , Material rendermat)
    {
        RenderTexture rt = RenderTexture.GetTemporary(1000,1000,32,RenderTextureFormat.ARGB32);
        rt.autoGenerateMips = false;
        rt.wrapMode = TextureWrapMode.Clamp;
        rt.filterMode = FilterMode.Point;
        Graphics.Blit(rt,rt,rendermat);
        Texture2D tex2d = new Texture2D(rt.width,rt.height,TextureFormat.RGBA32,false);
        tex2d.filterMode = FilterMode.Point;

        tex2d.ReadPixels(new Rect(0,0,rt.width,rt.height),0,0);
        tex2d.wrapMode = TextureWrapMode.Clamp;
        tex2d.Apply();
        
        
        RenderTexture.ReleaseTemporary(rt);

        // Destroy(rt);
    
        byte[] vs = ImageConversion.EncodeToPNG(tex2d);
        file.Write(vs , 0, vs.Length);
        file.Dispose();
        file.Close();
    }

    /**
        选择要输出的标准资产
    */
    public static void ExportNormalAsset()
    {
        if(Variables.bChoose == false)
        {  
            return; 
        }
        if(Variables.bodyPath == null && Variables.NPCPath == null)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认已选择主角部位或角色文件夹","确认",0);
            return;
        }

        Variables.bChoose = false;

        GameObject.Find("输出标准资产/输出全部").transform.localScale = new Vector3(1,1,1);

        if(Variables.NPCMode == false)
        {
            int btnPos = 0;
            int btnHeight = 30;

            List<String> renderPath = GetFiles.GetAllFiles(Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")));
            Variables.waitRenderName = new List<String>();
            GameObject panel_button = GameObject.Find("输出标准资产/Panel/Image/Panel_Button");
            var rectTransform = panel_button.transform.GetComponent<RectTransform>();
            panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * renderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * renderPath.Count);
            GameObject button_image = GameObject.Find("输出标准资产/Panel/Image");
            button_image.transform.localScale = new Vector3(1,1,1);
            for(int i = 0; i < renderPath.Count; i++)
            {
                GameObject goClone = UnityEngine.Object.Instantiate(Control.go);
                goClone.transform.SetParent(panel_button.transform,true);
                goClone.transform.localScale = new Vector3(1,1,1);
                goClone.transform.localPosition = new Vector3(0,btnPos,0);
                string buttonName = renderPath[i];
                goClone.GetComponentInChildren<Text>().text = buttonName;
                goClone.GetComponent<Button>().onClick.AddListener
                (
                    ()=>
                    {
                        if(Variables.waitRenderName.Contains(buttonName) == false)
                        {
                            GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(1,1,1);
                            Variables.waitRenderName.Add(buttonName);
                            GameObject panel_button2 = GameObject.Find("输出标准资产/Panel_2/Image/Panel_Button");
                            var rectTransform = panel_button2.transform.GetComponent<RectTransform>();
                            panel_button2.transform.localPosition = new Vector3(0,0 - (((btnHeight * Variables.waitRenderName.Count) / 2) - (rectTransform.rect.height / 2)),0);
                            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * Variables.waitRenderName.Count);
                            GameObject button_image2 = GameObject.Find("输出标准资产/Panel_2/Image");
                            button_image2.transform.localScale = new Vector3(1,1,1);
                            GameObject goClone2 = UnityEngine.Object.Instantiate(Control.go);
                            goClone2.transform.SetParent(panel_button2.transform,true);
                            goClone2.transform.localScale = new Vector3(1,1,1);
                            goClone2.transform.localPosition = new Vector3(0,btnPos,0);
                            string buttonName2 = buttonName;
                            goClone2.GetComponentInChildren<Text>().text = buttonName2;
                            goClone2.GetComponent<Button>().onClick.AddListener
                            (
                                ()=>
                                {
                                    UnityEngine.Object.DestroyImmediate(goClone2);
                                    Variables.waitRenderName.Remove(buttonName2);
                                    if(Variables.waitRenderName.Count == 0)
                                    {
                                        button_image2.transform.localScale = new Vector3(0,0,0);
                                        GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(0,0,0);
                                    }
                                }
                            );
                        }
                        
                        #if UNITY_EDITOR
                        AssetDatabase.Refresh();
                        #endif
                    }
                );
                btnPos = btnPos - btnHeight;
            }
        }
        else if(Variables.NPCMode == true)
        {
            int btnPos = 0;
            int btnHeight = 30;

            List<String> renderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            Variables.waitRenderName = new List<String>();
            GameObject panel_button = GameObject.Find("输出标准资产/Panel/Image/Panel_Button");
            var rectTransform = panel_button.transform.GetComponent<RectTransform>();
            panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * renderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * renderPath.Count);
            GameObject button_image = GameObject.Find("输出标准资产/Panel/Image");
            button_image.transform.localScale = new Vector3(1,1,1);
            for(int i = 0; i < renderPath.Count; i++)
            {
                GameObject goClone = UnityEngine.Object.Instantiate(Control.go);
                goClone.transform.SetParent(panel_button.transform,true);
                goClone.transform.localScale = new Vector3(1,1,1);
                goClone.transform.localPosition = new Vector3(0,btnPos,0);
                string buttonName = renderPath[i];
                goClone.GetComponentInChildren<Text>().text = buttonName;
                goClone.GetComponent<Button>().onClick.AddListener
                (
                    ()=>
                    {
                        if(Variables.waitRenderName.Contains(buttonName) == false)
                        {
                            GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(1,1,1);
                            Variables.waitRenderName.Add(buttonName);
                            GameObject panel_button2 = GameObject.Find("输出标准资产/Panel_2/Image/Panel_Button");
                            var rectTransform = panel_button2.transform.GetComponent<RectTransform>();
                            panel_button2.transform.localPosition = new Vector3(0,0 - (((btnHeight * Variables.waitRenderName.Count) / 2) - (rectTransform.rect.height / 2)),0);
                            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * Variables.waitRenderName.Count);
                            GameObject button_image2 = GameObject.Find("输出标准资产/Panel_2/Image");
                            button_image2.transform.localScale = new Vector3(1,1,1);
                            GameObject goClone2 = UnityEngine.Object.Instantiate(Control.go);
                            goClone2.transform.SetParent(panel_button2.transform,true);
                            goClone2.transform.localScale = new Vector3(1,1,1);
                            goClone2.transform.localPosition = new Vector3(0,btnPos,0);
                            string buttonName2 = buttonName;
                            goClone2.GetComponentInChildren<Text>().text = buttonName2;
                            goClone2.GetComponent<Button>().onClick.AddListener
                            (
                                ()=>
                                {
                                    UnityEngine.Object.DestroyImmediate(goClone2);
                                    Variables.waitRenderName.Remove(buttonName2);
                                    if(Variables.waitRenderName.Count == 0)
                                    {
                                        button_image2.transform.localScale = new Vector3(0,0,0);
                                        GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(0,0,0);
                                    }
                                }
                            );
                        }
                        
                        #if UNITY_EDITOR
                        AssetDatabase.Refresh();
                        #endif
                    }
                );
                btnPos = btnPos - btnHeight;
            }
        }
        
    }

    /**
        输出所选动作的标准资产
    */
     public static void ExportSelect()
    {
        Variables.bPlay = false;

        GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/输出全部").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/Panel/Image").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/Panel_2/Image").transform.localScale = new Vector3(0,0,0);
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allButton = new List<GameObject>();
        foreach(GameObject go in all)
        {
            if(go.name == "Button(Clone)")
            {
                allButton.Add(go);
            }
        }
        foreach(GameObject go in allButton)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }

        Control.matExport.SetFloat("_KPointView",0f);
        Control.matExport.SetVector("_BackColor",new Vector4(0,0,0,0));
        Control.matExport.SetVector("_Scale",Variables.scale);
        Control.matExport.SetVector("_CutValue",Variables.cutValue);
        Control.matExport.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
        Control.matExport.SetVector("_ScaleRescale",Variables.scaleRescale);
        Control.matDepth.SetVector("_Scale",Variables.scale);
        Control.matDepth.SetVector("_CutValue",Variables.cutValue);
        Control.matDepth.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
        Control.matDepth.SetVector("_ScaleRescale",Variables.scaleRescale);

        if(Variables.NPCMode == true)
        {
            
            Control.matExportNPC.SetTexture("_Body",Variables.blackTex);
            Control.matExportNPC.SetTexture("_Head",Variables.blackTex);
            Control.matExportNPC.SetTexture("_Weapon",Variables.blackTex);
            Control.matExportNPC.SetVector("_ScaleNPC",Variables.scaleNPC);
            Control.matExportNPC.SetVector("_CutValueNPC",Variables.cutValueNPC);
            Control.matExportNPC.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);
            Control.matExportNPC.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);
            Debug.Log(Variables.scaleNPC + "   " + Variables.cutValueNPC);

            if(Variables.bSwitch == false)
            {
                for(int i = 0; i < Variables.waitRenderName.Count ; i++)
                {
                    if(Variables.waitRenderName[i] == "attack" )
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetAttack);
                    }
                    else if(Variables.waitRenderName[i] == "stand")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetStand);
                    }
                    else if(Variables.waitRenderName[i] == "magic")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetMagic);
                    }
                    else if(Variables.waitRenderName[i] == "hit")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetHit);
                    }
                    else if(Variables.waitRenderName[i] == "die")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetDie);
                    }
                    else if(Variables.waitRenderName[i] == "walk")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetWalk);
                    }

                    GetImages.GetFilesAllImageNoForce(Variables.NPCRenderImages,Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                    string name = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/"));
                    name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                    string savePath = Variables.mainFolderPath + "/Export/export_NPC/" + name + "/" + Variables.waitRenderName[i];
                    Directory.CreateDirectory(savePath);
                    
                    for(int j = 0; j <= Variables.NPCRenderImages.Count-1 ; j++)
                    {
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "00")
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
                        }
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "20" && Variables.bVert == true)
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",-1f);
                        }
                        Control.matExportNPC.SetTexture("_NPC",Variables.NPCRenderImages[j]);
                        FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                        SaveImage(file,Control.matExportNPC);
                    }
                    Variables.NPCRenderImages.Clear();
                    Resources.UnloadUnusedAssets();
                }
            }
            if(Variables.bSwitch == true)
            {

                for(int i = 0; i < Variables.waitRenderName.Count ; i++)
                {
                    if(Variables.waitRenderName[i] == "attack" )
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetAttack);
                    }
                    else if(Variables.waitRenderName[i] == "stand")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetStand);
                    }
                    else if(Variables.waitRenderName[i] == "magic")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetMagic);
                    }
                    else if(Variables.waitRenderName[i] == "hit")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetHit);
                    }
                    else if(Variables.waitRenderName[i] == "die")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetDie);
                    }
                    else if(Variables.waitRenderName[i] == "walk")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetWalk);
                    }

                    GetImages.GetFilesAllImageNoForce(Variables.NPCRenderImages,Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                    string name = Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/"));
                    name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                    string savePath = Variables.mainFolderPath + "/Export/export_NPC/" + name + "/" + Variables.waitRenderName[i];
                    Directory.CreateDirectory(savePath);
                    
                    for(int j = 0; j <= Variables.NPCRenderImages.Count-1 ; j++)
                    {
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "00")
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
                        }
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "20" && Variables.bVert == true)
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",-1f);
                        }
                        Control.matExportNPC.SetTexture("_NPC",Variables.NPCRenderImages[j]);
                        FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                        SaveImage(file,Control.matExportNPC);
                    }
                    Variables.NPCRenderImages.Clear();
                    Resources.UnloadUnusedAssets();
                }
            }
            
            Variables.waitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Control.matExportNPC.SetVector("_BackColor",Variables.pixelColor);
            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }
    }

    /**
        输出全部动作的标准资产
    */
    public static void ExportAll()
    {
        Variables.bPlay = false;
        GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/输出全部").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/Panel/Image").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出标准资产/Panel_2/Image").transform.localScale = new Vector3(0,0,0);

        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allButton = new List<GameObject>();
        foreach(GameObject go in all)
        {
            if(go.name == "Button(Clone)")
            {
                allButton.Add(go);
            }
        }
        foreach(GameObject go in allButton)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }

        Variables.waitRenderName.Clear();
        Resources.UnloadUnusedAssets();
        
        Control.matExport.SetVector("_BackColor",new Vector4(0,0,0,0));
        Control.matExport.SetFloat("_KPointView",0f);

        Control.matExport.SetVector("_Scale",Variables.scale);
        Control.matExport.SetVector("_CutValue",Variables.cutValue);
        Control.matExport.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
        Control.matExport.SetVector("_ScaleRescale",Variables.scaleRescale);
        Control.matDepth.SetVector("_Scale",Variables.scale);
        Control.matDepth.SetVector("_CutValue",Variables.cutValue);
        Control.matDepth.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
        Control.matDepth.SetVector("_ScaleRescale",Variables.scaleRescale);

        if(Variables.NPCMode == true)
        {
            List<String> renderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            for(int k = 0; k < renderPath.Count; k++)
            {
                Variables.waitRenderName.Add(renderPath[k]);
            }
            
            Control.matExportNPC.SetVector("_ScaleNPC",Variables.scaleNPC);
            Control.matExportNPC.SetVector("_CutValueNPC",Variables.cutValueNPC);
            Control.matExportNPC.SetTexture("_Body",Variables.blackTex);
            Control.matExportNPC.SetTexture("_Head",Variables.blackTex);
            Control.matExportNPC.SetTexture("_Weapon",Variables.blackTex);
            Control.matExportNPC.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);
            Control.matExportNPC.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);

            if(Variables.bSwitch == false)
            {
                for(int i = 0; i < Variables.waitRenderName.Count ; i++)
                {
                    if(Variables.waitRenderName[i] == "attack" )
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetAttack);
                    }
                    else if(Variables.waitRenderName[i] == "stand")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetStand);
                    }
                    else if(Variables.waitRenderName[i] == "magic")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetMagic);
                    }
                    else if(Variables.waitRenderName[i] == "hit")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetHit);
                    }
                    else if(Variables.waitRenderName[i] == "die")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetDie);
                    }
                    else if(Variables.waitRenderName[i] == "walk")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetWalk);
                    }

                    GetImages.GetFilesAllImageNoForce(Variables.NPCRenderImages,Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                    string name = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/"));
                    name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                    string savePath = Variables.mainFolderPath + "/Export/export/" + name + "/" + Variables.waitRenderName[i];
                    Directory.CreateDirectory(savePath);
                    
                    for(int j = 0; j <= Variables.NPCRenderImages.Count-1 ; j++)
                    {
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "00")
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
                        }
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "20" && Variables.bVert == true)
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",-1f);
                        }
                        Control.matExportNPC.SetTexture("_NPC",Variables.NPCRenderImages[j]);
                        FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                        SaveImage(file,Control.matExportNPC);
                    }
                    Variables.NPCRenderImages.Clear();
                    Resources.UnloadUnusedAssets();
                }
            }
            if(Variables.bSwitch == true)
            {
                for(int i = 0; i < Variables.waitRenderName.Count ; i++)
                {
                    if(Variables.waitRenderName[i] == "attack" )
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetAttack);
                    }
                    else if(Variables.waitRenderName[i] == "stand")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetStand);
                    }
                    else if(Variables.waitRenderName[i] == "magic")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetMagic);
                    }
                    else if(Variables.waitRenderName[i] == "hit")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetHit);
                    }
                    else if(Variables.waitRenderName[i] == "die")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetDie);
                    }
                    else if(Variables.waitRenderName[i] == "walk")
                    {
                        Control.matExportNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetWalk);
                    }

                    GetImages.GetFilesAllImageNoForce(Variables.NPCRenderImages,Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.waitRenderName[i]);
                    FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                    string name = Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/"));
                    name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                    string savePath = Variables.mainFolderPath + "/Export/export/" + name + "/" + Variables.waitRenderName[i];
                    Directory.CreateDirectory(savePath);
                    
                    for(int j = 0; j <= Variables.NPCRenderImages.Count-1 ; j++)
                    {
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "00")
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
                        }
                        if(NPCFiles[j].Name.Substring(NPCFiles[j].Name.Length - 8,2) == "20" && Variables.bVert == true)
                        {
                            Control.matExportNPC.SetFloat("_KPointOffsetMult",-1f);
                        }
                        Control.matExportNPC.SetTexture("_NPC",Variables.NPCRenderImages[j]);
                        FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                        SaveImage(file,Control.matExportNPC);
                    }
                    Variables.NPCRenderImages.Clear();
                    Resources.UnloadUnusedAssets();
                }
            }
            Variables.waitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Control.matExportNPC.SetVector("_BackColor",Variables.pixelColor);
            Control.matExportNPC.SetFloat("_KPointOffsetMult",1f);
            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }

        
    }
    /**
        选择要输出的GIF资产
    */
    public static void ExportGIFAsset()
    {
        if(Variables.bChoose == false)
        {  
            return; 
        }
        if(Variables.bodyPath == null && Variables.NPCPath == null)
        {
            Messagebox.MessageBox(IntPtr.Zero,"请确认已选择主角部位或角色文件夹","确认",0);
            return;
        }

        Variables.bChoose = false;

        GameObject.Find("输出营销图资产/输出全部").transform.localScale = new Vector3(1,1,1);

        if(Variables.NPCMode == false)
        {
            int btnPos = 0;
            int btnHeight = 30;

            List<String> renderPath = GetFiles.GetAllFiles(Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")));
            Variables.GIFWaitRenderName = new List<String>();
            GameObject panel_button = GameObject.Find("输出营销图资产/Panel/Image/Panel_Button");
            var rectTransform = panel_button.transform.GetComponent<RectTransform>();
            panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * renderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * renderPath.Count);
            GameObject button_image = GameObject.Find("输出营销图资产/Panel/Image");
            button_image.transform.localScale = new Vector3(1,1,1);
            for(int i = 0; i < renderPath.Count; i++)
            {
                GameObject goClone = UnityEngine.Object.Instantiate(Control.go);
                goClone.transform.SetParent(panel_button.transform,true);
                goClone.transform.localScale = new Vector3(1,1,1);
                goClone.transform.localPosition = new Vector3(0,btnPos,0);
                string buttonName = renderPath[i];
                goClone.GetComponentInChildren<Text>().text = buttonName;
                goClone.GetComponent<Button>().onClick.AddListener
                (
                    ()=>
                    {
                        if(Variables.GIFWaitRenderName.Contains(buttonName) == false)
                        {
                            GameObject.Find("输出营销图资产/输出").transform.localScale = new Vector3(1,1,1);
                            Variables.GIFWaitRenderName.Add(buttonName);
                            GameObject panel_button2 = GameObject.Find("输出营销图资产/Panel_2/Image/Panel_Button");
                            var rectTransform = panel_button2.transform.GetComponent<RectTransform>();
                            panel_button2.transform.localPosition = new Vector3(0,0 - (((btnHeight * Variables.GIFWaitRenderName.Count) / 2) - (rectTransform.rect.height / 2)),0);
                            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * Variables.GIFWaitRenderName.Count);
                            GameObject button_image2 = GameObject.Find("输出营销图资产/Panel_2/Image");
                            button_image2.transform.localScale = new Vector3(1,1,1);
                            GameObject goClone2 = UnityEngine.Object.Instantiate(Control.go);
                            goClone2.transform.SetParent(panel_button2.transform,true);
                            goClone2.transform.localScale = new Vector3(1,1,1);
                            goClone2.transform.localPosition = new Vector3(0,btnPos,0);
                            string buttonName2 = buttonName;
                            goClone2.GetComponentInChildren<Text>().text = buttonName2;
                            goClone2.GetComponent<Button>().onClick.AddListener
                            (
                                ()=>
                                {
                                    UnityEngine.Object.DestroyImmediate(goClone2);
                                    Variables.GIFWaitRenderName.Remove(buttonName2);
                                    if(Variables.GIFWaitRenderName.Count == 0)
                                    {
                                        button_image2.transform.localScale = new Vector3(0,0,0);
                                        GameObject.Find("输出营销图资产/输出").transform.localScale = new Vector3(0,0,0);
                                    }
                                }
                            );
                        }
                        
                        #if UNITY_EDITOR
                        AssetDatabase.Refresh();
                        #endif
                    }
                );
                btnPos = btnPos - btnHeight;
            }
        }
        else if(Variables.NPCMode == true)
        {
            int btnPos = 0;
            int btnHeight = 30;

            List<String> renderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            Variables.GIFWaitRenderName = new List<String>();
            GameObject panel_button = GameObject.Find("输出营销图资产/Panel/Image/Panel_Button");
            var rectTransform = panel_button.transform.GetComponent<RectTransform>();
            panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * renderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * renderPath.Count);
            GameObject button_image = GameObject.Find("输出营销图资产/Panel/Image");
            button_image.transform.localScale = new Vector3(1,1,1);
            for(int i = 0; i < renderPath.Count; i++)
            {
                GameObject goClone = UnityEngine.Object.Instantiate(Control.go);
                goClone.transform.SetParent(panel_button.transform,true);
                goClone.transform.localScale = new Vector3(1,1,1);
                goClone.transform.localPosition = new Vector3(0,btnPos,0);
                string buttonName = renderPath[i];
                goClone.GetComponentInChildren<Text>().text = buttonName;
                goClone.GetComponent<Button>().onClick.AddListener
                (
                    ()=>
                    {
                        if(Variables.GIFWaitRenderName.Contains(buttonName) == false)
                        {
                            GameObject.Find("输出营销图资产/输出").transform.localScale = new Vector3(1,1,1);
                            Variables.GIFWaitRenderName.Add(buttonName);
                            GameObject panel_button2 = GameObject.Find("输出营销图资产/Panel_2/Image/Panel_Button");
                            var rectTransform = panel_button2.transform.GetComponent<RectTransform>();
                            panel_button2.transform.localPosition = new Vector3(0,0 - (((btnHeight * Variables.GIFWaitRenderName.Count) / 2) - (rectTransform.rect.height / 2)),0);
                            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * Variables.GIFWaitRenderName.Count);
                            GameObject button_image2 = GameObject.Find("输出营销图资产/Panel_2/Image");
                            button_image2.transform.localScale = new Vector3(1,1,1);
                            GameObject goClone2 = UnityEngine.Object.Instantiate(Control.go);
                            goClone2.transform.SetParent(panel_button2.transform,true);
                            goClone2.transform.localScale = new Vector3(1,1,1);
                            goClone2.transform.localPosition = new Vector3(0,btnPos,0);
                            string buttonName2 = buttonName;
                            goClone2.GetComponentInChildren<Text>().text = buttonName2;
                            goClone2.GetComponent<Button>().onClick.AddListener
                            (
                                ()=>
                                {
                                    UnityEngine.Object.DestroyImmediate(goClone2);
                                    Variables.GIFWaitRenderName.Remove(buttonName2);
                                    if(Variables.GIFWaitRenderName.Count == 0)
                                    {
                                        button_image2.transform.localScale = new Vector3(0,0,0);
                                        GameObject.Find("输出标准资产/输出").transform.localScale = new Vector3(0,0,0);
                                    }
                                }
                            );
                        }
                        
                        #if UNITY_EDITOR
                        AssetDatabase.Refresh();
                        #endif
                    }
                );
                btnPos = btnPos - btnHeight;
            }
        }
    }

    /**
        输出选中动作的GIF资产
    */
    public static void ExportSelectGIF()
    {
        Variables.bPlay = false;

        GameObject.Find("输出营销图资产/输出").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/输出全部").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/Panel/Image").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/Panel_2/Image").transform.localScale = new Vector3(0,0,0);
        
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allButton = new List<GameObject>();
        foreach(GameObject go in all)
        {
            if(go.name == "Button(Clone)")
            {
                allButton.Add(go);
            }
        }
        foreach(GameObject go in allButton)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }

        GameObject[] GifObj = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allGif = new List<GameObject>();
        foreach(GameObject go in GifObj)
        {
            if(go.name == "GifConverter")
            {
                allGif.Add(go);
            }
        }
        foreach(GameObject go in allGif)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }

        if(Variables.NPCMode == false)
        {
            Control.matExportGIF.SetVector("_Scale",Variables.scale);
            Control.matExportGIF.SetVector("_CutValue",Variables.cutValue);
            Control.matExportGIF.SetVector("_KPointOffset",Variables.KPointOffset + Variables.KPointOffsetAdd);
            string gifForce = "00";

            for(int i = 0; i < Variables.GIFWaitRenderName.Count; i++)
            {
                for(int l = 0; l < 4; l++)
                {
                    if(l == 0)
                    {
                        gifForce = "00";
                    }
                    if(l == 1)
                    {
                        gifForce = "10";
                    }
                    if(l == 2)
                    {
                        gifForce = "20";
                    }
                    if(l == 3)
                    {
                        gifForce = "30";
                    }
                    GetImages.GetFilesAllImage(Variables.bodyRenderGIFImages , gifForce , Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                    if(Variables.bodyRenderGIFImages.Count != 0)
                    {
                        GetImages.GetFilesAllImage(Variables.headRenderGIFImages , gifForce , Variables.headPath.Remove(Variables.headPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.bodyDepthRenderGIFImages , gifForce , Variables.bodyDepthPath.Remove(Variables.bodyDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.headDepthRenderGIFImages , gifForce , Variables.headDepthPath.Remove(Variables.headDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponRenderGIFImages , gifForce , Variables.weaponPath.Remove(Variables.weaponPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponDepthRenderGIFImages , gifForce , Variables.weaponDepthPath.Remove(Variables.weaponDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponEffectRenderGIFImages , gifForce , Variables.weaponEffectPath.Remove(Variables.weaponEffectPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.gemRenderGIFImages , gifForce , Variables.gemPath.Remove(Variables.gemPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        DirectoryInfo bodyDir = new DirectoryInfo(Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        FileInfo[] bodyFiles = bodyDir.GetFiles("*",SearchOption.AllDirectories);
                        
                        string name = Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/"));
                        name = name.Substring(name.LastIndexOf(@"/"));
                        string savePath = Variables.mainFolderPath + "/Export/export_GIF/Character" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                        Directory.CreateDirectory(savePath);
                        Directory.CreateDirectory(savePath + "/GIF");

                        for(int j = 0; j <= Variables.bodyRenderGIFImages.Count-1 ; j++)
                        {
                            Control.matExportGIF.SetTexture("_Body",Variables.bodyRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_Head",Variables.headRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_Weapon",Variables.weaponRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_BodyDepth",Variables.bodyDepthRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_HeadDepth",Variables.headDepthRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_WeaponDepth",Variables.weaponDepthRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_WeaponEffect",Variables.weaponEffectRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_WeaponGem",Variables.gemRenderGIFImages[j]);
                            
                            FileStream file = new FileStream(savePath + "/" + bodyFiles[j].Name.Remove(bodyFiles[j].Name.LastIndexOf(@".")) + ".tga" , FileMode.Create , FileAccess.Write);
                            SaveImage(file,Control.matExportGIF);
                        }
                        Variables.bodyRenderGIFImages.Clear();
                        Variables.headRenderGIFImages.Clear();
                        Variables.weaponRenderGIFImages.Clear();
                        Variables.bodyDepthRenderGIFImages.Clear();
                        Variables.headDepthRenderGIFImages.Clear();
                        Variables.weaponDepthRenderGIFImages.Clear();
                        Variables.weaponEffectRenderGIFImages.Clear();
                        Variables.gemRenderGIFImages.Clear();
                        
                        Clear();
                        Variables.tex2DList = new List<Texture2D>();
                        GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                        
                        string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                        GIFName = GIFName.Replace(@"\","");
                        string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                        
                        SaveGIF(Variables.tex2DList, GIFSavePath);
                        
                        Resources.UnloadUnusedAssets();
                    }
                    
                }
                    
            }
            Variables.GIFWaitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }
        else if(Variables.NPCMode == true)
        {
            
            Control.matExportGIFNPC.SetVector("_ScaleNPC",Variables.scaleNPC);
            Control.matExportGIFNPC.SetVector("_CutValueNPC",Variables.cutValueNPC);
            Control.matExportGIFNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC + Variables.KPointOffsetAddNPC);

            string gifForce = "00";
            for(int i = 0; i < Variables.GIFWaitRenderName.Count ; i++)
            {
                for(int l = 0; l < 4; l++)
                {
                    if(l == 0)
                    {
                        gifForce = "00";
                    }
                    if(l == 1)
                    {
                        gifForce = "10";
                    }
                    if(l == 2)
                    {
                        gifForce = "20";
                    }
                    if(l == 3)
                    {
                        gifForce = "30";
                    }

                    if(Variables.bSwitch == false)
                    {
                        GetImages.GetFilesAllImage(Variables.NPCRenderGIFImages , gifForce , Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        if(Variables.NPCRenderGIFImages.Count != 0)
                        {
                            DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                            FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                            string name = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/"));
                            name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                            string savePath = Variables.mainFolderPath + "/Export/export_GIF/NPC" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                            Directory.CreateDirectory(savePath);
                            Directory.CreateDirectory(savePath + "/GIF");
                            
                            for(int j = 0; j <= Variables.NPCRenderGIFImages.Count-1 ; j++)
                            {
                                Control.matExportGIFNPC.SetTexture("_NPC",Variables.NPCRenderGIFImages[j]);
                                FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(".")) + ".tga",FileMode.Create,FileAccess.Write);
                                SaveImage(file,Control.matExportGIFNPC);
                            }
                            Variables.NPCRenderGIFImages.Clear();

                            Clear();
                            Variables.tex2DList = new List<Texture2D>();
                            GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                            
                            string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                            GIFName = GIFName.Replace(@"\","");
                            string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                            
                            SaveGIF(Variables.tex2DList, GIFSavePath);
                            

                            
                            Resources.UnloadUnusedAssets();
                        }
                    }
                    if(Variables.bSwitch == true)
                    {
                        GetImages.GetFilesAllImage(Variables.NPCRenderGIFImages , gifForce , Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        if(Variables.NPCRenderGIFImages.Count != 0)
                        {
                            DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                            FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                            string name = Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/"));
                            name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                            string savePath = Variables.mainFolderPath + "/Export/export_GIF/NPC" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                            Directory.CreateDirectory(savePath);
                            Directory.CreateDirectory(savePath + "/GIF");
                            
                            for(int j = 0; j <= Variables.NPCRenderGIFImages.Count-1 ; j++)
                            {
                                Control.matExportGIFNPC.SetTexture("_NPC",Variables.NPCRenderGIFImages[j]);
                                FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(".")) + ".tga",FileMode.Create,FileAccess.Write);
                                SaveImage(file,Control.matExportGIFNPC);
                            }
                            Variables.NPCRenderGIFImages.Clear();

                            Clear();
                            Variables.tex2DList = new List<Texture2D>();
                            GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                            
                            string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                            GIFName = GIFName.Replace(@"\","");
                            string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                            
                            SaveGIF(Variables.tex2DList, GIFSavePath);
                            

                            
                            Resources.UnloadUnusedAssets();
                        }
                    }
                    
                }
            }
            Variables.GIFWaitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }
    }

    /**
        输出所有动作的GIF资产
    */
    public static void ExportGIFAll()
    {

        Variables.bPlay = false;
        GameObject.Find("输出营销图资产/输出").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/输出全部").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/Panel/Image").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("输出营销图资产/Panel_2/Image").transform.localScale = new Vector3(0,0,0);

        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allButton = new List<GameObject>();
        foreach(GameObject go in all)
        {
            if(go.name == "Button(Clone)")
            {
                allButton.Add(go);
            }
        }
        foreach(GameObject go in allButton)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }

        GameObject[] GifObj = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<GameObject> allGif = new List<GameObject>();
        foreach(GameObject go in GifObj)
        {
            if(go.name == "GifConverter")
            {
                allGif.Add(go);
            }
        }
        foreach(GameObject go in allGif)
        {
            UnityEngine.Object.DestroyImmediate(go);
        }


        Variables.GIFWaitRenderName.Clear();
        Resources.UnloadUnusedAssets();

        /*if(Variables.NPCMode == false)
        {
            List<String> renderPath = GetFiles.GetAllFiles(Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")));
            for(int k = 0; k < renderPath.Count; k++)
            {
                Variables.GIFWaitRenderName.Add(renderPath[k]);
            }
            
            Control.matExportGIF.SetVector("_Scale",Variables.scale);
            Control.matExportGIF.SetVector("_CutValue",Variables.cutValue);
            Control.matExportGIF.SetVector("_KPointOffset",Variables.KPointOffset + Variables.KPointOffsetAdd);
            string gifForce = "00";
            for(int i = 0; i < Variables.GIFWaitRenderName.Count; i++)
            {
                for(int l = 0;l < 4;l++)
                {
                    if(l == 0)
                    {
                        gifForce = "00";
                    }
                    if(l == 1)
                    {
                        gifForce = "10";
                    }
                    if(l == 2)
                    {
                        gifForce = "20";
                    }
                    if(l == 3)
                    {
                        gifForce = "30";
                    }
                    GetImages.GetFilesAllImage(Variables.bodyRenderGIFImages , gifForce , Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                    if(Variables.bodyRenderGIFImages.Count != 0)
                    {
                        GetImages.GetFilesAllImage(Variables.headRenderGIFImages , gifForce , Variables.headPath.Remove(Variables.headPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.bodyDepthRenderGIFImages , gifForce , Variables.bodyDepthPath.Remove(Variables.bodyDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.headDepthRenderGIFImages , gifForce , Variables.headDepthPath.Remove(Variables.headDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponRenderGIFImages , gifForce , Variables.weaponPath.Remove(Variables.weaponPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponDepthRenderGIFImages , gifForce , Variables.weaponDepthPath.Remove(Variables.weaponDepthPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.weaponEffectRenderGIFImages , gifForce , Variables.weaponEffectPath.Remove(Variables.weaponEffectPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        GetImages.GetFilesAllImage(Variables.gemRenderGIFImages , gifForce , Variables.gemPath.Remove(Variables.gemPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        
                        DirectoryInfo bodyDir = new DirectoryInfo(Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        FileInfo[] bodyFiles = bodyDir.GetFiles("*",SearchOption.AllDirectories);
                        
                        string name = Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/"));
                        name = name.Substring(name.LastIndexOf(@"/"));
                        string savePath = Variables.mainFolderPath + "/Export/export_GIF/Character" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                        Directory.CreateDirectory(savePath);
                        Directory.CreateDirectory(savePath + "/GIF");

                        for(int j = 0; j <= Variables.bodyRenderGIFImages.Count-1 ; j++)
                        {
                            Control.matExportGIF.SetTexture("_Body",Variables.bodyRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_Head",Variables.headRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_Weapon",Variables.weaponRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_BodyDepth",Variables.bodyDepthRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_HeadDepth",Variables.headDepthRenderGIFImages[j]);
                            Control.matExportGIF.SetTexture("_WeaponDepth",Variables.weaponDepthRenderGIFImages[j]);
                            if(Variables.validBody == true)
                            {
                                Control.matExportGIF.SetTexture("_WeaponEffect",Variables.weaponEffectRenderGIFImages[j]);
                            }
                            if(Variables.validGem == true)
                            {
                                Control.matExportGIF.SetTexture("_WeaponGem",Variables.gemRenderGIFImages[j]);
                            }
                            
                            FileStream file = new FileStream(savePath + "/" + bodyFiles[j].Name.Remove(bodyFiles[j].Name.LastIndexOf(@".")) + ".tga" , FileMode.Create , FileAccess.Write);
                            SaveImage(file,Control.matExportGIF);
                        }
                        Variables.bodyRenderGIFImages.Clear();
                        Variables.headRenderGIFImages.Clear();
                        Variables.weaponRenderGIFImages.Clear();
                        Variables.bodyDepthRenderGIFImages.Clear();
                        Variables.headDepthRenderGIFImages.Clear();
                        Variables.weaponDepthRenderGIFImages.Clear();
                        Variables.weaponEffectRenderGIFImages.Clear();
                        Variables.gemRenderGIFImages.Clear();

                        Clear();
                        Variables.tex2DList = new List<Texture2D>();
                        GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                        
                        string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                        GIFName = GIFName.Replace(@"\","");
                        string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                        
                        SaveGIF(Variables.tex2DList, GIFSavePath);
                        

                        
                        Resources.UnloadUnusedAssets();
                    }
                }
                
                
            }
            
            Variables.GIFWaitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }
         else*/
        if(Variables.NPCMode == true)
        {
            List<String> renderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            for(int k = 0; k < renderPath.Count; k++)
            {
                Variables.GIFWaitRenderName.Add(renderPath[k]);
            }
            
            Control.matExportGIFNPC.SetVector("_ScaleNPC",Variables.scaleNPC);
            Control.matExportGIFNPC.SetVector("_CutValueNPC",Variables.cutValueNPC);
            Control.matExportGIFNPC.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC + Variables.KPointOffsetAddNPC);
            string gifForce = "00";
            for(int i = 0; i < Variables.GIFWaitRenderName.Count ; i++)
            {
                for(int l = 0; l < 4 ; l++)
                {
                    if(l == 0)
                    {
                        gifForce = "00";
                    }
                    if(l == 1)
                    {
                        gifForce = "10";
                    }
                    if(l == 2)
                    {
                        gifForce = "20";
                    }
                    if(l == 3)
                    {
                        gifForce = "30";
                    }
                    if(Variables.bSwitch == false)
                    {
                        GetImages.GetFilesAllImage(Variables.NPCRenderGIFImages , gifForce , Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        if(Variables.NPCRenderGIFImages.Count != 0)
                        {
                            DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                            FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                            string name = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/"));
                            name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                            string savePath = Variables.mainFolderPath + "/Export/export_GIF/NPC" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                            Directory.CreateDirectory(savePath);
                            Directory.CreateDirectory(savePath + "/GIF");
                            
                            for(int j = 0; j <= Variables.NPCRenderGIFImages.Count-1 ; j++)
                            {
                                Control.matExportGIFNPC.SetTexture("_NPC",Variables.NPCRenderGIFImages[j]);
                                FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                                SaveImage(file,Control.matExportGIFNPC);
                            }
                            Variables.NPCRenderGIFImages.Clear();
                            
                            Clear();
                            Variables.tex2DList = new List<Texture2D>();
                            GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                            
                            string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                            GIFName = GIFName.Replace(@"\","");
                            string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                            
                            SaveGIF(Variables.tex2DList, GIFSavePath);

                            
                            Resources.UnloadUnusedAssets();
                        }
                    }
                    if(Variables.bSwitch == true)
                    {
                        GetImages.GetFilesAllImage(Variables.NPCRenderGIFImages , gifForce , Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                        if(Variables.NPCRenderGIFImages.Count != 0)
                        {
                            DirectoryInfo NPCDir = new DirectoryInfo(Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/")) + "/" + Variables.GIFWaitRenderName[i]);
                            FileInfo[] NPCFiles = NPCDir.GetFiles("*",SearchOption.AllDirectories);

                            string name = Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf(@"/"));
                            name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
                            string savePath = Variables.mainFolderPath + "/Export/export_GIF/NPC" + name + "/" + Variables.GIFWaitRenderName[i] + "/" + gifForce;
                            Directory.CreateDirectory(savePath);
                            Directory.CreateDirectory(savePath + "/GIF");
                            
                            for(int j = 0; j <= Variables.NPCRenderGIFImages.Count-1 ; j++)
                            {
                                Control.matExportGIFNPC.SetTexture("_NPC",Variables.NPCRenderGIFImages[j]);
                                FileStream file = new FileStream(savePath + "/" + NPCFiles[j].Name.Remove(NPCFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                                SaveImage(file,Control.matExportGIFNPC);
                            }
                            Variables.NPCRenderGIFImages.Clear();
                            
                            Clear();
                            Variables.tex2DList = new List<Texture2D>();
                            GetImages.GetFilesAllImageNoForce(Variables.tex2DList,savePath);
                            
                            string GIFName = name + Variables.GIFWaitRenderName[i] + gifForce;
                            GIFName = GIFName.Replace(@"\","");
                            string GIFSavePath = savePath  + "/GIF" + "/" + GIFName;
                            
                            SaveGIF(Variables.tex2DList, GIFSavePath);

                            
                            Resources.UnloadUnusedAssets();
                        }
                    }
                }
            }
            Variables.GIFWaitRenderName.Clear();
            Resources.UnloadUnusedAssets();

            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            
            Variables.bPlay = true; 
            Variables.bChoose = true;
        }
    }

    /**
        保存GIF
    */
    private static void SaveGIF(List<Texture2D> tex2DList , string GIFSavePath)
    {
        ProGifTexturesToGIF tex2Gif = ProGifTexturesToGIF.Create("GifConverter");
        tex2Gif.SetFileExtension(new List<string>{".jpg", ".tga"});

        tex2Gif.SetTransparent(true);
        tex2Gif.Save(tex2DList, Variables.GIFWidth , Variables.GIFHeight , Variables.GIFSpeed , Variables.GIFLoopCount , Variables.GIFQuality , GIFSavePath , OnFileSaved , OnFileSaveProgress , ProGifTexturesToGIF.ResolutionHandle.ResizeKeepRatio, autoClear:true);
    }

     private static void OnFileSaved(int id, string path)
	{
		Debug.Log("On file saved: " + path);
		// text1.text = "GIF saved: " + path;
		// ShowGIF(path);

		// displayImage.sprite = tex2Gif.GetSprite(0);
		// displayImage.SetNativeSize();
	}
    //GIF相关

    private static void OnFileSaveProgress(int id, float progress)
	{
		//Debug.Log("On file save progress: " + progress);
		// text1.text = "Save progress: " + Mathf.CeilToInt(progress * 100) + "%";
	}
    //GIF相关

    private static void Clear()
	{
		if(Variables.tex2Gif != null) Variables.tex2Gif.Clear();

		// if(displayImage != null && displayImage.sprite != null && displayImage.sprite.texture != null)
		// {
		// 	Texture2D.Destroy(displayImage.sprite.texture);
		// 	displayImage.sprite = null;
		// }

		//Clear texture
		if(Variables.tex2DList != null)
		{
			foreach(Texture2D tex in Variables.tex2DList)
			{
				if(tex != null)     
				{
					Texture2D.Destroy(tex);
				}
			}
			Variables.tex2DList = null;
		}
	}
    //GIF相关

    /**
        输出更改分辨率后的图片
    */
    public static void ReSamplingImage()
    {
        List<Texture2D> reSamplingImages = new List<Texture2D>();
        string reSamplingPath = OpenFunc.Open("请选择要重新更改分辨率的图片文件夹");
        GetImages.GetFilesAllImageReSampling(reSamplingImages , reSamplingPath);
        
        if(reSamplingImages.Count != 0)
        {
            float reSamplingWidthOld = reSamplingImages[0].width;
            float reSamplingHeightOld = reSamplingImages[0].height;
            float reSamplingScaleX = reSamplingWidthOld / Variables.reSamplingWidthNew;
            float reSamplingScaleY = reSamplingHeightOld / Variables.reSamplingHeightNew;
            Vector4 reSamplingScale = new Vector4(reSamplingScaleX , reSamplingScaleY , 0 , 0);
            Vector4 cutValue = new Vector4((Variables.reSamplingWidthNew - reSamplingWidthOld) / 2 / Variables.reSamplingWidthNew , (Variables.reSamplingHeightNew - reSamplingHeightOld) /2 / Variables.reSamplingHeightNew , 0 , 0);
            Control.matReSampling.SetVector("_ReSamplingScale", reSamplingScale);

            DirectoryInfo reSamplingDir = new DirectoryInfo(reSamplingPath);
            FileInfo[] reSamplingFiles = reSamplingDir.GetFiles("*",SearchOption.AllDirectories);

            string name = reSamplingPath;
            name = name.Substring(name.LastIndexOf(@"\",name.LastIndexOf(@"\") - 1));
            string savePath = Variables.mainFolderPath + "/Export/export_ReSampling" + name;
            Directory.CreateDirectory(savePath);
            
            for(int j = 0; j <= reSamplingImages.Count-1 ; j++)
            {
                Control.matReSampling.SetTexture("_Tex",reSamplingImages[j]);
                FileStream file = new FileStream(savePath + "/" + reSamplingFiles[j].Name.Remove(reSamplingFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                SaveResamplingImage(file);
            }
            reSamplingImages.Clear();
            Resources.UnloadUnusedAssets();
            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            Control.matReSampling.SetVector("_ReSamplingScale", new Vector4(0,0,0,0));
        }
        else if(reSamplingImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero, "请确认文件路径正确", "确认", 0);
        }
        
    }

    /**
        更改分辨率的图片存储方法
    */
    private static void SaveResamplingImage(FileStream file)
    { 
        RenderTexture rt = RenderTexture.GetTemporary(Variables.reSamplingWidthNew,Variables.reSamplingHeightNew,32,RenderTextureFormat.ARGB32);
        rt.autoGenerateMips = true;
        rt.wrapMode = TextureWrapMode.Clamp;
        rt.filterMode = FilterMode.Trilinear;
        Graphics.Blit(rt,rt,Control.matReSampling);
        Texture2D tex2d = new Texture2D(rt.width,rt.height,TextureFormat.RGBA32,true);
        tex2d.filterMode = FilterMode.Trilinear;

        tex2d.ReadPixels(new Rect(0,0,rt.width,rt.height),0,0);
        tex2d.wrapMode = TextureWrapMode.Clamp;
        tex2d.Apply();
        
        RenderTexture.ReleaseTemporary(rt);
    
        byte[] vs = ImageConversion.EncodeToPNG(tex2d);
        file.Write(vs , 0, vs.Length);
        file.Dispose();
        file.Close();
    }

    /**
        输出合并图1RGB通道与图2A通道的图像
    */
    public static void SaveMergeImage()
    {
        List<Texture2D> mergeRGBImages = new List<Texture2D>();
        string RGBPath = OpenFunc.Open("请选择要合并的RGB图片");
        GetImages.GetFilesAllImageNoForce(mergeRGBImages , RGBPath);
        List<Texture2D> mergeAImages = new List<Texture2D>();
        string APath = OpenFunc.Open("请选择要合并的A图片");
        GetImages.GetFilesAllImageNoForce(mergeAImages , APath);
        
        if(mergeRGBImages.Count != 0 && mergeAImages.Count != 0)
        {
            float imageWidth = mergeRGBImages[0].width;
            float imageHeight = mergeRGBImages[0].height;
            Vector4 imageScale = new Vector4(imageWidth , imageHeight , 0 , 0);
            Control.matMerge.SetVector("_ImageScale", imageScale);

            DirectoryInfo RGBDir = new DirectoryInfo(RGBPath);
            FileInfo[] RGBFiles = RGBDir.GetFiles("*",SearchOption.AllDirectories);
            DirectoryInfo ADir = new DirectoryInfo(APath);
            FileInfo[] AFiles = ADir.GetFiles("*",SearchOption.AllDirectories);

            string RGBname = RGBPath;
            RGBname = RGBname.Substring(RGBname.LastIndexOf(@"\",RGBname.LastIndexOf(@"\") - 1));
            string savePath = Variables.mainFolderPath + "/Export/export_MergeImage" + RGBname;
            Directory.CreateDirectory(savePath);
            
            for(int j = 0; j <= mergeRGBImages.Count-1 ; j++)
            {
                Control.matMerge.SetTexture("_Tex",mergeRGBImages[j]);
                Control.matMerge.SetTexture("_Tex2",mergeAImages[j]);
                FileStream file = new FileStream(savePath + "/" + RGBFiles[j].Name.Remove(RGBFiles[j].Name.LastIndexOf(@".")) + ".tga",FileMode.Create,FileAccess.Write);
                SaveMergeImageValue(file,(int)imageWidth,(int)imageHeight);
            }
            mergeRGBImages.Clear();
            mergeAImages.Clear();
            Resources.UnloadUnusedAssets();
            Messagebox.MessageBox(IntPtr.Zero, "输出完毕", "确认", 0);
            Control.matMerge.SetVector("_ImageScale", new Vector4(0,0,0,0));
        }
        else if(mergeRGBImages.Count == 0 || mergeAImages.Count == 0)
        {
            Messagebox.MessageBox(IntPtr.Zero, "请确认文件路径正确", "确认", 0);
        }
    }

    /**
        合并图存储方法
    */
    private static void SaveMergeImageValue(FileStream file,int imageWidth,int imageHeight)
    {
        RenderTexture rt = RenderTexture.GetTemporary(imageWidth,imageHeight,32,RenderTextureFormat.ARGB32);
        rt.autoGenerateMips = true;
        rt.wrapMode = TextureWrapMode.Clamp;
        rt.filterMode = FilterMode.Trilinear;
        Graphics.Blit(rt,rt,Control.matMerge);
        Texture2D tex2d = new Texture2D(rt.width,rt.height,TextureFormat.RGBA32,true);
        tex2d.filterMode = FilterMode.Trilinear;

        tex2d.ReadPixels(new Rect(0,0,rt.width,rt.height),0,0);
        tex2d.wrapMode = TextureWrapMode.Clamp;
        tex2d.Apply();
        
        RenderTexture.ReleaseTemporary(rt);
    
        byte[] vs = ImageConversion.EncodeToPNG(tex2d);
        file.Write(vs , 0, vs.Length);
        file.Dispose();
        file.Close();
    }
    //保存合并后的图像
   
}
