using UnityEngine;

public class highlightableShip : MonoBehaviour, IHighlightable
{
    [SerializeField] private Outline outline;
    //[SerializeField] private SimpleTooltip simpleTooltip;
    void Start()
    {
        outline = GetComponent<Outline>();
        //simpleTooltip = GetComponent<SimpleTooltip>();
    }
    private bool isHighlighted = false;
    public bool IsHighlighted =>  isHighlighted;
    
    public void Highlight()
    {
        isHighlighted = true;
        if (outline == null) return;
        outline.ShowOutline();
        // if (simpleTooltip == null) return;
        // simpleTooltip.ShowTooltip();
    }

    public void RemoveHighlight()
    {
        isHighlighted = false;
        if (outline == null) return;
        outline.HideOutline();
        // if (simpleTooltip == null) return;
        // simpleTooltip.HideTooltip();
    }
}
