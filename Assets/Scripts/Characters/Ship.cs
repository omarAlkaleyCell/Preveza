using UnityEngine;

public class Ship : MonoBehaviour
{
    string id = "Ship";
    void OnDestroy()
    {
        QuestSystem.Instance?.ReportProgress(id);
    }
}
