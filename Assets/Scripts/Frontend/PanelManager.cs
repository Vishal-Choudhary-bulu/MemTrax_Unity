using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject firstActive;

    public List<GameObject> allOtherPanels = new List<GameObject>();

    private void OnEnable()
    {
        firstActive.SetActive(true);
        allOtherPanels.ForEach(DisableObj);
    }

    private void DisableObj(GameObject obj)
    {
        obj.SetActive(false);
    }
}
