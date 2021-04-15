using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanLoader : MonoBehaviour
{

    public void LoadScene (string scene)
    {
        StartCoroutine(LoadYourAsyncScene(scene));
    }
    IEnumerator LoadYourAsyncScene(string scene)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
