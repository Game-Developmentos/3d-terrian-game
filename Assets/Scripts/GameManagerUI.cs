using UnityEngine;
using System;

public class GameManagerUI : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public ScoreManager scoreManager;
    public ProgressBar progressBar;
    private CollisionManager collisionManager;

    // Sets up event handlers for the CollisionManager events.
    private void Start()
    {
        collisionManager = GetComponent<CollisionManager>();
        collisionManager.OnPointGained += collisionManager_OnPointGained;
        collisionManager.OnHit += collisionManager_OnPointGained_OnHit;
        collisionManager.OnGameOver += collisionManager_OnGameOver;
    }

    // Event handler for the OnPointGained event of the CollisionManager.
    // Increases the score when a point is gained.
    private void collisionManager_OnPointGained(object sender, EventArgs e)
    {
        scoreManager.AddScore();
    }

    // Event handler for the OnHit event of the CollisionManager.
    // Decreases the health on hit.
    private void collisionManager_OnPointGained_OnHit(object sender, EventArgs e)
    {
        progressBar.DecreaseHealth();
    }

    // Event handler for the OnGameOver event of the CollisionManager.
    // Checks if the game is over based on the current health and sets up the game over screen if needed.  
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
