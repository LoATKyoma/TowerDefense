using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameControlUICanvas;
    public int playerHealth;

    void Start()
    {
        //开局应该暂停
        GameObject picture = GameControlUICanvas.transform.Find("State").gameObject;
        picture.transform.Find("WavesMessage").gameObject.SetActive(false);
        picture.transform.Find("StopMessage").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        GetMoney();
        WaveMessage();
        ShowHealth();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stop();
        }
        if(playerHealth == 0)
        {
            Failed();
        }
    }

    public void GetMoney()
    {
        int money = gameObject.GetComponent<BuildManager>().money;
        GameObject picture = GameControlUICanvas.transform.Find("ShowMoney").gameObject;
        picture.transform.Find("Money").gameObject.GetComponent<Text>().text = "金钱:" + money.ToString();
    }

    public void ShowHealth()
    {
        GameControlUICanvas.transform.Find("Health").Find("HealthMessage").gameObject.GetComponent<Text>().text = "玩家血量:" + playerHealth;
    }

    public void Stop()
    {
        if (Time.timeScale == 0)
        {
            GameObject picture = GameControlUICanvas.transform.Find("State").gameObject;
            picture.transform.Find("WavesMessage").gameObject.SetActive(true);
            picture.transform.Find("StopMessage").gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            GameObject picture = GameControlUICanvas.transform.Find("State").gameObject;
            picture.transform.Find("WavesMessage").gameObject.SetActive(false);
            picture.transform.Find("StopMessage").gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void WaveMessage()
    {
        int WaveSize = gameObject.GetComponent<EnemySpawnManager>().waves.Length;
        int WaveCount = gameObject.GetComponent<EnemySpawnManager>().waveCount;
        GameObject picture = GameControlUICanvas.transform.Find("State").gameObject;
        picture.transform.Find("WavesMessage").GetComponent<Text>().text = "WAVE(" + WaveCount + "/" + WaveSize + ")";
        if (WaveSize == WaveCount && EnemySpawnManager.aliveEnemyCount == 0 && !EnemySpawnManager.isSpawning && playerHealth>0)
            Win();
    }

    public void OnClickButton()
    {
        GameObject picture = GameControlUICanvas.transform.Find("State").gameObject;
        picture.transform.Find("WavesMessage").gameObject.SetActive(false);
        picture.transform.Find("StopMessage").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame(bool isQuitToMenu)
    {
        if(isQuitToMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Application.Quit();
        }
    }

    public void Win()
    {
        GameObject.Find("GameOver").transform.Find("WinCanvas").gameObject.SetActive(true);
    }

    public void Failed()
    {
        GameObject.Find("GameOver").transform.Find("FailedCanvas").gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
