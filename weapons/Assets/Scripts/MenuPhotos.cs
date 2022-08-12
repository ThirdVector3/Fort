using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPhotos : MonoBehaviour
{
    [SerializeField] private RawImage[] images;
    [SerializeField] private float delay;
    [SerializeField] private float delayIterations;
    int nowImage = 0;
    [SerializeField] Color color = Color.white;
    void Start()
    {
        if (images.Length > 1)
        {
            StartCoroutine(Switch());
        }
    }
    Color firstColor;
    Color secondColor;
    IEnumerator Switch()
    {
        yield return new WaitForSeconds(delay);
        firstColor = images[nowImage].color;
        if (nowImage + 1 == images.Length)
        {
            secondColor = images[0].color;
        }
        else
        {
            secondColor = images[nowImage + 1].color;
        }
        for (int i = 0; i < 100; i++)
        {
            firstColor.a -= 0.01f;
            secondColor.a += 0.01f;
            yield return new WaitForSeconds(delayIterations/100);
            images[nowImage].color = firstColor;
            if (nowImage + 1 == images.Length) {
                images[0].color = secondColor;
            }
            else {
                images[nowImage + 1].color = secondColor;
            }
        }
        if (nowImage + 1 == images.Length)
        {
            nowImage = 0;
        }
        else
        {
            nowImage++;
        }
        StartCoroutine(Switch());
    }
}
