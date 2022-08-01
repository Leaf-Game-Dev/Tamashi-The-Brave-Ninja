using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public SceneLoader loader;


    [Header("Chakra UI")]
    [SerializeField] Slider chakraSlider;
    [SerializeField] int maxChakra;
    [Header("Coin UI")]
    [SerializeField] TMPro.TMP_Text coinText;
    public float ShowTime;
    public GameObject GameOverObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        chakraSlider.maxValue = maxChakra;
        chakraSlider.value = maxChakra;

    }

    // Chakra

    public void AddChakras(int chakras)
    {
        chakraSlider.value += chakras;
        OnChakrasUpdated();
    }

    public bool ConsumeChakras(int chakras)
    {
        if (chakraSlider.value >= chakras)
        {
            chakraSlider.value -= chakras;
            OnChakrasUpdated();
            return true;
        }
        return false;
    }

    void OnChakrasUpdated()
    {

    }

    // coin

    public void addCoinCount()
    {
        coinText.text = int.Parse(coinText.text) + 1+"";
    }

    public void OnExit(){
    	loader.LoadScene("Menu");
SoundManager.PlaySound(SoundManager.Sound.Button, transform.position, 1f);

    }

    public void OnRestart(){
        loader.LoadScene(SceneManager.GetActiveScene().name);
SoundManager.PlaySound(SoundManager.Sound.Button, transform.position, 1f);
    }

    public void ShowGameOver(){
	Invoke(nameof(ActiveWindow),ShowTime);
    }

    public void ActiveWindow(){
	    GameOverObject.SetActive(true);
    }

}
