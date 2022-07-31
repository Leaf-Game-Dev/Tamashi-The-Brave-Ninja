using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    


    [Header("Chakra UI")]
    [SerializeField] Slider chakraSlider;
    [SerializeField] int maxChakra;
    [Header("Coin UI")]
    [SerializeField] TMPro.TMP_Text coinText;

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

}
