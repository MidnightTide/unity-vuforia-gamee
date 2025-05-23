using UnityEngine;
using UnityEngine.UI;  // for Text & Button

public class QuizManager : MonoBehaviour
{
    // ➤ Singleton so all scripts can do QuizManager.Instance.ShowQuiz(...)
    public static QuizManager Instance { get; private set; }

    [Header("UI references — drag these in the Inspector")]
    public GameObject quizPanel;   // the panel itself
    public Text questionText;      // the Text component showing the question
    public Button[] answerButtons; // size this to the number of choices (e.g. 3 or 4)

    private Treasure currentTreasure;

    void Awake()
    {
        // enforce singleton
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("QuizManager instance set");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Call this from Treasure.OnMouseDown() to pop up the quiz.
    /// </summary>
    public void ShowQuiz(Treasure treasure)
    {
        Debug.Log("ShowQuiz called for: " + treasure.gameObject.name);
        currentTreasure = treasure;

        // 1) Update the question label
        questionText.text = treasure.question;

        // 2) Update each answer button
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < treasure.answers.Length)
            {
                // show & label the button
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<Text>().text = treasure.answers[i];

                // clear old listeners
                answerButtons[i].onClick.RemoveAllListeners();

                // wire up correct vs. wrong callbacks
                if (i == treasure.correctAnswerIndex)
                    answerButtons[i].onClick.AddListener(OnCorrect);
                else
                    answerButtons[i].onClick.AddListener(OnWrong);
            }
            else
            {
                // hide any unused buttons
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // 3) Show panel and pause the game
        quizPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Called when the player picks the correct answer.
    /// </summary>
    public void OnCorrect()
    {
        // hide quiz, resume, collect the treasure
        quizPanel.SetActive(false);
        Time.timeScale = 1f;
        currentTreasure.Collect();
    }

    /// <summary>
    /// Called when the player picks a wrong answer.
    /// </summary>
    public void OnWrong()
    {
        // simple feedback—you can expand this
        Debug.Log("Wrong answer—try again!");
    }
}