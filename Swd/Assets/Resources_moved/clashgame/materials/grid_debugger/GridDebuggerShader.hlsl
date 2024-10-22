#ifndef CLASH_DEBUG_GRID_INCLUDED
#define CLASH_DEBUG_GRID_INCLUDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"

void EntryFunc_float(float2 uvInTile,float3 tileColor,float3 borderColor,out float4 result)
{
    const float borderSize = 0.1;
    float3 col = tileColor;
    if(uvInTile.x <= borderSize || uvInTile.x >= 1.0 - borderSize || uvInTile.y <= borderSize || uvInTile.y >= 1.0 - borderSize)
    {
        col = borderColor;
    }
    result = float4(col,1.0);
    //result = float4(uvInTile,0.0,1.0);
}

#endif // CLASH_DEBUG_GRID_INCLUDED