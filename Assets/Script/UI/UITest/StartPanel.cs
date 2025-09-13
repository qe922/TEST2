using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Xml.Serialization;


public class StartPanel : BasePanel
{
    static readonly string path = "Prefabs/UI/Panel/StartPanel";
    public StartPanel() : base(new UIType(path)) { }
    public override void OnEnter()
    {

        name = "StartPanel";
        CanvasGroup cg = GetCanvasGroup();
        cg.alpha = 0;
        cg.DOFade(1, 1f);

        UITool.GetOrAddComponentChilren<Button>("startgame").onClick.AddListener(() => OnExit());
        //UITool.GetOrAddComponentChilren<Button>("startgame").onClick.AddListener(() => MusicManager.Instance.UI[0].Play());

        //UITool.
        //PanelManager.Push(new SettingPanel());
    }
    public override void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public override void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public override void OnExit()
    {
        base.OnExit();
        //GameRoot.Instance.sceneSystem.SetScene(new MainScene());
    }
    public override void OnComplete()
    {
        DOTween.KillAll();
        //base.OnComplete();
        GameRoot.Instance.sceneSystem.SetScene(new MainScene());
        //MusicManager.Instance.Game[0].Play();
    }

}
