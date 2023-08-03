using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Play
{
    bool bAlphaMode = Variables.bAlphaMode;
    bool bSoldMode = Variables.bSoldMode;
    bool bDiffMode = Variables.bDiffMode;
    bool bPixelDiffMode = Variables.bPixelDiffMode;

    /**
        播放、上一帧、下一帧逻辑
    */
    public static void PlayFunc()
    {
        if(Variables.bPlay == false)
        {
            return;
        }
        else
        {
            //只显示NPC模式
            if(Variables.NPCMode == true)
            {
                if(Variables.playMode== 0)
                {
                    if(Variables.NPCImages.Count != 0)
                    {
                        Variables.countNPC += Variables.playSpeed;
                        if(Variables.countNPC > Variables.NPCImages.Count)
                        {
                            Variables.countNPC = 1;
                        }
                        Variables.indexNPC = (int)Variables.countNPC - 1;
                        Variables.currentFrame = Variables.indexNPC;
                        //PlayMode0NPC();   
                    }

                    if(Variables.NPCAddonImages.Count != 0)
                    {
                        Variables.indexNPCAddon = (int)Variables.indexNPC;      
                    }

                    if(Variables.NPC00Images.Count != 0)
                    {
                        Variables.indexNPC00 = (int)Variables.indexNPC;           
                    }

                    PlayMode0NPC();

                    Control.frameGO.GetComponent<Text>().text = "总:  " + Variables.currentFrame + "帧";
                }
                if(Variables.playMode== 1)
                { 
                    if(Variables.bOverlapNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexNPCOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexNPC;
                            }
                            
                            if(Variables.NPCImages.Count < Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                
                                Variables.countNPCOverlap += Variables.playSpeed;
                                if(Variables.countNPCOverlap > Variables.NPCOverlapImages.Count)
                                {
                                    Variables.countNPCOverlap = 1;
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }

                            if(Variables.NPCImages.Count > Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                    Variables.countNPCOverlap = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countNPCOverlap += Variables.playSpeed;
                                if(Variables.countNPCOverlap > Variables.NPCOverlapImages.Count)
                                {
                                    Variables.countNPCOverlap = Variables.NPCOverlapImages.Count;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }
                            
                            if(Variables.bSwitch == false)
                            {
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }
                               
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }

                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                            }
                            if(Variables.bSwitch == true)
                            {
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCImages[Variables.indexNPC]);    
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }
                                
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                            }
                            
                        }
                    }
                    else if(Variables.bOverlapNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexNPC;
                            }
                            
                            if(Variables.NPCImages.Count < Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countOverlap += Variables.playSpeed;
                                if(Variables.countOverlap > Variables.bodyOverlapImages.Count)
                                {
                                    Variables.countOverlap = 1;
                                    Variables.countNPC = 1;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            if(Variables.NPCImages.Count > Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC += Variables.playSpeed;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                    Variables.countOverlap = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countOverlap += Variables.playSpeed;
                                if(Variables.countOverlap > Variables.bodyOverlapImages.Count)
                                {
                                    Variables.countOverlap = Variables.bodyOverlapImages.Count;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            PlayMode1NPCCharacter();

                            Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                            Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;                        
                        }
                    }                   
                }
                /*if(Variables.playMode== 2)
                {
                    if(Variables.bCharacterNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCCharacterImages.Count != 0)
                        {
                            Variables.countNPC += Variables.playSpeed;
                            if(Variables.countNPC > Variables.NPCImages.Count)
                            {
                                Variables.countNPC = 1;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countNPCCharacter += Variables.playSpeed;
                            if(Variables.countNPCCharacter > Variables.NPCCharacterImages.Count)
                            {
                                Variables.countNPCCharacter = 1;
                            }
                            Variables.indexNPCCharacter = (int)Variables.countNPCCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexNPCCharacter;

                            PlayMode2NPCNPC();
                            
                        }
                    }
                    else if(Variables.bCharacterNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyCharacterImages.Count != 0)
                        {
                            Variables.countNPC += Variables.playSpeed;
                            if(Variables.countNPC > Variables.NPCImages.Count)
                            {
                                Variables.countNPC = 1;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countCharacter += Variables.playSpeed;
                            if(Variables.countCharacter > Variables.bodyCharacterImages.Count)
                            {
                                Variables.countCharacter = 1;
                            }
                            Variables.indexCharacter = (int)Variables.countCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexCharacter;

                            PlayMode2NPCCharacter();
                            
                        }
                    }
                    Control.frameGO3.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                    Control.frameCharacterGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameCharacter;
                }*/
            }
            
            
        }
    }

    /**
        上一帧
    */
    public static void LastFrame()
    {
        if(Variables.bChoose == false || Variables.bPlay == true)
        {
            return;
        }
        else
        {
            if(Variables.NPCMode == true)
            {
                if(Variables.playMode == 0)
                {  
                    if(Variables.NPCImages.Count != 0)
                    {
                        Variables.countNPC = Mathf.Floor(Variables.countNPC);
                        Variables.countNPC -= 1;
                        if(Variables.countNPC < 1)
                        {
                            Variables.countNPC = Variables.NPCImages.Count;
                        }
                        Variables.indexNPC = (int)Variables.countNPC - 1;
                        Variables.currentFrame = Variables.indexNPC;
                        
                    }

                    if(Variables.NPCAddonImages.Count != 0)
                    {
                        Variables.indexNPCAddon = (int)Variables.indexNPC;      
                    }

                    if(Variables.NPC00Images.Count != 0)
                    {
                        Variables.indexNPC00 = (int)Variables.indexNPC;           
                    }

                    PlayMode0NPC();

                    Control.frameGO.GetComponent<Text>().text = "总:  " + Variables.currentFrame + "帧";
                }
                else if(Variables.playMode == 1)
                {
                    if(Variables.bOverlapNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }
                            
                            if(Variables.NPCImages.Count < Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countOverlap = Mathf.Floor(Variables.countOverlap);
                                Variables.countOverlap -= 1;
                                if(Variables.countOverlap < 1)
                                {
                                    Variables.countOverlap = Variables.bodyOverlapImages.Count;
                                    Variables.countNPC = Variables.NPCImages.Count + 1;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            if(Variables.NPCImages.Count > Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countOverlap = Variables.bodyOverlapImages.Count + 1;
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countOverlap = Mathf.Floor(Variables.countOverlap);
                                Variables.countOverlap -= 1;
                                if(Variables.countOverlap < 1)
                                {
                                    Variables.countOverlap = 1;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            PlayMode1NPCCharacter();      
                            
                        }
                        Control.frameGO2.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameOverlap;
                    }
                    else if(Variables.bOverlapNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexNPCOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }
                            
                            if(Variables.NPCImages.Count < Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countNPCOverlap = Mathf.Floor(Variables.countNPCOverlap);
                                Variables.countNPCOverlap -= 1;
                                if(Variables.countNPCOverlap < 1)
                                {
                                    Variables.countNPCOverlap = Variables.NPCOverlapImages.Count;
                                    Variables.countNPC = Variables.NPCImages.Count + 1;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }

                            if(Variables.NPCImages.Count > Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC -= 1;
                                if(Variables.countNPC < 1)
                                {
                                    Variables.countNPCOverlap = Variables.NPCOverlapImages.Count + 1;
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countNPCOverlap = Mathf.Floor(Variables.countNPCOverlap);
                                Variables.countNPCOverlap -= 1;
                                if(Variables.countNPCOverlap < 1)
                                {
                                    Variables.countNPCOverlap = 1;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }

                            if(Variables.bSwitch == false)
                            {
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }
                               
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }

                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                            }
                            if(Variables.bSwitch == true)
                            {
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCImages[Variables.indexNPC]);    
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }
                                
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                            }
                            
                        }
                        
                    }
                    
                }
                /*else if(Variables.playMode == 2)
                {
                    if(Variables.bCharacterNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyCharacterImages.Count != 0)
                        {
                            Variables.countNPC = Mathf.Floor(Variables.countNPC);
                            Variables.countNPC -= 1;
                            if(Variables.countNPC < 1)
                            {
                                Variables.countNPC = Variables.NPCImages.Count;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countCharacter = Mathf.Floor(Variables.countCharacter);
                            Variables.countCharacter -= 1;
                            if(Variables.countCharacter < 1)
                            {
                                Variables.countCharacter = Variables.bodyCharacterImages.Count;
                            }
                            Variables.indexCharacter = (int)Variables.countCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexCharacter;

                            PlayMode2NPCCharacter();
                        }
                        Control.frameGO3.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameCharacterGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameCharacter;
                    }
                    else if(Variables.bCharacterNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCCharacterImages.Count != 0)
                        {
                            Variables.countNPC = Mathf.Floor(Variables.countNPC);
                            Variables.countNPC -= 1;
                            if(Variables.countNPC < 1)
                            {
                                Variables.countNPC = Variables.NPCImages.Count;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countNPCCharacter = Mathf.Floor(Variables.countNPCCharacter);
                            Variables.countNPCCharacter -= 1;
                            if(Variables.countNPCCharacter < 1)
                            {
                                Variables.countNPCCharacter = Variables.NPCCharacterImages.Count;
                            }
                            Variables.indexNPCCharacter = (int)Variables.countNPCCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexNPCCharacter;

                            PlayMode2NPCNPC();
                        }
                        Control.frameGO3.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameCharacterGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameCharacter;
                    }
                    
                }*/
            }
            
        }
    }

    /**
        下一帧
    */
    public static void NextFrame()
    {
         if(Variables.bChoose == false || Variables.bPlay == true)
        {
            return;
        }
        else
        {
            if(Variables.NPCMode == true)
            {
                if(Variables.playMode == 0)
                {
                    if(Variables.NPCImages.Count != 0)
                    {
                        Variables.countNPC = Mathf.Floor(Variables.countNPC);
                        Variables.countNPC += 1;
                        if(Variables.countNPC > Variables.NPCImages.Count)
                        {
                            Variables.countNPC = 1;
                        }
                        Variables.indexNPC = (int)Variables.countNPC - 1;
                        Variables.currentFrame = Variables.indexNPC;

                    }

                    if(Variables.NPCAddonImages.Count != 0)
                    {
                        Variables.indexNPCAddon = (int)Variables.indexNPC;      
                    }

                    if(Variables.NPC00Images.Count != 0)
                    {
                        Variables.indexNPC00 = (int)Variables.indexNPC;           
                    }

                    PlayMode0NPC();

                    Control.frameGO.GetComponent<Text>().text = "总:  " + Variables.currentFrame + "帧";
                }
                else if(Variables.playMode == 1)
                {
                    if(Variables.bOverlapNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.bodyImages.Count)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            if(Variables.NPCImages.Count < Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.bodyImages.Count)
                                {
                                    Variables.countNPC = Variables.bodyImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                
                                Variables.countOverlap = Mathf.Floor(Variables.countOverlap);
                                Variables.countOverlap += 1;
                                if(Variables.countOverlap > Variables.bodyOverlapImages.Count)
                                {
                                    Variables.countOverlap = 1;
                                    Variables.countNPC = 0;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }

                            if(Variables.NPCImages.Count > Variables.bodyOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.bodyImages.Count)
                                {
                                    Variables.countNPC = 1;
                                    Variables.countOverlap = 0;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                
                                Variables.countOverlap = Mathf.Floor(Variables.countOverlap);
                                Variables.countOverlap += 1;
                                if(Variables.countOverlap > Variables.bodyOverlapImages.Count)
                                {
                                    Variables.countOverlap = Variables.bodyOverlapImages.Count;
                                }
                                Variables.indexOverlap = (int)Variables.countOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexOverlap;
                            }
                            
                            PlayMode1NPCCharacter();

                        }
                        Control.frameGO2.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameOverlap;
                    }
                    else if(Variables.bOverlapNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCOverlapImages.Count != 0)
                        {
                            if(Variables.NPCImages.Count == Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;
                                Variables.indexNPCOverlap = (int)Variables.countNPC - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;

                            }

                            if(Variables.NPCImages.Count < Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = Variables.NPCImages.Count;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countNPCOverlap = Mathf.Floor(Variables.countNPCOverlap);
                                Variables.countNPCOverlap += 1;
                                if(Variables.countNPCOverlap > Variables.NPCOverlapImages.Count)
                                {
                                    Variables.countNPCOverlap = 1;
                                    Variables.countNPC = 0;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                                
                            }

                            if(Variables.NPCImages.Count > Variables.NPCOverlapImages.Count)
                            {
                                Variables.countNPC = Mathf.Floor(Variables.countNPC);
                                Variables.countNPC += 1;
                                if(Variables.countNPC > Variables.NPCImages.Count)
                                {
                                    Variables.countNPC = 1;
                                    Variables.countNPCOverlap = 0;
                                }
                                Variables.indexNPC = (int)Variables.countNPC - 1;
                                Variables.currentFrame = Variables.indexNPC;

                                Variables.countNPCOverlap = Mathf.Floor(Variables.countNPCOverlap);
                                Variables.countNPCOverlap += 1;
                                if(Variables.countNPCOverlap > Variables.NPCOverlapImages.Count)
                                {
                                    Variables.countNPCOverlap = Variables.NPCOverlapImages.Count;
                                }
                                Variables.indexNPCOverlap = (int)Variables.countNPCOverlap - 1;
                                Variables.currentFrameOverlap = Variables.indexNPCOverlap;
                            }        

                            if(Variables.bSwitch == false)
                            {
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }
                               
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }

                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                            }
                            if(Variables.bSwitch == true)
                            {
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.mat.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.mat.SetTexture("_NPC",Variables.blackTex);
                                }

                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
                                }
                                else
                                {
                                    Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                if(Variables.NPCImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCImages[Variables.indexNPC]);    
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
                                }
                                
                                if(Variables.NPCOverlapImages.Count != 0)
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
                                }
                                else
                                {
                                    Control.matContrast.SetTexture("_NPC",Variables.blackTex);
                                }
                                
                                Control.frameGO2.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrameOverlap;
                                Control.frameOverlapGO.GetComponent<Text>().text = "当前帧数: " + Variables.currentFrame;
                            }
                        }
                    }
                    
                }
                /*else if(Variables.playMode == 2)
                {
                    if(Variables.bCharacterNPC == false)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.bodyCharacterImages.Count != 0)
                        {
                            Variables.countNPC = Mathf.Floor(Variables.countNPC);
                            Variables.countNPC += 1;
                            if(Variables.countNPC > Variables.NPCImages.Count)
                            {
                                Variables.countNPC = 1;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countCharacter = Mathf.Floor(Variables.countCharacter);
                            Variables.countCharacter += 1;
                            if(Variables.countCharacter > Variables.bodyCharacterImages.Count)
                            {
                                Variables.countCharacter = 1;
                            }
                            Variables.indexCharacter = (int)Variables.countCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexCharacter;

                            PlayMode2NPCCharacter();

                        }
                        Control.frameGO3.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameCharacterGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameCharacter;
                    }
                    else if(Variables.bCharacterNPC == true)
                    {
                        if(Variables.NPCImages.Count != 0 && Variables.NPCCharacterImages.Count != 0)
                        {
                            Variables.countNPC = Mathf.Floor(Variables.countNPC);
                            Variables.countNPC += 1;
                            if(Variables.countNPC > Variables.NPCImages.Count)
                            {
                                Variables.countNPC = 1;
                            }
                            Variables.indexNPC = (int)Variables.countNPC - 1;
                            Variables.currentFrame = Variables.indexNPC;

                            Variables.countNPCCharacter = Mathf.Floor(Variables.countNPCCharacter);
                            Variables.countNPCCharacter += 1;
                            if(Variables.countNPCCharacter > Variables.NPCCharacterImages.Count)
                            {
                                Variables.countNPCCharacter = 1;
                            }
                            Variables.indexNPCCharacter = (int)Variables.countNPCCharacter - 1;
                            Variables.currentFrameCharacter = Variables.indexNPCCharacter;

                            PlayMode2NPCNPC();

                        }
                        Control.frameGO3.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrame;
                        Control.frameCharacterGO.GetComponent<Text>().text = "当前帧数:  " + Variables.currentFrameCharacter;
                    }
                    
                }*/
            }
            
        }
    }

    /**
        普通模式主角播放逻辑

    /*private static void PlayMode0Character()
    {
        if(Variables.validBody == true)
        {  
            Control.mat.SetTexture("_Body",Variables.bodyImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Body",Variables.blackTex);
        }

        if(Variables.validHead == true)
        {
            Control.mat.SetTexture("_Head",Variables.headImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Head",Variables.blackTex);
        }

        if(Variables.validWeapon == true)
        {
            Control.mat.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Weapon",Variables.blackTex);
        }

        if(Variables.validBodyDepth == true)
        {
            Control.mat.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepth == true)
        {
            Control.mat.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_HeadDepth",Variables.blackTex);
        }

        if(Variables.validWeaponDepth == true)
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffect == true)
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGem == true)
        {
            Control.mat.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponGem",Variables.blackTex);
        }    
    }

    /**
        重叠对比模式输出主角 重叠主角播放逻辑
    */
    /*private static void PlayMode1CharacterCharacter()
    {
        if(Variables.validBody == true)
        {
            Control.mat.SetTexture("_Body",Variables.bodyImages[Variables.index]);
            Control.matContrast.SetTexture("_Body",Variables.bodyImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Body",Variables.blackTex);
            Control.matContrast.SetTexture("_Body",Variables.blackTex);
        }

        if(Variables.validHead == true)
        {
            Control.mat.SetTexture("_Head",Variables.headImages[Variables.index]);
            Control.matContrast.SetTexture("_Head",Variables.headImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Head",Variables.blackTex);
            Control.matContrast.SetTexture("_Head",Variables.blackTex);
        }

        if(Variables.validWeapon == true)
        {
            Control.mat.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
            Control.matContrast.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Weapon",Variables.blackTex);
            Control.matContrast.SetTexture("_Weapon",Variables.blackTex);
        }

        if(Variables.validBodyDepth == true)
        {
            Control.mat.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_BodyDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepth == true)
        {
            Control.mat.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_HeadDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_HeadDepth",Variables.blackTex);
        }

        if(Variables.validWeaponDepth == true)
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffect == true)
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGem == true)
        {
            Control.mat.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponGem",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.validBodyOverlap == true)
        {
            Control.matOverlap.SetTexture("_Body",Variables.bodyOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_BodyOverlap",Variables.bodyOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_Body",Variables.blackTex);
            Control.matContrast.SetTexture("_BodyOverlap",Variables.blackTex);
        }

        if(Variables.validHeadOverlap == true)
        {  
            Control.matOverlap.SetTexture("_Head",Variables.headOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_HeadOverlap",Variables.headOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_Head",Variables.blackTex);
            Control.matContrast.SetTexture("_HeadOverlap",Variables.blackTex);
        }

        if(Variables.validWeaponOverlap == true)
        {
            Control.matOverlap.SetTexture("_Weapon",Variables.weaponOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_WeaponOverlap",Variables.weaponOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_Weapon",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponOverlap",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffectOverlap == true)
        {
            Control.matOverlap.SetTexture("_WeaponEffect",Variables.weaponEffectOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_WeaponEffectOverlap",Variables.weaponEffectOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_WeaponEffect",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponEffectOverlap",Variables.blackTex);
        }

        if(Variables.validGemOverlap == true)
        {
            Control.matOverlap.SetTexture("_WeaponGem",Variables.gemOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_WeaponGemOverlap",Variables.gemOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_WeaponGem",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponGemOverlap",Variables.blackTex);
        }

        if(Variables.validBodyDepthOverlap == true)
        {
            Control.matOverlap.SetTexture("_BodyDepth",Variables.bodyDepthOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_BodyDepthOverlap",Variables.bodyDepthOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_BodyDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_BodyDepthOverlap",Variables.blackTex);
        }

        if(Variables.validHeadDepthOverlap == true)
        {
            Control.matOverlap.SetTexture("_HeadDepth",Variables.headDepthOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_HeadDepthOverlap",Variables.headDepthOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_HeadDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_HeadDepthOverlap",Variables.blackTex);
        }
        
        if(Variables.validWeaponDepthOverlap == true)
        {
            Control.matOverlap.SetTexture("_WeaponDepth",Variables.weaponDepthOverlapImages[Variables.indexOverlap]);
            Control.matContrast.SetTexture("_WeaponDepthOverlap",Variables.weaponDepthOverlapImages[Variables.indexOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_WeaponDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponDepthOverlap",Variables.blackTex);
        }
    }

    /**
        重叠对比模式输出主角 重叠NPC播放逻辑
    */
    /*private static void PlayMode1CharacterNPC()
    {
        if(Variables.validBody == true)
        {
            Control.mat.SetTexture("_Body",Variables.bodyImages[Variables.index]);
            Control.matContrast.SetTexture("_Body",Variables.bodyImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Body",Variables.blackTex);
            Control.matContrast.SetTexture("_Body",Variables.blackTex);
        }
        
        if(Variables.validHead == true)
        {
            Control.mat.SetTexture("_Head",Variables.headImages[Variables.index]);
            Control.matContrast.SetTexture("_Head",Variables.headImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Head",Variables.blackTex);
            Control.matContrast.SetTexture("_Head",Variables.blackTex);
        }
        
        if(Variables.validWeapon == true)
        {
            Control.mat.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
            Control.matContrast.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Weapon",Variables.blackTex);
            Control.matContrast.SetTexture("_Weapon",Variables.blackTex);
        }

        if(Variables.validBodyDepth == true)
        {
            Control.mat.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_BodyDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_BodyDepth",Variables.blackTex);
        }
        
        if(Variables.validHeadDepth == true)
        {
            Control.mat.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_HeadDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_HeadDepth",Variables.blackTex);
        }

        if(Variables.validWeaponDepth == true)
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffect == true)
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponEffect",Variables.blackTex);
        }
        if(Variables.validGem == true)
        {
            Control.mat.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
            Control.matContrast.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponGem",Variables.blackTex);
            Control.matContrast.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.NPCOverlapImages.Count != 0)
        {
            Control.matOverlap.SetTexture("_NPC",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
            Control.matContrast.SetTexture("_NPCOverlap",Variables.NPCOverlapImages[Variables.indexNPCOverlap]);
        }
        else
        {
            Control.matOverlap.SetTexture("_NPC",Variables.blackTex);
            Control.matContrast.SetTexture("_NPCOverlap",Variables.blackTex);
        }
    }

    /**
        角色对比模式输出主角 对比主角播放逻辑
    */
    /*private static void PlayMode2CharacterCharacter()
    {
        if(Variables.validBody == true)
        {
            Control.mat.SetTexture("_Body",Variables.bodyImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Body",Variables.blackTex);
        }

        if(Variables.validHead == true)
        {
            Control.mat.SetTexture("_Head",Variables.headImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Head",Variables.blackTex);
        }

        if(Variables.validWeapon == true)
        {
            Control.mat.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Weapon",Variables.blackTex);
        }
        
        if(Variables.validBodyDepth == true)
        {
            Control.mat.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepth == true)
        {
            Control.mat.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_HeadDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponDepth == true)
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffect == true)
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGem == true)
        {
            Control.mat.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.validBodyCharacter == true)
        {
            Control.matCharacter.SetTexture("_Body",Variables.bodyCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Body",Variables.blackTex);
        }

        if(Variables.validHeadCharacter == true)
        {
            Control.matCharacter.SetTexture("_Head",Variables.headCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Head",Variables.blackTex);
        }
        
        if(Variables.validWeaponCharacter == true)
        {
            Control.matCharacter.SetTexture("_Weapon",Variables.weaponCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Weapon",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffectCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponEffect",Variables.weaponEffectCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGemCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponGem",Variables.weaponGemCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.validBodyDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_BodyDepth",Variables.bodyDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_HeadDepth",Variables.headDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_HeadDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponDepth",Variables.weaponDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponDepth",Variables.blackTex);
        }
    }

    /**
        角色对比模式输出主角 对比NPC播放逻辑
    */
    /*private static void PlayMode2CharacterNPC()
    {
        if(Variables.validBody == true)
        {
            Control.mat.SetTexture("_Body",Variables.bodyImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Body",Variables.blackTex);
        }
        
        if(Variables.validHead == true)
        {
            Control.mat.SetTexture("_Head",Variables.headImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Head",Variables.blackTex);
        }

        if(Variables.validWeapon == true)
        {
            Control.mat.SetTexture("_Weapon",Variables.weaponImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_Weapon",Variables.blackTex);
        }
        
        if(Variables.validBodyDepth == true)
        {
            Control.mat.SetTexture("_BodyDepth",Variables.bodyDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepth == true)
        {
            Control.mat.SetTexture("_HeadDepth",Variables.headDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_HeadDepth",Variables.blackTex);
        }

        if(Variables.validWeaponDepth == true)
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.weaponDepthImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffect == true)
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.weaponEffectImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGem == true)
        {
            Control.mat.SetTexture("_WeaponGem",Variables.gemImages[Variables.index]);
        }
        else
        {
            Control.mat.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.NPCCharacterImages.Count != 0)
        {
            Control.matCharacter.SetTexture("_NPC",Variables.NPCCharacterImages[Variables.indexNPCCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_NPC",Variables.blackTex);
        }
    }

    /**
        普通模式NPC播放逻辑
    */
    private static void PlayMode0NPC()
    {
        //NPC本体
        {
            if(Variables.aAnimAll == false)
            {
                    //NPC
                    if(Variables.NPCImages.Count != 0 && Variables.validNPC == true)
                        {
                            Control.mat.SetTexture("_NPC" , Variables.NPCImages[Variables.indexNPC]);
                            //Debug.Log("indexNPC:  " + Variables.indexNPC);
                        }
                        else
                        {
                            Control.mat.SetTexture("_NPC",Variables.blackTex);
                        }
                    //NPCAddon
                    if(Variables.NPCAddonImages.Count != 0 && Variables.validNPCAddon == true)
                        {
                            //Debug.Log("画出来了");
                            Control.mat.SetTexture("_NPCAddon",Variables.NPCAddonImages[Variables.indexNPCAddon]);
                            //Debug.Log("indexNPCAddon:  " + Variables.indexNPCAddon);
                            //Debug.Log("NPCAddonImages:  " + Variables.NPCAddonImages.Count);
                        }
                        else
                        {
                            Control.mat.SetTexture("_NPCAddon",Variables.blackTex);
                        }
                    //NPC00
                    if(Variables.NPC00Images.Count != 0 && Variables.validNPC00 == true)
                        {
                            //Debug.Log("画出来了");
                            Control.mat.SetTexture("_NPC00",Variables.NPC00Images[Variables.indexNPC00 % Variables.NPC00Images.Count]);
                        }
                        else
                        {
                            Control.mat.SetTexture("_NPC00",Variables.blackTex);
                        }
                
                    if(Variables.aForceAll)
                    {
                        //Variables.aAnimAll = false;

                        if(Variables.NPCImagesForce1.Count != 0 && Variables.validNPC == true)
                        {
                            Control.mat1.SetTexture("_NPC" , Variables.NPCImagesForce1[Variables.indexNPC]);
                        }
                        else
                        {
                            Control.mat1.SetTexture("_NPC",Variables.blackTex);
                        }

                        if(Variables.NPCImagesForce2.Count != 0 && Variables.validNPC == true)
                        {
                            Control.mat2.SetTexture("_NPC" , Variables.NPCImagesForce2[Variables.indexNPC]);
                        } 
                        else
                        {
                            Control.mat2.SetTexture("_NPC",Variables.blackTex);
                        }

                        if(Variables.NPCImagesForce3.Count != 0 && Variables.validNPC == true )
                        {
                            Control.mat3.SetTexture("_NPC" , Variables.NPCImagesForce3[Variables.indexNPC]);
                        }
                        else
                        {
                            Control.mat3.SetTexture("_NPC",Variables.blackTex);
                        }

                        if(Variables.NPCAddonImagesForce1.Count != 0 && Variables.validNPCAddon == true && Variables.aForceAll)
                        {
                            Control.mat1.SetTexture("_NPCAddon" , Variables.NPCAddonImagesForce1[Variables.indexNPCAddon]); 
                        }
                        else
                        {
                            Control.mat1.SetTexture("_NPCAddon",Variables.blackTex);
                        }
                        if(Variables.NPCAddonImagesForce2.Count != 0 && Variables.validNPCAddon == true && Variables.aForceAll)
                        {
                            Control.mat2.SetTexture("_NPCAddon" , Variables.NPCAddonImagesForce2[Variables.indexNPCAddon]);  
                        }
                        else
                        {
                            Control.mat2.SetTexture("_NPCAddon",Variables.blackTex);
                        }
                        if(Variables.NPCAddonImagesForce3.Count != 0 && Variables.validNPCAddon == true && Variables.aForceAll)
                        {
                            Control.mat3.SetTexture("_NPCAddon" , Variables.NPCAddonImagesForce3[Variables.indexNPCAddon]);
                            
                        }
                        else
                        {
                            Control.mat3.SetTexture("_NPCAddon",Variables.blackTex);
                        }

                        if(Variables.NPC00ImagesForce1.Count != 0 && Variables.validNPC00 == true && Variables.aForceAll)
                        {
                            Control.mat1.SetTexture("_NPC00" , Variables.NPC00ImagesForce1[Variables.indexNPC00]);
                        }
                        else
                        {
                            Control.mat1.SetTexture("_NPC00",Variables.blackTex);
                        }

                        if(Variables.NPC00ImagesForce2.Count != 0  && Variables.validNPC00 == true && Variables.aForceAll)
                        {
                            Control.mat2.SetTexture("_NPC00" , Variables.NPC00ImagesForce2[Variables.indexNPC00]);
                        }
                        else
                        {
                            Control.mat2.SetTexture("_NPC00",Variables.blackTex);
                        }

                        if(Variables.NPC00ImagesForce3.Count != 0  && Variables.validNPC00 == true && Variables.aForceAll)
                        {
                            Control.mat3.SetTexture("_NPC00" , Variables.NPC00ImagesForce3[Variables.indexNPC00]);
                        }
                        else
                        {
                            Control.mat3.SetTexture("_NPC00",Variables.blackTex);
                        }

                    }
            }
            else
            {
                {
                    if(Variables.NPCImages0.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[0].SetTexture("_NPC" , Variables.NPCImages0[Variables.indexNPC % Variables.NPCImages0.Count]);
                    }
                    else
                    {
                        Control.mat_anim[0].SetTexture("_NPC",Variables.blackTex);
                    }

                    if(Variables.NPCImages1.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[1].SetTexture("_NPC" , Variables.NPCImages1[Variables.indexNPC % Variables.NPCImages1.Count]);
                    }
                    else
                    {
                        Control.mat_anim[1].SetTexture("_NPC",Variables.blackTex);
                    }

                    if(Variables.NPCImages2.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[2].SetTexture("_NPC" , Variables.NPCImages2[Variables.indexNPC % Variables.NPCImages2.Count]);
                    }
                    else
                    {
                        Control.mat_anim[2].SetTexture("_NPC",Variables.blackTex);
                    }
                    
                    if(Variables.NPCImages3.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[3].SetTexture("_NPC" , Variables.NPCImages3[Variables.indexNPC % Variables.NPCImages3.Count]);
                    }
                    else
                    {
                        Control.mat_anim[3].SetTexture("_NPC",Variables.blackTex);
                    }
                    
                    if(Variables.NPCImages4.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[4].SetTexture("_NPC" , Variables.NPCImages4[Variables.indexNPC % Variables.NPCImages4.Count]);
                    }
                    else
                    {
                        Control.mat_anim[4].SetTexture("_NPC",Variables.blackTex);
                    }
                    
                    if(Variables.NPCImages5.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[5].SetTexture("_NPC" , Variables.NPCImages5[Variables.indexNPC % Variables.NPCImages5.Count]);
                    }
                    else
                    {
                        Control.mat_anim[5].SetTexture("_NPC",Variables.blackTex);
                    }
                    
                    if(Variables.NPCImages6.Count != 0 && Variables.validNPC == true)
                    {
                        Control.mat_anim[6].SetTexture("_NPC" , Variables.NPCImages6[Variables.indexNPC % Variables.NPCImages6.Count]);
                    }
                    else
                    {
                        Control.mat_anim[6].SetTexture("_NPC",Variables.blackTex);
                    }

                }
                //if()
                {
                    if(Variables.NPCAddonImages0.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[0].SetTexture("_NPCAddon" , Variables.NPCAddonImages0[Variables.indexNPCAddon % Variables.NPCAddonImages0.Count]);
                    }
                    else
                    {
                        Control.mat_anim[0].SetTexture("_NPCAddon",Variables.blackTex);
                    }

                    if(Variables.NPCAddonImages1.Count != 0  && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[1].SetTexture("_NPCAddon" , Variables.NPCAddonImages1[Variables.indexNPCAddon % Variables.NPCAddonImages1.Count]);
                    }
                    else
                    {
                        Control.mat_anim[1].SetTexture("_NPCAddon",Variables.blackTex);
                    }

                    if(Variables.NPCAddonImages2.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[2].SetTexture("_NPCAddon" , Variables.NPCAddonImages2[Variables.indexNPCAddon % Variables.NPCAddonImages2.Count]);
                    }
                    else
                    {
                        Control.mat_anim[2].SetTexture("_NPCAddon",Variables.blackTex);
                    }
                    
                    if(Variables.NPCAddonImages3.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[3].SetTexture("_NPCAddon" , Variables.NPCAddonImages3[Variables.indexNPCAddon % Variables.NPCAddonImages3.Count]);
                    }
                    else
                    {
                        Control.mat_anim[3].SetTexture("_NPCAddon",Variables.blackTex);
                    }
                    
                    if(Variables.NPCAddonImages4.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[4].SetTexture("_NPCAddon" , Variables.NPCAddonImages4[Variables.indexNPCAddon % Variables.NPCAddonImages4.Count]);
                    }
                    else
                    {
                        Control.mat_anim[4].SetTexture("_NPCAddon",Variables.blackTex);
                    }
                    
                    if(Variables.NPCAddonImages5.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[5].SetTexture("_NPCAddon" , Variables.NPCAddonImages5[Variables.indexNPCAddon % Variables.NPCAddonImages5.Count]);
                    }
                    else
                    {
                        Control.mat_anim[5].SetTexture("_NPCAddon",Variables.blackTex);
                    }
                    
                    if(Variables.NPCAddonImages6.Count != 0 && Variables.validNPCAddon == true)
                    {
                        Control.mat_anim[6].SetTexture("_NPCAddon" , Variables.NPCAddonImages6[Variables.indexNPCAddon % Variables.NPCAddonImages6.Count]);
                    }
                    else
                    {
                        Control.mat_anim[6].SetTexture("_NPCAddon",Variables.blackTex);
                    }

                }
                //if(Variables.validNPC00 == true)
                {
                    if(Variables.NPC00Images0.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[0].SetTexture("_NPC00" , Variables.NPC00Images0[Variables.indexNPC00 % Variables.NPC00Images0.Count]);
                    }
                    else
                    {
                        Control.mat_anim[0].SetTexture("_NPC00",Variables.blackTex);
                    }

                    if(Variables.NPC00Images1.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[1].SetTexture("_NPC00" , Variables.NPC00Images1[Variables.indexNPC00 % Variables.NPC00Images1.Count]);
                    }
                    else
                    {
                        Control.mat_anim[1].SetTexture("_NPC00",Variables.blackTex);
                    }

                    if(Variables.NPC00Images2.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[2].SetTexture("_NPC00" , Variables.NPC00Images2[Variables.indexNPC00 % Variables.NPC00Images2.Count]);
                    }
                    else
                    {
                        Control.mat_anim[2].SetTexture("_NPC00",Variables.blackTex);
                    }
                    
                    if(Variables.NPC00Images3.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[3].SetTexture("_NPC00" , Variables.NPC00Images3[Variables.indexNPC00 % Variables.NPC00Images3.Count]);
                    }
                    else
                    {
                        Control.mat_anim[3].SetTexture("_NPC00",Variables.blackTex);
                    }
                    
                    if(Variables.NPC00Images4.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[4].SetTexture("_NPC00" , Variables.NPC00Images4[Variables.indexNPC00 % Variables.NPC00Images4.Count]);
                    }
                    else
                    {
                        Control.mat_anim[4].SetTexture("_NPC00",Variables.blackTex);
                    }
                    
                    if(Variables.NPC00Images5.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[5].SetTexture("_NPC00" , Variables.NPC00Images5[Variables.indexNPC00 % Variables.NPC00Images5.Count]);
                    }
                    else
                    {
                        Control.mat_anim[5].SetTexture("_NPC00",Variables.blackTex);
                    }
                    
                    if(Variables.NPC00Images6.Count != 0 && Variables.validNPC00 == true)
                    {
                        Control.mat_anim[6].SetTexture("_NPC00" , Variables.NPC00Images6[Variables.indexNPC00 % Variables.NPC00Images6.Count]);
                    }
                    else
                    {
                        Control.mat_anim[6].SetTexture("_NPC00",Variables.blackTex);
                    }

                }
            }
        }

    }

    /**
        重叠对比模式输出NPC 重叠主角播放逻辑
    */
    private static void PlayMode1NPCCharacter()
    {
        if(Variables.NPCImages.Count != 0)
        {
            Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
            Control.matContrast.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
        }
        else
        {
            Control.mat.SetTexture("_NPC",Variables.blackTex);
            Control.matContrast.SetTexture("_NPC",Variables.blackTex);
        }
        if(Variables.NPCAddonImages.Count != 0)
        {
            Control.mat.SetTexture("_NPCAddon",Variables.NPCAddonImages[Variables.indexNPC]);
            Control.matContrast.SetTexture("_NPCAddon",Variables.NPCAddonImages[Variables.indexNPC]);
        }
        else
        {
            Control.mat.SetTexture("_NPCAddon",Variables.blackTex);
            Control.matContrast.SetTexture("_NPCAddon",Variables.blackTex);
        }
        if(Variables.NPC00Images.Count != 0)
        {
            Control.mat.SetTexture("_NPC00",Variables.NPC00Images[Variables.indexNPC]);
            Control.matContrast.SetTexture("_NPC00",Variables.NPC00Images[Variables.indexNPC]);
        }
        else
        {
            Control.mat.SetTexture("_NPC00",Variables.blackTex);
            Control.matContrast.SetTexture("_NPC00",Variables.blackTex);
        }
    }

    /**
        角色对比模式角色NPC 对比主角播放逻辑
    */
    /*private static void PlayMode2NPCCharacter()
    {
        if(Variables.NPCImages.Count != 0)
        {
            Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
        }
        else
        {
            Control.mat.SetTexture("_NPC",Variables.blackTex);
        }

        if(Variables.validBodyCharacter == true)
        {
            Control.matCharacter.SetTexture("_Body",Variables.bodyCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Body",Variables.blackTex);
        }

        if(Variables.validHeadCharacter == true)
        {
            Control.matCharacter.SetTexture("_Head",Variables.headCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Head",Variables.blackTex);
        }

        if(Variables.validWeaponCharacter == true)
        {
            Control.matCharacter.SetTexture("_Weapon",Variables.weaponCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_Weapon",Variables.blackTex);
        }
        
        if(Variables.validWeaponEffectCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponEffect",Variables.weaponEffectCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponEffect",Variables.blackTex);
        }

        if(Variables.validGemCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponGem",Variables.weaponGemCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponGem",Variables.blackTex);
        }

        if(Variables.validBodyDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_BodyDepth",Variables.bodyDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_BodyDepth",Variables.blackTex);
        }

        if(Variables.validHeadDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_HeadDepth",Variables.headDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_HeadDepth",Variables.blackTex);
        }
        
        if(Variables.validWeaponDepthCharacter == true)
        {
            Control.matCharacter.SetTexture("_WeaponDepth",Variables.weaponDepthCharacterImages[Variables.indexCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_WeaponDepth",Variables.blackTex);
        }
    }

    /**
        角色对比模式角色NPC 对比NPC播放逻辑
    */
    /*private static void PlayMode2NPCNPC()
    {
        if(Variables.NPCImages.Count != 0)
        {
            Control.mat.SetTexture("_NPC",Variables.NPCImages[Variables.indexNPC]);
        }
        else
        {
            Control.mat.SetTexture("_NPC",Variables.blackTex);
        }

        if(Variables.NPCCharacterImages.Count != 0)
        {
            Control.matCharacter.SetTexture("_NPC",Variables.NPCCharacterImages[Variables.indexNPCCharacter]);
        }
        else
        {
            Control.matCharacter.SetTexture("_NPC",Variables.blackTex);
        }
    }*/

}