Shader "G123/Render_Overlap_Contrast"
{
    Properties
    {
        _Body("Body",2D) = "black"{}
        _Head("Head",2D) = "black"{}
        _Weapon("Weapon",2D) = "black"{}
        _WeaponEffect("WeaponEffect",2D) = "black"{}
        _WeaponGem("WeaponGem",2D) = "black"{}
        _BodyDepth("BodyDepth",2D) = "black"{}
        _HeadDepth("HeadDepth",2D) = "black"{}
        _WeaponDepth("WeaponDepth",2D) = "black"{}
        _BodyOverlap("BodyOverlap",2D) = "black"{}
        _HeadOverlap("HeadOverlap",2D) = "black"{}
        _WeaponOverlap("WeaponOverlap",2D) = "black"{}
        _WeaponEffectOverlap("WeaponEffectOverlap",2D) = "black"{}
        _WeaponGemOverlap("WeaponGemOverlap",2D) = "black"{}
        _BodyDepthOverlap("BodyDepthOverlap",2D) = "black"{}
        _HeadDepthOverlap("HeadDepthOverlap",2D) = "black"{}
        _WeaponDepthOverlap("WeaponDepthOverlap",2D) = "black"{}
        _NPC("NPC",2D) = "black"{}
        _BackPic("BackPic",2D) = "balck"{}
        _NPCOverlap("_NPCOverlap",2D) = "black"{}
        _KPointTex("KPointTex",2D) = "black"{}
        _KPointView("KPointView",float) = 1

        _CutValue("CutValue",Vector) = (0,0,0,0)
        _CutValueNPC("CutValueNPC",Vector) = (0,0,0,0)
        _CutValueOverlap("CutValueOverlap",Vector) = (0,0,0,0)
        _CutValueOverlapNPC("CutValueOverlapNPC",Vector) = (0,0,0,0)

        _DiffMult("DiffMult",float) = 0
        _AlphaMult("AlphaMult",float) = 0
        _SoldMult("SoldMult",float) = 1
        _PixelDiffMult("PixelDiffMult",float) = 0

        _BackPicMult("BackPicMult",float) = 0

        _Controler("Controler",range(0,1)) = 0.5
        _FinalLerp("FinalLerp",range(0,1)) = 0
        _DiffLerp("DiffLerp",range(0,1)) = 0
        _PixelDiffLerp("PixelDiffLerp",range(0,1)) = 0

        _KPointOffset("KPointOffset",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetOverlap("KPointOffsetOverlap",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetNPC("KPointOffsetNPC",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetNPCOverlap("KPointOffsetNPCOverlap",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetMult("KPointOffsetMult",float) = 1

        _ScaleRescale("ScaleRescale",Vector) = (1,1,0,0)
        _ScaleNPCRescale("ScaleNPCRescale",Vector) = (1,1,0,0)
        _KPointOffsetRescale("KPointOffsetRescale",Vector) = (0,0,0,0)
        _KPointOffsetNPCRescale("KPointOffsetNPCRescale",Vector) = (0,0,0,0)

        _BackColor("BackColor",Color) = (0,0,0,0)
        _AlphaCut("AlphaCut",float) = 0
        
        _Scale("Scale",Vector) = (1,1,0,0)
        _ScaleNPC("ScaleNPC",Vector) = (1,1,0,0)
        _ScaleOverlap("ScaleOverlap",Vector) = (1,1,0,0)
        _ScaleOverlapNPC("ScaleOverlapNPC",Vector) = (1,1,0,0)
        _Coverage("Coverage",int) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 pos_world : TEXCOORD1;
            };

            Texture2D _Body;
            Texture2D _Head;
            Texture2D _Weapon;
            Texture2D _WeaponEffect;
            Texture2D _WeaponGem;
            Texture2D _BodyDepth;
            Texture2D _HeadDepth;
            Texture2D _WeaponDepth;
            SamplerState sampler_Body;
            Texture2D _BodyOverlap;
            Texture2D _HeadOverlap;
            Texture2D _WeaponOverlap;
            Texture2D _WeaponEffectOverlap;
            Texture2D _WeaponGemOverlap;
            Texture2D _BodyDepthOverlap;
            Texture2D _HeadDepthOverlap;
            Texture2D _WeaponDepthOverlap;
            SamplerState sampler_BodyOverlap;

            sampler2D _KPointTex;
            sampler2D _NPC;
            sampler2D _NPCOverlap;
            sampler2D _BackPic;

            float _KPointView;

            float _DiffMult;
            float _AlphaMult;
            float _SoldMult;
            float _PixelDiffMult;
            float _KPointOffsetMult;

            float _DiffLerp;
            float _PixelDiffLerp;
            float _Controler;
            float _FinalLerp;

            float _BackPicMult;
            float _AlphaCut;

            float4 _CutValue;
            float4 _CutValueNPC;
            float4 _CutValueOverlap;
            float4 _CutValueOverlapNPC;

            float4 _KPointOffset;
            float4 _KPointOffsetOverlap;
            float4 _KPointOffsetNPC;
            float4 _KPointOffsetNPCOverlap;
            float4 _Scale;
            float4 _ScaleOverlap;
            float4 _ScaleNPC;
            float4 _ScaleOverlapNPC;
            float3 _BackColor;
            int _Coverage;

            float4 _ScaleRescale;
            float4 _ScaleNPCRescale;
            float4 _KPointOffsetRescale;
            float4 _KPointOffsetNPCRescale;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.pos_world = mul(unity_ObjectToWorld,v.vertex);
                o.uv = v.uv;
                return o;
            }

            float GetCutTerm(float2 uv,float4 cutvalue)
            {
                float cutTerm = (saturate((uv.x - cutvalue.x) * 10000000) * (1 - saturate((uv.x - (1 - cutvalue.x)) * 10000000))) * (saturate((uv.y - cutvalue.y) * 10000000) * (1 - saturate((uv.y - (1 - cutvalue.y)) * 10000000)));
                return cutTerm;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                _Scale = _Scale * _ScaleRescale;
                _ScaleNPC = _ScaleNPC * _ScaleNPCRescale;
                float2 uv = (1 / _Scale.xy * i.uv + (1 / _Scale.xy * -0.5) + 0.5) + ( float2(_KPointOffset.x, _KPointOffset.y) * _KPointOffsetMult);
                uv = uv + float2(_KPointOffsetRescale.x,_KPointOffsetRescale.y);
                float2 uvNPC = (1 / _ScaleNPC.xy * i.uv + (1 / _ScaleNPC.xy * -0.5) + 0.5) + ( float2(_KPointOffsetNPC.x, _KPointOffsetNPC.y) * _KPointOffsetMult);
                uvNPC = uvNPC + float2(_KPointOffsetNPCRescale.x,_KPointOffsetNPCRescale.y);
                float2 uvOverlap = (1 / _ScaleOverlap.xy * i.uv + (1 / _ScaleOverlap.xy * -0.5) + 0.5) + ( float2(_KPointOffsetOverlap.x, _KPointOffsetOverlap.y));
                float2 uvNPCOverlap = (1 / _ScaleOverlapNPC.xy * i.uv + (1 / _ScaleOverlapNPC.xy * -0.5) + 0.5) + ( float2(_KPointOffsetNPCOverlap.x, _KPointOffsetNPCOverlap.y));
                
                float cutTermOverlapNPC = GetCutTerm(i.uv,_CutValueOverlapNPC);
                float cutTermOverlap = GetCutTerm(i.uv,_CutValueOverlap);
                float cutTermNPC = GetCutTerm(i.uv,_CutValueNPC);
                float cutTerm = GetCutTerm(i.uv,_CutValue);

                float4 headDepth = _HeadDepth.Sample(sampler_Body,uv);
                headDepth = lerp(float4(0,0,0,0),headDepth,cutTerm);
                float4 bodyDepth = _BodyDepth.Sample(sampler_Body,uv);
                bodyDepth = lerp(float4(0,0,0,0),bodyDepth,cutTerm);
                float4 weaponDepth = _WeaponDepth.Sample(sampler_Body,uv);
                weaponDepth = lerp(float4(0,0,0,0),weaponDepth,cutTerm);

                float4 headDepthOverlap = _HeadDepthOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                headDepthOverlap = lerp(float4(0,0,0,0),headDepthOverlap,cutTermOverlap);
                float4 bodyDepthOverlap = _BodyDepthOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                bodyDepthOverlap = lerp(float4(0,0,0,0),bodyDepthOverlap,cutTermOverlap);
                float4 weaponDepthOverlap = _WeaponDepthOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                weaponDepthOverlap = lerp(float4(0,0,0,0),weaponDepthOverlap,cutTermOverlap);

                float headDepthFinal = (headDepth.r + 0.0001) * headDepth.w;
                float bodyDepthFinal = bodyDepth.r * bodyDepth.w;
                float weaponDepthFinal = weaponDepth.r * weaponDepth.w;
                float headDepthOverlapFinal = (headDepthOverlap.r + 0.0001) * headDepthOverlap.w;
                float bodyDepthOverlapFinal = bodyDepthOverlap.r * bodyDepthOverlap.w;
                float weaponDepthOverlapFinal = weaponDepthOverlap.r * weaponDepthOverlap.w;

                float4 npc = tex2D(_NPC,uvNPC);
                npc = lerp(float4(0,0,0,0),npc,cutTermNPC);
                float4 head = _Head.Sample(sampler_Body,uv);
                head = lerp(float4(0,0,0,0),head,cutTerm);
                float4 body = _Body.Sample(sampler_Body,uv);
                body = lerp(float4(0,0,0,0),body,cutTerm);
                float4 weapon = _Weapon.Sample(sampler_Body,uv);
                float4 weaponEffect = _WeaponEffect.Sample(sampler_Body,uv);
                float4 weaponGem = _WeaponGem.Sample(sampler_Body,uv);
                weapon.rgb = lerp(weapon.rgb,weaponEffect.rgb,weaponEffect.a);
                weapon.rgb = lerp(weapon.rgb,weaponGem.rgb,weaponGem.a);
                weapon = lerp(float4(0,0,0,0),weapon,cutTerm);

                float3 backPic = tex2D(_BackPic,i.uv);

                float4 npcOverlap = tex2D(_NPCOverlap,uvNPCOverlap);
                npcOverlap = lerp(float4(0,0,0,0),npcOverlap,cutTermOverlapNPC);
                float4 headOverlap = _HeadOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                headOverlap = lerp(float4(0,0,0,0),headOverlap,cutTermOverlap);
                float4 bodyOverlap = _BodyOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                bodyOverlap = lerp(float4(0,0,0,0),bodyOverlap,cutTermOverlap);
                float4 weaponOverlap = _WeaponOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                float4 weaponEffectOverlap = _WeaponEffectOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                float4 weaponGemOverlap = _WeaponGemOverlap.Sample(sampler_BodyOverlap,uvOverlap);
                weaponOverlap.rgb = lerp(weaponOverlap.rgb,weaponEffectOverlap.rgb,weaponEffectOverlap.a);
                weaponOverlap.rgb = lerp(weaponOverlap.rgb,weaponGemOverlap.rgb,weaponGemOverlap.a);
                weaponOverlap = lerp(float4(0,0,0,0),weaponOverlap,cutTermOverlap);
                
                float hbBasic = (headDepthFinal - bodyDepthFinal) > 0 ? 1 : 0;
                float bwBasic = (weaponDepthFinal - max(headDepthFinal,bodyDepthFinal)) >= 0 ? 1 : 0;

                float2 bias = float2(0.5,0.5) - float2(0.5,0.32);
                float3 kPoint = tex2D(_KPointTex,i.uv + bias).a * float3(1,0,0) * _KPointView;
                float3 backGround = lerp(_BackColor + backPic * _BackPicMult,kPoint,kPoint.r);

                float3 finalBasic = lerp(body,head,hbBasic);
                finalBasic = lerp(finalBasic,weapon,bwBasic);

                float alpha = max(max(weapon.w,max(max(head.w,body.w),max(weaponEffect.a,weaponGem.a))),npc.a);
                finalBasic = lerp(float3(0,0,0),finalBasic + npc.rgb,alpha);

                float3 finalBasic2 = lerp(backGround,finalBasic , alpha);

                float hbOverlap = (headDepthOverlapFinal - bodyDepthOverlapFinal) > 0 ? 1 : 0;
                float bwOverlap = (weaponDepthOverlapFinal - max(headDepthOverlapFinal,bodyDepthOverlapFinal)) >= 0 ? 1 : 0;

                float3 finalOverlap = lerp(bodyOverlap,headOverlap,hbOverlap);
                finalOverlap = lerp(finalOverlap,weaponOverlap,bwOverlap);

                float alphaOverlap = max(max(weaponOverlap.w,max(max(headOverlap.w,bodyOverlap.w),max(weaponEffectOverlap.a,weaponGemOverlap.a))),npcOverlap.a);
                finalOverlap = lerp(float3(0,0,0),finalOverlap + npcOverlap.rgb , alphaOverlap);

                float3 finalOverlap2 = lerp(backGround,finalOverlap,alphaOverlap);
                
                float backTerm = (alpha + alphaOverlap) > 0 ? 1 : 0;

                float3 final = lerp(finalBasic2,finalOverlap2,saturate((alphaOverlap - _AlphaCut) * 1000));
                float3 final2 = lerp(finalOverlap2,finalBasic2,saturate((alpha - _AlphaCut) * 1000));
                final = lerp(final,final2,lerp(1 - _FinalLerp,_FinalLerp,_Coverage));

                float3 finalAlpha = lerp(finalBasic2,finalOverlap2,lerp(1 - _Controler,_Controler,_Coverage));

                float3 finalDiff = (saturate((alphaOverlap - _AlphaCut) * 1000) - saturate((alpha - _AlphaCut) * 1000)).xxx;
                float3 finalDiff2 = (saturate((alpha - _AlphaCut) * 1000) - saturate((alphaOverlap - _AlphaCut) * 1000)).xxx;
                finalDiff = lerp(finalDiff,finalDiff2,lerp(1 - _DiffLerp,_DiffLerp,_Coverage));
                
                finalBasic = finalBasic * alpha;
                finalOverlap = finalOverlap * alphaOverlap;
                
                float finalDiffTerm = ((finalBasic.r + finalBasic.g + finalBasic.b) - (finalOverlap.r + finalOverlap.g + finalOverlap.b)) != 0 ? 0 : 1;
                float3 finalPixelDiff = lerp(float3(1,0,0),final,finalDiffTerm);
                float3 finalPixelDiff2 = lerp(final,float3(1,0,0),finalDiffTerm);
                finalPixelDiff = lerp(finalPixelDiff,finalPixelDiff2,lerp(1 - _PixelDiffLerp,_PixelDiffLerp,_Coverage));

                final = final * _SoldMult + finalAlpha * _AlphaMult + finalDiff * _DiffMult + finalPixelDiff * _PixelDiffMult;
                return float4(final , 1);
            }
            ENDCG
        }
    }
}
