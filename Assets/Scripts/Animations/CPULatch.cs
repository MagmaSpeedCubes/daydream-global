using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class CPULatch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image lever;
    [SerializeField] private Image latch;
    [SerializeField] private Socket socket;
    [SerializeField] private float rotateTime = 0.5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick on CPULatch");
        if (socket.currentIndex == 1 || socket.currentIndex == 0)
        {
            Close();
        }
        else if (socket.currentIndex == 2 || socket.currentIndex == -1)
        {
            Open();
        }
    }
    
    public void Open()
    {
        Debug.Log("Attempting to open CPU latch");
        if (socket.currentIndex == 2)
        {
            StartCoroutine(RotateObject(lever, 180f, rotateTime));
            StartCoroutine(RotateObject(latch, 180f, rotateTime));
            socket.currentIndex = 1;
        }
        else if (socket.currentIndex == -1)
        {
            StartCoroutine(RotateObject(lever, 180f, rotateTime));
            StartCoroutine(RotateObject(latch, 180f, rotateTime));
            socket.currentIndex = 0;
        }

    }

    public void Close()
    {
        Debug.Log("Attempting to close CPU latch");
        if (socket.currentIndex == 1)
        {
            StartCoroutine(RotateObject(lever, 180f, rotateTime));
            StartCoroutine(RotateObject(latch, 180f, rotateTime));
            socket.currentIndex = 2;
        }
        else if (socket.currentIndex == 0)
        {
            StartCoroutine(RotateObject(lever, 180f, rotateTime));
            StartCoroutine(RotateObject(latch, 180f, rotateTime));
            socket.currentIndex = -1;
        } 
    }

    IEnumerator RotateObject(Image obj, float targetAngle, float duration)
    {
        float startAngle = lever.rectTransform.eulerAngles.x;
        float angleDifference = Mathf.DeltaAngle(startAngle, targetAngle);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAngle = startAngle + (angleDifference * (elapsed / duration));
            obj.rectTransform.eulerAngles = new Vector3(newAngle, 0, 0);
            yield return null;
            //rotate for aniamtion
        }
        obj.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        obj.rectTransform.localScale = new Vector3(1, -obj.rectTransform.localScale.y, 1);
        yield return null;
        //reset rotation and flip for final effect
        
    }
}
