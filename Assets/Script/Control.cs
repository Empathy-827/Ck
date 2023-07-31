using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Control : MonoBehaviour
{
    public  Material matI;
    public  Material matII;
    public  Material matIII;
    public  Material matIIII;

    public  Material matOverlapI;
    public  Material matContrastI;
    public  Material matCharacterI;
    public  Material matChooseColorI;
    public  Material matChooseGrayI;
    public  Material matDepthI;
    public  Material matTestI;
    public  Material matExportI;
    public  Material matExportNPCI;
    public  Material matExportGIFI;
    public  Material matExportGIFNPCI;
    public  Material matReSamplingI;
    public  Material matMergeI;

    public  Image colorImgI;
    public  Image grayImgI;
    public  Camera cameraI;
    public  Texture2D colorTexI;
    public  Texture2D garyTexI;

    public  GameObject goI;
    public  GameObject frameGOI;
    public  GameObject frameGO2I;
    public  GameObject frameOverlapGOI;
    public  GameObject frameGO3I;
    public  GameObject frameCharacterGOI;

    public static Material mat;
    public static Material mat1;
    public static Material mat2;
    public static Material mat3;


    public static Material matOverlap;
    public static Material matContrast;
    public static Material matCharacter;
    public static Material matChooseColor;
    public static Material matChooseGray;
    public static Material matDepth;
    public static Material matTest;
    public static Material matExport;
    public static Material matExportNPC;
    public static Material matExportGIF;
    public static Material matExportGIFNPC;
    public static Material matReSampling;
    public static Material matMerge;

    public static Image colorImg;
    public static Image grayImg;
    public static Camera camera;
    public static Texture2D colorTex;
    public static Texture2D grayTex;

    public static GameObject go;
    public static GameObject frameGO;
    public static GameObject frameGO2;
    public static GameObject frameOverlapGO;
    public static GameObject frameGO3;
    public static GameObject frameCharacterGO;

    // Start is called before the first frame update
    void Start()
    {
        mat = matI;
        mat1 = matII;
        mat2 = matIII;
        mat3 = matIIII;

        matOverlap = matOverlapI;
        matContrast = matContrastI;
        matCharacter = matCharacterI;
        matChooseColor = matChooseColorI;
        matChooseGray = matChooseGrayI;
        matDepth = matDepthI;
        matTest = matTestI;
        matExport = matExportI;
        matExportNPC = matExportNPCI;
        matExportGIF = matExportGIFI;
        matExportGIFNPC = matExportGIFNPCI;
        matReSampling = matReSamplingI;
        matMerge = matMergeI;

        colorImg = colorImgI;
        grayImg = grayImgI;
        camera = cameraI;
        colorTex = colorTexI;
        grayTex = garyTexI;

        go = goI;
        frameGO = frameGOI;
        frameGO2 = frameGO2I;
        frameOverlapGO = frameOverlapGOI;
        frameGO3 = frameGO3I;
        frameCharacterGO = frameCharacterGOI;
        
        ButtonExtension btnLeft = GameObject.Find("左").GetComponent<ButtonExtension>();
        btnLeft.onClick.AddListener(() => {Interaction.ClickLeft();});
        btnLeft.onPress.AddListener(() => {Interaction.PressLeft();});

        ButtonExtension btnRight = GameObject.Find("右").GetComponent<ButtonExtension>();
        btnRight.onClick.AddListener(() => {Interaction.ClickRight();});
        btnRight.onPress.AddListener(() => {Interaction.PressRight();});

        ButtonExtension btnUp = GameObject.Find("上").GetComponent<ButtonExtension>();
        btnUp.onClick.AddListener(() => {Interaction.ClickUp();});
        btnUp.onPress.AddListener(() => {Interaction.PressUp();});

        ButtonExtension btnDown = GameObject.Find("下").GetComponent<ButtonExtension>();
        btnDown.onClick.AddListener(() => {Interaction.ClickDown();});
        btnDown.onPress.AddListener(() => {Interaction.PressDown();});

        mat.SetFloat("_KPointView",0f);
        mat.SetFloat("_PicScale",1);
        matCharacter.SetFloat("_PicScale",1);
        matOverlap.SetFloat("_KPointView",0f);
        matContrast.SetFloat("_KPointView",0f);
        matCharacter.SetFloat("_KPointView",0f);

        mat.SetVector("_KPointOffset",new Vector4(0,0,0,0));
        matContrast.SetVector("_KPointOffset",new Vector4(0,0,0,0));
        mat.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));
        matContrast.SetVector("_KPointOffsetNPC",new Vector4(0,0,0,0));
        mat.SetVector("_ScaleRescale",new Vector4(1,1,0,0));
        mat.SetVector("_ScaleNPCRescale",new Vector4(1,1,0,0));
        mat.SetVector("_KPointOffsetRescale",new Vector4(0,0,0,0));
        mat.SetVector("_KPointOffsetNPCRescale",new Vector4(0,0,0,0));

        Variables.colorAmount = 0;
        Variables.grayAmount = 0;
        Variables.colorRectTransform = colorImg.transform as RectTransform;
        Variables.grayRectTransform = grayImg.transform as RectTransform;

        Variables.mainFolderPath = Directory.GetCurrentDirectory().Remove(Directory.GetCurrentDirectory().LastIndexOf(@"\")) + "/G123_QA_Res";

    }
    //start逻辑，初始化参数

    // Update is called once per frame
    void Update()
    {
        Variables.playSpeed = Time.deltaTime * Variables.speed;
        Play.PlayFunc();
    
        Interaction.ReScale();
        Interaction.ChooseColorValue();
        Interaction.ChooseGrayValue();

    }

    /**
        Slider交互调用
    */
    public void SliderAlphaClip(float value)
    {
        Interaction.OnValueChangeAlphaClip(value);
    }

    public void SliderChangeBlend(float value)
    {
        Interaction.OnValueChangBlend(value);
    }
    
    public void SliderPlaySpeed(float value)
    {
        Interaction.OnValueChangePlaySpeed(value);
    }

    public void SliderChangeStandardX(string value)
    {
        Modify.OnValueChangedX(value);
    }

    public void SliderChangeStandardY(string value)
    {
        Modify.OnValueChangedY(value);
    }

    public void SliderChangeGIFX(string value)
    {
        Modify.OnValueChangedGIFX(value);
    }

    public void SliderChangeGIFY(string value)
    {
        Modify.OnValueChangedGIFY(value);
    }

    public void SliderChangeGIFQuality(string value)
    {
        Modify.OnValueChangedGIFQuality(value);
    }

    public void SliderChangeGIFSpeed(string value)
    {
        Modify.OnValueChangedGIFSpeed(value);   
    }

    public void SliderChangeReSamplingWidth(string value)
    {
        Modify.OnValueChangedReSamplingWidth(value);
    }

    public void SliderChangeReSamplingHeight(string value)
    {
        Modify.OnValueChangedReSamplingHeight(value);
    }

    public void SliderChangeOffsetX(string value)
    {
        Modify.OnValueChangedOffsetX(value);
    }

    public void SliderChangeOffsetY(string value)
    {
        Modify.OnValueChangedOffsetY(value);
    }

    public void SliderChangeAbsPosX(string value)
    {
        Modify.OnValueChangedAbsPosX(value);
    }

    public void SliderChangeAbsPosY(string value)
    {
        Modify.OnValueChangedAbsPosY(value);
    }

    public void SliderChangeCharacterReScaleX(string value)
    {
        Modify.OnValueChangedCharacterRescaleX(value);
    }

    public void SliderChangeCharacterReScaleY(string value)
    {
        Modify.OnvalueChangedCharacterRescaleY(value);
    }

    /*
        按钮交互调用
    */
    public void ButtonApplyOffset()
    {
        Modify.ButtonApplyNumOffset();
    }

    public void ButtonContinueAnim()
    {
        Interaction.PlayAnim();
    }

    public void ButtonStopAnim()
    {
        Interaction.StopAnim();
    }

    public void ButtonVertOffset()
    {
        Interaction.VertOffset();
    }

    public void ButtonChooseBody()
    {
        //Interaction.ChooseBody();
    }

    //取消显示
    public void ButtonCancelNPC()
    {
        Interaction.CancelNPC();
    }

    public void ButtonCancelNPCAddon()
    {
        Interaction.CancelNPCAddon();
    }

    public void ButtonCancelNPC00()
    {
        Interaction.CancelNPC00();
    }

    //选择主体部分，已经注释
    /*
        public void ButtonChooseHead()
        {
            //Interaction.ChooseHead();
        }

        public void ButtonCancelHead()
        {
            //Interaction.CancelHead();
        }

        public void ButtonChooseWeapon()
        {
            //Interaction.ChooseWeapon();
        }

        public void ButtonCancelWeapon()
        {
        // Interaction.CancelWeapon();
        }

        public void ButtonChooseGem()
        {
            //Interaction.ChooseGem();
        }

        public void ButtonCancelGem()
        {
            //Interaction.CancelGem();
        }

        public void ButtonChooseBodyOverlap()
        {
            //Interaction.ChooseBodyOverlap();
        }

        public void ButtonCancelBodyOverlap()
        {
            //Interaction.CancelBodyOverlap();
        }

        public void ButtonChooseHeadOverlap()
        {
            //Interaction.ChooseHeadOverlap();
        }

        public void ButtonCancelHeadOverlap()
        {
            //Interaction.CancelHeadOverlap();
        }

        public void ButtonChooseWeaponOverlap()
        {
            //Interaction.ChooseWeaponOverlap();
        }

        public void ButtonCancelWeaponOverlap()
        {
            //Interaction.CancelWeaponOverlap();
        }

        public void ButtonChooseGemOverlap()
        {
            //Interaction.ChooseGemOverlap();
        }

        public void ButtonCancelGemOverlap()
        {
            //Interaction.CancelGemOverlap();
        }
    */

    public void ButtonChooseNPC()
    {
        Interaction.ChooseNPCFolder();
    }

    public void ButtonChooseAnim()
    {
        Interaction.ChooseAnim();
    }

    public void ButtonForceAll()
    {
        Interaction.ChooseForceAll();
    }


    public void ButtonForce0()
    {
        Interaction.ChooseForce0();
    }

    public void ButtonForce1()
    {
        Interaction.ChooseForce1();
    }

    public void ButtonForce2()
    {
        Interaction.ChooseForce2();
    }

    public void ButtonForce3()
    {
        Interaction.ChooseForce3();
    }

    public static void ButtonNext()
    {
        Play.NextFrame();
        
    }

    public static void ButtonLast()
    {
        Play.LastFrame();
        
    }

    public void ButtonModeOverlapContrast()
    {
        Interaction.ModeOverlapContrast();
    }

    public void ButtonModeOverlapContrastImportNPC()
    {
        Interaction.ModeOverlapContrastImportNPC();
    }

    public void ButtonChooseAlphaMode()
    {
        Interaction.ChooseAlphaMode();
    }

    public void ButtonChooseSoldMode()
    {
        Interaction.ChooseSoldMode();
    }

    public void ButtonChoosePixelDiffMode()
    {
        Interaction.ChoosePixelDiffMode();
    }

    public void ButtonChooseDiffMode()
    {
        Interaction.ChooseDiffMode();
    }

    public void ButtonRLSwitch()
    {
        Interaction.RLSwitch();
    }

    public void ButtonCoverageSwitch()
    {
        Interaction.CoverageSwitch();
    }

    public void ButtonChooseModeCharacterContrast()
    {
        Interaction.ChooseModeCharacterContrast();
    }
    
    public void ButtonModeCharacterContrastImportCharacter()
    {
        Interaction.ModeCharacterContrastImportCharacter();
    }

    public void ButtonModeCharacterContrastImportNPC()
    {
        Interaction.ModeCharacterContrastImportNPC();
    }

    // public void ButtonReimportOverlap()
    // {
    //     Interaction.ReimportOverlap();
    // }

    public void ButtonReimportCharacter()
    {
        Interaction.ReimportCharacter();
    }

    public void ButtonSetKPointView()
    {
        Interaction.SetKPointView();
    }

    public void ButtonResetKPoint()
    {
        Interaction.ResetKPoint();
    }

    public void ButtonChooseColor()
    {
        Interaction.ChooseColor();
    }

    public void ButtonCloseColorBar()
    {
        Interaction.CloseColorBar();
    }

    public void ButtonChooseGray()
    {
        Interaction.ChooseGray();
    }
    
    public void ButtonCloseGrayBar()
    {
        Interaction.CloseGrayBar();
    }

    public void ButtonPixelCorrection()
    {
        Interaction.PixelCorrection();
    }

    public void ButtonCharacterReScale()
    {
        Interaction.CharacterRescale();
    }
   
    public void ButtonChooseCharacterMode()
    {
        Interaction.ChooseCharacter();
    }

    public void ButtonChooseNPCMode()
    {
        Interaction.ChooseNPC();
    }

    public void ButtonExportNormalAsset()
    {
        Export.ExportNormalAsset();
    }

    public void ButtonExportSelect()
    {
        Export.ExportSelect();
    }

    public void ButtonExportAll()
    {
        Export.ExportAll();
    }

    public void ButtonExportAllGem()
    {
        Export.ExportAllGem();
    }

    public void ButtonResetOffset()
    {
        Interaction.ResetOffset();
    }

    public void ButtonAddPicBackground()
    {
        Interaction.AddPicBackground();
    }

    public void ButtonScrollScaling()
    {
        Interaction.ScrollScaling();
    }

    public void ButtonScrollPicScaling()
    {
        Interaction.ScrollPicScaling();
    }

    public void ButtonCharacterModeReScale()
    {
        Interaction.CharacterModeReScale();
    }

    public void ButtonExportGIFAsset()
    {
        Export.ExportGIFAsset();
    }

    public void ButtonExportSelectGIF()
    {
        Export.ExportSelectGIF();
    }

    public void ButtonExportGIFAll()
    {
        Export.ExportGIFAll();
    }

    public void ButtonReSamplingImage()
    {
        Export.ReSamplingImage();
    }

    public void ButtonSaveMergeImage()
    {
        Export.SaveMergeImage();
    }

}



