using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppSceneManager : MonoBehaviour
{
    public static AppSceneManager Scenes;

    void Awake()
    {
        if(Scenes != null && Scenes != this)
        {
            Destroy(this);
        }
        else
        {
            Scenes = this;
        }
        
        DontDestroyOnLoad(this);

        AppEvents.current.onUserLogin += LoadSceneOnLogin;
        AppEvents.current.onUserLogout += LoadSceneOnLogOut;
    }


    private void LoadSceneOnLogin()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadSceneOnLogOut()
    {
        SceneManager.LoadScene(0);
    }

}
