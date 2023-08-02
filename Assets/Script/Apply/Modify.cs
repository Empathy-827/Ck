using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Modify
{
    public static void ButtonApplyNumOffset()
    {
        if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetAddNPC = new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0);
            Variables.KPointOffsetNPC = new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0);
            Control.mat.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC);
            Control.matContrast.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC);
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
            string animName = Variables.animName;
            if(animName == "attack")
            {
                Variables.KPointOffsetAttack = Variables.KPointOffsetNPC;
            }
            if(animName == "die")
            {
                Variables.KPointOffsetDie = Variables.KPointOffsetNPC;
            }
            if(animName == "hit")
            {
                Variables.KPointOffsetHit = Variables.KPointOffsetNPC;
            }
            if(animName == "magic")
            {
                Variables.KPointOffsetMagic = Variables.KPointOffsetNPC;
            }
            if(animName == "stand")
            {
                Variables.KPointOffsetStand = Variables.KPointOffsetNPC;
            }
            if(animName == "walk")
            {
                Variables.KPointOffsetWalk = Variables.KPointOffsetNPC;
            }
        }
    }
    //Ӧ��ƫ��ֵ

    public static void ButtonApplyNumOffset2()
    {
        if(Variables.NPCMode == true)
        {
            Variables.KPointOffsetNPC = new Vector4(Variables.KPointOffsetABSPosX,Variables.KPointOffsetABSPosY,0,0);
            Control.mat.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC);
            Control.matContrast.SetVector("_KPointOffsetNPC",Variables.KPointOffsetNPC);
            GameObject.Find("����ƫ��X").GetComponent<InputField>().text = "0";
            GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = "0";
        }
    }
    //Ӧ��ƫ��ֵ2

    public static void OnValueChangedX(string value)
    {
        float x;
        if(float.TryParse(value , out x))
        {
            Variables.standardPixel.x = x;
        }   
        Debug.Log(Variables.standardPixel.x);
        
    }
    //��������X�����
    public static void OnValueChangedY(string value)
    {
        float y;
        if(float.TryParse(value , out y))
        {
            Variables.standardPixel.y = y;
        }
        Debug.Log(Variables.standardPixel.y);
    }
    //��������Y�����
    public static void OnValueChangedGIFX(string value)
    {
        int x;
        if(int.TryParse(value , out x))
        {
            Variables.GIFWidth = x;
        }
        
    }
    //GIFX�����
    public static void OnValueChangedGIFY(string value)
    {
        int y;
        if(int.TryParse(value , out y))
        {
            Variables.GIFHeight = y;
        }
        
    }
    //GIFY�����
    public static void OnValueChangedGIFQuality(string value)
    {
        int q;
        if(int.TryParse(value , out q))
        {
            Variables.GIFQuality = q;
        }
        
    }
    //GIF�������������
    public static void OnValueChangedGIFSpeed(string value)
    {
        int s;
        if(int.TryParse(value , out s))
        {
            Variables.GIFSpeed = s;
        }
    }
    //GIF���ʵ��������
    public static void OnValueChangedReSamplingWidth(string value)
    {
        int w;
        if(int.TryParse(value , out w))
        {
            Variables.reSamplingWidthNew = w;
        }
    }
    //�ֱ��ʵ���X�����
    public static void OnValueChangedReSamplingHeight(string value)
    {
        int h;
        if(int.TryParse(value , out h))
        {
            Variables.reSamplingHeightNew = h;
        }
        
    }
    //�ֱ��ʵ���Y�����
    public static void OnValueChangedOffsetX(string value)
    {
        float x;
        if(float.TryParse(value , out x))
        {
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC += new Vector4(x / 500,0,0,0);
                Variables.KPointOffsetABSPosX = (Variables.KPointOffsetNPC.x + Variables.KPointOffsetAddNPC.x * 500);
                GameObject.Find("����ƫ��X").GetComponent<InputField>().text = Variables.KPointOffsetABSPosX.ToString();
            }
        }
    }
    //ƫ��X�����
    public static void OnValueChangedOffsetY(string value)
    {
        float y;
        if(float.TryParse(value , out y))
        {
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetAddNPC += new Vector4(0,y / 500,0,0);
                Variables.KPointOffsetABSPosY = (Variables.KPointOffsetNPC.y + Variables.KPointOffsetAddNPC.y * 500);
                GameObject.Find("����ƫ��Y").GetComponent<InputField>().text = Variables.KPointOffsetABSPosY.ToString();
            }
        }
    }
    //ƫ��Y�����
    public static void OnValueChangedAbsPosX(string value)
    {
        float x;
        if(float.TryParse(value , out x))
        {
            Variables.KPointOffsetABSPosX = x / 500;
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetNPC = new Vector4(Variables.KPointOffsetABSPosX, Variables.KPointOffsetNPC.y , 0 , 0);
            }
        }
    }
    //����ƫ��ֵX�����
    public static void OnValueChangedAbsPosY(string value)
    {
        float y;
        if(float.TryParse(value , out y))
        {
            Variables.KPointOffsetABSPosY = y / 500;
            if(Variables.NPCMode == true)
            {
                Variables.KPointOffsetNPC = new Vector4(Variables.KPointOffset.x , Variables.KPointOffsetABSPosY , 0 , 0);
            }
        }
    }
    //����ƫ��ֵY�����
    public static void OnValueChangedCharacterRescaleX(string value)
    {
        float x;
        if(float.TryParse(value , out x))
        {
            Variables.characterRescaleX = x;
        }
    }
    //��ɫ����X�����
    public static void OnvalueChangedCharacterRescaleY(string value)
    {
        float y;
        if(float.TryParse(value , out y))
        {
            Variables.characterRescaleY = y;
        }
    }
    //��ɫ����Y�����

}
