using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//管理全局的一些东西
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    public SceneSystem sceneSystem { get; private set; }
    private void Awake()
    {
        //Debug.Log("start");
        /*
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        */
        sceneSystem = new SceneSystem();
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        //Debug.Log("start");
        sceneSystem.SetScene(new StartScene());
        if (Instance != null&&Instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
