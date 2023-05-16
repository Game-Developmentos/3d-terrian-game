using UnityEngine;
using System.Collections;
using System;

public class CollisionManager : MonoBehaviour
{
    public event EventHandler OnPointGained;
    public event EventHandler OnHit;
    public event EventHandler OnGameOver;
    private bool isTimerRunning = false;

    private void OnCollisionStay(Collision other)
    {
        if (gameObject && other.gameObject.CompareTag("Enemy"))
        {
            if (!isTimerRunning)
            {
                StartCoroutine(InvokeEventsWithDelay());
            }
        }
    }

    private IEnumerator InvokeEventsWithDelay()
    {
        isTimerRunning = true;
        yield return new WaitForSeconds(1f);

        // Invoke the events after the 2-second delay
        OnHit?.Invoke(this, EventArgs.Empty);
        OnGameOver?.Invoke(this, EventArgs.Empty);

        isTimerRunning = false;
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
