using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EaseMovement : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private int frames;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;

    void Start()
    {
        MoveToFirstPos();
    }
    public void MoveToSecondPos()
    {
        Vector3 secondPos = target2.transform.position;
        LeanTween.move(gameObject, secondPos, time).setEase(LeanTweenType.easeInOutSine);
    }

    public void MoveToFirstPos()
    {
        Vector3 firstPos = target1.transform.position;
        LeanTween.move(gameObject, firstPos, time).setEase(LeanTweenType.easeInOutSine);
    }
}
