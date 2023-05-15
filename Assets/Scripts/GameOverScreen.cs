using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    public TMP_Text pointsText;

    private void Awake()
    {
        sceneToLoad = SceneManager.GetActiveScene().name;
    }
    public void setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "You gained " + score.ToString() + " points!";
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
