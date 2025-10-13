using System;
using UnityEngine;
public class Performance
{
    // Weights for each use case: CST, CMT, GPU, SRAM, VRAM
    static readonly float[] webBrowsingBars = { 100f, 20f, 10f, 30f, 10f };
    static readonly float[] softwareCompilingBars = { 100f, 100f, 20f, 80f, 40f };
    static readonly float[] modelingRenderingBars = { 30f, 100f, 100f, 90f, 100f };
    static readonly float[] photoEditingBars = { 100f, 60f, 100f, 80f, 70f };
    static readonly float[] videoEditingBars = { 100f, 100f, 100f, 70f, 80f };
    static readonly float[] aimlBars = { 100f, 100f, 100f, 80f, 100f };
    static readonly float[] gamingBars = { 60f, 60f, 100f, 80f, 80f };
    static readonly float[] musicProductionBars = { 100f, 100f, 20f, 70f, 10f };
    static readonly float[] physicsSimBars = { 30f, 100f, 100f, 80f, 80f };

    static readonly float[] maxInputs = { 5100f, 70200f, 39150f, 638803f, 32f }; // CST, CMT, GPU, SRAM, VRAM

    // Weighted exponentiation product
    static float WeightedExpProduct(float[] weights, int cst, int cmt, int gpu, int sram, int vram)
    {
        // Normalize weights to 0-1
        float w0 = weights[0] / 100f;
        float w1 = weights[1] / 100f;
        float w2 = weights[2] / 100f;
        float w3 = weights[3] / 100f;
        float w4 = weights[4] / 100f;

        // Avoid zero or negative values for exponentiation
        float v0 = Mathf.Max(1, cst);
        float v1 = Mathf.Max(1, cmt);
        float v2 = Mathf.Max(1, gpu);
        float v3 = Mathf.Max(1, sram);
        float v4 = Mathf.Max(1, vram);

        return
            Mathf.Pow(v0, w0) *
            Mathf.Pow(v1, w1) *
            Mathf.Pow(v2, w2) *
            Mathf.Pow(v3, w3) *
            Mathf.Pow(v4, w4);
    }

    // Maximum possible score for normalization
    static float MaxWeightedExpProduct(float[] weights)
    {
        float w0 = weights[0] / 100f;
        float w1 = weights[1] / 100f;
        float w2 = weights[2] / 100f;
        float w3 = weights[3] / 100f;
        float w4 = weights[4] / 100f;

        float v0 = Mathf.Max(1, maxInputs[0]);
        float v1 = Mathf.Max(1, maxInputs[1]);
        float v2 = Mathf.Max(1, maxInputs[2]);
        float v3 = Mathf.Max(1, maxInputs[3]);
        float v4 = Mathf.Max(1, maxInputs[4]);

        return
            Mathf.Pow(v0, w0) *
            Mathf.Pow(v1, w1) *
            Mathf.Pow(v2, w2) *
            Mathf.Pow(v3, w3) *
            Mathf.Pow(v4, w4);
    }

    // Normalize to 0-1000
    static float Normalize(float raw, float max)
    {
        float safeRaw = Mathf.Max(raw, 1e-6f);
        float safeMax = Mathf.Max(max, 1e-6f);
        float norm = Mathf.Clamp01(Mathf.Log(safeRaw) / Mathf.Log(safeMax));
        return Mathf.Pow(norm, 4f) * 100f; // Exaggerate differences
    }

    public static float WebBrowsing(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(webBrowsingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(webBrowsingBars);
        return Normalize(raw, max);
    }

    public static float SoftwareCompiling(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(softwareCompilingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(softwareCompilingBars);
        return Normalize(raw, max);
    }

    public static float ModelingRendering(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(modelingRenderingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(modelingRenderingBars);
        return Normalize(raw, max);
    }

    public static float PhotoEditing(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(photoEditingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(photoEditingBars);
        return Normalize(raw, max);
    }

    public static float VideoEditing(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(videoEditingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(videoEditingBars);
        return Normalize(raw, max);
    }

    public static float AIML(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(aimlBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(aimlBars);
        return Normalize(raw, max);
    }

    public static float Gaming(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(gamingBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(gamingBars);
        return Normalize(raw, max);
    }

    public static float MusicProduction(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(musicProductionBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(musicProductionBars);
        return Normalize(raw, max);
    }

    public static float PhysicsSimulations(int cst, int cmt, int gpu, int sram, int vram)
    {
        float raw = WeightedExpProduct(physicsSimBars, cst, cmt, gpu, sram, vram);
        float max = MaxWeightedExpProduct(physicsSimBars);
        return Normalize(raw, max);
    }
}