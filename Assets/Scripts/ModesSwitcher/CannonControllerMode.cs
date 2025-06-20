using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CannonControllerMode : MonoBehaviour, IMode
{
    [SerializeField] private Transform cannon;
    [SerializeField] private ImageFader aimingUI;
    [SerializeField] private UnityEvent onEnterMode;
    [SerializeField] private UnityEvent onExitMode;

    void Start()
    {
        Initialize();
    }

    public void EnterMode()
    {
        CameraController.Instance.MoveCameraToTargetLocation(cannon, () =>
        {
            aimingUI.Show();
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
