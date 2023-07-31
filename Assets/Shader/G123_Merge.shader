Shader "G123/Merge"
{
    Properties
    {
        _Tex("Tex",2D) = "black"{}
        _Tex2("Tex2",2D) = "black"{}
        _ReSamplingScale("ReSamplingScale",vector) = (0,0,0,0)
        _CutValue("CutValue",vector) = (0,0,0,0)
        _Sharpeness("Sharpeness",float) = 1
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
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _Tex;
            uniform half4 _Tex_TexelSize;
            sampler2D _Tex2;
            float4 _ReSamplingScale;
            float4 _CutValue;
            float _Sharpeness;

            float GetCutTerm(float2 uv,float4 cutvalue)
            {
                float cutTerm = (saturate((uv.x - cutvalue.x) * 10000000) * (1 - saturate((uv.x - (1 - cutvalue.x)) * 10000000))) * (saturate((uv.y - cutvalue.y) * 10000000) * (1 - saturate((uv.y - (1 - cutvalue.y)) * 10000000)));
                return cutTerm;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float3 color = tex2D(_Tex,uv).rgb;
                float alpha = tex2D(_Tex2,uv).a;
				
				return float4(color,alpha);

            }
            ENDCG
        }
    }
}
