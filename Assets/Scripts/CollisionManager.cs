using UnityEngine;
using System;

public class CollisionManager : MonoBehaviour
{
    public event EventHandler OnPointGained;
    public event EventHandler OnHit;
    public event EventHandler OnGameOver;

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject && other.gameObject.CompareTag("Enemy"))
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnHit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                OnPointGained?.Invoke(this, EventArgs.Empty);
            }
            else if (other.gameObject.CompareTag("Food"))
            {
                Destroy(other.gameObject);
                OnPointGained?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
