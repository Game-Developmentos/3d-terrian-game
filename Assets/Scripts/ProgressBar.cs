using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int minimum = 0;
    [SerializeField] private int maximum = 200;
    [SerializeField] private int currentHealth = 200;
    public Image mask;
    public Image fill;
    private Color color;
    private int damageTaken = 5;

    // Updates the progress bar fill based on the current health value.
    void Update()
    {
        GetCurrentFill();
    }

    // Returns the current health value.
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Decreases the current health by the specified damage amount.
    public void DecreaseHealth()
    {
        currentHealth -= damageTaken;
    }

    // Calculates and sets the fill amount of the progress bar based on the current health.
    void GetCurrentFill()
    {
        float currentOffset = currentHealth - minimum;
        float maximumOffset = maximum - minimum;
        float FillAmount = currentOffset / maximumOffset;
        mask.fillAmount = FillAmount;
        fill.color = color;
    }
}
