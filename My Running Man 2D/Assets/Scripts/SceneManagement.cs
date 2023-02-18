using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    private Stack<string> sceneStack = new Stack<string>();

    public void PushScene(string sceneName)
    {
        sceneStack.Push(sceneName);
    }

    public void PopScene()
    {
        if (sceneStack.Count > 0)
        {
            string previousScene = sceneStack.Pop();
            UnityEngine.SceneManagement.SceneManager.LoadScene(previousScene);
        }
    }
}
