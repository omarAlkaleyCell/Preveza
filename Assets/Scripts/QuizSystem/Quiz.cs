using System.Collections.Generic;
using ArabicSupport;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private UnityEvent onAnswerCorrect;
    [SerializeField] private UnityEvent onAnswerIncorrect;
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
            onAnswerCorrect?.Invoke();
        }
        else
        {
            onAnswerIncorrect?.Invoke();
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
        scoreText.text = ArabicFixer.Fix("لقد أجبت بشكل صحيح عدد "+ $"\"{score}\"" +" سؤال من " + $"\"{quizData.questions.Length}\"" + " أسئلة ",true);
    }
}
