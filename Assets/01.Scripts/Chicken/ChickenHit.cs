using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    [SerializeField] private AudioSource _chickenPeckSound;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Hand"))
        {
            other.GetComponent<ObstacleMove>().ReturnObstacle();
            ChickenAnimation.Instance.ChickenPeckAnimation();
            _chickenPeckSound.Play();
        }
    }
}
