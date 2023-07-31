Shader "G123/back"
{
    Properties
    {
        _Head("Head",2D) = "black"{}
        _Body("Body",2D) = "black"{}
        _Weapon("Weapon",2D) = "black"{}
        _NPC("NPC",2D) = "black"{}

        _Test("Test",2D) = "black"{}

        _Scale("Scale",Vector) = (1,1,0,0)
        _ScaleNPC("ScaleNPC",Vector) = (1,1,0,0)

        _HeadDepth("HeadDepth",2D) = "black"{}
        _HeadDepthMult("HeadDepthMult",Float) = 0
        _BodyDepth("BodyDepth",2D) = "black"{}
        _BodyDepthMult("HeadDepthMult",Float) = 0
        _WeaponDepth("WeaponDepth",2D) = "black"{}
        _WeaponDepthMult("HeadDepthMult",Float) = 0

        _CutValueNPC("CutValueNPC",Vector) = (0,0,0,0)

        _KPointTex("KPointTex",2D) = "black"{}

        _KPointView("KPointView",float) = 1
        _KPointOffset("KPointOffset",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetNPC("KPointOffsetNPC",Vector) = (-0.5,-0.5,0,0)
        _BackColor("BackColor",Color) = (0,0,0,0)
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

            sampler2D _HeadDepth;
            sampler2D _BodyDepth;
            sampler2D _WeaponDepth;
            sampler2D _Head;
            sampler2D _Body;
            sampler2D _Weapon;
            sampler2D _KPointTex;
            sampler2D _NPC;
            sampler2D _Test;
            float _KPointView;
            float4 _KPointOffset;
            float4 _KPointOffsetNPC;
            float4 _Scale;
            float4 _ScaleNPC;
            float3 _BackColor;
            float _HeadDepthMult;
            float _BodyDepthMult;
            float _WeaponDepthMult;
            float4 _CutValueNPC;

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

                return float4(0.5,0.5,0.5,1);
            }
            ENDCG
        }
    }
}
