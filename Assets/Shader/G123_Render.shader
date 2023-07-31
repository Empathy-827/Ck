Shader "G123/Render"
{
    Properties
    {
        _NPC("NPC",2D) = "black"{}
        _NPCAddon("NPCAddon",2D) = "black"{}
        _NPC00("NPC00",2D) = "black"{}

        _BackPic("BackPic",2D) = "black"{}
        _Test("Test",2D) = "black"{}

        _Scale("Scale",Vector) = (1,1,0,0)
        _ScaleNPC("ScaleNPC",Vector) = (0.5,0.5,0,0)
        _PicScale("PicScale",float) = 1

        _BackPicMult("BackPicMult",float) = 0

        _KPointOffsetRescale("KPointOffsetRescale",Vector) = (0,0,0,0)
        _KPointOffsetNPCRescale("KPointOffsetNPCRescale",Vector) = (0,0,0,0)
        _ScaleRescale("ScaleRescale",Vector) = (1,1,0,0)
        _ScaleNPCRescale("ScaleNPCRescale",Vector) = (1,1,0,0)
        
        _CutValue("CutValue",Vector) = (0,0,0,0)
        _CutValueNPC("CutValueNPC",Vector) = (0,0,0,0)

        _KPointTex("KPointTex",2D) = "black"{}

        _KPointView("KPointView",float) = 1
        _KPointOffsetMult("KPointOffsetMult",float) = 1
        _KPointOffset("KPointOffset",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetNPC("KPointOffsetNPC",Vector) = (-0.5,-0.5,0,0)
        _BackColor("BackColor",Color) = (0,0,0,0)

        _CutSwitch("CutSwitch",int) = 0
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

            sampler2D _KPointTex;
            sampler2D _NPC;
            sampler2D _NPCAddon;
            sampler2D _NPC00;
            sampler2D _Test;
            sampler2D _BackPic;

            float _KPointView;
            float4 _CutValue;
            float4 _CutValueNPC;
            float4 _KPointOffset;
            float4 _KPointOffsetNPC;
            float4 _Scale;
            float4 _ScaleNPC;
            float3 _BackColor;
            float _HeadDepthMult;
            float _BodyDepthMult;
            float _WeaponDepthMult;
            float _BackPicMult;
            float _PicScale;
            float _KPointOffsetMult;
            int _CutSwitch;

            float4 _KPointOffsetRescale;
            float4 _KPointOffsetNPCRescale;
            float4 _ScaleRescale;
            float4 _ScaleNPCRescale;

            float GetCutTerm(float2 uv,float4 cutvalue)
            {
                float cutTerm = (saturate((uv.x - cutvalue.x) * 10000000) * (1 - saturate((uv.x - (1 - cutvalue.x)) * 10000000))) * (saturate((uv.y - cutvalue.y) * 10000000) * (1 - saturate((uv.y - (1 - cutvalue.y)) * 10000000)));
                return cutTerm;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.pos_world = mul(unity_ObjectToWorld,v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                _Scale = _Scale * _ScaleRescale;
                _ScaleNPC = _ScaleNPC * _ScaleNPCRescale;

                float2 uv = (1 / _Scale.xy / _PicScale * i.uv + (1 / _Scale.xy / _PicScale * -0.5) + 0.5) + ( float2(_KPointOffset.x , _KPointOffset.y) * _KPointOffsetMult);
                uv = uv + float2(_KPointOffsetRescale.x,_KPointOffsetRescale.y);

                float2 uvNPC = (1 / _ScaleNPC.xy / _PicScale * i.uv + (1 / _ScaleNPC.xy / _PicScale * -0.5) + 0.5) + ( float2(_KPointOffsetNPC.x ,_KPointOffsetNPC.y) * _KPointOffsetMult);
                uvNPC = uvNPC + float2(_KPointOffsetNPCRescale.x,_KPointOffsetNPCRescale.y);

                float3 final_rgb_out = float3(0,0,0);
                //添加NPC

                float4 npc = tex2D(_NPC,uvNPC);
                float3 final_rgb_npc = npc.rgb * npc.a;
                final_rgb_out = lerp(float3(0,0,0), final_rgb_npc , npc.a);

                float4 npcAddon = tex2D(_NPCAddon,uvNPC);
                float3 final_rgb_npcAddon = npcAddon.rgb * npcAddon.a;
                final_rgb_out = lerp(final_rgb_out, final_rgb_npcAddon , npcAddon.a);

                float4 npc00 = tex2D(_NPC00,uvNPC);
                float3 final_rgb_npc00 = npc00.rgb * npc00.a;
                final_rgb_out = lerp(final_rgb_out , final_rgb_out + npc00, npc00.a);


                float3 backPic = tex2D(_BackPic,i.uv);

                float2 bias = float2(0.5,0.5) - float2(0.5,0.315);
                float3 kPoint = tex2D(_KPointTex,i.uv + bias).a * float3(1,0,0) * _KPointView;
                float3 backGround = lerp(_BackColor + backPic * _BackPicMult ,kPoint,kPoint.r);

                
                
                
                
                float final_a_out = max(max(npc.a , npcAddon.a),npc00.a);



                //float finalA = max(max(weapon.w,max(head.w,body.w)),npc.a);

                return float4(final_rgb_out,final_a_out);
            }
            ENDCG
        }
    }
}
