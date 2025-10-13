using UnityEngine;
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class ChallengeGameHandler : MonoBehaviour
{

    /*
    some ideas for modifiers:
    Sanctioned Goods: No access to the top tier components(5090, 9950x3d, 128gb ram, 8tb ssd)
    Tariffs: Products are 25% more expensive

    */
    [SerializeField] protected Timer timer;
    [SerializeField] protected ShopHUD shophud;
    [SerializeField] protected TextMeshProUGUI[] infoText;

    [SerializeField] protected AudioClip titleMusic, casualMusic, challengeMusic;
    [SerializeField] protected Canvas endCanvas;
    [SerializeField] protected TextMeshProUGUI endText;
    [SerializeField] protected Canvas benchmarkCanvas;
    protected AudioSource audioSource;

    public bool gameActive = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        InitializeRound();
    }

    void InitializeRound()
    {
        RoundStats.customer = CustomerGenerator.GenerateCustomerStatic();
        RoundStats.customerBudget = RoundStats.customer.budget;

        Debug.Log(RoundStats.customer.ToString());
        foreach (TextMeshProUGUI i in infoText)
        {
            i.text = RoundStats.customer.ToFormattedString();
        }

        shophud.InitializeHUD();

        audioSource.clip = casualMusic;
        audioSource.loop = true;
        audioSource.Play();
        audioSource.loop = true;


    }

    public void StartRound()
    {
        Debug.Log("Round Started");

        timer.SetTime(RoundStats.customer.patience * 10f);
        timer.SetCountingDown(true);
        timer.StartTimer();
        gameActive = true;

        audioSource.clip = challengeMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void FinishRound()
    {

        benchmarkCanvas.enabled = false;
        RoundStats.buildTime = RoundStats.customer.patience * 10f - timer.GetTime();
        endCanvas.enabled = true;
        endText.text = CustomerScoring.CalculateCustomerScore(RoundStats.customer);
    }

    void Update()
    {

    }
}
