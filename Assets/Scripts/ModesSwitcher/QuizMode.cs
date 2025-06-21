using UnityEngine;
using UnityEngine.Events;

public class QuizMode : MonoBehaviour, IMode
{
    [SerializeField] private Transform QuizLocation;
    [SerializeField] private GameObject QuizPanel;
    [SerializeField] private UnityEvent onEnterMode;
    [SerializeField] private UnityEvent onExitMode;
    void Start()
    {
        
    }
    public void EnterMode()
    {
        CameraController.Instance.MoveCameraToTargetLocation(QuizLocation, () =>
        {
            QuizPanel.SetActive(true);
        });
        onEnterMode?.Invoke();
    }

    public void ExitMode()
    {
        QuizPanel.SetActive(false);
        onExitMode?.Invoke();
    }

    public void Initialize()
    {
        
    }
}
