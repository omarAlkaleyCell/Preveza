using UnityEngine;
using UnityEngine.Events;
public class CharacterControllerMode : MonoBehaviour,IMode
{
    [SerializeField] private Transform theBaseTransform;
    [SerializeField] private GameObject character;
    [SerializeField] private UnityEvent onEnterMode;
    [SerializeField] private UnityEvent onExitMode;
    public void EnterMode()
    {
        CameraController.Instance.MoveCameraToTargetLocation(theBaseTransform, () =>
        {
            character.SetActive(true);
        });
        onEnterMode?.Invoke();
    }

    public void ExitMode()
    {
        character.SetActive(false);
        onExitMode?.Invoke();
    }

    public void Initialize()
    {
        ModeManager.Instance.RegisterMode(0, this);
    }

    void Start()
    {
        Initialize();
    }
}
