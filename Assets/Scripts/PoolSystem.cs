using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    public List<Ball> listBallPool;
    public Ball ballPrefab;
    public int initialAmountBalls;
    private GameObject _ballsHolder;

    void Start()
    {
        InitializePool();
    }

    public void ResetPool()
    {
        for (int i = 0; i < listBallPool.Count; i++)
        {
            listBallPool[i].gameObject.SetActive(false);
        }
    }

    void InitializePool()
    {
        _ballsHolder = new GameObject("--- Balls Pool");
        _ballsHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountBalls; index++)
        {
            Ball _ball = Instantiate(ballPrefab);
            _ball.transform.SetParent(_ballsHolder.transform);
            _ball.gameObject.SetActive(false);
            listBallPool.Add(_ball);
        }
    }

    public Ball TryToGetBall()
    {
        Ball _toReturn = null;

        for (int index = 0; index < listBallPool.Count; index++)
        {
            Ball _possibleBall = listBallPool[index];
            if (!_possibleBall.gameObject.activeSelf)
            {
                _toReturn = _possibleBall;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(ballPrefab);
            _toReturn.transform.SetParent(_ballsHolder.transform);
            listBallPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
