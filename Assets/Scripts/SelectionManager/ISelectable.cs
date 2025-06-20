using UnityEngine;

public interface IHighlightable
{
    bool IsHighlighted { get;}
    void Highlight();
    void RemoveHighlight();
}
