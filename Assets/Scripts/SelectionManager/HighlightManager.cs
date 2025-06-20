using UnityEngine;

public class HighlightManager : Singleton<HighlightManager>
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float highlightDistance = 5000f;

    private IHighlightable currentHighlight;
    public Transform GetCurrentHighlightedObject{ get; private set;}

    void Update()
    {
        if (ModeManager.Instance.CurrentMode == null) return;

        if (!(ModeManager.Instance.CurrentMode is CannonControllerMode))
        {
            print($"Current mode is {ModeManager.Instance.CurrentMode}, skipping highlight update.");
            RemoveCurrentHighlight();
            return;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, highlightDistance, layerMask))
        {
            if (hit.collider.TryGetComponent(out IHighlightable newHighlight))
            {
                // Only update highlight if it's a different object
                if (newHighlight != currentHighlight)
                {
                    RemoveCurrentHighlight();
                    currentHighlight = newHighlight;
                    currentHighlight.Highlight();
                    GetCurrentHighlightedObject = hit.collider.transform;
                }
            }
            else
            {
                RemoveCurrentHighlight();
            }
        }
        else
        {
            RemoveCurrentHighlight();
        }
    }

    private void RemoveCurrentHighlight()
    {
        if (currentHighlight != null && currentHighlight.IsHighlighted)
        {
            currentHighlight.RemoveHighlight();
            currentHighlight = null;
            GetCurrentHighlightedObject = null;

        }
    }
}
