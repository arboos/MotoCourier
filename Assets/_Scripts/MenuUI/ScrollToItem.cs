using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToItem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentTransform;

    public void OnClickScrollTo(string targetName) // CarTemplate
    {
        Canvas.ForceUpdateCanvases();

        RectTransform target = FindChildRectTransformByName(contentTransform, targetName); // content, CarTemplate

        if (target != null)
        {
            Vector2 viewPortLocalPosition = scrollRect.viewport.localPosition;
            Vector2 targetLocalPosition = target.localPosition;

            Vector2 newTargetLocalPosition = new Vector2(
                0 - (viewPortLocalPosition.x + targetLocalPosition.x) - (scrollRect.viewport.rect.width / 2) + (target.rect.width / 2),
                0 - (viewPortLocalPosition.y + targetLocalPosition.y)
            );

            contentTransform.localPosition = newTargetLocalPosition;
        }
        else
        {
            Debug.LogWarning($"Target with name {targetName} not found.");
        }
    }

    private RectTransform FindChildRectTransformByName(RectTransform parent, string name)
    {
        foreach (RectTransform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
        }

        return null;
    }
}