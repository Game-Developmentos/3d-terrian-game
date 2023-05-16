using UnityEngine;
using System;

public class GameManagerUI : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public ScoreManager scoreManager;
    public ProgressBar progressBar;
    private CollisionManager collisionManager;

    private void Start()
    {
        collisionManager = GetComponent<CollisionManager>();
        collisionManager.OnPointGained += collisionManager_OnPointGained;
        collisionManager.OnHit += collisionManager_OnPointGained_OnHit;
        collisionManager.OnGameOver += collisionManager_OnGameOver;
    }
    private void collisionManager_OnPointGained(object sender, EventArgs e)
    {
        scoreManager.AddScore();
    }
    private void collisionManager_OnPointGained_OnHit(object sender, EventArgs e)
    {
        progressBar.DecreaseHealth();
    }
    private void collisionManager_OnGameOver(object sender, EventArgs e)
    {
        int currHealth = progressBar.GetCurrentHealth();
        int currScore = scoreManager.getScore();
        if (currHealth <= 0)
        {
            gameOverScreen.Setup(currScore);
        }
    }

}
