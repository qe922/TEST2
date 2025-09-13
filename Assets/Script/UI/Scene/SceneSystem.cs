using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//场景的状态管理系统
public class SceneSystem
{
    SceneState sceneState;
    public void SetScene(SceneState state)
    {
        if (sceneState != null)
        {
            sceneState.OnExit();
        }
        sceneState = state;
        if (sceneState != null)
        {
            sceneState.OnEnter();
        }
        
    }
}
