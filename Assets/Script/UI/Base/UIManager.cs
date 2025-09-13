using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//存储UI信息
public class UIManager
{
    //每个UI信息对应一个GameObject
    private Dictionary<UIType, GameObject> dicUI;
    public UIManager()
    {
        dicUI = new Dictionary<UIType, GameObject>();
    }
    //获取UI对象
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("Canvas不存在");
            return null;
        }
        if (dicUI.ContainsKey(type))
        {
            return dicUI[type];
        }
        GameObject UI = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        UI.name = type.Name;
        Debug.Log(UI.name);
        dicUI.Add(type, UI);
        return UI;

    }
    public void DestroyUI(UIType type)
    {
        if (dicUI.ContainsKey(type))
        {
            
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}
