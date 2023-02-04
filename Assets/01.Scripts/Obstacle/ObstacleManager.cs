using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleManager : MonoBehaviour
{
    [Header("ObstacleMove List")]
    [SerializeField] private List<ObstacleMove> _firstObstacle = new List<ObstacleMove>();
    [SerializeField] private List<ObstacleMove> _secondObstacle = new List<ObstacleMove>();
    
    [Header("First Balance")]
    [SerializeField] private float _firstDelayTime = 3;
    [SerializeField] private float _firstRemovePercent = 200;

    [Header("Second Balance")]
    [SerializeField] private float _secondDelayTime = 4;
    [SerializeField] private float _secondRemovePercent = 250;

    private float _firstMoveSpeed = 2;
    private float _secondMoveSpeed = 3;
    public bool isSecond = false;

    private void Start() 
    {
        ObstacleStart();
    }

    private void Update() 
    {
        if (_firstDelayTime <= 1.9f && !isSecond)
        {
            isSecond = true;
            StartCoroutine(SecondObstacleMoveCO());
        }
    }
    
    public void ObstacleStart()
    {
        StartCoroutine(FirstObstacleMoveCO());
        isSecond = false;
    }

    private IEnumerator FirstObstacleMoveCO()
    {
        while (true)
        {
            yield return new WaitForSeconds(_firstDelayTime);
            ObstacleMove(_firstObstacle, _firstMoveSpeed);
            _firstDelayTime = _firstDelayTime - (_firstDelayTime / _firstRemovePercent);
            _firstMoveSpeed = _firstMoveSpeed - (_firstMoveSpeed / 250);
        }
    }

    private IEnumerator SecondObstacleMoveCO()
    {
        while (true)
        {
            ObstacleMove(_secondObstacle, _secondMoveSpeed);
            yield return new WaitForSeconds(_secondDelayTime);
            _secondDelayTime = _secondDelayTime - (_secondDelayTime / _secondRemovePercent);
            _secondMoveSpeed = _secondMoveSpeed - (_secondMoveSpeed / 350);
        }
    }

    private void ObstacleMove(List<ObstacleMove> obstacleMove, float moveSpeed)
    {
        int obstacleIndex = Random.Range(0, 4);
        if (obstacleMove[obstacleIndex].isMove)
        {
            ObstacleMove(obstacleMove, moveSpeed);
            return;
        }

        obstacleMove[obstacleIndex].ThisMove(moveSpeed);
    }
}
