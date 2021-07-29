using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject nextLevelPanel;
    public GameObject gameIsFinishedPanel;
    public Text shootsText;
    public Text maxShootsText;

    void Update()
    {
        SetShoots();
        SetMaxShoots();
        ActivateEndGamePanel();
        ActivateNextLevelPanel();
    }

    void SetShoots()
    {
        shootsText.text = GameManager.instance.amountShoots.ToString();
    }

    void SetMaxShoots()
    {
        maxShootsText.text = GameManager.instance.maxAmountShoots.ToString();
    }

    void ActivateEndGamePanel()
    {
        if(GameManager.instance.endGame)
        {
            endGamePanel.SetActive(true);
        }
    }

    void ActivateNextLevelPanel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == GameManager.instance.finalLevel && GameManager.instance.levelComplete)
        {
            gameIsFinishedPanel.SetActive(true);
        }
        else if(GameManager.instance.levelComplete)
        {
            nextLevelPanel.SetActive(true);
        }
    }
}