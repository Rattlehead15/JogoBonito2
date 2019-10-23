Shader "Hidden/DeepFry"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Contraste("Contraste", Float) = 1.0
		_Brillo("Brillo", Float) = 1.0
		// Gracias señor chino de Github por este shader
		_Size("Tamaño", Float) = 1.0
		_Inten("Intensidad", Float) = 1.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Contraste;
			float _Brillo;
			float _Size;
			float _Inten;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col -= tex2D(_MainTex, i.uv - _Size)*7.0*_Inten;
				col += tex2D(_MainTex, i.uv + _Size)*7.0*_Inten;
				// Contraste y brillo
				col.rgb = _Contraste * col.rgb + _Brillo;
				return col;
			}
			ENDCG
		}
	}
}
