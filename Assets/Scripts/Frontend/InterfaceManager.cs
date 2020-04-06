using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject authPanel;
    public GameObject interfacesPanel;
    public GameObject TestPanel;
    public GameObject ResultsPanel;


    void Awake()
    {
        authPanel.SetActive(true);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(false);
        ResultsPanel.SetActive(false);

        AppEvents.OnUserLogin += Login;
        AppEvents.OnUserLogout += Logout;
        AppEvents.OnTestStart += OpenTest;
        AppEvents.OnTestComplete += OpenResults;
    }

    private void OnDisable()
    {
        AppEvents.OnUserLogin -= Login;
        AppEvents.OnUserLogout -= Logout;
        AppEvents.OnTestStart -= OpenTest;
        AppEvents.OnTestComplete -= OpenResults;
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
        ResultsPanel.SetActive(false);
    }

    public void OpenTest()
    {
        authPanel.SetActive(false);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(true);
        ResultsPanel.SetActive(false);
    }

    public void OpenAuth()
    {
        authPanel.SetActive(true);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(false);
        ResultsPanel.SetActive(false);
    }

    public void OpenResults()
    {
        authPanel.SetActive(false);
        interfacesPanel.SetActive(false);
        TestPanel.SetActive(false);
        ResultsPanel.SetActive(true);
    }

}
