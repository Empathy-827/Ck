Shader "G123/Render_Export_NPC"
{
    Properties
    {

        _NPC("NPC",2D) = "black"{}


        _ScaleNPC("ScaleNPC",Vector) = (1,1,0,0)
        _ScaleNPCRescale("ScaleNPCRescale",Vector) = (1,1,0,0)
        _KPointOffsetNPCRescale("KPointOffsetNPCRescale",Vector) = (0,0,0,0)

        _CutValueNPC("CutValueNPC",Vector) = (0,0,0,0)


        _KPointOffsetNPC("KPointOffsetNPC",Vector) = (-0.5,-0.5,0,0)
        _KPointOffsetMult("KPointOffsetMult",float) = 1
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

            sampler2D _NPC;
            float4 _ScaleNPC;
            float4 _KPointOffsetNPC;
            float3 _BackColor;
            float4 _CutValueNPC;
            float _KPointOffsetMult;
            
            float4 _ScaleNPCRescale;
            float4 _KPointOffsetNPCRescale;

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
                _ScaleNPC = _ScaleNPC * _ScaleNPCRescale;
                float2 uvNPC = (1 / _ScaleNPC.xy * i.uv + (1 / _ScaleNPC.xy * -0.5) + 0.5) + ( float2(_KPointOffsetNPC.x, _KPointOffsetNPC.y) * _KPointOffsetMult);
                uvNPC = uvNPC + float2(_KPointOffsetNPCRescale.x,_KPointOffsetNPCRescale.y);

                float cutTermNPC = GetCutTerm(i.uv,_CutValueNPC);
                
                float4 npc = tex2D(_NPC,uvNPC);
                npc = lerp(float4(0,0,0,0) , npc , cutTermNPC);

                return float4(npc.rgb,npc.a);
            }
            ENDCG
        }
    }
}
