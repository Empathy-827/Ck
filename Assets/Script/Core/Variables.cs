using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Variables
{
    public static string mainFolderPath;
    public static bool NPCMode = false;

    public static bool bAlphaMode = true;
    public static bool bSoldMode = false;
    public static bool bDiffMode = false;
    public static bool bPixelDiffMode = false;

    public static bool bCoverage = false;


    public static bool validBody = false;
    public static bool validBodyDepth = false;
    public static bool validHead = false;
    public static bool validHeadDepth = false;
    public static bool validWeapon = false;
    public static bool validWeaponDepth = false;
    public static bool validWeaponEffect = false;
    public static bool validGem = false;

    public static bool validBodyOverlap = false;
    public static bool validBodyDepthOverlap = false;
    public static bool validHeadOverlap = false;
    public static bool validHeadDepthOverlap = false;
    public static bool validWeaponOverlap = false;
    public static bool validWeaponDepthOverlap = false;
    public static bool validWeaponEffectOverlap = false;
    public static bool validGemOverlap = false;

    public static bool validBodyCharacter = false;
    public static bool validBodyDepthCharacter = false;
    public static bool validHeadCharacter = false;
    public static bool validHeadDepthCharacter = false;
    public static bool validWeaponCharacter = false;
    public static bool validWeaponDepthCharacter = false;
    public static bool validWeaponEffectCharacter = false;
    public static bool validGemCharacter = false;

    //NPC

    public static bool validNPC = false;
    public static bool validNPCOverlap = false;
    public static bool validNPCCharacter = false;

    public static bool validNPCAddon = false;
    public static bool validNPCAddonOverlap = false;
    public static bool validNPCAddonCharacter = false;

    public static bool validNPC00 = false;
    public static bool validNPC00Overlap = false;
    public static bool validNPC00Character = false;

    public static bool validNPC01 = false;
    public static bool validNPC01Overlap = false;
    public static bool validNPC01Character = false;

    public static bool validNPC02 = false;
    public static bool validNPC02Overlap = false;
    public static bool validNPC02Character = false;


    public static bool aForceAll = false;

    public static bool bVert = false;

    public static bool bOverlapNPC = false;
    public static bool bCharacterNPC = false;

    public static bool bSwitch = false;

    public static bool bPlay = false;
    public static bool bChoose = true;
    public static bool bChooseColor = false;
    public static bool bChooseGray = false;
    public static bool bNPC = false;

    public static bool bReset = false;
    public static bool bCorrection = false;
    
    public static bool bPicScale = false;
    public static bool bScale = false;

    public static float count = 1;
    public static int index = 0;
    public static float countOverlap = 1;
    public static int indexOverlap = 0;
    public static float countCharacter = 1;
    public static int indexCharacter = 0;


    public static float countNPC = 1;
    public static int indexNPC = 0;
    public static float countNPCOverlap = 1;
    public static int indexNPCOverlap = 0;
    public static float countNPCCharacter = 1;
    public static int indexNPCCharacter = 0;

    //npc addon
    public static float countNPCAddon = 1;
    public static int indexNPCAddon = 0;
    public static float countNPCAddonOverlap = 1;
    public static int indexNPCAddonOverlap = 0;
    public static float countNPCAddonCharacter = 1;
    public static int indexNPCAddonCharacter = 0;

    //npc 00
    public static float countNPC00 = 1;
    public static int indexNPC00 = 0;
    public static float countNPC00Overlap = 1;
    public static int indexNPC00Overlap = 0;
    public static float countNPC00Character = 1;
    public static int indexNPC00Character = 0;

    public static string bodyPath;
    public static string bodyDepthPath;
    public static string headPath;
    public static string headDepthPath;
    public static string weaponPath;
    public static string weaponDepthPath;
    public static string weaponEffectPath;
    public static string gemPath;

    public static string bodyOverlapPath;
    public static string bodyDepthOverlapPath;
    public static string headOverlapPath;
    public static string headDepthOverlapPath;
    public static string weaponOverlapPath;
    public static string weaponDepthOverlapPath;
    public static string weaponEffectOverlapPath;
    public static string gemOverlapPath;

    public static string bodyCharacterPath;
    public static string bodyDepthCharacterPath;
    public static string headCharacterPath;
    public static string headDepthCharacterPath;
    public static string weaponCharacterPath;
    public static string weaponDepthCharacterPath;
    public static string weaponEffectCharacterPath;
    public static string gemCharacterPath;

    public static string NPCPath;
    public static string NPCOverlapPath;
    public static string NPCCharacterPath;

    public static string NPCAddonPath;
    public static string NPCAddonOverlapPath;
    public static string NPCAddonCharacterPath;


    public static string NPC00Path;
    public static string NPC00OverlapPath;
    public static string NPC00CharacterPath;



    public static string force = "00";
    public static string animName = "stand";
    public static int playMode = 0;

    public static Texture2D blackTex;
    public static Texture2D whiteTex;

    public static List<Texture2D> bodyImages = new List<Texture2D>();
    public static List<Texture2D> bodyDepthImages = new List<Texture2D>();
    public static List<Texture2D> headImages = new List<Texture2D>();
    public static List<Texture2D> headDepthImages = new List<Texture2D>();
    public static List<Texture2D> weaponImages = new List<Texture2D>();
    public static List<Texture2D> weaponDepthImages = new List<Texture2D>();
    public static List<Texture2D> weaponEffectImages = new List<Texture2D>();
    public static List<Texture2D> gemImages = new List<Texture2D>();

    public static List<Texture2D> bodyOverlapImages = new List<Texture2D>();
    public static List<Texture2D> bodyDepthOverlapImages = new List<Texture2D>();
    public static List<Texture2D> headOverlapImages = new List<Texture2D>();
    public static List<Texture2D> headDepthOverlapImages = new List<Texture2D>();
    public static List<Texture2D> weaponOverlapImages = new List<Texture2D>();
    public static List<Texture2D> weaponDepthOverlapImages = new List<Texture2D>();
    public static List<Texture2D> weaponEffectOverlapImages = new List<Texture2D>();
    public static List<Texture2D> gemOverlapImages = new List<Texture2D>();

    public static List<Texture2D> bodyCharacterImages = new List<Texture2D>();
    public static List<Texture2D> bodyDepthCharacterImages = new List<Texture2D>();
    public static List<Texture2D> headCharacterImages = new List<Texture2D>();
    public static List<Texture2D> headDepthCharacterImages = new List<Texture2D>();
    public static List<Texture2D> weaponCharacterImages = new List<Texture2D>();
    public static List<Texture2D> weaponDepthCharacterImages = new List<Texture2D>();
    public static List<Texture2D> weaponEffectCharacterImages = new List<Texture2D>();
    public static List<Texture2D> gemCharacterImages = new List<Texture2D>();


    //npc
    public static List<Texture2D> NPCImages = new List<Texture2D>();

    public static List<Texture2D> NPCImagesForce1 = new List<Texture2D>();
    public static List<Texture2D> NPCImagesForce2 = new List<Texture2D>();
    public static List<Texture2D> NPCImagesForce3 = new List<Texture2D>();

    public static List<Texture2D> NPCOverlapImages = new List<Texture2D>();
    public static List<Texture2D> NPCCharacterImages = new List<Texture2D>();

    public static List<Texture2D> bodyImagesNoForce = new List<Texture2D>();
    public static List<Texture2D> bodyOverlapImagesNoForce = new List<Texture2D>();

    public static List<Texture2D> NPCImagesNoForce = new List<Texture2D>();
    public static List<Texture2D> NPCOverlapImagesNoForce = new List<Texture2D>();

    //Npc Addon
    public static List<Texture2D> NPCAddonImages = new List<Texture2D>();

    public static List<Texture2D> NPCAddonImagesForce1 = new List<Texture2D>();
    public static List<Texture2D> NPCAddonImagesForce2 = new List<Texture2D>();
    public static List<Texture2D> NPCAddonImagesForce3 = new List<Texture2D>();

    public static List<Texture2D> NPCAddonOverlapImages = new List<Texture2D>();
    public static List<Texture2D> NPCAddonCharacterImages = new List<Texture2D>();


    public static List<Texture2D> NPCAddonImagesNoForce = new List<Texture2D>();
    public static List<Texture2D> NPCAddonOverlapImagesNoForce = new List<Texture2D>();

    //Npc 00
    public static List<Texture2D> NPC00Images = new List<Texture2D>();

    public static List<Texture2D> NPC00ImagesForce1 = new List<Texture2D>();
    public static List<Texture2D> NPC00ImagesForce2 = new List<Texture2D>();
    public static List<Texture2D> NPC00ImagesForce3 = new List<Texture2D>();


    public static List<Texture2D> NPC00OverlapImages = new List<Texture2D>();
    public static List<Texture2D> NPC00CharacterImages = new List<Texture2D>();

    public static List<Texture2D> NPC00ImagesNoForce = new List<Texture2D>();
    public static List<Texture2D> NPC00OverlapImagesNoForce = new List<Texture2D>();



    public static List<Texture2D> tex2DList = new List<Texture2D>();
    public static ProGifTexturesToGIF tex2Gif = new ProGifTexturesToGIF();

    public static List<RenderTexture> GIFList = new List<RenderTexture>();

    public static List<Texture2D> bodyRenderImages = new List<Texture2D>();
    public static List<Texture2D> bodyDepthRenderImages = new List<Texture2D>();
    public static List<Texture2D> headRenderImages = new List<Texture2D>();
    public static List<Texture2D> headDepthRenderImages = new List<Texture2D>();
    public static List<Texture2D> weaponRenderImages = new List<Texture2D>();
    public static List<Texture2D> weaponDepthRenderImages = new List<Texture2D>();

    public static List<Texture2D> NPCRenderImages = new List<Texture2D>();
    
    public static List<Texture2D> weaponEffectRenderImages = new List<Texture2D>();
    public static List<Texture2D> weaponGemRenderImages = new List<Texture2D>();

    public static List<Texture2D> weaponGemCharacterImages = new List<Texture2D>();

    public static List<Texture2D> bodyRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> headRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> weaponRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> bodyDepthRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> headDepthRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> weaponDepthRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> weaponEffectRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> gemRenderGIFImages = new List<Texture2D>();
    public static List<Texture2D> NPCRenderGIFImages = new List<Texture2D>();

    public static List<Texture2D> switchImages = new List<Texture2D>();

    
    public static int exportCount = 0;
    public static int exportForceCount = 0;
    public static int overlapCount = 0;
    public static int overlapForceCount = 0;

    public static float exportWidth = 0;
    public static float exportHeight = 0;
    public static float overlapWidth = 0;
    public static float overlapHeight = 0;

    public static float exportKPointX = 0;
    public static float exportKPointY = 0;
    public static float overlapKPointX = 0;
    public static float overlapKPointY = 0;

    public static Vector4 cutValue = new Vector4(0,0,0,0);
    public static Vector4 cutValueOverlap = new Vector4(0,0,0,0);
    public static Vector4 cutValueCharacter = new Vector4(0,0,0,0);
    public static Vector4 cutValueNPC = new Vector4(0,0,0,0);
    public static Vector4 cutValueNPCOverlap = new Vector4(0,0,0,0);
    public static Vector4 cutValueNPCCharacter = new Vector4(0,0,0,0);

    public static Vector2 standardPixel = new Vector2(1000f,1000f);
    public static float standardKx = 250f;
    public static float standardKy = 343f;


    public static Vector4 KPointOffset = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetAdd = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetStand = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetWalk = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetAttack = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetMagic = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetDie = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetHit = new Vector4(0,0,0,0);

    public static Vector4 KPointOffsetNPC = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetAddNPC = new Vector4(0,0,0,0);
    public static Vector4 KPointJsonAddNPC = new Vector4(0,0,0,0);
    public static Vector4 KPointOffsetNPCJson = new Vector4(0,0,0,0);

    public static Vector4 KPointOffsetJson = new Vector4(0,0,0,0);
    public static Vector4 KPointJsonAdd = new Vector4(0,0,0,0);
    
    public static Vector4 scale = new Vector4(1,1,0,0);
    public static Vector4 scaleOverlap = new Vector4(1,1,0,0);
    public static Vector4 scaleCharacter = new Vector4(1,1,0,0);

    public static Vector4 scaleNPC = new Vector4(1,1,0,0);
    public static Vector4 scaleNPCOverlap = new Vector4(1,1,0,0);
    public static Vector4 scaleNPCCharacter = new Vector4(1,1,0,0);

    public static float characterRescaleX = 500;
    public static float characterRescaleY = 500;
    public static Vector2 scaleRescale = new Vector2(1,1);
    public static Vector4 KPointOffsetRescale = new Vector4(0,0,0,0);
    public static Vector2 scaleNPCRescale = new Vector2(1,1);
    public static Vector4 KPointOffsetNPCRescale = new Vector4(0,0,0,0);

    public static float KPointOffsetMult = 1;
    public static float KPointView = 0;
    public static float KPointOffsetABSPosX = 0;
    public static float KPointOffsetABSPosY = 0;

    public static float picScale = 1;

    public static int GIFWidth = 1024;
    public static int GIFHeight = 1024;
    public static int GIFSpeed = 8;
    public static int GIFLoopCount = 0;
    public static int GIFQuality = 100;

    public static int reSamplingWidthNew = 1000;
    public static int reSamplingHeightNew = 1000;

    public static RectTransform colorRectTransform;
    public static RectTransform grayRectTransform;
    public static Vector4 pixelColor = new Vector4(0,0,0,0);
    public static float colorAmount = 0;
    public static float grayAmount = 0;

    public static float playSpeed = 0;
    public static float speed = 5;

    public static int currentFrame = 0;
    public static int currentFrameOverlap = 0;
    public static int currentFrameCharacter = 0;

    public static List<String> waitRenderName;
    public static List<String> GIFWaitRenderName;




}