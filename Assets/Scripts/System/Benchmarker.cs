using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System;

public class Benchmarker : MonoBehaviour
{
    [SerializeField] private Canvas BenchmarkerUI;
    [SerializeField] private BuildHUD buildHUD;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (RoundStats.partsSelected == false)
        {
            buildHUD.ShowPopup("Info", "Benchmark Zone", "Build your PC to run benchmarks!", 3);
            return;
        }
        else
        {
            BenchmarkerUI.enabled = true;
            Debug.Log("Player entered benchmark zone");
        }



    }



    public static int RunBenchmark(string benchmark)
    {

        BuiltPC pc = RoundStats.builtPC;

        Debug.Log(pc.cpu);
        Debug.Log(pc.cooler);
        float coolingPercentage = Math.Min(1, pc.cpu.powerConsumption / pc.cooler.coolingPower);

        

        int cst = (int)(pc.cpu.singleCore * coolingPercentage);
        int cmt = (int)(pc.cpu.multiCore * coolingPercentage);
        int gpu = pc.gpu != null ? pc.gpu.performance : pc.cpu.integratedGraphics;
        int sram = (int)(pc.ram.size * pc.ram.clockSpeed * Mathf.Log(pc.ram.channels));
        int vram = (int)(pc.gpu != null ? pc.gpu.VRAM + Mathf.Log(pc.ram.size) : Mathf.Log(pc.ram.size));

        Debug.Log("CST: " + cst + " CMT: " + cmt + " GPU: " + gpu + " SRAM:" + sram + " VRAM: " + vram);

        float performance = 0;

        switch (benchmark)
        {
            case "Web Browsing":
                performance = Performance.WebBrowsing(cst, cmt, gpu, sram, vram);
                break;
            case "Software Compiling":
                performance = Performance.SoftwareCompiling(cst, cmt, gpu, sram, vram);
                break;
            case "Modeling/Rendering":
                performance = Performance.ModelingRendering(cst, cmt, gpu, sram, vram);
                break;
            case "Photo Editing":
                performance = Performance.PhotoEditing(cst, cmt, gpu, sram, vram);
                break;
            case "Video Editing":
                performance = Performance.VideoEditing(cst, cmt, gpu, sram, vram);
                break;
            case "AI/ML":
                performance = Performance.AIML(cst, cmt, gpu, sram, vram);
                break;
            case "Gaming":
                performance = Performance.Gaming(cst, cmt, gpu, sram, vram); //placeholder
                break;
            case "Music Production":
                performance = Performance.MusicProduction(cst, cmt, gpu, sram, vram); //placeholder
                break;
            case "Physics Simulations":
                performance = Performance.PhysicsSimulations(cst, cmt, gpu, sram, vram); //placeholder
                break;
        }

        return (int)performance;
    }

    public void OnTriggerExit2D(Collider2D other)
    {

        ExitBuildZone();

    }

    public void ExitBuildZone()
    {
        BenchmarkerUI.enabled = false;
        Debug.Log("Player exited benchmark zone");
    }
}