using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "You gained " + score.ToString() + " points!";
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
