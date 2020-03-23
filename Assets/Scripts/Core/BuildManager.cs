using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Towerdefense.Tower;

public class BuildManager : MonoBehaviour
{

    //当前选择需要建造的炮台
    public TowerLevel selectedTower;

    public int money = 10;

    private BuildArea buildArea;

    void Update()
    {
        GetBuildArea();
    }

    public void GetBuildArea()
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
                    buildArea = hit.collider.GetComponent<BuildArea>(); //得到点击的建筑方块
                }
            }
        }
    }

    public void BuildTower()
    {
        if (buildArea.currentTower == null)
        {
            //空则建造
            if (money >= selectedTower.cost)
            {
                money -= selectedTower.cost;
                buildArea.currentTower = GameObject.Instantiate(selectedTower.towerPrefab, buildArea.transform.position, Quaternion.identity);
                //buildArea.currentTower.GetComponent<TowerUIControl>().UICanvas = upgradeCanvas01;
            }
            else
            {
                //TODO 提示钱不够了
            }
        }
    }

    public void UpdateTower()
    {
        if(buildArea.currentTower!=null)
        {
            if(money >= selectedTower.cost)
            {
                money -= selectedTower.cost;
                GameObject.Destroy(buildArea.currentTower);
                buildArea.currentTower = GameObject.Instantiate(selectedTower.towerPrefab, buildArea.transform.position, Quaternion.identity);
                //buildArea.currentTower.GetComponent<TowerUIControl>().UICanvas = upgradeCanvas02;
            }
            else
            {
                //TODO 提示钱不够了
            }
        }
    }

    public void DestroyTower()
    {
        money += buildArea.currentTower.GetComponent<TowerLevel>().levelData.cost;
        GameObject.Destroy(buildArea.currentTower);
        buildArea.currentTower = null;
    }

    public void SelecteTower(TowerLevel towers)
    {
        selectedTower = towers;
    }
}
