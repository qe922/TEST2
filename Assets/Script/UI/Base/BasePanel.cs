using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG;
using DG.Tweening;
using UnityEngine.UI;

public abstract class BasePanel
{
    public string name;
    //UI信息
    public UIType UIType { get; private set; }
    //UI管理工具
    public UITool UITool { get; private set; }
    //面板管理器
    public PanelManager PanelManager { get; private set; }
    //UI管理器
    public UIManager UIManager { get; private set; }
  
    public BasePanel(UIType uIType)
    {
        UIType = uIType;
    }
    public BasePanel()
    {

    }
    //初始化UITool
    public void Initialize(UITool tool)
    {
        UITool = tool;
    }
    //初始化面板管理器
    public void Initialize(PanelManager panelManager)
    {
        PanelManager = panelManager;
    }
    public void Initialize(UIManager uIManager)
    {
        UIManager = uIManager;
    }
    //UI进入时执行的操作，只执行一次
    public virtual void OnEnter()
    {

    }
    //UI暂停时执行的操作
    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    //UI继续时执行的操作
    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;

    }
    public virtual void OnComplete()
    {
        UIManager.DestroyUI(UIType);
    }
    public CanvasGroup GetCanvasGroup()
    {
        CanvasGroup cg = GameObject.Find(name).GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = GameObject.Find(name).AddComponent<CanvasGroup>();
        }
        return cg;
    }
    
    public virtual void OnExit()
    {
        Debug.Log(name+"被删");
        CanvasGroup cg = GetCanvasGroup();
        cg.DOFade(0, 0.5f).OnComplete(() => OnComplete());
    }
}
