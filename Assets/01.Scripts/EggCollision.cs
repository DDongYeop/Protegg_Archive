using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
