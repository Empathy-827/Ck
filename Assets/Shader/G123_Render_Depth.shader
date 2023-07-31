Shader "G123/Render_Depth"
{
    Properties
    {

        _HeadDepth("HeadDepth",2D) = "black"{}
        _BodyDepth("BodyDepth",2D) = "black"{}
        _WeaponDepth("WeaponDepth",2D) = "black"{}

        _CutValue("CutValue",Vector) = (0,0,0,0)

        _Scale("Scale",Vector) = (1,1,0,0)
        _ScaleRescale("ScaleRescale",Vector) = (1,1,0,0)
        _KPointOffsetRescale("KPointOffsetRescale",Vector) = (0,0,0,0)
        _KPointOffset("KPointOffset",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetMult("KPointOffsetMult",float) = 1
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
            float4 _KPointOffset;
            float4 _Scale;
            float4 _CutValue;
            float _KPointOffsetMult;
            float4 _ScaleRescale;
            float4 _KPointOffsetRescale;

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
                float2 uv = (1 / _Scale.xy * i.uv + (1 / _Scale.xy * -0.5) + 0.5) + ( float2(_KPointOffset.x , _KPointOffset.y) * _KPointOffsetMult);
                uv = uv + float2(_KPointOffsetRescale.x,_KPointOffsetRescale.y);
                float cutTerm = GetCutTerm(i.uv,_CutValue);

                float4 headDepth = tex2D(_HeadDepth,uv);
                headDepth = lerp(float4(0,0,0,0),headDepth,cutTerm);
                float4 bodyDepth = tex2D(_BodyDepth,uv);
                bodyDepth = lerp(float4(0,0,0,0),bodyDepth,cutTerm);
                float4 weaponDepth = tex2D(_WeaponDepth,uv);
                weaponDepth = lerp(float4(0,0,0,0),weaponDepth,cutTerm);

                float4 final = headDepth + bodyDepth + weaponDepth;

                return float4(final);
            }
            ENDCG
        }
    }
}
