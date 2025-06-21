using UnityEngine;
public enum QuestType
{
    Collect,
    Find,
    Kill
}
[CreateAssetMenu(fileName = "NewQuest", menuName = "QusetMenu/CreateNewQuest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public string questID;
    public QuestType questType;
    public string targetID;
    public int requiredAmount;
}
