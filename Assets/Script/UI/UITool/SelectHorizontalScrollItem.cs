using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectHorizontalScrollItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image image;
    public Text nametext;
    public CanvasGroup canvasGroup;
    public int itemIndex;
    public int infoIndex;
    public string description;
    public SelectHorizontalScroll selectHorizontalScroll;
    public RectTransform rectTransform;
    private bool isDrag;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetInfo(Sprite sprite, string name, string description, int infoIndex, SelectHorizontalScroll selectHorizontalScroll)
    {
        image.sprite = sprite;
        nametext.text = name;
        this.description = description;
        this.infoIndex = infoIndex;
        this.selectHorizontalScroll = selectHorizontalScroll;
    }
    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        selectHorizontalScroll.OnDrag(eventData);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = false;
        selectHorizontalScroll.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDrag)
        {
            selectHorizontalScroll.Select(itemIndex, infoIndex, rectTransform);
        }
        selectHorizontalScroll.OnPointerUp(eventData);
    }
}
