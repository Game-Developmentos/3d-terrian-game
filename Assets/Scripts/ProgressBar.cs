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
    void Update()
    {
        GetCurrentFill();
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }
    public void DecreaseHealth() {
        currentHealth-=damageTaken;
    }
    void GetCurrentFill()
    {
        float currentOffset = currentHealth - minimum;
        float maximumOffset = maximum - minimum;
        float FillAmount = currentOffset / maximumOffset;
        mask.fillAmount = FillAmount;
        fill.color = color;
    }
}
