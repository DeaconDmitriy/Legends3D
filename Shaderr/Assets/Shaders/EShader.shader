Shader "Unlit/EShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainTex2 ("Texture", 2D) = "white" {}
        _blend ("Texture blend", Range (0,1)) = 0

        _PlayerPosition ("Player Position", Vector) = (0,0,0,0)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 offset : TEXCOORD2; //float 3/4?
                float4 worldPos : TEXCOORD3;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _MainTex2;
            float4 _MainTex2_ST;

            float _blend;

            float4 _PlayerPosition;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                float3 diff = o.worldPos - _PlayerPosition;
                o.worldPos.xyz += diff;

                v.vertex = mul(unity_WorldToObject, o.worldPos);

                o.offset = float3(0,0,0);
                v.vertex.xyz += o.offset.xyz;


                //o.offset = v.vertex * _PlayerPosition;
                //v.vertex.y += o.offset.y;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col2 = tex2D(_MainTex2, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return lerp(col, col2, 0.5 + i.offset.y * 0.5);
            }
            ENDCG
        }
    }
}