using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private BarChart[] barCharts;
    [SerializeField] private Canvas HUD;

    void Update()
    {

        switch (RoundStats.highlightedComponent)
        {
            case "CPU":
                HUD.enabled = true;
                Title.text = RoundStats.highlightedCPU.name;
                
                barCharts[0].setBounds(0, 6000);
                barCharts[0].SetLabel("Single Core");
                barCharts[0].AnimateToValue(RoundStats.highlightedCPU.singleCore, 0.5f);

                barCharts[1].setBounds(0, 60000);
                barCharts[1].SetLabel("Multi Core");
                barCharts[1].AnimateToValue(RoundStats.highlightedCPU.multiCore, 0.5f);


                barCharts[2].setBounds(0, 200);
                barCharts[2].SetLabel("Power");
                barCharts[2].AnimateToValue(RoundStats.highlightedCPU.powerConsumption, 0.5f);

                barCharts[3].setBounds(0, 800);
                barCharts[3].SetLabel("Price");
                barCharts[3].AnimateToValue(RoundStats.highlightedCPU.price, 0.5f);


                break;
            default:
                for (int i = 0; i < barCharts.Length; i++)
                {
                    barCharts[i].Reset();
                }
                Title.text = "";
                HUD.enabled = false;
                break;
        }

    }
    

}
