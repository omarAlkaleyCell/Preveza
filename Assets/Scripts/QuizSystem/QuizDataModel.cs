using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
}

[System.Serializable]
public class QuizData
{
    public QuizQuestion[] questions;
}
