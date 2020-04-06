using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class AppEvents : MonoBehaviour
{
    public static AppEvents current;

    public static FirebaseUser appUser;

    private void Awake()
    {
        current = this;
    }

    //To handle login
    public delegate void LoginHandler();

    public static event LoginHandler OnUserLogin;

    public static void RaiseUserLogin()
    {
        OnUserLogin?.Invoke();
    }

    //to handle logout
    public delegate void LogoutHandler();

    public static event LogoutHandler OnUserLogout;

    public static void RaiseUserLogout()
    {
        OnUserLogout?.Invoke();
    }

    //to handle testStart
    public delegate void TestStartHandler();

    public static event LogoutHandler OnTestStart;

    public static void RaiseTestStart()
    {
        OnTestStart?.Invoke();
    }

    //to handle testStart
    public delegate void TestCompleteHandler();

    public static event TestCompleteHandler OnTestComplete;

    public static void RaiseTestComplete()
    {
        OnTestComplete?.Invoke();
    }

}
