using UnityEngine;

public class Treasure : MonoBehaviour
{
    public AudioSource audioSource;

    /* ----------  Quiz data (fill in per-treasure in the Inspector) ---------- */
    [Header("Quiz Data")]
    [TextArea] public string question;          // one-line or multi-line question
    public string[] answers = new string[3];     // answer choices
    [Tooltip("Which element in the array is the correct answer? (0-based index)")]
    public int correctAnswerIndex = 0;
    /* ----------------------------------------------------------------------- */

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Runs when player clicks / taps this object
    void OnMouseDown()
    {
        Debug.Log("Treasure clicked: " + name);
        if (QuizManager.Instance != null)
            QuizManager.Instance.ShowQuiz(this);
        else
            Debug.LogError("No QuizManager instance found!");
    }


    public void Collect()
    {
        if (audioSource != null) audioSource.Play();
        GameManager.Instance.IncrementScore();
        gameObject.SetActive(false);
    }
}
