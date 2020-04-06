using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestManager : MonoBehaviour
{
    public static Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>();

    public Image image;

    public Image testProgress;

    public TMP_Text imageNumberText;

    public float secondsBetweenImages = 1f;

    private bool IsTestStarted;

    private float currentTime = 0f;

    private int imageCount = 0;

    void Start()
    {
        IsTestStarted = false;
        AppEvents.OnTestStart += ShowImages;
    }

    private void OnDisable()
    {
        AppEvents.OnTestStart -= ShowImages;
    }

    private void ShowImages()
    {
        IsTestStarted = true;
        LoadImage();
    }

    public void LoadImage()
    {
        if (sprites.Count == 0)
        {
            AppEvents.RaiseTestComplete();
            return;
        }
        var i = UnityEngine.Random.Range(0, sprites.Count - 1);
        var a = new List<int>(sprites.Keys);
        i = a[i];
        image.sprite = sprites[i];
        sprites.Remove(i);
        currentTime = 0;
        imageCount++;
        testProgress.fillAmount = imageCount / 50f;
        imageNumberText.text = imageCount.ToString();
    }

    private void Update()
    {

        if (IsTestStarted)
        {
            if(currentTime >= secondsBetweenImages)
            {
                LoadImage();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

}
