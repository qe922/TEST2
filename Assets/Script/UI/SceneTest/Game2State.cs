using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState2 : SceneState
{
    //x -340 160
    //y -80 -200
    readonly string sceneName = "Game2";
    PanelManager panelManager;
    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            panelManager.Push(new StartPanel());
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        panelManager.popAll();


    }
    //场景加载完毕后使用的方法
    private void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        
        //panelManager.Push(new GamePanel());
        //GameManager.Instance.LoadGameObject();

    }
}
