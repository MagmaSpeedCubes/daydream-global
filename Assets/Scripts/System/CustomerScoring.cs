using UnityEngine;

public class CustomerScoring : MonoBehaviour
{
    public static string CalculateCustomerScore(Customer c)
    {
        BuiltPC pc = RoundStats.builtPC;
        int score = 0;
        System.Text.StringBuilder log = new System.Text.StringBuilder();

        log.AppendLine($"Customer: {c.name}");
        log.AppendLine($"Budget: {c.budget}, Flexibility: {c.flexibility}, Patience: {c.patience}");

        Debug.Log(pc.totalPrice);
        Debug.Log(RoundStats.buildTime);

        log.AppendLine($"Build Price: {pc.totalPrice}, Build Time: {RoundStats.buildTime}");

        // Generate performance expectation based on budget
        float perfExpectation = GeneratePerformanceExpectation(c.budget);

        for (int i = 0; i < c.useCases.Length; i++)
        {
            string useCase = c.useCases[i];
            float frequency = c.useFrequency[i];
            float performance = 0;

            int cst = pc.cpu.singleCore;
            int cmt = pc.cpu.multiCore;
            int gpu = pc.gpu != null ? pc.gpu.performance : pc.cpu.integratedGraphics;
            int sram = (int)(pc.ram.size * pc.ram.clockSpeed * Mathf.Log(pc.ram.channels));
            int vram = (int)(pc.gpu != null ? pc.gpu.VRAM + Mathf.Log(pc.ram.size) : Mathf.Log(pc.ram.size));

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
                    performance = Performance.Gaming(cst, cmt, gpu, sram, vram);
                    break;
                case "Music Production":
                    performance = Performance.MusicProduction(cst, cmt, gpu, sram, vram);
                    break;
                case "Physics Simulations":
                    performance = Performance.PhysicsSimulations(cst, cmt, gpu, sram, vram);
                    break;
                default:
                    log.AppendLine($"Unknown use case: {useCase}");
                    continue;
            }

            int perfScore = PerformanceScore(perfExpectation, performance);
            int useCaseScore = (perfScore > 0)
                ? (int)(perfScore * frequency)
                : (int)(perfScore * frequency * 100 / c.flexibility);

            score += useCaseScore;

            log.AppendLine(
                $"{useCase}: {useCaseScore}\n"
            );
        }

        int timeBonus = TimeBonus(c);
        int budgetBonus = BudgetBonus(c, pc.totalPrice);

        score += timeBonus;
        score += budgetBonus;

        log.AppendLine($"Time Bonus: {timeBonus}");
        log.AppendLine($"Budget Bonus: {budgetBonus}");
        log.AppendLine($"Final Score: {score}");

        return log.ToString();
    }

    public static float GeneratePerformanceExpectation(int budget)
    {
        // Budget range: 600 (min) to 6000 (max)
        // Performance range: 0 (min) to 1000 (max)
        float minBudget = 600f;
        float maxBudget = 6000f;
        float minPerformance = 0f;
        float maxPerformance = 100f;

        // Use logarithmic scaling
        float safeBudget = Mathf.Max(budget, minBudget);
        float logMin = Mathf.Log(minBudget);
        float logMax = Mathf.Log(maxBudget);
        float logBudget = Mathf.Log(safeBudget);

        float t = Mathf.Clamp01((logBudget - logMin) / (logMax - logMin));
        return Mathf.Lerp(minPerformance, maxPerformance, t);
    }

    public static int BudgetBonus(Customer c, int totalPrice)
    {
        return (c.budget - totalPrice) * 1000;
    }

    public static int TimeBonus(Customer c)
    {
        return (int)((c.patience * 10 - RoundStats.buildTime) * 1000);
    }

    public static int PerformanceScore(float perfExpectation, float performance)
    {
        float performanceDifference = performance - perfExpectation;
        return (int)(performanceDifference * 1000);
        //assuming performance calculation is normalized
    }
}