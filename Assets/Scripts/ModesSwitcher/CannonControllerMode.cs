using UnityEngine;
using UnityEngine.Events;
public class CannonControllerMode : MonoBehaviour, IMode
{
    [SerializeField] private Transform cannon;
    [SerializeField] private ImageFader aimingUI;
    [SerializeField] private UnityEvent onEnterMode;
    [SerializeField] private UnityEvent onExitMode;
    [SerializeField] Quest quest;
    private bool isQuestStarted;
    void Start()
    {
        Initialize();
    }

    public void EnterMode()
    {
        CameraController.Instance.MoveCameraToTargetLocation(cannon, () =>
        {
            aimingUI.Show();
            if (!isQuestStarted)
            {
                QuestSystem.Instance.StartQuest(quest);
                isQuestStarted = true;
            }
        });
        onEnterMode?.Invoke();
    }

    public void ExitMode()
    {
        aimingUI.Hide();
        onExitMode?.Invoke();
    }

    public void Initialize()
    {
        ModeManager.Instance.RegisterMode(1, this);
    }
}
