using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject scrollImage;
    public Text ProgessText;
    public Image SliderImage;

    private string m_SceneName;
    private float m_TargetProgress;
    private float m_CurrentProgress;


    public void LoadScene(string SceneName)
    {
        m_SceneName = SceneName;
        m_CurrentProgress = 0f;
        m_TargetProgress = 0f;
        //SceneManager.LoadScene(SceneName);
        //SceneManager.LoadSceneAsync(SceneName);
        EnemySpawnManager.aliveEnemyCount = 0;
        scrollImage.SetActive(true);
        StartCoroutine(LoadingScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //异步加载场景
    private IEnumerator LoadingScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_SceneName);
        asyncOperation.allowSceneActivation = false;
        while(asyncOperation.progress<0.9f)
        {
            m_TargetProgress += (asyncOperation.progress * 100);
            yield return LoadProgess();
        }
        m_TargetProgress = 100;
        yield return LoadProgess();
        scrollImage.SetActive(false);
        asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator LoadProgess()
    {
        while(m_CurrentProgress<m_TargetProgress)
        {
            m_CurrentProgress++;
            SliderImage.fillAmount = m_CurrentProgress / 100;
            ProgessText.text = ((int)m_CurrentProgress).ToString() + "%";
            yield return new WaitForEndOfFrame();
        }
    }
}
