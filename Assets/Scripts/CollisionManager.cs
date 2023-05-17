using UnityEngine;
using System.Collections;
using System;

public class CollisionManager : MonoBehaviour
{
    public event EventHandler OnPointGained;
    public event EventHandler OnHit;
    public event EventHandler OnGameOver;

    private float hitDelayTime = 0.1f;
    private bool isTimerRunning = false;

    // Called when a collision with an enemy is continuously detected.
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

    // Invokes events after a delay using a coroutine.
    private IEnumerator InvokeEventsWithDelay()
    {
        isTimerRunning = true;
        yield return new WaitForSeconds(hitDelayTime);

        // Invoke the events after the 2-second delay
        OnHit?.Invoke(this, EventArgs.Empty);
        OnGameOver?.Invoke(this, EventArgs.Empty);
        isTimerRunning = false;
    }

    // Called when a trigger collision occurs.
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
