Shader "Custom/Main" {
    Properties {
        _MainTex ("Main Tex", 2D) = "white" {}
		[Toggle(FlowOutwards)] _FlowOutward("Flow Outwards", Int) = 1
		_CenterX("Wave Center X", Range(-1, 2)) = 0.5
		_CenterY("Wave Center Y", Range(-1, 2)) = 0.5
		_WaveLength("Wave Length", Range(0.0001, 5)) = 0.02
		_WaveAmplitude("Wave Amplitude", Range(0, 2)) = 0.02
		_WavePhase("Wave Phase", Range(0, 1)) = 0
		_WaveSpeed("Wave Speed", Range(0, 100)) = 1
		// _NoiseOffset ("Noise Offset", Float) = 0
    }
    SubShader {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma target 2.0

            #include "UnityCG.cginc"
			#include "UnityUI.cginc"

			struct appdata_t {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

            struct v2f {
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
            };

            /*v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }*/

            sampler2D _MainTex;
			uniform float4 _ClipRect;
			uniform int _FlowOutward;
			uniform float _CenterX;
			uniform float _CenterY;
			uniform float _WaveLength;
			uniform float _WaveAmplitude;
			uniform float _WavePhase;
			uniform float _WaveSpeed;

			v2f vert(appdata_t IN) {
				v2f OUT;
				/*UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);*/
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				OUT.texcoord = IN.texcoord;

				OUT.color = IN.color;
				return OUT;
			}

            fixed4 frag (v2f IN) : SV_Target {
				float offsetX = IN.texcoord.x - _CenterX;
				float offsetY = IN.texcoord.y - _CenterY;
				float dis = sqrt(abs(offsetX * offsetX + offsetY * offsetY));

				float v1;
				if (_FlowOutward != 0) {
					v1 = frac(dis / _WaveLength + _WavePhase - _Time * _WaveSpeed);
				}
				else {
					v1 = frac(dis / _WaveLength + _WavePhase + _Time * _WaveSpeed);
				}

				float vSin = _WaveAmplitude * sin(v1*6.2832f);
				float2 coordOffset = float2(vSin*offsetX / dis, vSin*offsetY / dis);
				fixed4 color = tex2D(_MainTex, IN.texcoord + coordOffset);

				color.a *= UnityGet2DClipping(IN.vertex.xy, _ClipRect);

		/*#ifdef UNITY_UI_ALPHACLIP
				clip(color.a - 0.001);
		#endif*/

				return color;
            }
            ENDCG
        }
    }
}

