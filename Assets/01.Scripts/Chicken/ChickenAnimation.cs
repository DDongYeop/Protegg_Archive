using UnityEngine;

public class ChickenAnimation : MonoBehaviour
{
    public static ChickenAnimation Instance = null;
    
    private Animator _animator;
    private readonly int _chickenMoveHash = Animator.StringToHash("IsMove");
    private readonly int _chickenIncubateHash = Animator.StringToHash("IsIncubate");
    private readonly int _chickenPeckHash = Animator.StringToHash("IsPeck");

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        _animator = GetComponent<Animator>();
    }
    public void ChickenMoveAnimation(bool type) => _animator.SetBool(_chickenMoveHash, type);
    public void ChickenIncubateAnimation(bool type) => _animator.SetBool(_chickenIncubateHash, type);
    public void ChickenPeckAnimation() => _animator.SetTrigger(_chickenPeckHash);
}
