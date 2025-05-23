using UnityEngine;
using UnityEngine.UI;    // for the standard UI Text
#if UNITY_EDITOR
using UnityEditor;       // for EditorApplication.isPlaying
#endif

public class GameManager : MonoBehaviour
{
    // Singleton for easy global access from Treasure.cs
    public static GameManager Instance { get; private set; }

    [Header("Score UI")]
    public Text scoreText;         // drag your ScoreText UI here
    public int totalTreasures = 7; // set to the number of treasures in your scene

    [Header("Victory UI & Audio")]
    public GameObject victoryPanel;  // drag your VictoryPanel here
    public AudioSource victoryAudio; // optional: drag a jingle AudioSource here

    private int score = 0;

    void Awake()
    {
        // enforce the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // hide victory at the start and init score display
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
        UpdateScoreUI();
    }

    /// <summary>
    /// Call this to add one point when a treasure is collected.
    /// </summary>
    public void IncrementScore()
    {
        score++;
        UpdateScoreUI();

        // if we've found them all, trigger victory
        if (score >= totalTreasures)
            OnVictory();
    }

    /// <summary>
    /// Updates the UI Text element to show the current score.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Called once when score == totalTreasures.
    /// Shows the victory panel and plays the jingle.
    /// </summary>
    private void OnVictory()
    {
        // play your victory sound (if assigned)
        if (victoryAudio != null)
            victoryAudio.Play();

        // show the victory panel
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    /// <summary>
    /// Hook this method to your Quit button’s OnClick.
    /// In a build it quits the application; in the Editor it stops playmode.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
