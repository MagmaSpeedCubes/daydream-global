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

        if (RoundStats.selectedPSU != null)
        {
            psuText.text = "PSU: " + RoundStats.selectedPSU.name;
            totalPrice += RoundStats.selectedPSU.price;
        }
        else
        {
            psuText.text = "PSU: None";
        }
        //PSU

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




    }

    public void ToggleHUD()
    {
        shopHUDCanvas.enabled = !shopHUDCanvas.enabled;
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
    

}
