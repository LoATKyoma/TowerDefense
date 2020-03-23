using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildArea : MonoBehaviour
{
    //[HideInInspector]
    public GameObject currentTower = null; //保存当前炮塔

    //渲染
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    //鼠标移动到Cube实现Cube变色
    void OnMouseEnter()
    {
        if(currentTower == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            //获取参数并修改RGBA的值
            float r = renderer.material.color.r * 0.5f;
            float g = renderer.material.color.g * 0.5f;
            float b = renderer.material.color.b * 0.5f;
            float a = renderer.material.color.r * 0.5f;

            //修改材质
            renderer.material.color = new Color(r, g, b, a);
        }
    }

    void OnMouseExit()
    {
        //获取参数并修改Alpha的值
        float r = renderer.material.color.r / 0.5f;
        float g = renderer.material.color.g / 0.5f;
        float b = renderer.material.color.b / 0.5f;
        float a = renderer.material.color.r / 0.5f;

        //修改材质
        renderer.material.color = new Color(r, g, b, a);
    }
}
