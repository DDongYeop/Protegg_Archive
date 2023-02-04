using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMove : MonoBehaviour
{
    public float returnMoveTime;
    public bool isMove = false;
    [SerializeField] private Vector2 _movePos;

    private Sequence sequenceMove;
    private Vector2 _firstPos;

    private void Start() 
    {
        _firstPos = transform.position;
    }

    public void ThisMove(float moveSpeed)
    {
        isMove = true;

        sequenceMove = DOTween.Sequence();
        sequenceMove.Append(transform.DOMove(_movePos, moveSpeed).SetLoops(2, LoopType.Yoyo));
        sequenceMove.AppendCallback(() => isMove = false);
    }

    public void ReturnObstacle()
    {
        sequenceMove.Kill();
        isMove = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_firstPos, returnMoveTime));
        sequence.AppendCallback(() => isMove = false);
    }
}
