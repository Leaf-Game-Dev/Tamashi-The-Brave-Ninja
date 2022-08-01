using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public SceneLoader sceneLoader;
    public void OnStoryClicked()
    {
        sceneLoader.LoadScene("StartCutscene");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

}
