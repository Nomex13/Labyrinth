Shader "Custom/ThresholdTransparentBumpedDiffuse"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_BumpMap ("Normal map", 2D) = "bump" {}
		_ThresholdMap ("Threshold map", 2D) = "bump" {}
		_Threshold ("Threshold value", Range(0, 1)) = 0
		_BorderColor ("Border color", Color) = (1,1,1,1)
		_BorderThickness ("Border thickness", Range(0, 1)) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}
		LOD 300
	
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _ThresholdMap;
		float _Threshold;
		float _BorderThickness;
		fixed4 _Color;
		fixed4 _BorderColor;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 colorMain = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 colorThreshold = tex2D(_ThresholdMap, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			fixed lumence = dot(colorMain.rgb, float3(0.299, 0.587, 0.144));

			float threshold = (colorThreshold.r + colorThreshold.g + colorThreshold.b) / 3;
			if (threshold > _Threshold)
			{
				colorMain.a = 0;
			}
			/*if (threshold < _Threshold && threshold > _Threshold - _BorderThickness)
			{
				colorMain.rgb += lerp (fixed3(0, 0, 0), _BorderColor, (threshold - _Threshold) / _BorderThickness);
			}*/
			if (threshold < _Threshold && threshold > _Threshold - _BorderThickness)
			{
				colorMain.rgb -= fixed3(lumence, lumence, lumence);
				colorMain.rgb += lerp (fixed3(0, 0, 0), _BorderColor, (_Threshold - threshold) / _BorderThickness);
			}

			o.Albedo = colorMain.rgb;
			o.Alpha = colorMain.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
