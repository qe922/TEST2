using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
//面板管理器，用栈管理UI
public class PanelManager
{
    private Stack<BasePanel> stackPanel;
    private UIManager uIManager;
    private BasePanel panel;
    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
        uIManager = new UIManager();
    }
    //入栈,该操作会显示一个面板
    public void Push(BasePanel nextPanel)
    {
        if (stackPanel.Count > 0)
        {
            panel = stackPanel.Peek();
            panel.OnPause();
        }
        stackPanel.Push(nextPanel);
        GameObject panelGo = uIManager.GetSingleUI(nextPanel.UIType);
        //Debug.Log("1");
        nextPanel.Initialize(new UITool(panelGo));
        nextPanel.Initialize(this);
        nextPanel.Initialize(uIManager);

        nextPanel.OnEnter();
    }
    //出栈,此操作会执行面板的Exit操作
    public void pop()
    {
        if (stackPanel.Count > 0)
        {
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
        }
        if (stackPanel.Count > 0)
        {
            stackPanel.Peek().OnResume();
        }
    }
    public void popAll()
    {
        while (stackPanel.Count > 0)
        {
            stackPanel.Pop().OnExit();
        }
    }
}
