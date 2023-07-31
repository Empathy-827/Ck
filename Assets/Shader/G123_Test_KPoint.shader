Shader "G123/Test_KPoint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset("Offset" , Vector) = (0,0,0,0)
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 uv = i.uv;
                float2 bias = float2(0.5 , 0.5);
                bias -= float2(0.5,0.62);
                fixed4 col = tex2D(_MainTex, uv - bias).aaaa * float4(1,0,0,1);

                return col;
            }
            ENDCG
        }
    }
}
