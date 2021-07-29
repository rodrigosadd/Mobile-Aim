using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> objsToBeDestroyed;
    public static GameManager instance;
    public PoolSystem poolSystem;
    public UIController uiController;
    public Cannon cannon;

    [Header("Variables")]
    public string finalLevel;
    public int amountShoots;
    public int maxAmountShoots;
    public bool levelComplete;
    public bool endGame;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CheckEndGame();
        LevelComplete();
    }

    public static PoolSystem GetPool()
    {
        return instance.poolSystem;
    }

    public static UIController GetUI()
    {
        return instance.uiController;
    }
    
    void LevelComplete()
    {
        bool _isComplete = true;

        for (int i = 0; i < objsToBeDestroyed.Count; i++)
        {
            if (objsToBeDestroyed[i].activeSelf)
            {
                _isComplete = false;
                break;
            }
        }

        if (_isComplete)
        {
            levelComplete = true;
        }
    }

    void CheckEndGame()
    {   
        if(amountShoots >= maxAmountShoots)
        {
            cannon.canShoot = false;
            StartCoroutine("TimeToEndGame");
        }
    }

    IEnumerator TimeToEndGame()
    {
        yield return new WaitForSeconds(2f);
        endGame = true;
    }

    public void PlayAgain()
    {
        amountShoots = 0;
        endGame = false;
        poolSystem.ResetPool();
        uiController.endGamePanel.SetActive(false);
        for (int i = 0; i < objsToBeDestroyed.Count; i++)
        {
            objsToBeDestroyed[i].SetActive(true);
            cannon.canShoot = true;
        }
    }

    public void LoadNextLevel(int index)
    {
        cannon.canShoot = true;
        SceneManager.LoadScene(index);
    }
}
