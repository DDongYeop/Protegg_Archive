using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChickenMove : MonoBehaviour
{
    [SerializeField] private ChickenState _chickenState = ChickenState.Center;
    [SerializeField] private List<Vector2> _movePos = new List<Vector2>();
    [SerializeField] private float _moveTime = 1f;

    [SerializeField] private List<ButtonDownCheck> _moveButton;
    private BoxCollider2D _collision;
    private bool _isMove = false;

    private void Awake() 
    {
        _collision = GetComponentInChildren<BoxCollider2D>();
        _chickenState = ChickenState.Center;
        _collision.enabled = false;
    }

    private void Update() 
    {
        ButtonDown();
        ScoreUp();
    }

    private void ButtonDown()
    {
        if (_moveButton[1].isDown && _moveButton[2].isDown)
            ChickenMoveMethod(ChickenState.Up, 0);
        else if (_moveButton[1].isDown && _moveButton[3].isDown)
            ChickenMoveMethod(ChickenState.Left, 1, 0);
        else if (_moveButton[2].isDown && _moveButton[4].isDown)
            ChickenMoveMethod(ChickenState.Right, 2, 180);
        else if (_moveButton[3].isDown && _moveButton[4].isDown)
            ChickenMoveMethod(ChickenState.Down, 3);
        else if (_moveButton[0].isDown)
            ChickenMoveMethod(ChickenState.Center, 4);
        else if (_moveButton[1].isDown)
            ChickenMoveMethod(ChickenState.LeftUp, 5);
        else if (_moveButton[2].isDown)
            ChickenMoveMethod(ChickenState.RightUp, 6, 180);
        else if (_moveButton[3].isDown)
            ChickenMoveMethod(ChickenState.LeftDown, 7);
        else if (_moveButton[4].isDown)
            ChickenMoveMethod(ChickenState.RightDown, 8, 180);
    }

    private void ScoreUp()
    {
        if (_chickenState == ChickenState.Center && _moveButton[0].isDown)
            GameManager.Instance.ScoreUp();
        else
            ChickenAnimation.Instance.ChickenIncubateAnimation(false);
    }

    private void ChickenMoveMethod(ChickenState moveState, int index, int rotationindex = 0)
    {
        if (_isMove)
            return;

            SpriteFlip(rotationindex);
        
        if (_chickenState != moveState)
        {
            _isMove = true;
            ChickenAnimation.Instance.ChickenMoveAnimation(_isMove);
            _collision.enabled = false;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(_movePos[index], _moveTime));
            seq.AppendCallback(() =>
            {
                _chickenState = moveState;
                if (_chickenState == ChickenState.Center)
                    _collision.enabled = false;
                else
                    _collision.enabled = true;
                _isMove = false;
                ChickenAnimation.Instance.ChickenMoveAnimation(_isMove);
            });
        }
    }

    private void SpriteFlip(float index)
    {
        transform.localRotation = Quaternion.Euler(0, index, 0);
    }
}
