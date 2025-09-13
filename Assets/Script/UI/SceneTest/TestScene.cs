using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    PanelManager panelManager;
    void Start()
    {
        panelManager = new PanelManager();
        //panelManager.Push(new TestPanel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
