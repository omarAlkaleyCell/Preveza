using UnityEngine;
using UnityEngine.InputSystem;
public class CannonBallShooter_Player : MonoBehaviour
{
    [SerializeField] CannonController[] cannons;
    [SerializeField] GameObject[] cannonHolders;
    [SerializeField] private InputActionReference fireCannonAction;
    void Start()
    {
        fireCannonAction.action.performed += onFireCannon;
    }
    private void onFireCannon(InputAction.CallbackContext context)
    {
        if (cannons == null || cannons.Length <= 0) return;
        if (ModeManager.Instance.CurrentMode is not CannonControllerMode) return;
        foreach (var cannon in cannons)
        {
            cannon.FireCannon();
        }
    }
    private void Update()
    {
        if (cannons == null || cannons.Length <= 0) return;
        if (ModeManager.Instance.CurrentMode is not CannonControllerMode) return;
        if (HighlightManager.Instance.GetCurrentHighlightedObject == null) return;
        foreach (var cannonHolder in cannonHolders)
        {
            cannonHolder.transform.rotation = Quaternion.LookRotation(HighlightManager.Instance.GetCurrentHighlightedObject.position - cannonHolder.transform.position);
        }
    }
}
