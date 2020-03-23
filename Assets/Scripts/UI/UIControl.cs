using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Towerdefense.Tower;
using Towerdefense.Tower.Data;

public class UIControl : MonoBehaviour
{
    //空地层UI
    public GameObject BuildUICanvas;
    //最高级时候的摧毁UI
    public GameObject DestoryUICanvas;
    //箭塔升级UI
    public GameObject[] ArrowUpgradeUICanvas;
    //魔法塔升级UI
    public GameObject[] MagicUpgradeUICanvas;
    //炮塔升级UI
    public GameObject[] CannonUpgradeUICanvas;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("BuildArea"));
                if (isCollider)
                {
                    HideBuildUI();
                    for (int n = 1; n <= 3; n++)
                    {
                        HideMagicUpgradeUI(n);
                        HideArrowUpgradeUI(n);
                        HideCannonUpgradeUI(n);
                    }
                    GameObject currentTower = hit.collider.gameObject.GetComponent<BuildArea>().currentTower;
                    if (currentTower == null)
                    {
                        ShowBuildUI(hit.collider.gameObject.transform.position);
                    }
                    else
                    {
                        switch (currentTower.GetComponent<TowerLevel>().towerType)
                        {
                            case TowerLevelData.TowerTypes.Arrow:
                                {
                                    ShowArrowUpgradeUI(hit.collider.gameObject.transform.position, currentTower.GetComponent<TowerLevel>().level);
                                    break;
                                }
                            case TowerLevelData.TowerTypes.Magic:
                                {
                                    ShowMagicUpgradeUI(hit.collider.gameObject.transform.position, currentTower.GetComponent<TowerLevel>().level);
                                    break;
                                }
                            case TowerLevelData.TowerTypes.Cannon:
                                {
                                    ShowCannonUpgradeUI(hit.collider.gameObject.transform.position, currentTower.GetComponent<TowerLevel>().level);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    HideBuildUI();
                    for(int n = 1;n<=3;n++)
                    {
                        HideMagicUpgradeUI(n);
                        HideArrowUpgradeUI(n);
                        HideCannonUpgradeUI(n);
                    }
                }
            }
        }
    }

    //显示建筑平台的ui
    public void ShowBuildUI(Vector3 pos)
    {
        BuildUICanvas.SetActive(true);
        BuildUICanvas.transform.position = pos;
    }

    //显示箭塔的ui
    public void ShowArrowUpgradeUI(Vector3 pos, int level)
    {
        if(level<3)
        {
            ArrowUpgradeUICanvas[level - 1].SetActive(true);
            ArrowUpgradeUICanvas[level - 1].transform.position = pos;
        }
        else
        {
            DestoryUICanvas.SetActive(true);
            DestoryUICanvas.transform.position = pos;
        }
    }

    //显示魔法塔的ui
    public void ShowMagicUpgradeUI(Vector3 pos, int level)
    {
        if (level < 3)
        {
            MagicUpgradeUICanvas[level - 1].SetActive(true);
            MagicUpgradeUICanvas[level - 1].transform.position = pos;
        }
        else
        {
            DestoryUICanvas.SetActive(true);
            DestoryUICanvas.transform.position = pos;
        }
    }

    //显示炮塔的ui
    public void ShowCannonUpgradeUI(Vector3 pos, int level)
    {
        if (level < 3)
        {
            CannonUpgradeUICanvas[level - 1].SetActive(true);
            CannonUpgradeUICanvas[level - 1].transform.position = pos;
        }
        else
        {
            DestoryUICanvas.SetActive(true);
            DestoryUICanvas.transform.position = pos;
        }
    }

    //隐藏建筑平台的ui
    public void HideBuildUI()
    {
        BuildUICanvas.SetActive(false);
    }

    //隐藏箭塔的ui
    public void HideArrowUpgradeUI(int level)
    {
        if (level < 3)
        {
            ArrowUpgradeUICanvas[level - 1].SetActive(false);
        }
        else
        {
            DestoryUICanvas.SetActive(false);
        }
    }

    //隐藏魔法塔的ui
    public void HideMagicUpgradeUI(int level)
    {
        if (level < 3)
        {
            MagicUpgradeUICanvas[level - 1].SetActive(false);
        }
        else
        {
            DestoryUICanvas.SetActive(false);
        }
    }

    //隐藏炮塔的ui
    public void HideCannonUpgradeUI(int level)
    {
        if (level < 3)
        {
            CannonUpgradeUICanvas[level - 1].SetActive(false);
        }
        else
        {
            DestoryUICanvas.SetActive(false);
        }
    }


}
