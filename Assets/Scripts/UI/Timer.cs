using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class Timer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI timerText;
    [SerializeField] protected TextMeshProUGUI infoText;
    protected float time = 0;
    protected bool countingDown;
    [SerializeField] protected bool isRunning = false;
    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime * (countingDown ? -1 : 1);

            timerText.text = "" + time;
        }

    }

    public void SetCountingDown(bool icd)
    {
        countingDown = icd;
    }

    public float GetTime()
    {
        return time;
    }

    public void SetTime(float t)
    {
        time = t;
    }

    public void AddTime(float t)
    {
        time += t;
        DisplayInfo("+" + t + "s", 2);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        Debug.Log("Timer Started");
        isRunning = true;
    }

    public void DisplayInfo(string info, float duration)
    {
        StartCoroutine(DisplayInfoCoroutine(info, duration));
    }

    private IEnumerator DisplayInfoCoroutine(string info, float duration)
    {
        infoText.text = info;
        yield return new WaitForSeconds(duration);
        infoText.text = "";
        yield return null;
    }
}
