using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonobehaviour : MonoBehaviour { }


    private static Action onLoaderCallback;

    private static AsyncOperation operation;
    public static void Load(int sceneIndex)
    { 
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject();
            loadingGameObject.AddComponent<LoadingMonobehaviour>().StartCoroutine(LoadAsync(sceneIndex));
        };

        SceneManager.LoadScene("LoadingScene");
     
    }

    private static IEnumerator LoadAsync(int sceneIndex)
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;            
        }
    }

    public static float getProgess()
    {
        if (operation != null) 
        { 
            return Mathf.Clamp01(operation.progress / 0.9f);
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
   
}
