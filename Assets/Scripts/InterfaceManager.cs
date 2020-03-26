using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject authPanel;
    public GameObject interfacesPanel;
    public GameObject TestPanel;

    void Awake()
    {
        authPanel.SetActive(true);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(false);

        AppEvents.OnUserLogin += Login;
        AppEvents.OnUserLogout += Logout;
        AppEvents.OnTestStart += OpenTest;
    }

    private void Logout()
    {
        OpenAuth();
    }

    private void Login()
    {
        OpenInterface();
    }

    private void OpenInterface()
    {
        authPanel.SetActive(false);
        interfacesPanel.SetActive(true);
        TestPanel.SetActive(false);
    }

    public void OpenTest()
    {
        authPanel.SetActive(false);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(true);
    }

    public void OpenAuth()
    {
        authPanel.SetActive(true);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(false);
    }

}
