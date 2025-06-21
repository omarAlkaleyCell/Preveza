using System.Collections.Generic;
using ArabicSupport;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public GameObject quizPanel;
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;

    private QuizData quizData;
    private int currentQuestionIndex = 0;
    private int score = 0;
    void Start()
    {
        LoadQuizData();
        ShowQuestion();
    }
    void LoadQuizData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("quiz_data");
        quizData = JsonUtility.FromJson<QuizData>(jsonFile.text);
    }
    void ShowQuestion()
    {
        if (currentQuestionIndex < quizData.questions.Length)
        {
            QuizQuestion question = quizData.questions[currentQuestionIndex];
            questionText.text = ArabicFixer.Fix(question.question,true);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ArabicFixer.Fix(question.answers[i]);
                int index = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
        }
        else
        {
            EndQuiz();
        }
    }
    void OnAnswerSelected(int selectedIndex)
    {
        if (quizData.questions[currentQuestionIndex].correctAnswerIndex == selectedIndex)
        {
            score++;
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
        scoreText.text = $"You got {score}/{quizData.questions.Length} correct!";
    }
    // void SpawnObjectFrom(GameObject[] objects)
    // {
    //     int index = UnityEngine.Random.Range(0, objects.Length);
    //     GameObject instance = Instantiate(objects[index], goodAndBadAnswersVFXPosition, Quaternion.identity);
    //     Destroy(instance, 3f);
    // }
}
