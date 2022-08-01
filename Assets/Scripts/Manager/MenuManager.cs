using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public SceneLoader sceneLoader;
    public void OnStoryClicked()
    {
        sceneLoader.LoadScene("StartCutscene");
SoundManager.PlaySound(SoundManager.Sound.Button, transform.position, 1f);
    }

    public void OnExitClicked()
    {
SoundManager.PlaySound(SoundManager.Sound.Button, transform.position, 1f);
        Application.Quit();
    }

}
