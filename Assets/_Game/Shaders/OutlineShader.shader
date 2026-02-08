Shader "Sprites/Outline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        
        // Outline
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.03
        _OutlineAlphaThreshold ("Alpha Threshold", Range(0, 1)) = 0.01
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            fixed4 _OutlineColor;
            float _OutlineThickness;
            float _OutlineAlphaThreshold;

            struct v2f_outline
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            fixed4 frag(v2f_outline IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                
                // Если пиксель уже непрозрачный - возвращаем как есть
                if (c.a > _OutlineAlphaThreshold)
                {
                    c.rgb *= c.a;
                    return c;
                }
                
                // Проверяем 8 соседних пикселей
                float2 texelSize = float2(_OutlineThickness, _OutlineThickness);
                
                // 4 основных направления
                fixed4 up = SampleSpriteTexture(IN.texcoord + float2(0, texelSize.y));
                fixed4 down = SampleSpriteTexture(IN.texcoord - float2(0, texelSize.y));
                fixed4 left = SampleSpriteTexture(IN.texcoord - float2(texelSize.x, 0));
                fixed4 right = SampleSpriteTexture(IN.texcoord + float2(texelSize.x, 0));
                
                // 4 диагональных направления
                fixed4 upLeft = SampleSpriteTexture(IN.texcoord + float2(-texelSize.x, texelSize.y));
                fixed4 upRight = SampleSpriteTexture(IN.texcoord + float2(texelSize.x, texelSize.y));
                fixed4 downLeft = SampleSpriteTexture(IN.texcoord - float2(texelSize.x, texelSize.y));
                fixed4 downRight = SampleSpriteTexture(IN.texcoord - float2(-texelSize.x, texelSize.y));
                
                // Проверяем наличие непрозрачных пикселей вокруг
                bool hasOutline = 
                    up.a > _OutlineAlphaThreshold ||
                    down.a > _OutlineAlphaThreshold ||
                    left.a > _OutlineAlphaThreshold ||
                    right.a > _OutlineAlphaThreshold ||
                    upLeft.a > _OutlineAlphaThreshold ||
                    upRight.a > _OutlineAlphaThreshold ||
                    downLeft.a > _OutlineAlphaThreshold ||
                    downRight.a > _OutlineAlphaThreshold;
                
                if (hasOutline)
                {
                    return _OutlineColor * _OutlineColor.a;
                }
                
                // Прозрачный пиксель без обводки
                return c;
            }
            ENDCG
        }
    }
}