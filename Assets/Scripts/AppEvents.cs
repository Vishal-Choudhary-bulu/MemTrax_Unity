using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents : MonoBehaviour
{
    public static AppEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onUserLogin;
    public event Action onUserLogout;

    public void UserLogin()
    {
        onUserLogin?.Invoke();
    }
    
    public void UserLogout()
    {
        onUserLogout?.Invoke();
    }
    
}
