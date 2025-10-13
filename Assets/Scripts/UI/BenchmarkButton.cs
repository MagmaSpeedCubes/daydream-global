using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]
public class BenchmarkButton : MonoBehaviour
{
    [SerializeField] protected string benchmark;
    [SerializeField] protected TextMeshProUGUI benchmarkTitle;
    [SerializeField] protected BarChart benchmarkBar;
    [SerializeField] protected AudioClip clickSound;
    protected AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClick()
    {
        audioSource.PlayOneShot(clickSound);
        int score = Benchmarker.RunBenchmark(benchmark);
        benchmarkTitle.text = benchmark + " Benchmark";

        benchmarkBar.AnimateToValue(score, 0.5f);
    }

}
