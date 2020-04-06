using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTest : MonoBehaviour
{
    
    public void EnterTest()
    {
        AppEvents.RaiseTestStart();
    }
}
