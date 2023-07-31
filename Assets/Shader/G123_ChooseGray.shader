Shader "G123/ChooseGray"
{
	Properties
	{
		_Radius0("radius0",float) = 0.05
		_Radius1("radius1",float) = 0.45
		_Span("span",Range(0,0.5)) = 0.01
		_Position("point",vector) = (0,0,0,0)
		_ChooseColor("ChooseColor",color) = (0,0,0,0)
		_MainTex("MainTex",2D) = "black"{}
		_Amount("Amount",range(0,1)) = 0
	}
		SubShader
	{
		Tags
		{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
		}
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			float _Hue;
			float _Level;
			float _Radius0;
			float _Radius1;
			float _Span;
			float4 _Position;
			float4 _ChooseColor;
			float _Amount;

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}
			float3 HSVToRGB(float3 hsv)
			{
				return lerp(float3(1, 1, 1), saturate(3.0*abs(1.0 - 2.0*frac(hsv.r + float3(0.0, -1.0 / 3.0, 1.0 / 3.0))) - 1), hsv.g)*hsv.b;
			}
			float3 EdgeToHSV(float3 edge)
			{
				float hm = 1 / _Hue;
				float sm = 1 / (_Level*0.5);
				return float3(floor(edge.r)*hm, floor(edge.g)*sm, min(1, ceil(edge.b)*sm));
			}
			float3 GetEdge(float r,float2 uv)
			{
				float hm = 1 / _Hue;
				float h = (atan2(uv.y, uv.x) + 3.1415926) / 6.2831852 / hm;
				float sm = 1 / (_Level*0.5);
				r = r / _Radius1 * 2;
				float s = r / sm;
				float v = (2 - r) / sm;
				return float3(h,s,v);
			}
#define Delta 0.005
			fixed4 frag(v2f i) : SV_Target
			{
				_Hue = lerp(0,15,saturate(_Amount));
				_Level = lerp(0,8,saturate(_Amount));
				_Radius1 = lerp(0,0.5,saturate(_Amount));
				float2 uv = i.uv - float2(0.5,0.5);
				float2 _uv = _Position.xy;
				float r = length(uv);
				float _r = length(_uv);
				fixed4 col = fixed4(0, 0, 0, 0);
				half4 choose_color = half4(1,1,1,1);
				if (r > _Radius0 && r < _Radius1)
				{
					col.a = 1 - smoothstep(_Radius1 - Delta,_Radius1,r);
					float3 e = GetEdge(r,uv);
					float3 hsv = EdgeToHSV(e);
					col.rgb = HSVToRGB(hsv);

					float3 _e = GetEdge(_r, _uv);
					float3 _hsv = EdgeToHSV(_e);
					

					if (hsv.r == _hsv.r&&hsv.g == _hsv.g&&hsv.b == _hsv.b)
					{
						float h = frac(e.r);
						r *= 5;
						if (h < _Span / r || h >(1 - _Span / r) || frac(e.g) < _Span || frac(e.b) < _Span)
						{
							if (length(col.rgb) > 0.5)
							{
								col.rgb *= 3;
							}
							else
								col.rgb *= 5;
						}
					}
					choose_color.rgb = HSVToRGB(_hsv);
				}
				_ChooseColor = choose_color;
				float gray = dot(col.rgb,float3(0.2989, 0.5870, 0.1140));
				
				return float4(gray.xxx,col.a);
			}
		ENDCG
		}
	}
}