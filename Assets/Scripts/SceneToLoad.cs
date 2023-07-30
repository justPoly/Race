using System.Collections;
using UnityEngine;

public class SceneToLoad : MonoBehaviour
{
    public string sceneName;
    public int countDownTime;
    public SceneFader sceneFader;
    
    public void MoveToLoading()
    {
        GameStateManager.ApplicationManager.PlayGame();
        GameStateManager.ApplicationManager.OnSceneLoad.Raise();
        StartCoroutine(CountDown());
    }

    public void RestartScene()
    {
        GameStateManager.ApplicationManager.OnSceneLoad.Raise();
        StartCoroutine(CountDownToSame());
    }

    IEnumerator CountDown()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countDownTime--;
        }

        sceneFader.FadeTo(sceneName);
    }

    IEnumerator CountDownToSame()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countDownTime--;
        }

        sceneFader.FadeToSame();


    }

}
