using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static int aliveEnemyCount = 0;
    public static bool isSpawning = false;
    public Wave[] waves;
    public Transform startPoint;
    public Transform endPoint;

    //当前波数
    [HideInInspector]
    public int waveCount;

    void Start()
    {
        waveCount = 0;
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        Vector3 iniDir = (Node.nodePosition[1].position - Node.nodePosition[0].position).normalized;
        Quaternion quaDir = Quaternion.LookRotation(iniDir, Vector3.up);
        foreach(Wave wave in waves)
        {
            waveCount++;
            for(int i = 0; i<wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, startPoint.position, quaDir);
                aliveEnemyCount++;
                isSpawning = true;
                yield return new WaitForSeconds(wave.rate);
            }
            isSpawning = false;
            //当有敌人存在时，不刷新下一波
            while(aliveEnemyCount>0)
            {
                yield return 0;
            }
        }
    }
}
