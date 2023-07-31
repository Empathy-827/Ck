Shader "G123/Render/ExportGIF"
{
    Properties
    {
        _Head("Head",2D) = "black"{}
        _Body("Body",2D) = "black"{}
        _Weapon("Weapon",2D) = "black"{}

        _BackPic("BackPic",2D) = "black"{}

        _Scale("Scale",Vector) = (1,1,0,0)
        _PicScale("PicScale",float) = 1

        _HeadDepth("HeadDepth",2D) = "black"{}
        _HeadDepthMult("HeadDepthMult",Float) = 0
        _BodyDepth("BodyDepth",2D) = "black"{}
        _BodyDepthMult("HeadDepthMult",Float) = 0
        _WeaponDepth("WeaponDepth",2D) = "black"{}
        _WeaponDepthMult("HeadDepthMult",Float) = 0
        _BackPicMult("BackPicMult",float) = 0
        
        _CutValue("CutValue",Vector) = (0,0,0,0)

        _KPointTex("KPointTex",2D) = "black"{}

        _KPointView("KPointView",float) = 1
        _KPointOffset("KPointOffset",Vector) = (-0.5,-0.5,0,0)
        _BackColor("BackColor",Color) = (0,0,0,0)

        _WeaponEffect("WeaponEffect",2D) = "black"{}
        _WeaponGem("WeaponGem",2D) = "black"{}

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

            sampler2D _HeadDepth;
            sampler2D _BodyDepth;
            sampler2D _WeaponDepth;
            sampler2D _Head;
            sampler2D _Body;
            sampler2D _Weapon;
            sampler2D _KPointTex;
            sampler2D _BackPic;
            sampler2D _WeaponEffect;
            sampler2D _WeaponGem;

            float _KPointView;
            float4 _CutValue;
            float4 _KPointOffset;
            float4 _Scale;
            float3 _BackColor;
            float _HeadDepthMult;
            float _BodyDepthMult;
            float _WeaponDepthMult;
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
                float2 uv = (1 / _Scale.xy / _PicScale * i.uv + (1 / _Scale.xy / _PicScale * -0.5) + 0.5) + ( float2(_KPointOffset.x, _KPointOffset.y));

                float cutTerm = GetCutTerm(i.uv,_CutValue);
                cutTerm = lerp(cutTerm,1,_CutSwitch);
                
                float4 headDepth = tex2D(_HeadDepth,uv);
                headDepth = lerp(float4(0,0,0,0),headDepth,cutTerm);
                float4 bodyDepth = tex2D(_BodyDepth,uv);
                bodyDepth = lerp(float4(0,0,0,0),bodyDepth,cutTerm);
                float4 weaponDepth = tex2D(_WeaponDepth,uv);
                weaponDepth = lerp(float4(0,0,0,0),weaponDepth,cutTerm);

                float4 weaponEffect = tex2D(_WeaponEffect,uv);
                float4 weaponGem = tex2D(_WeaponGem,uv);
                
                float headDepthFinal = lerp(headDepth.r * headDepth.w , 1 , _HeadDepthMult);
                float bodyDepthFinal = lerp(bodyDepth.r * bodyDepth.w , 1 , _BodyDepthMult);
                float weaponDepthFinal = lerp(weaponDepth.r * weaponDepth.w , 1 , _WeaponDepthMult);

                float4 head = tex2D(_Head,uv);
                head = lerp(float4(0,0,0,0),head,cutTerm);
                float4 body = tex2D(_Body,uv);
                body = lerp(float4(0,0,0,0),body,cutTerm);
                float4 weapon = tex2D(_Weapon,uv);
                weapon = lerp(float4(0,0,0,0),weapon,cutTerm);
                weapon.rgb = lerp(weapon,weaponEffect.rgb,weaponEffect.a);
                weapon.rgb = lerp(weapon,weaponGem.rgb,weaponGem.a);
                float3 backPic = tex2D(_BackPic,i.uv);

                float hb = (headDepthFinal - bodyDepthFinal) > 0 ? 1 : 0;
                float bw = (weaponDepthFinal - max(headDepthFinal,bodyDepthFinal)) >= 0 ? 1 : 0;

                float2 bias = float2(0.5,0.5) - float2(0.5,0.315);
                float3 kPoint = tex2D(_KPointTex,i.uv + bias).a * float3(1,0,0) * _KPointView;
                float3 backGround = lerp(_BackColor , backPic , _BackPicMult);

                float3 final = lerp(body,head,hb);
                final = lerp(final,weapon,bw);
                final = lerp(backGround,final,max(weapon.w,max(max(head.w,body.w),max(weaponEffect.a,weaponGem.a))));

                return float4(final,1);
            }
            ENDCG
        }
    }
}
