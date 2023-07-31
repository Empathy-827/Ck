Shader "G123/Render/ExportGIFNPC"
{
    Properties
    {
        _NPC("NPC",2D) = "black"{}

        _BackPic("BackPic",2D) = "black"{}


        _ScaleNPC("ScaleNPC",Vector) = (1,1,0,0)
        _PicScale("PicScale",float) = 1

        _BackPicMult("BackPicMult",float) = 0
        
        _CutValueNPC("CutValueNPC",Vector) = (0,0,0,0)

        _KPointTex("KPointTex",2D) = "black"{}

        _KPointView("KPointView",float) = 1
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
            sampler2D _BackPic;

            float _KPointView;
            float4 _CutValueNPC;
            float4 _KPointOffsetNPC;
            float4 _ScaleNPC;
            float3 _BackColor;
            float _BackPicMult;
            float _PicScale;
            int _CutSwitch;

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
                float2 uvNPC = (1 / _ScaleNPC.xy / _PicScale * i.uv + (1 / _ScaleNPC.xy / _PicScale * -0.5) + 0.5) + ( float2(_KPointOffsetNPC.x, _KPointOffsetNPC.y));

                float cutTermNPC = GetCutTerm(i.uv,_CutValueNPC);
                cutTermNPC = lerp(cutTermNPC,1,_CutSwitch);
                
                float4 npc = tex2D(_NPC,uvNPC);
                npc = lerp(float4(0,0,0,0),npc,cutTermNPC);
                float3 backPic = tex2D(_BackPic,i.uv);


                float2 bias = float2(0.5,0.5) - float2(0.5,0.315);
                float3 kPoint = tex2D(_KPointTex,i.uv + bias).a * float3(1,0,0) * _KPointView;
                float3 backGround = lerp(_BackColor , backPic , _BackPicMult);

                float3 final = npc.rgb;
                final = lerp(backGround,final,npc.a);

                return float4(final,1);
            }
            ENDCG
        }
    }
}
