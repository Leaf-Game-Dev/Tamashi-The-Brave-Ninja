using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CircleWipeController circleWipe;

    private void Start()
    {
        circleWipe.FadeIn();
    }

    

    string SceneName;
    int sceneIndex = -1;

    public void LoadScene(string sceneName)
    {
        SceneName = sceneName;
        circleWipe.FadeOut();
        //SoundManager.PlaySound(SoundManager.Sound.UiSlide, 0.5f);

        Invoke(nameof(loadscen),1f);
    }

    public void LoadScene(int sceneName)
    {
        sceneIndex = sceneName;
        circleWipe.FadeOut();
        //SoundManager.PlaySound(SoundManager.Sound.UiSlide, 0.5f);

        Invoke(nameof(loadscen), 1f);
    }

    void loadscen()
    {

        if (sceneIndex == -1)
            SceneManager.LoadScene(SceneName);
        else
            SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
