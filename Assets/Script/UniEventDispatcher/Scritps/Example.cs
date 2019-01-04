﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniEventDispatcher;

public class Example : MonoBehaviour 
{
    /// <summary>
    /// R数值的Slider
    /// </summary>
    private Slider sliderR;

    /// <summary>
    /// G数值的Slider
    /// </summary>
    private Slider sliderG;

    /// <summary>
    /// B数值的Slider
    /// </summary>
    private Slider sliderB;

    void Start () 
    {
        //在接收者中注册事件及其回调方法 
        NotificationCenter.Get().AddEventListener("ChangeColor", ChangeColor);

        //在发送者中分发事件，这里以UI逻辑为例
        sliderR = GameObject.Find("Canvas/SliderR").GetComponent<Slider>();
        sliderG = GameObject.Find("Canvas/SliderG").GetComponent<Slider>();
        sliderB = GameObject.Find("Canvas/SliderB").GetComponent<Slider>();
        //注册UI事件 unity自带的
        sliderR.onValueChanged.AddListener(OnValueChanged);
        sliderG.onValueChanged.AddListener(OnValueChanged);
        sliderB.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(float value)
    {
        //获得RGB数值
        float r = sliderR.value;
        float g = sliderG.value;
        float b = sliderB.value;
        //分发事件,注意和接收者协议一致
        //NotificationCenter.Get().DispatchEvent("ChangeColor", new Color(r, g, b));//传递参数
        NotificationCenter.Get().DispatchEvent("ChangeColor", this.gameObject,new Color(r, g, b));
    }

    /// <summary>
    /// 改变物体材质颜色
    /// </summary>
    /// <param name="notific"></param>
    public void ChangeColor(Notification notific)//接受方
    {
        Debug.Log(notific.ToString());
        //设置颜色

        gameObject.GetComponent<Renderer>().material.color = (Color)notific.param;
    }
}