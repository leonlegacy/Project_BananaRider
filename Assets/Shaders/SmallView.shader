Shader "Custom/SmallView"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
		_CenterX("Center X", Range(0, 1)) = 0.5
		_CenterY("Center Y", Range(0, 1)) = 0.5
		_Amount("Amount", float) = 0.5
		_Color("Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
			float4 _Color;
			float _Amount;
			uniform float _CenterX;
			uniform float _CenterY;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = (1 - step(_Amount, length(i.uv - float2(_CenterX,_CenterY)))) * tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
    }
    FallBack "Diffuse"
}
