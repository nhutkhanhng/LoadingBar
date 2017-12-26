using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ProgressLoading : MonoBehaviour
{
    private AsyncOperation asyncOperator;

    public Slider slider;

    public void LoadingScenes(int indexScene)
    {
        Debug.Log("Loading Scene");
        StartCoroutine(AsyncLoadingScenes(indexScene));
    }


    IEnumerator AsyncLoadingScenes(int indexScene)
    {
        asyncOperator = SceneManager.LoadSceneAsync(indexScene);
        asyncOperator.allowSceneActivation = false;
        // Coroutines are functions that run separately from the main game loop and must yield and return in order to wait. 
        // However, doing so will not exit the function. You need to add a "yield return" and an IEnumerator value to your function.
        // Answer by Daggett110 ----- https://answers.unity.com/users/665285/daggett110.html
        /* 
                float progress = Mathf.Clamp01(asyncOperator.progress / .9f);
        */
        while (!asyncOperator.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperator.progress / .9f);
            Debug.Log("<color=yellow>" + asyncOperator.progress + "</color>");

            Debug.Log("<color=blue>" + progress + "</color>");
            slider.value = progress;

            if (Mathf.Approximately(asyncOperator.progress, .9f))
            {
                yield return new WaitForSeconds(5);
                asyncOperator.allowSceneActivation = true;
            }


            yield return null;
        }
    }
}
