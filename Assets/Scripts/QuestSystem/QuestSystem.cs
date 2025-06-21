using ArabicSupport;
using UnityEditor.VersionControl;
using UnityEngine;

public class QuestSystem : Singleton<QuestSystem>
{
    [SerializeField] private Quest currentQuest;
    [SerializeField] private QuestCompleteUI questCompleteUI;
    
    private int currentProgress = 0;

    public void StartQuest(Quest quest)
    {
        string message = ArabicFixer.Fix("مهمتك الآن هي " + quest.title);
        questCompleteUI.ShowMessage(message);
        currentQuest = quest;
        currentProgress = 0;
        Debug.Log("Started quest: " + quest.title);
    }

    public void ReportProgress(string targetID)
    {
        if (currentQuest == null) return;
        if (targetID == currentQuest.targetID)
        {
            currentProgress++;
            Debug.Log("Progress: " + currentProgress + "/" + currentQuest.requiredAmount);
            string[] message = new[] { ArabicFixer.Fix("أحسنت"), ArabicFixer.Fix("انت رائع"), ArabicFixer.Fix(""), ArabicFixer.Fix("اصابة رائعة") };
            
            questCompleteUI.ShowMessage(message[Random.Range(0, message.Length)]);
            
            if (currentProgress >= currentQuest.requiredAmount)
            {
                CompleteQuest();
            }
        }
    }

    void CompleteQuest()
    {
        string message = ArabicFixer.Fix("لقد اكملت المهمة بنجاح");
        questCompleteUI.ShowMessage(message);
        if (currentQuest.questType == QuestType.Kill)
        {
            QuizMode quizMode = FindAnyObjectByType<QuizMode>();
            if (quizMode == null) return;
            ModeManager.Instance.RegisterMode(2, quizMode);
            ModeManager.Instance.SetCurrentModeById(2);
        }
        Debug.Log("Quest Complete: " + currentQuest.title);
        currentQuest = null;
    }
}
