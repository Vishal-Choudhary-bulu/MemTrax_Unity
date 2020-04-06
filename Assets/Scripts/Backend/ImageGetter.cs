using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageGetter : MonoBehaviour
{
    public string mainLink = "https://memtrax.com/wp-content/uploads/";
    public string category = "wonder";

    public static Dictionary<int, Sprite> textures = new Dictionary<int, Sprite>();

    public Image progressbar;

    public GameObject counter, loader;
    public Animator interfaceAnimator;


    void Start()
    {
        progressbar.fillAmount = 0;
    }

    public void GetNewImages()
    {
        var uri = mainLink + category;
        StartCoroutine(GetTexture(uri));
    }

    IEnumerator GetTexture(string uri)
    {
        for(int i = 1; i <= 25; i++)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri + i.ToString()+".jpg");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D newTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                var newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
                textures.Add(i, newSprite);

                TestManager.sprites.Add(i, newSprite);//first entry
                TestManager.sprites.Add(i+25, newSprite);//duplicate entry

                progressbar.fillAmount = i / 25f;
            }
        }

        counter.SetActive(true);
        loader.SetActive(false);
        interfaceAnimator.enabled = true;
    }
}
    
