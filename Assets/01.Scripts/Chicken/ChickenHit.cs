using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    [SerializeField] private AudioSource _chickenPeckSound;
    [SerializeField] private Transform _particleSpawnPos;
    [SerializeField] private GameObject _particle;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Hand"))
        {
            Instantiate(_particle, _particleSpawnPos.position, Quaternion.identity);
            other.GetComponent<ObstacleMove>().ReturnObstacle();
            ChickenAnimation.Instance.ChickenPeckAnimation();
            _chickenPeckSound.Play();
        }
    }
}
