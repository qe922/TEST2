using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI管理工具，包括获取某个子对象的操作
public  class UITool
{
    GameObject activePanel;
    public UITool(GameObject panel)
    {
        activePanel = panel;
    }
    //给当前的活动面板添加一个组件
    public T GetOrAddComponent<T>() where T : Component
    {
        if (activePanel.GetComponent<T>() == null)
        {
            activePanel.AddComponent<T>();
        }
        return activePanel.GetComponent<T>();
    }
    //根据名称查找一个子对象
    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = activePanel.GetComponentsInChildren<Transform>();
        foreach (var item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }

        }
        Debug.Log("找不到子组件");
        return null;
    }
    //根据名称获取一个子对象组件
    public T GetOrAddComponentChilren<T>(string name) where T : Component
    {
        GameObject child = FindChildGameObject(name);
        if (child)
        {
            if (child.GetComponent<T>() == null)
            {
                child.AddComponent<T>();
            }
            return child.GetComponent<T>();
        }
        return null;

    }
}
