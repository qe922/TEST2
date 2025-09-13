using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//场景状态
public abstract class SceneState
{
    public abstract void OnEnter();
    public abstract void OnExit();
    
}
