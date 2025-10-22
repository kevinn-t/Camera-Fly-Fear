using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NUnit.Framework;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string imageID;

    [HideInInspector]
    public bool isSnapped = false;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Transform startParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isSnapped = false;
        transform.SetParent(transform.root, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // uses the change from the event data adjusted by canvas scaling
        rectTransform.anchoredPosition += eventData.delta / transform.root.localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // reset

        if (!isSnapped)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        isSnapped = false;
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
    } 
}
