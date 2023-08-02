using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Interaction
{
     /**
        透明裁切滚动条
    */
    public static void OnValueChangeAlphaClip(float value)
    {
        Control.mat.SetFloat("_AlphaCut",value);
    }

    /**
        播放速度滚动条
    */
    public static void OnValueChangePlaySpeed(float value)
    {
        Variables.speed = 5 + value * 10;
    }

    /**
        混合阈值滚动条
    */
    public static void OnValueChangBlend(float value)
    {
        if(Variables.bAlphaMode == true)
        {
            Control.matContrast.SetFloat("_Controler",value);
        }
        if(Variables.bSoldMode == true)
        {
            Control.matContrast.SetFloat("_FinalLerp",value);
        }
        if(Variables.bDiffMode == true)
        {
            Control.matContrast.SetFloat("_DiffLerp",value);
        }
        if(Variables.bPixelDiffMode == true)
        {
            Control.matContrast.SetFloat("_PixelDiffLerp",value);
        }
    }

    /**
        显示、关闭主界面
    */
    public static void ShowMain()
    {
        Control.frameGO.transform.localScale = new Vector3(1,1,1);
        GameObject.Find("QA_Quad").transform.localScale = new Vector3(10,10,10);
    }

    private static void CloseMain()
    {
        Control.frameGO.transform.localScale = new Vector3(0,0,0);
        GameObject.Find("QA_Quad").transform.localScale = new Vector3(0,0,0);
        Variables.bSwitch = false; 
    }

    /**
        显示、关闭重叠对比界面
    */
    private static void ShowOverlap()
    {
        GameObject.Find("重叠控制器").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("Overlap_Quad_Right").transform.localScale = new Vector3(4,4,4);
        GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(7,7,7);
        GameObject.Find("Overlap_Quad_Left").transform.localScale = new Vector3(4,4,4);
        GameObject.Find("数据对比提示").transform.localScale = new Vector3(1,1,1);
        //GameObject.Find("对比主角选择按钮").transform.localScale = new Vector3(1,1,1);

        GameObject.Find("Canvas/Panel/重叠对比").GetComponentInChildren<Text>().text = "返回";
    }

    private static void CloseOverlap()
    {
        GameObject.Find("Overlap_Quad_Right").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Overlap_Quad_Left").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("数据对比提示").transform.localScale = new Vector3(0,0,0);
        //GameObject.Find("对比主角选择按钮").transform.localScale = new Vector3(0,0,0);

        GameObject.Find("Canvas/Panel/重叠对比").GetComponentInChildren<Text>().text = "重叠对比";
        GameObject.Find("重叠控制器").transform.localScale = new Vector3(0,0,0);
    }

    /**
        显示、关闭角色对比界面
    */
    private static void ShowCharacter()
    {
        GameObject.Find("Character_Quad_Right").transform.localScale = new Vector3(7,7,7);
        GameObject.Find("Character_Quad_Left").transform.localScale = new Vector3(7,7,7);
        
        GameObject.Find("角色对比").GetComponentInChildren<Text>().text = "返回";
        GameObject.Find("角色控制器").transform.localScale = new Vector3(1,1,1);
        Control.matCharacter.SetInt("_CutSwitch",1);
        Control.mat.SetInt("_CutSwitch",1);
    }

    private static void CloseCharacter()
    {
        GameObject.Find("Character_Quad_Right").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Character_Quad_Left").transform.localScale = new Vector3(0,0,0);

        GameObject.Find("角色对比").GetComponentInChildren<Text>().text = "角色对比";
        GameObject.Find("角色控制器").transform.localScale = new Vector3(0,0,0);
        Control.matCharacter.SetInt("_CutSwitch",0);
        Control.matCharacter.SetFloat("_PicScale",1);
        Control.mat.SetInt("_CutSwitch",0);
        Control.mat.SetFloat("_PicScale",1);
        Variables.picScale = 1;
    }

    /**
        通过点击选择颜色
    */
    private static void ChooseColorByClickColorWheel() 
    {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
        {
            // Texture tex = GetRT().target;
            Vector2 pos = new Vector2(-805,-145);
            float posX = Screen.width / 2 + pos.x;
            float posY = Screen.height / 2 + pos.y;
            Vector2 centerPos = new Vector2(posX, posY);
			Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			int x = Mathf.FloorToInt(mousePos.x - centerPos.x);
			int y = Mathf.FloorToInt(mousePos.y - centerPos.y);

			if(x*x + y*y <= Mathf.Pow(Control.colorTex.width/2,2)) {
				//Control.colorTex为色盘图
			    Variables.pixelColor = Control.colorTex.GetPixel(x + Control.colorTex.width/2 , y + Control.colorTex.height/2);
			}
		}
	}

    /**
        通过点击选择灰度
    */
    private static void ChooseGrayByClickColorWheel() 
    {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) 
        {
            // Texture tex = GetRT().target;
            Vector2 pos = new Vector2(-805,-145);
            float posX = Screen.width / 2 + pos.x;
            float posY = Screen.height / 2 + pos.y;
            Vector2 centerPos = new Vector2(posX, posY);
			Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			int x = Mathf.FloorToInt(mousePos.x - centerPos.x);
			int y = Mathf.FloorToInt(mousePos.y - centerPos.y);

			if(x*x + y*y <= Mathf.Pow(Control.grayTex.width/2,2)) {
				//Control.grayTex为色盘图
			    Variables.pixelColor = Control.grayTex.GetPixel(x + Control.grayTex.width/2 , y + Control.grayTex.height/2);
			}
		}
	}

    /**
        点击、按住按钮控制资产上下左右移动
    */
    public static void ClickLeft()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd += new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        
    }

    public static void PressLeft()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd += new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x ;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
    }

    public static void ClickRight()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd -= new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        
    }

    public static void PressRight()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd -= new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
    }

    public static void ClickUp()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd -= new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        
    }

    public static void PressUp()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd -= new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
    }

    public static void ClickDown()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd += new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        
    }

    public static void PressDown()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd += new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
    }

    /**
        刷新提示内容数据
    */
    public static void RefreshData()
    {
        //Variables.NPCImagesNoForce.Clear();
        Variables.bodyImagesNoForce.Clear();
        Variables.NPCOverlapImagesNoForce.Clear();
        Variables.bodyOverlapImagesNoForce.Clear();
        Resources.UnloadUnusedAssets();
        if(Variables.playMode == 1)
        {
            if(Variables.bCorrection == true)
            {
                Variables.exportWidth = Variables.standardPixel.x;
                Variables.exportHeight = Variables.standardPixel.y;
            }
            if(Variables.bReset == true)
            {
                Variables.exportKPointX = Variables.standardKx;
                Variables.exportKPointY = Variables.standardKy;
            }
            GameObject.Find("输出像素尺寸").GetComponentInChildren<Text>().text = Variables.exportWidth + "x" + Variables.exportHeight;
            GameObject.Find("对比像素尺寸").GetComponentInChildren<Text>().text = Variables.overlapWidth + "x" + Variables.overlapHeight;
            if(Variables.exportWidth != Variables.overlapWidth || Variables.exportHeight != Variables.overlapHeight)
            {
                GameObject.Find("输出像素尺寸").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"像素尺寸有差异","确认",0);
            }
            else
            {
                GameObject.Find("输出像素尺寸").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }

            GameObject.Find("当前方向帧数/输出帧数").GetComponentInChildren<Text>().text = Variables.exportForceCount.ToString();
            GameObject.Find("当前方向帧数/对比帧数").GetComponentInChildren<Text>().text = Variables.overlapForceCount.ToString();
            if(Variables.exportForceCount != Variables.overlapForceCount)
            {
                GameObject.Find("当前方向帧数/输出帧数").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"当前动作方向帧数有差异","确认",0);
            }
            else
            {
                GameObject.Find("当前方向帧数/输出帧数").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }

            GameObject.Find("当前动作总帧数/输出帧数").GetComponentInChildren<Text>().text = Variables.exportCount.ToString();
            GameObject.Find("当前动作总帧数/对比帧数").GetComponentInChildren<Text>().text = Variables.overlapCount.ToString();
            if(Variables.exportCount != Variables.overlapCount)
            {
                GameObject.Find("当前动作总帧数/输出帧数").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"当前动作总帧数有差异","确认",0);
            }
            else
            {
                GameObject.Find("当前动作总帧数/输出帧数").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }
            GameObject.Find("输出K点位置信息").GetComponentInChildren<Text>().text = (int)Variables.exportKPointX + "," + (int)Variables.exportKPointY;
            GameObject.Find("对比K点位置信息").GetComponentInChildren<Text>().text = (int)Variables.overlapKPointX + "," + (int)Variables.overlapKPointY;
            if(Variables.exportKPointX != Variables.overlapKPointX || Variables.exportKPointY != Variables.overlapKPointY)
            {
                GameObject.Find("输出K点位置信息").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"K点位置有差异","确认",0);
            }
            else
            {
                GameObject.Find("对比K点位置信息").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }
        }
    }

    /**
        重叠对比模式、角色对比模式缩放逻辑
    */
    public static void ScaleFunc()
    {
        if(Variables.playMode == 2 && Variables.bPicScale == true)
        {
            Variables.picScale += Input.mouseScrollDelta.y * 0.05f;
            if(Variables.picScale >= 3f)
            {
                Variables.picScale = 3f;
            }
            if(Variables.picScale <= 0.5f)
            {
                Variables.picScale = 0.5f;
            }
            Control.matCharacter.SetFloat("_PicScale",Variables.picScale);
            Control.mat.SetFloat("_PicScale",Variables.picScale);
        }
        if(Variables.bPlay == false && Variables.bScale == false && Variables.bPicScale == false)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll > 0f)
            {
                Play.LastFrame();
            }
            if(scroll < 0f)
            {
                Play.NextFrame();
            }
        }
        if(Variables.playMode == 1 && Variables.bScale == true)
        {
            GameObject.Find("Overlap_Quad_Mid").transform.localScale += new Vector3(Input.mouseScrollDelta.y * 0.5f,Input.mouseScrollDelta.y * 0.5f,Input.mouseScrollDelta.y * 0.5f);
            if(GameObject.Find("Overlap_Quad_Mid").transform.localScale.x >= 30f)
            {
                GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(30,30,30);
            }
            if(GameObject.Find("Overlap_Quad_Mid").transform.localScale.x <= 7f)
            {
                GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(7f,7f,7f);
            }
        }
    }

    /**
        滚轮相关交互
    */
    public static void ReScale()
    {
        if(Variables.playMode == 2 && Variables.bPicScale == true)
        {
            Variables.picScale += Input.mouseScrollDelta.y * 0.05f;
            if(Variables.picScale >= 3f)
            {
                Variables.picScale = 3f;
            }
            if(Variables.picScale <= 0.5f)
            {
                Variables.picScale = 0.5f;
            }
            Control.matCharacter.SetFloat("_PicScale",Variables.picScale);
            Control.mat.SetFloat("_PicScale",Variables.picScale);
        }
        if(Variables.bPlay == false && Variables.bScale == false && Variables.bPicScale == false)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll > 0f)
            {
                Play.LastFrame();
            }
            if(scroll < 0f)
            {
                Play.NextFrame();
            }
        }
        if(Variables.playMode == 1 && Variables.bScale == true)
        {
            GameObject.Find("Overlap_Quad_Mid").transform.localScale += new Vector3(Input.mouseScrollDelta.y * 0.5f,Input.mouseScrollDelta.y * 0.5f,Input.mouseScrollDelta.y * 0.5f);
            if(GameObject.Find("Overlap_Quad_Mid").transform.localScale.x >= 30f)
            {
                GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(30,30,30);
            }
            if(GameObject.Find("Overlap_Quad_Mid").transform.localScale.x <= 7f)
            {
                GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(7f,7f,7f);
            }
        }
    }
        

    /**
        颜色、灰度选择逻辑
    */
    public static void ChooseColorValue()
    {
        if(Variables.bChooseColor == true)
        {
            if(Variables.colorAmount <= 1f)
            {
                Variables.colorAmount += 0.05f;
                Control.matChooseColor.SetFloat("_Amount",Variables.colorAmount);
            }
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                ChooseColorByClickColorWheel();
                Control.mat.SetVector("_BackColor",Variables.pixelColor);
                Control.matOverlap.SetVector("_BackColor",Variables.pixelColor);
                Control.matContrast.SetVector("_BackColor",Variables.pixelColor);
                Control.matCharacter.SetVector("_BackColor",Variables.pixelColor);
                Control.matExportGIF.SetVector("_BackColor",Variables.pixelColor);
                Control.matExportGIFNPC.SetVector("_BackColor",Variables.pixelColor);

                Vector2 pos;
                if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Variables.colorRectTransform,Input.mousePosition,Control.camera,out pos))
                {
                    Control.matChooseColor.SetVector("_Position", new Vector4(pos.x/280,pos.y/280,0,0));
                }
            }
        }
        if(Variables.bChooseColor == false)
        {
            if(Variables.colorAmount >= 0f)
            {
                Variables.colorAmount -= 0.05f;
                Control.matChooseColor.SetFloat("_Amount",Variables.colorAmount);
            }
            if(Variables.colorAmount == 0.015f)
            {
                GameObject.Find("颜色选择器").transform.localScale = new Vector3(0,0,0);
            }
        }
    }

    public static void ChooseGrayValue()
    {
        if(Variables.bChooseGray == true)
        {
            if(Variables.grayAmount <= 1f)
            {
                Variables.grayAmount += 0.05f;
                Control.matChooseGray.SetFloat("_Amount",Variables.grayAmount);
            }
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                ChooseGrayByClickColorWheel();
                Control.mat.SetVector("_BackColor",Variables.pixelColor);
                Control.matOverlap.SetVector("_BackColor",Variables.pixelColor);
                Control.matContrast.SetVector("_BackColor",Variables.pixelColor);
                Control.matCharacter.SetVector("_BackColor",Variables.pixelColor);
                Control.matExportGIF.SetVector("_BackColor",Variables.pixelColor);
                Control.matExportGIFNPC.SetVector("_BackColor",Variables.pixelColor);

                Vector2 posGray;
                if(RectTransformUtility.ScreenPointToLocalPointInRectangle(Variables.grayRectTransform,Input.mousePosition,Control.camera,out posGray))
                {
                    Control.matChooseGray.SetVector("_Position", new Vector4(posGray.x/280,posGray.y/280,0,0));
                }
            }
        }
        if(Variables.bChooseGray == false)
        {
            if(Variables.grayAmount >= 0f)
            {
                Variables.grayAmount -= 0.05f;
                Control.matChooseGray.SetFloat("_Amount",Variables.grayAmount);
            }
            if(Variables.grayAmount == 0.015f)
            {
                GameObject.Find("灰度选择器").transform.localScale = new Vector3(0,0,0);
            }
        }
    }

    /**
        播放动画
    */
    public static void PlayAnim()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.NPCMode == true)
        {
            if(Variables.NPCImages.Count != 0)
            {
                Variables.bPlay = true;
            }
            if(Variables.NPCImages.Count == 0)
            {
                Messagebox.MessageBox(IntPtr.Zero, "缺少NPC图片", "确认", 0);
            }
        }   
    }

    /**
        暂停播放动画
    */    
    public static void StopAnim()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.bPlay = false;
    }

    /**
        输出时10方向、20方向偏移值是否反转
    */
    public static void VertOffset()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.bVert == true)
        {
            GameObject.Find("是否反转/反转").GetComponentInChildren<Text>().text = "未反转";
            Variables.bVert = false;
        }
        else if(Variables.bVert == false)
        {
            GameObject.Find("是否反转/反转").GetComponentInChildren<Text>().text = "反转";
            Variables.bVert = true;
        }
    }


    /**
        选择、取消部位资产
    */
    public static void CancelNPC()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC = !Variables.validNPC;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPC选择按钮/BASE/000000").GetComponentInChildren<Text>().text = Variables.validNPC ? "Y" : "X";
    }


    public static void CancelNPCAddon()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPCAddon = !Variables.validNPCAddon;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPC选择按钮/ADDON/000000").GetComponentInChildren<Text>().text = Variables.validNPCAddon ? "Y" : "X";
    }

    public static void CancelNPC00()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC00 = !Variables.validNPC00;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPC选择按钮/00/000000").GetComponentInChildren<Text>().text = Variables.validNPC00 ? "Y" : "X";
    }

    public static void CancelNPC01()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC01 = !Variables.validNPC01;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPC选择按钮/01/000000").GetComponentInChildren<Text>().text = Variables.validNPC01 ? "Y" : "X";
    }

    public static void CancelNPC02()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC02 = !Variables.validNPC02;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPC选择按钮/01/000000").GetComponentInChildren<Text>().text = Variables.validNPC02 ? "Y" : "X";
    }

    /**
        选择NPC资产
    */
    public static void ChooseNPCFolder()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.bChoose = false;

        Variables.scaleNPC = new Vector4(1,1,0,0);

        //mat
        Control.mat.SetVector("_ScaleNPC",Variables.scaleNPC);
        Control.matContrast.SetVector("_ScaleNPC",Variables.scaleNPC);
        Control.mat.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));
        Control.matContrast.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));

        //mat1
        Control.mat1.SetVector("_ScaleNPC",Variables.scaleNPC);
        //Control.mat1Contrast.SetVector("_ScaleNPC",Variables.scaleNPC);
        Control.mat1.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));
        //Control.mat1Contrast.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));

        //mat2
        Control.mat2.SetVector("_ScaleNPC",Variables.scaleNPC);
        Control.mat2.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));

        //mat3
        Control.mat3.SetVector("_ScaleNPC",Variables.scaleNPC);
        Control.mat3.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));


        Variables.KPointOffsetNPC = new Vector4(0,0,0,0);
        Variables.KPointOffsetABSPosX = 0;
        Variables.KPointOffsetABSPosY = 0;
        Variables.KPointOffsetAddNPC = new Vector4(0,0,0,0);
        Variables.KPointOffsetAttack = new Vector4(0,0,0,0);
        Variables.KPointOffsetStand = new Vector4(0,0,0,0);
        Variables.KPointOffsetHit = new Vector4(0,0,0,0);
        Variables.KPointOffsetDie = new Vector4(0,0,0,0);
        Variables.KPointOffsetWalk = new Vector4(0,0,0,0);
        Variables.KPointOffsetMagic = new Vector4(0,0,0,0);
        GameObject.Find("绝对偏移X").GetComponent<InputField>().text = "0";
        GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = "0";

        Variables.animName = "stand";
        GameObject.Find("动作/attack").GetComponentInChildren<Text>().text = "stand";

        Variables.NPCPath = OpenFunc.Open("请选择NPC文件夹") + "/" + Variables.animName;

        //点击导入NPC资产就显示stand，不用重新点击动画
        //Import.AddNPC(Variables.NPCPath);
        //Import.AddNPCAddon(Variables.NPCPath);
        //Import.AddNPC00(Variables.NPCPath);

        if(Variables.NPCImages.Count != 0)
        {
            Variables.exportWidth = Variables.NPCImages[0].width;
            Variables.exportHeight = Variables.NPCImages[0].height;
        }
        
        RefreshData();
        Variables.bCorrection = false;
        Variables.bReset = false;
        Variables.bChoose = true;
                
    }

    /**
        选择动作
    */
    public static void ChooseAnim()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.bChoose = false;

        int btnPos = 0;
        int btnHeight = 30;
        string lastAnimName = Variables.animName;
        
        List<string> animFolderPath = null;
        if(Variables.NPCPath != null)
        {
            animFolderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            //删除addon和00
            animFolderPath.RemoveAll(filePath => filePath.EndsWith("addon") || filePath.EndsWith("00") || filePath.EndsWith("01"));
        }
        else
        {
            Debug.LogError("请先选择NPC文件夹");
        }


        //新生成的Button位置
        GameObject panel_button = GameObject.Find("动作/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * animFolderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * animFolderPath.Count);

        //显示背景
        GameObject button_image = GameObject.Find("动作/Panel/Image");
        button_image.transform.localScale = new Vector3(1,1,1);

        for( int i = 0 ; i < animFolderPath.Count;i++)
        {
            GameObject goClone = UnityEngine.Object.Instantiate(Control.go);
            goClone.transform.SetParent(panel_button.transform,true);
            goClone.transform.localScale = new Vector3(1,1,1);
            goClone.transform.localPosition = new Vector3(0,btnPos,0);

            string buttonName = animFolderPath[i];
            goClone.GetComponentInChildren<Text>().text = buttonName;
            goClone.GetComponent<Button>().onClick.AddListener
            (
                ()=>
                {
                    GameObject attackButton = GameObject.Find("动作/attack");
                    attackButton.GetComponentInChildren<Text>().text = buttonName;
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

                    #if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    #endif

                    button_image.transform.localScale = new Vector3(0,0,0);
                    Variables.animName = buttonName;

                    Variables.NPCPath = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/") + 1) + Variables.animName;

                    
                    if(Variables.animName == "attack")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetAttack;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.animName == "stand")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetStand;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.animName == "magic")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetMagic;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.animName == "walk")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetWalk;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.animName == "hit")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetHit;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.animName == "die")
                    {
                        if(Variables.NPCMode == true)
                        {
                            Variables.KPointOffsetNPC = Variables.KPointOffsetDie;
                            Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPC;
                            Variables.KPointOffsetABSPosX = Variables.KPointOffsetNPC.x;
                            Variables.KPointOffsetABSPosY = Variables.KPointOffsetNPC.y;
                            float px = Variables.KPointOffsetABSPosX * 1000;
                            float py = Variables.KPointOffsetABSPosY * 1000;
                            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                            Modify.ButtonApplyNumOffset2();    
                        }
                    }
                    if(Variables.NPCMode == true)
                    {
                            if(Variables.NPCPath != null)
                            {
                                Import.AddNPC(Variables.NPCPath);
                                Import.AddNPCAddon(Variables.NPCPath);
                                Import.AddNPC00(Variables.NPCPath);
                            }
                            else
                            {
                                Messagebox.MessageBox(IntPtr.Zero, "缺少路径", "确认", 0);
                            }

                            Variables.bChoose = true;
                    }


                    if(Variables.playMode == 1)
                    {
                        if(Variables.bOverlapNPC == true)
                        {
                            if(Variables.NPCOverlapPath != null)
                            {
                                Variables.NPCOverlapPath = Variables.NPCOverlapPath.Remove(Variables.NPCOverlapPath.LastIndexOf("/") + 1) + Variables.animName;
                            }
                            Import.AddNPCOverlap(Variables.NPCOverlapPath);
                        }
                        if(Variables.bOverlapNPC == false)
                        {
                            if(Variables.bodyOverlapPath != null)
                            {
                                if(Variables.validBodyOverlap)
                                {
                                    Variables.bodyOverlapPath = Variables.bodyOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validBodyDepthOverlap == true)
                                {
                                    Variables.bodyDepthOverlapPath = Variables.bodyDepthOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validHeadOverlap == true)
                                {
                                    Variables.headOverlapPath = Variables.headOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validHeadDepthOverlap == true)
                                {
                                    Variables.headDepthOverlapPath = Variables.headDepthOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validWeaponOverlap == true)
                                {
                                    Variables.weaponOverlapPath = Variables.weaponOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validWeaponDepthOverlap == true)
                                {
                                    Variables.weaponDepthOverlapPath = Variables.weaponDepthOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validGemOverlap == true)
                                {
                                    Variables.gemOverlapPath = Variables.gemOverlapPath.Replace(lastAnimName , Variables.animName);
                                }
                                if(Variables.validWeaponEffectOverlap == true)
                                {
                                    Variables.weaponEffectOverlapPath = Variables.weaponEffectOverlapPath.Replace(lastAnimName,Variables.animName);
                                }
                                
                            }
                            if(Variables.validBodyOverlap == true && Variables.validBodyDepthOverlap == true)
                            {
                                Import.AddBodyOverlap(Variables.bodyOverlapPath, Variables.bodyDepthOverlapPath);
                            }
                            if(Variables.validHeadOverlap == true && Variables.validHeadDepthOverlap == true)
                            {
                                Import.AddHeadOverlap(Variables.headOverlapPath, Variables.headDepthOverlapPath);
                            }
                            if(Variables.validWeaponOverlap == true && Variables.validWeaponDepthOverlap == true)
                            {
                                Import.AddWeaponOverlap(Variables.weaponOverlapPath, Variables.weaponDepthOverlapPath);
                                Import.AddWeaponEffectOverlap(Variables.weaponEffectOverlapPath);
                            }
                            if(Variables.validGemOverlap == true)
                            {
                                Import.AddGemOverlap(Variables.gemOverlapPath);
                            }
                            if(Variables.validWeaponEffectOverlap == true)
                            {
                                Import.AddWeaponEffectOverlap(Variables.weaponEffectOverlapPath);
                            }                       
                        }
                    }
                    RefreshData();
                }
            );
            btnPos = btnPos - btnHeight;
        }


    }

    public static void ChooseAinmAll()
    {
        Debug.Log(Variables.aAnimAll);

        if(Variables.playMode == 0)
        {
            Variables.aAnimAll = !Variables.aAnimAll;

            for (int i = 0; i < Control.mat_anim.Length; i++)
            {
                Control.mat_anim[i].SetVector("_ScaleNPC", Variables.scaleNPC);
                Control.mat_anim[i].SetVector("_KPointOffsetNPC", new Vector4(0, 0, 0, 0));
            }

            List<string> animFolderPath = GetFiles.GetAllFiles(Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/")));
            //删除addon和00
            animFolderPath.RemoveAll(filePath => filePath.EndsWith("addon") || filePath.EndsWith("00") || filePath.EndsWith("01"));

            


            //设置好多个NPC的列表
            List<List<Texture2D>> NPCImagesList = 
                new List<List<Texture2D>> {Variables.NPCImages0 , Variables.NPCImages1, Variables.NPCImages2, Variables.NPCImages3,Variables.NPCImages4, Variables.NPCImages5,Variables.NPCImages6, Variables.NPCImages7 };
            List<List<Texture2D>> NPCAddonImagesList = 
                new List<List<Texture2D>> {Variables.NPCAddonImages0 , Variables.NPCAddonImages1, Variables.NPCAddonImages2, Variables.NPCAddonImages3,Variables.NPCAddonImages4, Variables.NPCAddonImages5,Variables.NPCAddonImages6, Variables.NPCAddonImages7 };
            List<List<Texture2D>> NPC00ImagesList = 
                new List<List<Texture2D>> {Variables.NPC00Images0 , Variables.NPC00Images1, Variables.NPC00Images2, Variables.NPC00Images3,Variables.NPC00Images4, Variables.NPC00Images5,Variables.NPC00Images6, Variables.NPC00Images7 };

            for (int i = 0; i < animFolderPath.Count && i < NPCImagesList.Count; i++)
            {
                string newNPCPath = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/") + 1) + animFolderPath[i];
                Import.AddNPCAll(newNPCPath, NPCImagesList[i]);
                Import.AddNPCAddonAll(newNPCPath, NPCAddonImagesList[i]);
                Import.AddNPC00All(newNPCPath, NPC00ImagesList[i]);

                //给底下的anim图片设置名称
                string objectName = "Anim" + (i + 1);
                GameObject qaQuad00 = GameObject.Find(objectName)?.gameObject;
                if (qaQuad00 != null) {
                    Text text = qaQuad00.GetComponent<Text>();
                    if (text != null) {
                        text.text = animFolderPath[i];
                    }
                }
            }

            GameObject showObject = GameObject.Find("Show");

            if(Variables.aAnimAll)
                {
                    GameObject.Find("anim_all_name").transform.localScale = new Vector3(1, 1, 1);

                    Transform qaQuad = showObject.transform.Find("QA_Quad");
                        if (qaQuad != null)
                        {
                            qaQuad.position = new Vector3(-3.36f,2.56f, 1.4f);
                            qaQuad.localScale = new Vector3(3, 3, 3);
                            
                        }
                    Transform qaQuad1 = showObject.transform.Find("QA_Quad_Anim1");
                        if (qaQuad1 != null)
                        {
                            qaQuad1.position = new Vector3(-0.13f,2.56f, 1.4f);
                            qaQuad1.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad2 = showObject.transform.Find("QA_Quad_Anim2");
                        if (qaQuad2 != null)
                        {
                            qaQuad2.position = new Vector3(3.16f, 2.56f, 1.4f);
                            qaQuad2.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad3 = showObject.transform.Find("QA_Quad_Anim3");
                        if (qaQuad3 != null)
                        {
                            qaQuad3.position = new Vector3(6.33f, 2.56f, 1.4f);
                            qaQuad3.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad4 = showObject.transform.Find("QA_Quad_Anim4");
                        if (qaQuad4 != null)
                        {
                            qaQuad4.position = new Vector3(-3.36f, -2.12f, 1.4f);
                            qaQuad4.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad5 = showObject.transform.Find("QA_Quad_Anim5");
                        if (qaQuad5 != null)
                        {
                            qaQuad5.position = new Vector3(-0.13f, -2.12f, 1.4f);
                            qaQuad5.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad6 = showObject.transform.Find("QA_Quad_Anim6");
                        if (qaQuad6 != null)
                        {
                            qaQuad6.position = new Vector3(3.16f, -2.12f, 1.4f);
                            qaQuad6.localScale = new Vector3(3, 3, 3);
                        }
                    Transform qaQuad7 = showObject.transform.Find("QA_Quad_Anim7");
                        if (qaQuad7 != null)
                        {
                            qaQuad7.position = new Vector3(6.33f, -2.12f, 1.4f);
                            qaQuad7.localScale = new Vector3(3, 3, 3);
                        }
                }
                else
                {
                    GameObject.Find("anim_all_name").transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

                    Transform qaQuad = showObject.transform.Find("QA_Quad");
                        if (qaQuad != null)
                        {
                            qaQuad.position = new Vector3(0.19f, 0.02f, 1.4f);
                            qaQuad.localScale = new Vector3(8, 8, 8);
                        }
                    Transform qaQuad1 = showObject.transform.Find("QA_Quad_Anim1");
                        if (qaQuad1 != null)
                        {
                            qaQuad1.position = new Vector3(2.30f, 2.118f, 1.4f);
                            qaQuad1.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad2 = showObject.transform.Find("QA_Quad_Anim2");
                        if (qaQuad2 != null)
                        {
                            qaQuad2.position = new Vector3(-1.854f, -1.99f, 1.4f);
                            qaQuad2.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad3 = showObject.transform.Find("QA_Quad_Anim3");
                        if (qaQuad3 != null)
                        {
                            qaQuad3.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad3.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad4 = showObject.transform.Find("QA_Quad_Anim4");
                        if (qaQuad4 != null)
                        {
                            qaQuad4.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad4.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad5 = showObject.transform.Find("QA_Quad_Anim5");
                        if (qaQuad5 != null)
                        {
                            qaQuad5.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad5.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad6 = showObject.transform.Find("QA_Quad_Anim6");
                        if (qaQuad6 != null)
                        {
                            qaQuad6.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad6.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad7 = showObject.transform.Find("QA_Quad_Anim7");
                        if (qaQuad7 != null)
                        {
                            qaQuad7.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad7.localScale = new Vector3(0, 0, 0);
                        }
                }
            
        }
    }
    /**
        选择全部 00、10、20、30方向
    */
    
    public static void ChooseForceAll()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.playMode == 0)
        {
            Variables.aForceAll = !Variables.aForceAll;

            GameObject showObject = GameObject.Find("Show");
            if(Variables.aForceAll)
                {
                    Transform qaQuad = showObject.transform.Find("QA_Quad");
                        if (qaQuad != null)
                        {
                            qaQuad.position = new Vector3(-1.854f, 2.118f, 1.4f);
                            qaQuad.localScale = new Vector3(4, 4, 4);
                        }

                    Transform qaQuad1 = showObject.transform.Find("QA_Quad1");
                        if (qaQuad1 != null)
                        {
                            qaQuad1.position = new Vector3(2.30f, 2.118f, 1.4f);
                            qaQuad1.localScale = new Vector3(4, 4, 4);
                        }
                    Transform qaQuad2 = showObject.transform.Find("QA_Quad2");
                        if (qaQuad2 != null)
                        {
                            qaQuad2.position = new Vector3(-1.854f, -1.99f, 1.4f);
                            qaQuad2.localScale = new Vector3(4, 4, 4);
                        }
                    Transform qaQuad3 = showObject.transform.Find("QA_Quad3");
                        if (qaQuad3 != null)
                        {
                            qaQuad3.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad3.localScale = new Vector3(4, 4, 4);
                        }
                }
                else
                {
                    Transform qaQuad = showObject.transform.Find("QA_Quad");
                        if (qaQuad != null)
                        {
                            qaQuad.position = new Vector3(0.19f, 0.02f, 1.4f);
                            qaQuad.localScale = new Vector3(8, 8, 8);
                        }

                    Transform qaQuad1 = showObject.transform.Find("QA_Quad1");
                        if (qaQuad1 != null)
                        {
                            //qaQuad1.position = new Vector3(2.30f, 2.118f, 1.4f);
                            qaQuad1.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad2 = showObject.transform.Find("QA_Quad2");
                        if (qaQuad2 != null)
                        {
                            qaQuad2.position = new Vector3(-1.854f, -1.99f, 1.4f);
                            qaQuad2.localScale = new Vector3(0, 0, 0);
                        }
                    Transform qaQuad3 = showObject.transform.Find("QA_Quad3");
                        if (qaQuad3 != null)
                        {
                            qaQuad3.position = new Vector3(2.30f, -1.99f, 1.4f);
                            qaQuad3.localScale = new Vector3(0, 0, 0);
                        }

                }

            
            /*Control.mat.SetFloat("_KPointOffsetMult",1f);
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }*/
                
        }
        else if(Variables.playMode == 1)
        {
        }
        else if(Variables.playMode == 2)
        {
        }
    }

    public static void ChooseForce0()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.force = "00";
        //string force0 = "00";
        if(Variables.playMode == 0)
        {
            Control.mat.SetFloat("_KPointOffsetMult",1f);
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            
        }
        else if(Variables.playMode == 1)
        {
            Control.matContrast.SetFloat("_KPointOffsetMult",1f);
            if(Variables.NPCPath != null)
            {
                //Debug.Log(Variables.NPCPath);
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCOverlapPath != null)
            {
                Debug.Log(Variables.NPCOverlapPath);
                Import.AddNPCOverlap(Variables.NPCOverlapPath);
            }
            RefreshData();
            
        }
        else if(Variables.playMode == 2)
        {
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCCharacterPath != null)
            {
                Import.AddNPCCharacter(Variables.NPCCharacterPath);
            }    
        }
    }

    public static void ChooseForce1()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.force = "10";
        if(Variables.playMode == 0)
        {
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);

            }
        }
        else if(Variables.playMode == 1)
        {
            if(Variables.NPCPath != null)
            {
                Debug.Log(Variables.NPCPath);
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCOverlapPath != null)
            {
                Debug.Log(Variables.NPCOverlapPath);
                Import.AddNPCOverlap(Variables.NPCOverlapPath);
            }
            RefreshData();
        }
        else if(Variables.playMode == 2)
        {
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCCharacterPath != null)
            {
                Import.AddNPCCharacter(Variables.NPCCharacterPath);
            }
        }
    }

    public static void ChooseForce2()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.force = "20";
        
        if(Variables.playMode == 0)
        {
            if(Variables.bVert == true)
            {
                Control.mat.SetFloat("_KPointOffsetMult",-1f);
            }
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
        }
        else if(Variables.playMode == 1)
        {
            if(Variables.bVert == true)
            {
                Control.matContrast.SetFloat("_KPointOffsetMult",-1f);
            }
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCOverlapPath != null)
            {
                Debug.Log(Variables.NPCOverlapPath);
                Import.AddNPCOverlap(Variables.NPCOverlapPath);
            }
            RefreshData();
            
        }
        else if(Variables.playMode == 2)
        {
            if(Variables.bodyPath != null)
            {
                Import.AddBody(Variables.bodyPath , Variables.bodyDepthPath);
                Import.AddHead(Variables.headPath , Variables.headDepthPath);
                Import.AddWeapon(Variables.weaponPath , Variables.weaponDepthPath);
                Import.AddWeaponEffect(Variables.weaponEffectPath);
                Import.AddGem(Variables.gemPath);
            }
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCCharacterPath != null)
            {
                Import.AddNPCCharacter(Variables.NPCCharacterPath);
            }
            if(Variables.bodyCharacterPath != null)
            {
                Import.AddBodyCharacter(Variables.bodyCharacterPath, Variables.bodyDepthCharacterPath);
                Import.AddHeadCharacter(Variables.headCharacterPath, Variables.headDepthCharacterPath);
                Import.AddWeaponCharacter(Variables.weaponCharacterPath, Variables.weaponDepthCharacterPath);
                Import.AddWeaponEffectCharacter(Variables.weaponEffectCharacterPath);
                Import.AddGemCharacter(Variables.gemCharacterPath);  
            }         
            
        }
    }

    public static void ChooseForce3()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.force = "30";
        if(Variables.playMode == 0)
        {
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
        }
        else if(Variables.playMode == 1)
        {
            if(Variables.NPCPath != null)
            {
                Debug.Log(Variables.NPCPath);
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCOverlapPath != null)
            {
                Debug.Log(Variables.NPCOverlapPath);
                Import.AddNPCOverlap(Variables.NPCOverlapPath);
            }
            
            RefreshData();
        }
        else if(Variables.playMode == 2)
        {
            if(Variables.NPCPath != null)
            {
                Import.AddNPC(Variables.NPCPath);
                Import.AddNPCAddon(Variables.NPCPath);
                Import.AddNPC00(Variables.NPCPath);
            }
            if(Variables.NPCCharacterPath != null)
            {
                Import.AddNPCCharacter(Variables.NPCCharacterPath);
            }
        }
    }


    /**
        切换重叠对比模式
    */
    public static void ModeOverlapContrast()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.playMode == 0)
        {
            Variables.playMode = 1;
            CloseMain();
            ShowOverlap();
        }
        else if(Variables.playMode == 2)
        {
            Variables.playMode = 1;
            CloseCharacter();
            ShowOverlap(); 
        }
        else if(Variables.playMode == 1)
        {
            Variables.playMode = 0;
            CloseOverlap();
            ShowMain();
        }
            
    }

    /**
        导入NPC重叠对比资产
    */
    public static void ModeOverlapContrastImportNPC()
    {
        Variables.bOverlapNPC = true;

        Import.OpenOverlapFolder(Control.matOverlap, Control.matContrast);
        Variables.overlapWidth = Variables.NPCOverlapImages[0].width;
        Variables.overlapHeight = Variables.NPCOverlapImages[0].height;
        RefreshData();

        Control.matOverlap.SetTexture("_Body",Variables.blackTex);
        Control.matOverlap.SetTexture("_Head",Variables.blackTex);
        Control.matOverlap.SetTexture("_Weapon",Variables.blackTex);
        Control.matOverlap.SetTexture("_BodyDepth",Variables.blackTex);
        Control.matOverlap.SetTexture("_HeadDepth",Variables.blackTex);
        Control.matOverlap.SetTexture("_WeaponDepth",Variables.blackTex);
        
        Control.matContrast.SetTexture("_BodyOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_HeadOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_WeaponOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_WeaponEffectOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_WeaponGemOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_BodyDepthOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_HeadDepthOverlap",Variables.blackTex);
        Control.matContrast.SetTexture("_WeaponDepthOverlap",Variables.blackTex);
    }

    /**
        切换透明对比模式
    */
    public static void ChooseAlphaMode()
    {
        Variables.bAlphaMode = true;
        Variables.bSoldMode = false;
        Variables.bDiffMode = false;
        Variables.bPixelDiffMode = false;

        Control.matContrast.SetFloat("_SoldMult",0);
        Control.matContrast.SetFloat("_AlphaMult",1);
        Control.matContrast.SetFloat("_PixelDiffMult",0);
        Control.matContrast.SetFloat("_DiffMult",0);
        GameObject.Find("混合阈值").GetComponentInChildren<Text>().text = "透明混合阈值";
        GameObject.Find("混合阈值").GetComponentInChildren<Slider>().value = 0.5f;
    }

    /**
        切换实体对比模式
    */
    public static void ChooseSoldMode()
    {
        Variables.bAlphaMode = false;
        Variables.bSoldMode = true;
        Variables.bDiffMode = false;
        Variables.bPixelDiffMode = false;

        Control.matContrast.SetFloat("_SoldMult",1);
        Control.matContrast.SetFloat("_AlphaMult",0);
        Control.matContrast.SetFloat("_PixelDiffMult",0);
        Control.matContrast.SetFloat("_DiffMult",0);
        GameObject.Find("混合阈值").GetComponentInChildren<Text>().text = "前后混合阈值";
        GameObject.Find("混合阈值").GetComponentInChildren<Slider>().value = 1.0f;
    }

    /**
        切换像素对比模式
    */
    public static void ChoosePixelDiffMode()
    {
        Variables.bAlphaMode = false;
        Variables.bSoldMode = false;
        Variables.bDiffMode = false;
        Variables.bPixelDiffMode = true;

        Control.matContrast.SetFloat("_SoldMult",0);
        Control.matContrast.SetFloat("_AlphaMult",0);
        Control.matContrast.SetFloat("_PixelDiffMult",1);
        Control.matContrast.SetFloat("_DiffMult",0);
        GameObject.Find("混合阈值").GetComponentInChildren<Text>().text = "异同混合阈值";
        GameObject.Find("混合阈值").GetComponentInChildren<Slider>().value = 0.0f;
    }

    /**
        切换差异对比模式
    */
    public static void ChooseDiffMode()
    {
        Variables.bAlphaMode = false;
        Variables.bSoldMode = false;
        Variables.bDiffMode = true;
        Variables.bPixelDiffMode = false;

        Control.matContrast.SetFloat("_SoldMult",0);
        Control.matContrast.SetFloat("_AlphaMult",0);
        Control.matContrast.SetFloat("_PixelDiffMult",0);
        Control.matContrast.SetFloat("_DiffMult",1);
        GameObject.Find("混合阈值").GetComponentInChildren<Text>().text = "交差混合阈值";
        GameObject.Find("混合阈值").GetComponentInChildren<Slider>().value = 0.0f;
    }

    /**
        重叠对比模式下，左右资产对调
    */
    public static void RLSwitch()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        
        if(Variables.bSwitch == false)
        {
            Variables.bSwitch = true;
        }
        else if(Variables.bSwitch == true)
        {
            Variables.bSwitch = false;
        }
    }

    /**
        重叠对比模式下，图层切换
    */
    public static void CoverageSwitch()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.bCoverage == false)
        {
            Control.matContrast.SetInt("_Coverage",0);
            Variables.bCoverage = true;
        }
        else if(Variables.bCoverage == true)
        {
            Control.matContrast.SetInt("_Coverage",1);
            Variables.bCoverage = false;
        }
        
    }

    /**
        切换角色对比模式
    */
    public static void ChooseModeCharacterContrast()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.playMode == 0)
        {
            Variables.playMode = 2;
            CloseMain();
            ShowCharacter();
        }
        else if(Variables.playMode == 1)
        {
            Variables.playMode = 2;
            CloseOverlap();
            ShowCharacter();
        }
        else if(Variables.playMode == 2)
        {
            Variables.playMode = 0;
            CloseCharacter();
            ShowMain();
        }

    }

    /**
        角色对比模式导入主角
    */
    public static void ModeCharacterContrastImportCharacter()
    {
        Variables.bCharacterNPC = false;

        Import.OpenCharacterFolder(Control.matCharacter);
        GameObject.Find("角色对比导入选择2").transform.localScale = new Vector3(0,0,0);

        Control.matCharacter.SetTexture("_NPC",Variables.blackTex);
    }

    /**
        角色对比模式导入NPC
    */
    public static void ModeCharacterContrastImportNPC()
    {
        Variables.bCharacterNPC = true;

        Import.OpenCharacterFolder(Control.matCharacter);
        GameObject.Find("角色对比导入选择2").transform.localScale = new Vector3(0,0,0);

        Control.matCharacter.SetTexture("_Body",Variables.blackTex);
        Control.matCharacter.SetTexture("_Head",Variables.blackTex);
        Control.matCharacter.SetTexture("_Weapon",Variables.blackTex);
        Control.matCharacter.SetTexture("_BodyDepth",Variables.blackTex);
        Control.matCharacter.SetTexture("_HeadDepth",Variables.blackTex);
        Control.matCharacter.SetTexture("_WeaponDepth",Variables.blackTex);
    }

    /**
        重新导入角色对比模式资产
    */
    public static void ReimportCharacter()
    {
        GameObject.Find("角色对比导入选择2").transform.localScale = new Vector3(1,1,1);
    }

    /**
        显示、关闭K点
    */
    public static void SetKPointView()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.KPointView == 1)
        {
            Variables.KPointView = 0;
            Control.mat.SetFloat("_KPointView",0f);
            Control.matOverlap.SetFloat("_KPointView",0f);
            Control.matContrast.SetFloat("_KPointView",0f);
            Control.matCharacter.SetFloat("_KPointView",0f);
        }
        else if(Variables.KPointView == 0)
        {
            Variables.KPointView = 1;
            Control.mat.SetFloat("_KPointView",1f);
            Control.matOverlap.SetFloat("_KPointView",1f);
            Control.matContrast.SetFloat("_KPointView",1f);
            Control.matCharacter.SetFloat("_KPointView",1f);
        }
    }

    /**
        K点复位
    */
    public static void ResetKPoint()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.playMode == 0)
        {
            if(Variables.NPCMode == false)
            {
                Variables.KPointOffsetAdd = Variables.KPointOffsetJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x ;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPCJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
        }
        if(Variables.playMode == 1)
        {
            RefreshData();
            if(Variables.NPCMode == false)
            {
                Variables.KPointOffsetAdd = Variables.KPointOffsetJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAdd.x;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAdd.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPCJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("绝对偏移X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
        }

        Variables.bReset = true;
    }

    /**
        选择颜色
    */
    public static void ChooseColor()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        Variables.bChoose = false;

        Variables.bChooseColor = true;
        GameObject.Find("颜色选择器").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("退出颜色选择").transform.localScale = new Vector3(1,1,1);

        Control.mat.SetTexture("_BackPic",Variables.blackTex);
        Control.matOverlap.SetTexture("_BackPic",Variables.blackTex);
        Control.matCharacter.SetTexture("_BackPic",Variables.blackTex);
        Control.matContrast.SetTexture("_BackPic",Variables.blackTex);

        Control.matContrast.SetFloat("_BackPicMult",0);
        Control.matOverlap.SetFloat("_BackPicMult",0);
        Control.matCharacter.SetFloat("_BackPicMult",0);
        Control.matExportGIF.SetFloat("_BackPicMult",0);
        Control.matExportGIFNPC.SetFloat("_BackPicMult",0);
        Control.mat.SetFloat("_BackPicMult",0);
    }

    /**
        关闭颜色色盘
    */
    public static void CloseColorBar()
    {
        Variables.bChoose = true;
        Variables.bChooseColor = false;
        
        GameObject.Find("退出颜色选择").transform.localScale = new Vector3(0,0,0);
    }

    /**
        选择灰色
    */
    public static void ChooseGray()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        Variables.bChoose = false;

        Variables.bChooseGray = true;
        GameObject.Find("灰度选择器").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("退出灰度选择").transform.localScale = new Vector3(1,1,1);

        Control.mat.SetTexture("_BackPic",Variables.blackTex);
        Control.matOverlap.SetTexture("_BackPic",Variables.blackTex);
        Control.matCharacter.SetTexture("_BackPic",Variables.blackTex);
        Control.matContrast.SetTexture("_BackPic",Variables.blackTex);

        Control.matContrast.SetFloat("_BackPicMult",0);
        Control.matOverlap.SetFloat("_BackPicMult",0);
        Control.matCharacter.SetFloat("_BackPicMult",0);
        Control.matExportGIF.SetFloat("_BackPicMult",0);
        Control.matExportGIFNPC.SetFloat("_BackPicMult",0);
        Control.mat.SetFloat("_BackPicMult",0);
    }

    /**
        关闭灰色色盘
    */
    public static void CloseGrayBar()
    {
        Variables.bChoose = true;
        Variables.bChooseGray = false;
        
        GameObject.Find("退出灰度选择").transform.localScale = new Vector3(0,0,0);
    }

    /**
        像素矫正
    */
    public static void PixelCorrection()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.bCorrection = true;

        if(Variables.playMode == 0)
        {
            if(Variables.NPCMode == false)
            {
                if(Variables.bodyImages.Count != 0)
                {
                    Variables.scale = new Vector4(Variables.bodyImages[0].width / Variables.standardPixel.x , Variables.bodyImages[0].height / Variables.standardPixel.y,0,0);
                    Control.mat.SetVector("_Scale",Variables.scale);
                    Control.matContrast.SetVector("_Scale",Variables.scale);
                }
            }
            
            if(Variables.NPCMode == true)
            {
                if(Variables.NPCImages.Count != 0)
                {
                    Variables.scaleNPC = new Vector4(Variables.NPCImages[0].width / Variables.standardPixel.x , Variables.NPCImages[0].height / Variables.standardPixel.y,0,0);
                    Control.mat.SetVector("_ScaleNPC",Variables.scaleNPC);
                    Control.matContrast.SetVector("_ScaleNPC",Variables.scaleNPC);
                }
            }
        }
        if(Variables.playMode == 1)
        {
            RefreshData();
            if(Variables.NPCMode == false)
            {
                if(Variables.bodyImages.Count != 0)
                {
                    Variables.scale = new Vector4(Variables.bodyImages[0].width / Variables.standardPixel.x , Variables.bodyImages[0].height / Variables.standardPixel.y,0,0);
                    Control.mat.SetVector("_Scale",Variables.scale);
                    Control.matContrast.SetVector("_Scale",Variables.scale);
                }
            }
            
            if(Variables.NPCMode == true)
            {
                if(Variables.NPCImages.Count != 0)
                {
                    Variables.scaleNPC = new Vector4(Variables.NPCImages[0].width / Variables.standardPixel.x , Variables.NPCImages[0].height / Variables.standardPixel.y,0,0);
                    Control.mat.SetVector("_ScaleNPC",Variables.scaleNPC);
                    Control.matContrast.SetVector("_ScaleNPC",Variables.scaleNPC);
                }
            }
        }
    }

    /**
        角色缩放
    */
    public static void CharacterRescale()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.playMode == 0)
        {
            /*if(Variables.NPCMode == false)
            {
                if(Variables.bodyImages.Count != 0)
                {
                    Variables.scaleRescale = new Vector2(Variables.characterRescaleX / Variables.standardPixel.x,Variables.characterRescaleY / Variables.standardPixel.y);
                    Variables.KPointOffsetRescale = new Vector4(0,(Variables.standardKy-(Variables.characterRescaleY * (Variables.standardKy / Variables.standardPixel.y) + (Variables.standardPixel.y - Variables.characterRescaleY) / 2)) / Variables.standardPixel.y / (Variables.characterRescaleY / Variables.standardPixel.y),0,0);
                    Control.mat.SetVector("_ScaleRescale",Variables.scaleRescale);
                    Control.mat.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
                    Control.matContrast.SetVector("_ScaleRescale",Variables.scaleRescale);
                    Control.matContrast.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
                }
            }*/
            
            if(Variables.NPCMode == true)
            {
                if(Variables.NPCImages.Count != 0)
                {
                    Variables.scaleNPCRescale = new Vector2(Variables.characterRescaleX / Variables.standardPixel.x,Variables.characterRescaleY / Variables.standardPixel.y);
                    Variables.KPointOffsetNPCRescale = new Vector4(0,(Variables.standardKy-(Variables.characterRescaleY * (Variables.standardKy / Variables.standardPixel.y) + (Variables.standardPixel.y - Variables.characterRescaleY) / 2)) / Variables.standardPixel.y / (Variables.characterRescaleY / Variables.standardPixel.y) ,0,0);
                    Control.mat.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);          
                    Control.mat.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);
                    Control.matContrast.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);
                    Control.matContrast.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);
                }
            }
        }
        if(Variables.playMode == 1)
        {
            RefreshData();
            if(Variables.NPCMode == false)
            {
                if(Variables.bodyImages.Count != 0)
                {
                    Variables.scaleRescale = new Vector2(Variables.characterRescaleX / Variables.standardPixel.x,Variables.characterRescaleY / Variables.standardPixel.y);
                    Variables.KPointOffsetRescale = new Vector4(0,(Variables.standardKy-(Variables.characterRescaleY * (Variables.standardKy / Variables.standardPixel.y) + (Variables.standardPixel.y - Variables.characterRescaleY) / 2)) / Variables.standardPixel.y / (Variables.characterRescaleY / Variables.standardPixel.y),0,0);
                    Control.mat.SetVector("_ScaleRescale",Variables.scaleRescale);
                    Control.mat.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
                    Control.matContrast.SetVector("_ScaleRescale",Variables.scaleRescale);
                    Control.matContrast.SetVector("_KPointOffsetRescale",Variables.KPointOffsetRescale);
                }
            }
            
            if(Variables.NPCMode == true)
            {
                if(Variables.NPCImages.Count != 0)
                {
                    Variables.scaleNPCRescale = new Vector2(Variables.characterRescaleX / Variables.standardPixel.x,Variables.characterRescaleY / Variables.standardPixel.y);
                    Variables.KPointOffsetNPCRescale = new Vector4(0,(Variables.standardKy-(Variables.characterRescaleY * (Variables.standardKy / Variables.standardPixel.y) + (Variables.standardPixel.y - Variables.characterRescaleY) / 2)) / Variables.standardPixel.y / (Variables.characterRescaleY / Variables.standardPixel.y) ,0,0);
                    Control.mat.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);          
                    Control.mat.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);
                    Control.matContrast.SetVector("_ScaleNPCRescale",Variables.scaleNPCRescale);
                    Control.matContrast.SetVector("_KPointOffsetNPCRescale",Variables.KPointOffsetNPCRescale);
                }
            }
        }
    }

    /**
        选择主角模式
    */
    /*public static void ChooseCharacter()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        GameObject.Find("主角").transform.SetSiblingIndex(3);
        GameObject.Find("主角").transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        GameObject.Find("主角").GetComponent<Image>().color = new Vector4(1,1,1,1);
        GameObject.Find("NPC").GetComponent<Image>().color = new Vector4(0.7f,0.7f,0.7f,1);
        GameObject.Find("NPC").transform.localScale = new Vector3(1f,1f,1f);
        GameObject.Find("主角选择按钮").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("NPC选择按钮").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("重叠控制器/左右对调").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("重叠控制器/导入NPC").transform.localScale = new Vector3(1,1,1);
        Variables.bSwitch = false;
        
        if(Variables.bodyImages.Count != 0 && Variables.headImages.Count != 0 && Variables.weaponImages.Count != 0)
        {
            if(Variables.validBody == true)
            {
                Variables.bodyPath = Variables.bodyPath.Remove(Variables.bodyPath.LastIndexOf(@"/") + 1) + Variables.animName;
                Variables.bodyDepthPath = Variables.bodyDepthPath.Remove(Variables.bodyDepthPath.LastIndexOf(@"/") + 1) + Variables.animName;
            }
            
            if(Variables.validHead == true)
            {
                Variables.headPath = Variables.headPath.Remove(Variables.headPath.LastIndexOf(@"/") + 1) + Variables.animName;
                Variables.headDepthPath = Variables.headDepthPath.Remove(Variables.headDepthPath.LastIndexOf(@"/") + 1) + Variables.animName;
            }
            
            if(Variables.validWeapon == true)
            {
                Variables.weaponPath = Variables.weaponPath.Remove(Variables.weaponPath.LastIndexOf(@"/") + 1) + Variables.animName;
                Variables.weaponDepthPath = Variables.weaponDepthPath.Remove(Variables.weaponDepthPath.LastIndexOf(@"/") + 1) + Variables.animName;
            }
            
            if(Variables.validWeaponEffect == true)
            {
                Variables.weaponEffectPath = Variables.weaponPath.Remove(Variables.weaponEffectPath.LastIndexOf(@"/") + 1) + Variables.animName;
            }
            if(Variables.validGem == true)
            {
                Variables.gemPath = Variables.gemPath.Remove(Variables.gemPath.LastIndexOf(@"/") + 1) + Variables.animName;
            }
            
            Import.AddBody(Variables.bodyPath,Variables.bodyDepthPath);
            Import.AddHead(Variables.headPath,Variables.headDepthPath);
            Import.AddWeapon(Variables.weaponPath,Variables.weaponDepthPath);
            Import.AddWeaponEffect(Variables.weaponEffectPath);
            Import.AddGem(Variables.gemPath);
        }

        Control.mat.SetTexture("_NPC",  Variables.blackTex);
        Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
        Control.matContrast.SetTexture("_NPC",  Variables.blackTex);
        ResetOffset();

        Variables.NPCMode = false;
    }

    /**
        选择NPC模式
    */
    public static void ChooseNPC()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        GameObject.Find("NPC").transform.SetSiblingIndex(3);
        GameObject.Find("NPC").transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        GameObject.Find("NPC").GetComponent<Image>().color = new Vector4(1,1,1,1);
        GameObject.Find("主角").GetComponent<Image>().color = new Vector4(0.7f,0.7f,0.7f,1);
        GameObject.Find("主角").transform.localScale = new Vector3(1f,1f,1f);
        GameObject.Find("NPC选择按钮").transform.localScale = new Vector3(1,1,1);
        //GameObject.Find("主角选择按钮").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("重叠控制器/左右对调").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("重叠控制器/导入NPC").transform.localScale = new Vector3(1,1,1);
        
        Variables.animName = "stand";
        GameObject.Find("动作/attack").GetComponentInChildren<Text>().text = "stand";
        
        if(Variables.NPCImages.Count != 0)
        {   
            Variables.NPCPath = Variables.NPCPath.Remove(Variables.NPCPath.LastIndexOf(@"/") + 1) + Variables.animName;

            Import.AddNPC(Variables.NPCPath);
            Import.AddNPCAddon(Variables.NPCPath);
            Import.AddNPC00(Variables.NPCPath);
        }

        /*Control.mat.SetTexture("_Body" ,  Variables.blackTex);
        Control.mat.SetTexture("_Head" ,  Variables.blackTex);
        Control.mat.SetTexture("_Weapon" ,  Variables.blackTex);
        Control.mat.SetTexture("_BodyDepth" ,  Variables.blackTex);
        Control.mat.SetTexture("_HeadDepth" ,  Variables.blackTex);
        Control.mat.SetTexture("_WeaponDepth" ,  Variables.blackTex);
        Control.mat.SetTexture("_WeaponEffect" , Variables.blackTex);
        Control.mat.SetTexture("_WeaponGem" , Variables.blackTex);

        Control.matOverlap.SetTexture("_Body" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_Head" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_Weapon" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_BodyDepth" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_HeadDepth" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_WeaponDepth" ,  Variables.blackTex);
        Control.matOverlap.SetTexture("_WeaponEffect" , Variables.blackTex);
        Control.matOverlap.SetTexture("_WeaponGem" , Variables.blackTex);

        Control.matContrast.SetTexture("_Body" ,  Variables.blackTex);
        Control.matContrast.SetTexture("_Head" ,  Variables.blackTex);
        Control.matContrast.SetTexture("_Weapon" ,  Variables.blackTex);
        Control.matContrast.SetTexture("_BodyDepth" ,  Variables.blackTex);
        Control.matContrast.SetTexture("_HeadDepth" ,  Variables.blackTex);
        Control.matContrast.SetTexture("_WeaponDepth" ,  Variables.blackTex);*/

        ResetOffset();

        Variables.NPCMode = true;
    }

    /**
        复位偏移值
    */
    public static void ResetOffset()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        if(Variables.NPCMode == false)
        {
            Variables.KPointOffsetAdd = new Vector4(0,0,0,0);
            Control.mat.SetVector("_KPointOffset",Variables.KPointOffset - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Control.matContrast.SetVector("_KPointOffset",Variables.KPointOffset - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Variables.KPointOffsetABSPosX = 0;
            Variables.KPointOffsetABSPosY = 0;
            GameObject.Find("增量偏移X").GetComponent<InputField>().text = "0";
            GameObject.Find("增量偏移Y").GetComponent<InputField>().text = "0";
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = "0";
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = "0";
            Debug.Log(Variables.KPointOffsetABSPosX + GameObject.Find("绝对偏移X").GetComponent<InputField>().text);
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC = new Vector4(0,0,0,0);
            Control.mat.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Control.matContrast.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Variables.KPointOffsetABSPosX = 0;
            Variables.KPointOffsetABSPosY = 0;
            GameObject.Find("增量偏移X").GetComponent<InputField>().text = "0";
            GameObject.Find("增量偏移Y").GetComponent<InputField>().text = "0";
            GameObject.Find("绝对偏移X").GetComponent<InputField>().text = "0";
            GameObject.Find("绝对偏移Y").GetComponent<InputField>().text = "0";
        }
        Variables.bReset = false;
        
    }

    /**
        添加图片背景
    */
    public static void AddPicBackground()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        Texture2D backImage = new Texture2D(100,100);

        string imgtype = "BMP|JPG|GIF|PNG|TGA";
        string[] ImageType = imgtype.Split('|');
        string backPicPath = OpenFunc.OpenFile("请选择要添加为背景的图片" , ImageType);
        if(backPicPath.Length == 0)
        {
            return;
        }

        if(backPicPath.Remove(backPicPath.LastIndexOf(".")) == "tga")
        {
            Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
            tx = TGALoader.LoadTGA(backPicPath);
            tx.wrapMode = TextureWrapMode.Clamp;
            backImage = tx;
        }
        else
        {
            Texture2D tx = new Texture2D(100 , 100 , TextureFormat.ARGB32 , false);
            tx.LoadImage(GetImages.GetImageByte(backPicPath));
            tx.Apply();
            tx.wrapMode = TextureWrapMode.Clamp;
            //转化成Texture2D添加到列表使用
            backImage = tx;
        }

        Control.mat.SetVector("_BackColor",new Vector4(0,0,0,0));
        Control.matOverlap.SetVector("_BackColor",new Vector4(0,0,0,0));
        Control.matCharacter.SetVector("_BackColor",new Vector4(0,0,0,0));
        Control.matContrast.SetVector("_BackColor",new Vector4(0,0,0,0));

        Control.mat.SetFloat("_BackPicMult",1);
        Control.matOverlap.SetFloat("_BackPicMult",1);
        Control.matCharacter.SetFloat("_BackPicMult",1);
        Control.matContrast.SetFloat("_BackPicMult",1);
        Control.matExportGIF.SetFloat("_BackPicMult",1);
        Control.matExportGIFNPC.SetFloat("_BackPicMult",1);

        Control.mat.SetTexture("_BackPic",backImage);
        Control.matOverlap.SetTexture("_BackPic",backImage);
        Control.matCharacter.SetTexture("_BackPic",backImage);
        Control.matContrast.SetTexture("_BackPic",backImage);
        Control.matExportGIF.SetTexture("_BackPic",backImage);
        Control.matExportGIFNPC.SetTexture("_BackPic",backImage);
    }

    /**
        开启关闭滚轮缩放
    */
    public static void ScrollScaling()
    {
        if(Variables.bScale == true)
        {
            Variables.bScale = false;
            GameObject.Find("滚轮缩放尺寸/缩放按钮").GetComponentInChildren<Text>().text = "已关闭缩放";
        }
        else if(Variables.bScale == false)
        {
            Variables.bScale = true;
            GameObject.Find("滚轮缩放尺寸/缩放按钮").GetComponentInChildren<Text>().text = "已开启缩放";
        }
    }

    /**
        开启关闭角色对比模式滚轮缩放
    */
    public static void ScrollPicScaling()
    {
        if(Variables.bPicScale == true)
        {
            Variables.bPicScale = false;
            GameObject.Find("点击按钮缩放图像/图像缩放按钮").GetComponentInChildren<Text>().text = "已关闭缩放";
        }
        else if(Variables.bPicScale == false)
        {
            Variables.bPicScale = true;
            GameObject.Find("点击按钮缩放图像/图像缩放按钮").GetComponentInChildren<Text>().text = "已开启缩放";
        }
    }

    /**
        复位角色对比缩放
    */
     public static void CharacterModeReScale()
    {
        Control.mat.SetFloat("_PicScale",1);
        Control.matCharacter.SetFloat("_PicScale",1);
        Variables.picScale = 1;
    }
       
    /*private static void ChooseFunc(string assetType, string assetTypeButton, string mainFolderPath, string part, string partButtonName, GameObject go)
    {
        int btnPos = 0;
        int btnHeight = 30;

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "资产png/" + part);
        List<string> folderDepthPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "资产深度/" + part);

        if(folderPath.Count == 0 || folderDepthPath.Count == 0)
        {
            Variables.bChoose = true;
            Messagebox.MessageBox(IntPtr.Zero,"颜色文件与深度文件数量不一致","确认",0);
        }
        GameObject panel_button = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image");
        button_image.transform.localScale = new Vector3(1,1,1);
        for(int i = 0; i < folderPath.Count; i++)
        {
            GameObject goClone = UnityEngine.Object.Instantiate(go);
            goClone.transform.SetParent(panel_button.transform,true);
            goClone.transform.localScale = new Vector3(1,1,1);
            goClone.transform.localPosition = new Vector3(0,btnPos,0);
            string buttonName = folderPath[i];
            goClone.GetComponentInChildren<Text>().text = buttonName;
            goClone.GetComponent<Button>().onClick.AddListener
            (
                ()=>
                {
                    GameObject goButton = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/000000");
                    goButton.GetComponentInChildren<Text>().text = buttonName;
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
                    
                    #if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    #endif

                    button_image.transform.localScale = new Vector3(0,0,0);
                    string partPath = Variables.mainFolderPath + "/" + assetType + "资产png/" + part + "/" + buttonName + "/" + Variables.animName;
                    string partDepthPath = Variables.mainFolderPath + "/" + assetType + "资产深度/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(part == "body" && assetTypeButton == "主角")
                    {
                        Import.AddBody(partPath, partDepthPath);
                        Variables.bodyPath = partPath;
                        Variables.bodyDepthPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "主角")
                    {
                        Import.AddHead(partPath, partDepthPath);
                        Variables.headPath = partPath;
                        Variables.headDepthPath = partDepthPath;
                    }
                    if(part == "body" && assetTypeButton == "对比主角")
                    {
                        Import.AddBodyOverlap(partPath, partDepthPath);
                        Variables.bodyOverlapPath = partPath;
                        Variables.bodyDepthOverlapPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "对比主角")
                    {
                        Import.AddHeadOverlap(partPath, partDepthPath);
                        Variables.headOverlapPath = partPath;
                        Variables.headDepthOverlapPath = partDepthPath;
                    }

                    if(part == "body")
                    {
                        GetPartJson(partPath);
                        RefreshData();
                    }
                }
            );
            btnPos = btnPos - btnHeight;
        }
    }*/

    /*private static void ChooseFuncNew(string assetType, string assetTypeButton, string mainFolderPath, string part, string partButtonName, GameObject go)
    {
        int btnPos = 0;
        int btnHeight = 30;

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "资产png/" + part);
        List<string> folderDepthPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "资产深度/" + part);

        if(folderPath.Count == 0 || folderDepthPath.Count == 0)
        {
            Variables.bChoose = true;
            Messagebox.MessageBox(IntPtr.Zero,"颜色文件与深度文件数量不一致","确认",0);
        }
        GameObject panel_button = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image");
        button_image.transform.localScale = new Vector3(1,1,1);
        for(int i = 0; i < folderPath.Count; i++)
        {
            GameObject goClone = UnityEngine.Object.Instantiate(go);
            goClone.transform.SetParent(panel_button.transform,true);
            goClone.transform.localScale = new Vector3(1,1,1);
            goClone.transform.localPosition = new Vector3(0,btnPos,0);
            string buttonName = folderPath[i];
            goClone.GetComponentInChildren<Text>().text = buttonName;
            goClone.GetComponent<Button>().onClick.AddListener
            (
                ()=>
                {
                    GameObject goButton = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/000000");
                    goButton.GetComponentInChildren<Text>().text = buttonName;
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
                    
                    #if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    #endif

                    button_image.transform.localScale = new Vector3(0,0,0);
                    string partPath = Variables.mainFolderPath + "/" + assetType + "资产png/" + part + "/" + buttonName + "/" + Variables.animName;
                    string partDepthPath = Variables.mainFolderPath + "/" + assetType + "资产深度/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(part == "body" && assetTypeButton == "主角")
                    {
                        Import.AddBody(partPath, partDepthPath);
                        Variables.bodyPath = partPath;
                        Variables.bodyDepthPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "主角")
                    {
                        Import.AddHead(partPath, partDepthPath);
                        Variables.headPath = partPath;
                        Variables.headDepthPath = partDepthPath;
                    }
                    if(part == "body" && assetTypeButton == "对比主角")
                    {
                        Import.AddBodyOverlap(partPath, partDepthPath);
                        Variables.bodyOverlapPath = partPath;
                        Variables.bodyDepthOverlapPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "对比主角")
                    {
                        Import.AddHeadOverlap(partPath, partDepthPath);
                        Variables.headOverlapPath = partPath;
                        Variables.headDepthOverlapPath = partDepthPath;
                    }

                    if(part == "body")
                    {
                        GetPartJson(partPath);
                        RefreshData();
                    }
                }
            );
            btnPos = btnPos - btnHeight;
        }
    }*/

    /*private static void ChooseFuncSingle(string assetType, string assetTypeButton, string mainFolderPath, string part, string partButtonName, GameObject go)
    {
        int btnPos = 0;
        int btnHeight = 30;

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "资产png/" + part);

        GameObject panel_button = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/Panel/Image");
        button_image.transform.localScale = new Vector3(1,1,1);
        for(int i = 0; i < folderPath.Count; i++)
        {
            GameObject goClone = UnityEngine.Object.Instantiate(go);
            goClone.transform.SetParent(panel_button.transform,true);
            goClone.transform.localScale = new Vector3(1,1,1);
            goClone.transform.localPosition = new Vector3(0,btnPos,0);
            string buttonName = folderPath[i];
            goClone.GetComponentInChildren<Text>().text = buttonName;
            goClone.GetComponent<Button>().onClick.AddListener
            (
                ()=>
                {
                    GameObject goButton = GameObject.Find(assetTypeButton + "选择按钮/" + partButtonName + "/000000");
                    goButton.GetComponentInChildren<Text>().text = buttonName;
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
                    
                    #if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    #endif

                    button_image.transform.localScale = new Vector3(0,0,0);
                    string partPath = Variables.mainFolderPath + "/" + assetType + "资产png/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(assetTypeButton == "主角")
                    {
                        Import.AddGem(partPath);
                        Variables.gemPath = partPath;
                    }
                    if(assetTypeButton == "对比主角")
                    {
                        Import.AddGemOverlap(partPath);
                        Variables.gemOverlapPath = partPath;
                    }
                }
            );
            btnPos = btnPos - btnHeight;
        }
    }*/

    /*private static void GetPartJson(string partPath)
    {
        string jsonPath = partPath.Remove(partPath.LastIndexOf(@"/")) + "/kpoint.json";
        bool bSafeJson = File.Exists(jsonPath);
        if(bSafeJson == true)
        {
            Data_Class dataClass = GetJsonFunc.GetJson(jsonPath);

            float width = float.Parse(dataClass.width);
            Variables.exportWidth = width;
            float height = float.Parse(dataClass.height);
            Variables.exportHeight = height;
            float kx = float.Parse(dataClass.kx);
            Variables.exportKPointX = kx;
            float ky = float.Parse(dataClass.ky);
            Variables.exportKPointY = ky;

            Variables.KPointOffsetJson = new Vector4(-((Variables.standardKx - ((Variables.standardPixel.x - width) / 2 + kx)) / Variables.standardPixel.x / (width / Variables.standardPixel.x)),((Variables.standardKy - ((Variables.standardPixel.y - height) / 2 + ky)) / Variables.standardPixel.y / (height / Variables.standardPixel.y)),0,0);
        }
        if(bSafeJson == false)
        {                       
            Variables.KPointOffsetJson = new Vector4(0,0,0,0);
        }
    
        Variables.exportWidth = Variables.bodyImages[0].width;
        Variables.exportHeight = Variables.bodyImages[0].height;
    }*/
}
