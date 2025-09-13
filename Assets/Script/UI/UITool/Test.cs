using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SelectHorizontalScroll horizontalScroll;
    public Sprite[] sprite;
    public string[] names;
    public string[] descriptions;

    private void Awake()
    {
        
        if (horizontalScroll != null)
        {
            int num = 2;
            names = new string[num];
            //sprite = new Sprite[num];
            descriptions = new string[num];
            names[0] = "晨曦森林";
            names[1] = "海岬尽头";
            descriptions[0] = "介绍1";
            descriptions[1] = "介绍2";
            horizontalScroll.SetItemsInfo(names, sprite, descriptions);

            horizontalScroll.SelectAction += (index) =>
            {
                EventCenter.Instance.Publish<int>("ToGame", index);
                print(index);
            };
        }
    }
}