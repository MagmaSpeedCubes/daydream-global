using UnityEngine;

public class CustomerScoring : MonoBehaviour
{
    public void CalculateCustomerScore(Customer c)
    {
        BuiltPC pc = c.finalBuild;
        int score = 0;


        for (int i = 0; i < c.useCases.Length; i++)
        {
            string useCase = c.useCases[i];
            float frequency = c.useFrequency[i];
            float performance = 0;

            int cst = pc.cpu.singleCore;
            int cmt = pc.cpu.multiCore;
            int gpu = pc.gpu != null ? pc.gpu.performance : pc.cpu.integratedGraphics;
            int sram = (int)(pc.ram.size * pc.ram.clockSpeed * Mathf.Log(pc.ram.channels));
            int vram = (int)(pc.gpu != null ? pc.gpu.VRAM + Mathf.Log(pc.ram.size): Mathf.Log(pc.ram.size));

            switch (useCase)
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
                default:
                    Debug.LogWarning("Unknown use case: " + useCase);
                    break;
            }

            score += (int)((PerformanceScore(c, performance) > 0)
            ? (int)(PerformanceScore(c, performance) * frequency)
            : (PerformanceScore(c, performance) * frequency * 100 / c.flexibility));



        }
        score += TimeBonus(c);
        score += BudgetBonus(c, pc.totalPrice);
        
        
    }
    
    public int BudgetBonus(Customer c, int totalPrice)
    {
        return (c.budget - totalPrice) * 100;
    }

    public int TimeBonus(Customer c)
    {
        return (int)((RoundStats.buildTime - c.patience * 10) * 100);
    }

    public int PerformanceScore(Customer c, float performance)
    {
        float performanceDifference = performance - c.performanceExpectation;
        return (int)(performanceDifference * 1000);
        //assuming performance calulation is normalized
    }
}
