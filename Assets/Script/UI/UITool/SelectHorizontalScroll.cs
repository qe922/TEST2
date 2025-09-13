using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectHorizontalScroll : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private struct ItemInfo
    {
        public string name;
        public Sprite sprite;
        public string description;
        public ItemInfo(string name, Sprite sprite, string description)
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
        }
    }
    public GameObject itemPrefab;
    public RectTransform itemParent;
    public Text descriptionText;
    [SerializeField]private ItemInfo[] itemInfos;
    private int displayNumber=9;
    private float itemSpace=500;
    private float moveSmooth=0.9f;
    private float dragSpeed=2;
    private float scaleMultiplying=0.001f;
    public float alphaMultiplying=0.001f;
    public event Action<int> SelectAction;
    public SelectHorizontalScrollItem[] items;
    private float displayWidth;
    private int offsetTimes;
    private bool isDrag;
    private int currentItemIndex;
    private float[] distances;
    private float selectItemX;
    private bool isSelectMove;
    private bool isSelected;
    private void Start()
    {
        Init();
        MoveItems(0);
    }
    private void Init()
    {
        displayWidth = (displayNumber - 1) * itemSpace;
        items = new SelectHorizontalScrollItem[displayNumber];
        for (int i = 0; i < displayNumber; i++)
        {
            SelectHorizontalScrollItem item = Instantiate(itemPrefab, itemParent).GetComponent<SelectHorizontalScrollItem>();
            item.itemIndex = i;
            items[i] = item;
        }

    }
    public void SetItemsInfo(string[] names, Sprite[] sprites, string[] descriptions)
    {
        if (names.Length != sprites.Length || sprites.Length != descriptions.Length || names.Length != descriptions.Length)
        {
            Debug.Log("选择数据不完整");
            return;
        }

        itemInfos = new ItemInfo[names.Length];
        for (int i = 0; i < itemInfos.Length; i++)
        {
            itemInfos[i] = new ItemInfo(names[i], sprites[i], descriptions[i]);
        }

        SelectAction = null;
        isSelected = false;

    }
    public void Select(int itemIndex, int infoIndex, RectTransform itemRectTransform)
    {
        if (!isSelected && itemIndex == currentItemIndex)
        {
            SelectAction?.Invoke(infoIndex);
            isSelected = true;
            Debug.Log("select " + (infoIndex + 1).ToString());
        }
        else
        {
            isSelectMove = true;
            selectItemX = itemRectTransform.localPosition.x;
        }


    }
    private void MoveItems(int offsetTimes)
    {
        for (int i = 0; i < displayNumber; i++)
        {
            float x = itemSpace * (i - offsetTimes) - displayWidth / 2;
            items[i].rectTransform.localPosition = new Vector2(x, items[i].rectTransform.localPosition.y);
        }

        int middle;
        if (offsetTimes > 0)
        {
            middle = itemInfos.Length - offsetTimes % itemInfos.Length;
        }
        else
        {
            middle = -offsetTimes % itemInfos.Length;
        }
        int infoIndex = middle;

        for (int i = Mathf.FloorToInt(displayNumber / 2f); i < displayNumber; i++)
        {
            if (infoIndex >= itemInfos.Length)
            {
                infoIndex = 0;
            }
            items[i].SetInfo(itemInfos[infoIndex].sprite, itemInfos[infoIndex].name, itemInfos[infoIndex].description, infoIndex, this);
            infoIndex++;
        }

        infoIndex = middle - 1;
        for (int i = Mathf.FloorToInt(displayNumber / 2f) - 1; i >= 0; i--)
        {
            if (infoIndex <= -1)
            {
                infoIndex = itemInfos.Length - 1;
            }
            items[i].SetInfo(itemInfos[infoIndex].sprite, itemInfos[infoIndex].name, itemInfos[infoIndex].description, infoIndex, this);
            infoIndex--;
        }


    }
    private void Update()
    {
        if (!isDrag)
        {
            Adsorption();
        }
        int currentOffsetTimes = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);
        if (currentOffsetTimes != offsetTimes)
        {
            offsetTimes = currentOffsetTimes;
            MoveItems(offsetTimes);
        }
        ItemsControl();

    }
    private void ItemsControl()
    {
        distances = new float[displayNumber];
        for (int i = 0; i < displayNumber; i++)
        {
            float distance = Mathf.Abs(items[i].rectTransform.position.x - transform.position.x);
            distances[i] = distance;
            float scale = 1 - distance * scaleMultiplying;
            items[i].rectTransform.localScale = new Vector3(scale, scale, 1);
            items[i].SetAlpha(1 - distance * alphaMultiplying);
        }

        float minDistance = itemSpace * displayNumber;
        int minIndex = 0;
        for (int i = 0; i < displayNumber; i++)
        {
            if (distances[i] < minDistance)
            {
                minDistance = distances[i];
                minIndex = i;
            }
        }
        descriptionText.text = items[minIndex].description;
        currentItemIndex = items[minIndex].itemIndex;

    }
    private void Adsorption()
    {
        float targetX;
        if (!isSelectMove)
        {
            float distance = itemParent.localPosition.x % itemSpace;
            int times = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);

            if (distance > 0)
            {
                if (distance < itemSpace / 2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
            else
            {
                if (distance < -itemSpace / 2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
        }
        else
        {
            targetX = -selectItemX;
        }

        itemParent.localPosition = new Vector2(
            Mathf.Lerp(itemParent.localPosition.x, targetX, moveSmooth / 10),
            itemParent.localPosition.y
        );


    }

    public void OnDrag(PointerEventData eventData)
    {
        isSelectMove = false;
        itemParent.localPosition = new Vector2(itemParent.localPosition.x + eventData.delta.x * dragSpeed, itemParent.localPosition.y);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
    }
}
