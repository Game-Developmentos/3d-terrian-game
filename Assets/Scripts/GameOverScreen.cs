using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;

    // Activates the game over screen and displays the score.
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "You gained " + score.ToString() + " points!";
    }

    // Restarts the current scene when the "Try Again" button is clicked.
    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
