using UnityEngine;
using System;
public class Performance
{
    /*
    

    Web browsing, text docs, emails: 
    CST: Linear
    CMT: Logarithmic
    GPU: Irrelevant
    SRAM: Sqrt up to 8, then Cbrt
    VRAM: Irrelevant

    Software compiling:
    CST: Linear
    CMT: Linear 
    GPU: Irrelevant
    SRAM: Linear up to 32, then Sqrt
    VRAM: Irrelevant

    3d modeling/rendering:
    CST: Linear
    CMT: Linear
    GPU: Linear
    SRAM: Linear up to 32, then Logarithmic
    VRAM: Linear

    Photo editing:
    CST: Linear
    CMT: Logarithmic
    GPU: Linear
    SRAM: Linear up to 32, then Cbrt
    VRAM: Linear up to 6, then Cbrt

    Video editing:
    CST: Linear
    CMT: Linear
    GPU: Linear
    SRAM: Logarithmic up to 64, then Sqrt
    VRAM: Linear up to 16, then Logarithmic

    AI/ML:
    CST: Linear
    CMT: Linear
    GPU: Linear
    SRAM: Linear up to 128, then Logarithmic
    VRAM: Binary, model needs to fit in VRAM
    (scored by % of models that can fit)

    Gaming
    CST: Linear + Logarithmic
    CMT: Logarithmic
    GPU: Linear
    SRAM: Linear up to 32, then Sqrt
    VRAM: Linear up to 12, then Logarithmic

    Music production
    CST: Linear
    CMT: Linear
    GPU: Irrelevant
    SRAM: Linear up to 64, then Sqrt
    VRAM: Irrelevant

    Physics/Scientific simulations
    CST: Linear
    CMT: Linear
    GPU: Linear
    SRAM: Linear to 128, then Logarithmic
    VRAM: Linear to 24, then Logarithmic


    */
    //at some point, normalize all these functions to a 0-100 scale for easier comparison

    public static float WebBrowsing(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    Mathf.Log(cmt) *
    ((sram < 8) ? Mathf.Sqrt(sram) : Mathf.Sqrt(8) + (float)Math.Cbrt(sram - 8));
    }

    public static float SoftwareCompiling(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    cmt *
    ((sram < 32) ? sram : 32 + Mathf.Sqrt(sram - 32));
    }

    public static float ModelingRendering(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    cmt *
    gpu *
    (((sram < 32) ? sram : 32 + Mathf.Log(sram - 32)) * vram);
    }

    public static float PhotoEditing(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    Mathf.Log(cmt) *
    gpu *
    ((sram < 32) ? sram : 32 + (float)Math.Cbrt(sram - 32)) *
    ((vram < 6) ? vram : 6 + (float)Math.Cbrt(vram - 6));
    }

    public static float VideoEditing(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    cmt *
    gpu *
    ((sram < 64) ? Mathf.Log(sram) : Mathf.Log(64) + Mathf.Sqrt(sram - 64)) *
    ((vram < 16) ? vram : 16 + Mathf.Log(vram - 16));
    }

    public static float AIML(int cst, int cmt, int gpu, int sram, int vram)
    {
        float[] modelSizes = new float[] { 4, 6, 6, 9, 15, 33 }; // in GB
        int modelsThatFit = 0;
        foreach (float size in modelSizes)
        {
            if (vram >= size)
            {
                modelsThatFit++;
            }
        }

        //THESE VALUES ARE PLACEHOLDERS, FIND ACTUAL MODEL SIZE DATA
    return
    cst *
    cmt *
    gpu *
    ((sram < 128) ? Mathf.Log(sram) : Mathf.Log(128) + Mathf.Log(sram - 128)) *
    ((float)modelsThatFit / modelSizes.Length); // percentage of models that can fit in VRAM
    }

    public static float Gaming(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    (cst + Mathf.Log(cst)) *
    Mathf.Log(cmt) *
    gpu *
    ((sram < 32) ? sram : 32 + Mathf.Sqrt(sram - 32)) *
    ((vram < 12) ? vram : 12 + Mathf.Log(vram - 12));
    }

    public static float MusicProduction(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    cmt *
    ((sram < 64) ? sram : 64 + Mathf.Sqrt(sram - 64));
    }
    
    public static float PhysicsSimulations(int cst, int cmt, int gpu, int sram, int vram)
    {
    return
    cst *
    cmt *
    gpu *
    ((sram < 128) ? sram : 128 + Mathf.Log(sram - 128)) *
    ((vram < 24) ? vram : 24 + Mathf.Log(vram - 24));
    }

}
