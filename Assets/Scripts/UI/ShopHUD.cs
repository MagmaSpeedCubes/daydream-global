using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class ShopHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private BarChart[] barCharts;
    [SerializeField] private Canvas InfoBar;
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip selectSound;

    [SerializeField] private Canvas shopHUDCanvas;
    [SerializeField] private Canvas useCaseHUDCanvas;
    [SerializeField] private TextMeshProUGUI cpuText;
    [SerializeField] private TextMeshProUGUI gpuText;
    [SerializeField] private TextMeshProUGUI ramText;
    [SerializeField] private TextMeshProUGUI storageText;
    [SerializeField] private TextMeshProUGUI caseText;
    [SerializeField] private TextMeshProUGUI psuText;
    [SerializeField] private TextMeshProUGUI coolerText;
    [SerializeField] private BarChart budget;
    [SerializeField] private BarChart power;

    [SerializeField] private BuildHUD buildHUD; // reference to the BuildHUD script to display error messages
    public string selectedUseCase;

    [SerializeField] protected BarChart[] useCaseBars; // Order: CST, CMT, GPU, SRAM, VRAM
    [SerializeField] protected TextMeshProUGUI useCaseTitle;




    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InfoBar.enabled = false;
        InitializeHUD();
    }

    void Update()
    {
        UpdateInfoBar();

        UpdateHUD();
    }

    public void InitializeHUD()
    {
        budget.setBounds(0, RoundStats.customerBudget);
        budget.SetLabel("Budget");
        budget.SetValue(RoundStats.customerBudget);

        power.setBounds(0, 2000);
        power.SetLabel("Power");
        power.SetValue(0);
    }

    public void UpdateHUD()
    {
        if (RoundStats.partsSelected == true)
        {
            cpuText.color = Color.green;
            gpuText.color = Color.green;
            ramText.color = Color.green;
            storageText.color = Color.green;
            coolerText.color = Color.green;
            psuText.color = Color.green;

            return;
        }
        int totalPrice = 0;
        int totalPower = 0;
        if (RoundStats.selectedCPU != null)
        {
            cpuText.text = "CPU: " + RoundStats.selectedCPU.name;
            totalPrice += RoundStats.selectedCPU.price;
            totalPower += RoundStats.selectedCPU.powerConsumption;
        }
        else
        {
            cpuText.text = "CPU: None";
        }
        //CPU

        if (RoundStats.selectedGPU != null)
        {
            gpuText.text = "GPU: " + RoundStats.selectedGPU.name;
            totalPrice += RoundStats.selectedGPU.price;
            totalPower += RoundStats.selectedGPU.powerConsumption;
        }
        else
        {
            gpuText.text = "GPU: None";
        }
        //GPU

        if (RoundStats.selectedRAM != null)
        {
            ramText.text = "RAM: " + RoundStats.selectedRAM.name;
            totalPrice += RoundStats.selectedRAM.price;
            totalPower += RoundStats.selectedRAM.channels * 3;
        }
        else
        {
            ramText.text = "RAM: None";
        }
        //RAM

        if (RoundStats.selectedCooler != null)
        {
            coolerText.text = "Cooler: " + RoundStats.selectedCooler.name;
            totalPrice += RoundStats.selectedCooler.price;
            totalPower += 5;
        }
        else
        {
            coolerText.text = "Cooler: None";
        }
        //Cooler

        if (RoundStats.selectedStorage != null && RoundStats.selectedStorage.Length > 0)
        {
            storageText.text = "Storage: ";

            storageText.text += "" + RoundStats.selectedStorage[0].name;
            storageText.text += RoundStats.selectedStorage.Length > 1 ? " and " + (RoundStats.selectedStorage.Length - 1) + " more" : "";
            foreach (Storage s in RoundStats.selectedStorage)
            {
                totalPrice += s.price;
            }
            totalPower += RoundStats.selectedStorage.Length * 5;


        }
        else
        {
            storageText.text = "Storage: None";
        }

        //Storage

        //PSU

        int[] powerSupplyWattage = new int[] { 650, 750, 850, 1000, 1250, 1500, 2500 };
        int[] powerSupplyCost = new int[] { 69, 99, 119, 159, 189, 349, 799 };

        int selectedPSUIndex = -1;
        for (int i = 0; i < powerSupplyWattage.Length; i++)
        {
            if (powerSupplyWattage[i] >= totalPower)
            {
                selectedPSUIndex = i;
                break;
            }
        }
        if (selectedPSUIndex != -1)
        {
            // Create a new PSU object or fetch from a list/database if available
            PSU autoPSU = new PSU
            {
                name = powerSupplyWattage[selectedPSUIndex] + "W PSU",
                power = powerSupplyWattage[selectedPSUIndex],
                price = powerSupplyCost[selectedPSUIndex]
            };
            RoundStats.selectedPSU = autoPSU;
            totalPrice += autoPSU.price;
            psuText.text = "PSU: " + autoPSU.name;
        }

        if (budget.GetValue() != RoundStats.customerBudget - totalPrice)
        {
            budget.AnimateToValue(RoundStats.customerBudget - totalPrice, 0.5f);
            if (RoundStats.customerBudget - totalPrice < 0)
            {
                buildHUD.ShowPopup("Warning", "Over Budget", "Your current build exceeds the customer's budget", 3);

            }
        }
        if (power.GetValue() != totalPower)
        {
            power.AnimateToValue(totalPower, 0.5f);
        }
        RoundStats.builtPC.totalPrice = totalPrice;
        Debug.Log(RoundStats.builtPC.totalPrice);


        





    }

    public void ToggleHUD()
    {
        shopHUDCanvas.enabled = !shopHUDCanvas.enabled;
    }

    public void ToggleUseCaseHUD()
    {
        useCaseHUDCanvas.enabled = !useCaseHUDCanvas.enabled;
    }

    public void UpdateUseCaseHUD()
    {
        // Order: CST, CMT, GPU, SRAM, VRAM

        float[] webBrowsingBars = { 100f, 0f, 0f, 30f, 0f };
        float[] softwareCompilingBars = { 100f, 100f, 0f, 80f, 0f };
        float[] modelingRenderingBars = { 30f, 100f, 100f, 90f, 100f };
        float[] photoEditingBars = { 100f, 60f, 100f, 80f, 70f };
        float[] videoEditingBars = { 100f, 100f, 100f, 70f, 80f };
        float[] aimlBars = { 100f, 100f, 100f, 80f, 100f };
        float[] gamingBars = { 60f, 60f, 100f, 80f, 80f };
        float[] musicProductionBars = { 100f, 100f, 0f, 70f, 0f };
        float[] physicsSimBars = { 30f, 100f, 100f, 80f, 80f };

        switch (selectedUseCase)
        {
            case "Web Browsing":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(webBrowsingBars[i], 0.5f);
                }
                break;
            case "Software Compiling":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(softwareCompilingBars[i], 0.5f);
                }
                break;
            case "3D Modeling & Rendering":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(modelingRenderingBars[i], 0.5f);
                }
                break;
            case "Photo Editing":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(photoEditingBars[i], 0.5f);
                }
                break;
            case "Video Editing":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(videoEditingBars[i], 0.5f);
                }
                break;
            case "AI/ML":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(aimlBars[i], 0.5f);
                }
                break;
            case "Gaming":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(gamingBars[i], 0.5f);
                }
                break;
            case "Music Production":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(musicProductionBars[i], 0.5f);
                }
                break;
            case "Physics Simulation":
                for (int i = 0; i < useCaseBars.Length; i++)
                {
                    useCaseBars[i].AnimateToValue(physicsSimBars[i], 0.5f);
                }
                break;


        }
        useCaseTitle.text = selectedUseCase;
    }

    public void UpdateInfoBar()
    {
        switch (RoundStats.highlightedComponent)
        {
            case "CPU":
                InfoBar.enabled = true;
                Title.text = RoundStats.highlightedCPU.name;

                barCharts[0].setBounds(0, 6000);
                barCharts[0].SetLabel("Single Core");
                barCharts[0].AnimateToValue(RoundStats.highlightedCPU.singleCore, 0.5f);

                barCharts[1].setBounds(0, 75000);
                barCharts[1].SetLabel("Multi Core");
                barCharts[1].AnimateToValue(RoundStats.highlightedCPU.multiCore, 0.5f);


                barCharts[2].setBounds(0, 250);
                barCharts[2].SetLabel("Power");
                barCharts[2].AnimateToValue(RoundStats.highlightedCPU.powerConsumption, 0.5f);

                barCharts[3].setBounds(0, 800);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedCPU.price, 0.5f);


                break;
            case "GPU":
                InfoBar.enabled = true;
                Title.text = RoundStats.highlightedGPU.name;


                barCharts[0].setBounds(0, 40000);
                barCharts[0].SetLabel("Compute Score");
                barCharts[0].AnimateToValue(RoundStats.highlightedGPU.performance, 0.5f);

                barCharts[1].setBounds(0, 32);
                barCharts[1].SetLabel("VRAM (GB)");
                barCharts[1].AnimateToValue(RoundStats.highlightedGPU.VRAM, 0.5f);

                barCharts[2].setBounds(0, 600);
                barCharts[2].SetLabel("Power");
                barCharts[2].AnimateToValue(RoundStats.highlightedGPU.powerConsumption, 0.5f);

                barCharts[3].setBounds(0, 2500);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedGPU.price, 0.5f);
                break;
            case "Storage":
                InfoBar.enabled = true;
                Title.text = RoundStats.highlightedStorage.name;

                barCharts[0].setBounds(0, 24000);
                barCharts[0].SetLabel("Capacity (GB)");
                barCharts[0].AnimateToValue(RoundStats.highlightedStorage.capacity, 0.5f);

                barCharts[1].setBounds(0, 7000);
                barCharts[1].SetLabel("Read Speed (MB/s)");
                barCharts[1].AnimateToValue(RoundStats.highlightedStorage.readSpeed, 0.5f);

                barCharts[2].setBounds(0, 7000);
                barCharts[2].SetLabel("Write Speed (MB/s)");
                barCharts[2].AnimateToValue(RoundStats.highlightedStorage.writeSpeed, 0.5f);

                barCharts[3].setBounds(0, 700);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedStorage.price, 0.5f);
                break;
            case "RAM":
                InfoBar.enabled = true;
                Title.text = RoundStats.highlightedRAM.name;

                barCharts[0].setBounds(0, 128);
                barCharts[0].SetLabel("Size (GB)");
                barCharts[0].AnimateToValue(RoundStats.highlightedRAM.size, 0.5f);

                barCharts[1].setBounds(0, 4);
                barCharts[1].SetLabel("Channels");
                barCharts[1].AnimateToValue(RoundStats.highlightedRAM.channels, 0.5f);

                barCharts[2].setBounds(0, 6000);
                barCharts[2].SetLabel("Clock Speed (MHz)");
                barCharts[2].AnimateToValue(RoundStats.highlightedRAM.clockSpeed, 0.5f);

                barCharts[3].setBounds(0, 500);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedRAM.price, 0.5f);
                break;
            case "Cooler":
                InfoBar.enabled = true;

                Title.text = RoundStats.highlightedCooler.name;

                barCharts[0].setBounds(0, 0);
                barCharts[0].SetLabel("");
                barCharts[0].AnimateToValue(0, 0.5f);

                barCharts[1].setBounds(0, 0);
                barCharts[1].SetLabel("");
                barCharts[1].AnimateToValue(0, 0.5f);

                barCharts[2].setBounds(0, 300);
                barCharts[2].SetLabel("Cooling Power");
                barCharts[2].AnimateToValue(RoundStats.highlightedCooler.coolingPower, 0.5f);

                barCharts[3].setBounds(0, 300);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedCooler.price, 0.5f);
                break;
            default:
                for (int i = 0; i < barCharts.Length; i++)
                {
                    barCharts[i].Reset();
                }
                Title.text = "";
                InfoBar.enabled = false;
                break;
        }
    }

    public void Select()
    {
        switch (RoundStats.highlightedComponent)
        {
            case "CPU":
                RoundStats.selectedCPU = RoundStats.highlightedCPU;
                break;
            case "GPU":
                RoundStats.selectedGPU = RoundStats.highlightedGPU;
                break;
            case "Cooler":
                RoundStats.selectedCooler = RoundStats.highlightedCooler;
                break;
            case "RAM":
                RoundStats.selectedRAM = RoundStats.highlightedRAM;
                break;
            case "Storage":
                //add to array
                if (RoundStats.selectedStorage == null)
                {
                    RoundStats.selectedStorage = new Storage[] { RoundStats.highlightedStorage };
                }
                else
                {
                    System.Array.Resize(ref RoundStats.selectedStorage, RoundStats.selectedStorage.Length + 1);
                    RoundStats.selectedStorage[RoundStats.selectedStorage.Length - 1] = RoundStats.highlightedStorage;
                }
                break;
            case "PSU":
                RoundStats.selectedPSU = RoundStats.highlightedPSU;
                break;

            default:
                break;
        }
        audioSource.PlayOneShot(selectSound);
    }

    public void Remove()
    {
        switch(RoundStats.highlightedComponent)
        {
            case "CPU":
                RoundStats.selectedCPU = null;
                break;
            case "GPU":
                RoundStats.selectedGPU = null;
                break;
            case "Cooler":
                RoundStats.selectedCooler = null;
                break;
            case "RAM":
                RoundStats.selectedRAM = null;
                break;
            case "Storage":
                if (RoundStats.selectedStorage != null && RoundStats.selectedStorage.Length > 0)
                {
                    int index = System.Array.IndexOf(RoundStats.selectedStorage, RoundStats.highlightedStorage);
                    if (index >= 0)
                    {
                        for (int i = index; i < RoundStats.selectedStorage.Length - 1; i++)
                        {
                            RoundStats.selectedStorage[i] = RoundStats.selectedStorage[i + 1];
                        }
                        System.Array.Resize(ref RoundStats.selectedStorage, RoundStats.selectedStorage.Length - 1);
                    }
                }
                break;
            case "PSU":
                RoundStats.selectedPSU = null;
                break;

            default:
                break;
        }

    }
    

}
