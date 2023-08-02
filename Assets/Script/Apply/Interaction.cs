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
        ͸�����й�����
    */
    public static void OnValueChangeAlphaClip(float value)
    {
        Control.mat.SetFloat("_AlphaCut",value);
    }

    /**
        �����ٶȹ�����
    */
    public static void OnValueChangePlaySpeed(float value)
    {
        Variables.speed = 5 + value * 10;
    }

    /**
        �����ֵ������
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
        ��ʾ���ر�������
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
        ��ʾ���ر��ص��ԱȽ���
    */
    private static void ShowOverlap()
    {
        GameObject.Find("�ص�������").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("Overlap_Quad_Right").transform.localScale = new Vector3(4,4,4);
        GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(7,7,7);
        GameObject.Find("Overlap_Quad_Left").transform.localScale = new Vector3(4,4,4);
        GameObject.Find("���ݶԱ���ʾ").transform.localScale = new Vector3(1,1,1);
        //GameObject.Find("�Ա�����ѡ��ť").transform.localScale = new Vector3(1,1,1);

        GameObject.Find("Canvas/Panel/�ص��Ա�").GetComponentInChildren<Text>().text = "����";
    }

    private static void CloseOverlap()
    {
        GameObject.Find("Overlap_Quad_Right").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Overlap_Quad_Mid").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Overlap_Quad_Left").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("���ݶԱ���ʾ").transform.localScale = new Vector3(0,0,0);
        //GameObject.Find("�Ա�����ѡ��ť").transform.localScale = new Vector3(0,0,0);

        GameObject.Find("Canvas/Panel/�ص��Ա�").GetComponentInChildren<Text>().text = "�ص��Ա�";
        GameObject.Find("�ص�������").transform.localScale = new Vector3(0,0,0);
    }

    /**
        ��ʾ���رս�ɫ�ԱȽ���
    */
    private static void ShowCharacter()
    {
        GameObject.Find("Character_Quad_Right").transform.localScale = new Vector3(7,7,7);
        GameObject.Find("Character_Quad_Left").transform.localScale = new Vector3(7,7,7);
        
        GameObject.Find("��ɫ�Ա�").GetComponentInChildren<Text>().text = "����";
        GameObject.Find("��ɫ������").transform.localScale = new Vector3(1,1,1);
        Control.matCharacter.SetInt("_CutSwitch",1);
        Control.mat.SetInt("_CutSwitch",1);
    }

    private static void CloseCharacter()
    {
        GameObject.Find("Character_Quad_Right").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("Character_Quad_Left").transform.localScale = new Vector3(0,0,0);

        GameObject.Find("��ɫ�Ա�").GetComponentInChildren<Text>().text = "��ɫ�Ա�";
        GameObject.Find("��ɫ������").transform.localScale = new Vector3(0,0,0);
        Control.matCharacter.SetInt("_CutSwitch",0);
        Control.matCharacter.SetFloat("_PicScale",1);
        Control.mat.SetInt("_CutSwitch",0);
        Control.mat.SetFloat("_PicScale",1);
        Variables.picScale = 1;
    }

    /**
        ͨ�����ѡ����ɫ
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
				//Control.colorTexΪɫ��ͼ
			    Variables.pixelColor = Control.colorTex.GetPixel(x + Control.colorTex.width/2 , y + Control.colorTex.height/2);
			}
		}
	}

    /**
        ͨ�����ѡ��Ҷ�
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
				//Control.grayTexΪɫ��ͼ
			    Variables.pixelColor = Control.grayTex.GetPixel(x + Control.grayTex.width/2 , y + Control.grayTex.height/2);
			}
		}
	}

    /**
        �������ס��ť�����ʲ����������ƶ�
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
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
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
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
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
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0.0003f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
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
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0.0001f,0,0,0);
            Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
            float px = Variables.KPointOffsetABSPosX * 1000;
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
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
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC -= new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0,0.0003f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC += new Vector4(0,0.0001f,0,0);
            Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
            float py = Variables.KPointOffsetABSPosY * 1000;
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
            Modify.ButtonApplyNumOffset2();
        }
    }

    /**
        ˢ����ʾ��������
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
            GameObject.Find("������سߴ�").GetComponentInChildren<Text>().text = Variables.exportWidth + "x" + Variables.exportHeight;
            GameObject.Find("�Ա����سߴ�").GetComponentInChildren<Text>().text = Variables.overlapWidth + "x" + Variables.overlapHeight;
            if(Variables.exportWidth != Variables.overlapWidth || Variables.exportHeight != Variables.overlapHeight)
            {
                GameObject.Find("������سߴ�").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"���سߴ��в���","ȷ��",0);
            }
            else
            {
                GameObject.Find("������سߴ�").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }

            GameObject.Find("��ǰ����֡��/���֡��").GetComponentInChildren<Text>().text = Variables.exportForceCount.ToString();
            GameObject.Find("��ǰ����֡��/�Ա�֡��").GetComponentInChildren<Text>().text = Variables.overlapForceCount.ToString();
            if(Variables.exportForceCount != Variables.overlapForceCount)
            {
                GameObject.Find("��ǰ����֡��/���֡��").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"��ǰ��������֡���в���","ȷ��",0);
            }
            else
            {
                GameObject.Find("��ǰ����֡��/���֡��").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }

            GameObject.Find("��ǰ������֡��/���֡��").GetComponentInChildren<Text>().text = Variables.exportCount.ToString();
            GameObject.Find("��ǰ������֡��/�Ա�֡��").GetComponentInChildren<Text>().text = Variables.overlapCount.ToString();
            if(Variables.exportCount != Variables.overlapCount)
            {
                GameObject.Find("��ǰ������֡��/���֡��").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"��ǰ������֡���в���","ȷ��",0);
            }
            else
            {
                GameObject.Find("��ǰ������֡��/���֡��").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }
            GameObject.Find("���K��λ����Ϣ").GetComponentInChildren<Text>().text = (int)Variables.exportKPointX + "," + (int)Variables.exportKPointY;
            GameObject.Find("�Ա�K��λ����Ϣ").GetComponentInChildren<Text>().text = (int)Variables.overlapKPointX + "," + (int)Variables.overlapKPointY;
            if(Variables.exportKPointX != Variables.overlapKPointX || Variables.exportKPointY != Variables.overlapKPointY)
            {
                GameObject.Find("���K��λ����Ϣ").GetComponentInChildren<Text>().color = new Vector4(1f,0.3f,0.36f,1.0f);
                Messagebox.MessageBox(IntPtr.Zero,"K��λ���в���","ȷ��",0);
            }
            else
            {
                GameObject.Find("�Ա�K��λ����Ϣ").GetComponentInChildren<Text>().color = new Vector4(0.3f,1.0f,0.6f,1.0f);
            }
        }
    }

    /**
        �ص��Ա�ģʽ����ɫ�Ա�ģʽ�����߼�
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
        ������ؽ���
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
        ��ɫ���Ҷ�ѡ���߼�
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
                GameObject.Find("��ɫѡ����").transform.localScale = new Vector3(0,0,0);
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
                GameObject.Find("�Ҷ�ѡ����").transform.localScale = new Vector3(0,0,0);
            }
        }
    }

    /**
        ���Ŷ���
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
                Messagebox.MessageBox(IntPtr.Zero, "ȱ��NPCͼƬ", "ȷ��", 0);
            }
        }   
    }

    /**
        ��ͣ���Ŷ���
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
        ���ʱ10����20����ƫ��ֵ�Ƿ�ת
    */
    public static void VertOffset()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        if(Variables.bVert == true)
        {
            GameObject.Find("�Ƿ�ת/��ת").GetComponentInChildren<Text>().text = "δ��ת";
            Variables.bVert = false;
        }
        else if(Variables.bVert == false)
        {
            GameObject.Find("�Ƿ�ת/��ת").GetComponentInChildren<Text>().text = "��ת";
            Variables.bVert = true;
        }
    }


    /**
        ѡ��ȡ����λ�ʲ�
    */
    public static void CancelNPC()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC = !Variables.validNPC;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPCѡ��ť/BASE/000000").GetComponentInChildren<Text>().text = Variables.validNPC ? "Y" : "X";
    }


    public static void CancelNPCAddon()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPCAddon = !Variables.validNPCAddon;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPCѡ��ť/ADDON/000000").GetComponentInChildren<Text>().text = Variables.validNPCAddon ? "Y" : "X";
    }

    public static void CancelNPC00()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC00 = !Variables.validNPC00;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPCѡ��ť/00/000000").GetComponentInChildren<Text>().text = Variables.validNPC00 ? "Y" : "X";
    }

    public static void CancelNPC01()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC01 = !Variables.validNPC01;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPCѡ��ť/01/000000").GetComponentInChildren<Text>().text = Variables.validNPC01 ? "Y" : "X";
    }

    public static void CancelNPC02()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        Variables.validNPC02 = !Variables.validNPC02;
        //Variables.validBodyDepth = false;
        GameObject.Find("NPCѡ��ť/01/000000").GetComponentInChildren<Text>().text = Variables.validNPC02 ? "Y" : "X";
    }

    /**
        ѡ��NPC�ʲ�
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
        GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
        GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";

        Variables.animName = "stand";
        GameObject.Find("����/attack").GetComponentInChildren<Text>().text = "stand";

        Variables.NPCPath = OpenFunc.Open("��ѡ��NPC�ļ���") + "/" + Variables.animName;

        //�������NPC�ʲ�����ʾstand���������µ������
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
        ѡ����
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
            //ɾ��addon��00
            animFolderPath.RemoveAll(filePath => filePath.EndsWith("addon") || filePath.EndsWith("00") || filePath.EndsWith("01"));
        }
        else
        {
            Debug.LogError("����ѡ��NPC�ļ���");
        }


        //�����ɵ�Buttonλ��
        GameObject panel_button = GameObject.Find("����/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * animFolderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * animFolderPath.Count);

        //��ʾ����
        GameObject button_image = GameObject.Find("����/Panel/Image");
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
                    GameObject attackButton = GameObject.Find("����/attack");
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                                Messagebox.MessageBox(IntPtr.Zero, "ȱ��·��", "ȷ��", 0);
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
            //ɾ��addon��00
            animFolderPath.RemoveAll(filePath => filePath.EndsWith("addon") || filePath.EndsWith("00") || filePath.EndsWith("01"));

            


            //���úö��NPC���б�
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

                //�����µ�animͼƬ��������
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
        ѡ��ȫ�� 00��10��20��30����
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
        �л��ص��Ա�ģʽ
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
        ����NPC�ص��Ա��ʲ�
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
        �л�͸���Ա�ģʽ
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
        GameObject.Find("�����ֵ").GetComponentInChildren<Text>().text = "͸�������ֵ";
        GameObject.Find("�����ֵ").GetComponentInChildren<Slider>().value = 0.5f;
    }

    /**
        �л�ʵ��Ա�ģʽ
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
        GameObject.Find("�����ֵ").GetComponentInChildren<Text>().text = "ǰ������ֵ";
        GameObject.Find("�����ֵ").GetComponentInChildren<Slider>().value = 1.0f;
    }

    /**
        �л����ضԱ�ģʽ
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
        GameObject.Find("�����ֵ").GetComponentInChildren<Text>().text = "��ͬ�����ֵ";
        GameObject.Find("�����ֵ").GetComponentInChildren<Slider>().value = 0.0f;
    }

    /**
        �л�����Ա�ģʽ
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
        GameObject.Find("�����ֵ").GetComponentInChildren<Text>().text = "��������ֵ";
        GameObject.Find("�����ֵ").GetComponentInChildren<Slider>().value = 0.0f;
    }

    /**
        �ص��Ա�ģʽ�£������ʲ��Ե�
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
        �ص��Ա�ģʽ�£�ͼ���л�
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
        �л���ɫ�Ա�ģʽ
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
        ��ɫ�Ա�ģʽ��������
    */
    public static void ModeCharacterContrastImportCharacter()
    {
        Variables.bCharacterNPC = false;

        Import.OpenCharacterFolder(Control.matCharacter);
        GameObject.Find("��ɫ�Աȵ���ѡ��2").transform.localScale = new Vector3(0,0,0);

        Control.matCharacter.SetTexture("_NPC",Variables.blackTex);
    }

    /**
        ��ɫ�Ա�ģʽ����NPC
    */
    public static void ModeCharacterContrastImportNPC()
    {
        Variables.bCharacterNPC = true;

        Import.OpenCharacterFolder(Control.matCharacter);
        GameObject.Find("��ɫ�Աȵ���ѡ��2").transform.localScale = new Vector3(0,0,0);

        Control.matCharacter.SetTexture("_Body",Variables.blackTex);
        Control.matCharacter.SetTexture("_Head",Variables.blackTex);
        Control.matCharacter.SetTexture("_Weapon",Variables.blackTex);
        Control.matCharacter.SetTexture("_BodyDepth",Variables.blackTex);
        Control.matCharacter.SetTexture("_HeadDepth",Variables.blackTex);
        Control.matCharacter.SetTexture("_WeaponDepth",Variables.blackTex);
    }

    /**
        ���µ����ɫ�Ա�ģʽ�ʲ�
    */
    public static void ReimportCharacter()
    {
        GameObject.Find("��ɫ�Աȵ���ѡ��2").transform.localScale = new Vector3(1,1,1);
    }

    /**
        ��ʾ���ر�K��
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
        K�㸴λ
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
                GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPCJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
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
                GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC = Variables.KPointOffsetNPCJson;
                Variables.KPointOffsetABSPosX = Variables.KPointOffsetAddNPC.x;
                Variables.KPointOffsetABSPosY = Variables.KPointOffsetAddNPC.y;
                float px = Variables.KPointOffsetABSPosX * 1000;
                float py = Variables.KPointOffsetABSPosY * 1000;
                GameObject.Find("����ƫ��X").GetComponent<InputField>().text = px.ToString();
                GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = py.ToString();
                Modify.ButtonApplyNumOffset2();
            }
        }

        Variables.bReset = true;
    }

    /**
        ѡ����ɫ
    */
    public static void ChooseColor()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        Variables.bChoose = false;

        Variables.bChooseColor = true;
        GameObject.Find("��ɫѡ����").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("�˳���ɫѡ��").transform.localScale = new Vector3(1,1,1);

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
        �ر���ɫɫ��
    */
    public static void CloseColorBar()
    {
        Variables.bChoose = true;
        Variables.bChooseColor = false;
        
        GameObject.Find("�˳���ɫѡ��").transform.localScale = new Vector3(0,0,0);
    }

    /**
        ѡ���ɫ
    */
    public static void ChooseGray()
    {
        if(Variables.bChoose == false)
        {
            return;
        }

        Variables.bChoose = false;

        Variables.bChooseGray = true;
        GameObject.Find("�Ҷ�ѡ����").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("�˳��Ҷ�ѡ��").transform.localScale = new Vector3(1,1,1);

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
        �رջ�ɫɫ��
    */
    public static void CloseGrayBar()
    {
        Variables.bChoose = true;
        Variables.bChooseGray = false;
        
        GameObject.Find("�˳��Ҷ�ѡ��").transform.localScale = new Vector3(0,0,0);
    }

    /**
        ���ؽ���
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
        ��ɫ����
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
        ѡ������ģʽ
    */
    /*public static void ChooseCharacter()
    {
        if(Variables.bChoose == false)
        {
            return;
        }
        GameObject.Find("����").transform.SetSiblingIndex(3);
        GameObject.Find("����").transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        GameObject.Find("����").GetComponent<Image>().color = new Vector4(1,1,1,1);
        GameObject.Find("NPC").GetComponent<Image>().color = new Vector4(0.7f,0.7f,0.7f,1);
        GameObject.Find("NPC").transform.localScale = new Vector3(1f,1f,1f);
        GameObject.Find("����ѡ��ť").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("NPCѡ��ť").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("�ص�������/���ҶԵ�").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("�ص�������/����NPC").transform.localScale = new Vector3(1,1,1);
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
        ѡ��NPCģʽ
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
        GameObject.Find("����").GetComponent<Image>().color = new Vector4(0.7f,0.7f,0.7f,1);
        GameObject.Find("����").transform.localScale = new Vector3(1f,1f,1f);
        GameObject.Find("NPCѡ��ť").transform.localScale = new Vector3(1,1,1);
        //GameObject.Find("����ѡ��ť").transform.localScale = new Vector3(0,0,0);
        GameObject.Find("�ص�������/���ҶԵ�").transform.localScale = new Vector3(1,1,1);
        GameObject.Find("�ص�������/����NPC").transform.localScale = new Vector3(1,1,1);
        
        Variables.animName = "stand";
        GameObject.Find("����/attack").GetComponentInChildren<Text>().text = "stand";
        
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
        ��λƫ��ֵ
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
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
            Debug.Log(Variables.KPointOffsetABSPosX + GameObject.Find("����ƫ��X").GetComponent<InputField>().text);
        }
        else if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC = new Vector4(0,0,0,0);
            Control.mat.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Control.matContrast.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC - new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0));
            Variables.KPointOffsetABSPosX = 0;
            Variables.KPointOffsetABSPosY = 0;
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
        }
        Variables.bReset = false;
        
    }

    /**
        ���ͼƬ����
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
        string backPicPath = OpenFunc.OpenFile("��ѡ��Ҫ���Ϊ������ͼƬ" , ImageType);
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
            //ת����Texture2D��ӵ��б�ʹ��
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
        �����رչ�������
    */
    public static void ScrollScaling()
    {
        if(Variables.bScale == true)
        {
            Variables.bScale = false;
            GameObject.Find("�������ųߴ�/���Ű�ť").GetComponentInChildren<Text>().text = "�ѹر�����";
        }
        else if(Variables.bScale == false)
        {
            Variables.bScale = true;
            GameObject.Find("�������ųߴ�/���Ű�ť").GetComponentInChildren<Text>().text = "�ѿ�������";
        }
    }

    /**
        �����رս�ɫ�Ա�ģʽ��������
    */
    public static void ScrollPicScaling()
    {
        if(Variables.bPicScale == true)
        {
            Variables.bPicScale = false;
            GameObject.Find("�����ť����ͼ��/ͼ�����Ű�ť").GetComponentInChildren<Text>().text = "�ѹر�����";
        }
        else if(Variables.bPicScale == false)
        {
            Variables.bPicScale = true;
            GameObject.Find("�����ť����ͼ��/ͼ�����Ű�ť").GetComponentInChildren<Text>().text = "�ѿ�������";
        }
    }

    /**
        ��λ��ɫ�Ա�����
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

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part);
        List<string> folderDepthPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "�ʲ����/" + part);

        if(folderPath.Count == 0 || folderDepthPath.Count == 0)
        {
            Variables.bChoose = true;
            Messagebox.MessageBox(IntPtr.Zero,"��ɫ�ļ�������ļ�������һ��","ȷ��",0);
        }
        GameObject panel_button = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image");
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
                    GameObject goButton = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/000000");
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
                    string partPath = Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part + "/" + buttonName + "/" + Variables.animName;
                    string partDepthPath = Variables.mainFolderPath + "/" + assetType + "�ʲ����/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(part == "body" && assetTypeButton == "����")
                    {
                        Import.AddBody(partPath, partDepthPath);
                        Variables.bodyPath = partPath;
                        Variables.bodyDepthPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "����")
                    {
                        Import.AddHead(partPath, partDepthPath);
                        Variables.headPath = partPath;
                        Variables.headDepthPath = partDepthPath;
                    }
                    if(part == "body" && assetTypeButton == "�Ա�����")
                    {
                        Import.AddBodyOverlap(partPath, partDepthPath);
                        Variables.bodyOverlapPath = partPath;
                        Variables.bodyDepthOverlapPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "�Ա�����")
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

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part);
        List<string> folderDepthPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "�ʲ����/" + part);

        if(folderPath.Count == 0 || folderDepthPath.Count == 0)
        {
            Variables.bChoose = true;
            Messagebox.MessageBox(IntPtr.Zero,"��ɫ�ļ�������ļ�������һ��","ȷ��",0);
        }
        GameObject panel_button = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image");
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
                    GameObject goButton = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/000000");
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
                    string partPath = Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part + "/" + buttonName + "/" + Variables.animName;
                    string partDepthPath = Variables.mainFolderPath + "/" + assetType + "�ʲ����/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(part == "body" && assetTypeButton == "����")
                    {
                        Import.AddBody(partPath, partDepthPath);
                        Variables.bodyPath = partPath;
                        Variables.bodyDepthPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "����")
                    {
                        Import.AddHead(partPath, partDepthPath);
                        Variables.headPath = partPath;
                        Variables.headDepthPath = partDepthPath;
                    }
                    if(part == "body" && assetTypeButton == "�Ա�����")
                    {
                        Import.AddBodyOverlap(partPath, partDepthPath);
                        Variables.bodyOverlapPath = partPath;
                        Variables.bodyDepthOverlapPath = partDepthPath;
                    }
                    if(part == "head" && assetTypeButton == "�Ա�����")
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

        List<string> folderPath = GetFiles.GetAllFiles(Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part);

        GameObject panel_button = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image/Panel_Button");
        var rectTransform = panel_button.transform.GetComponent<RectTransform>();
        panel_button.transform.localPosition = new Vector3(0,0 - (((btnHeight * folderPath.Count) / 2) - (rectTransform.rect.height / 2)),0);
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width,btnHeight * folderPath.Count);
        GameObject button_image = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/Panel/Image");
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
                    GameObject goButton = GameObject.Find(assetTypeButton + "ѡ��ť/" + partButtonName + "/000000");
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
                    string partPath = Variables.mainFolderPath + "/" + assetType + "�ʲ�png/" + part + "/" + buttonName + "/" + Variables.animName;
                    
                    if(assetTypeButton == "����")
                    {
                        Import.AddGem(partPath);
                        Variables.gemPath = partPath;
                    }
                    if(assetTypeButton == "�Ա�����")
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
