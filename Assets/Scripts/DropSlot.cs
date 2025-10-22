using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class ImageDropEvent : UnityEvent<string> { }
public class DropSlot : MonoBehaviour, IDropHandler
{
    public ImageDropEvent OnImageDropped;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            DraggableImage draggable = droppedObject.GetComponent<DraggableImage>();

            if (draggable != null)
            {
                draggable.isSnapped = true;
                draggable.transform.position = this.transform.position;
                OnImageDropped.Invoke(draggable.imageID);
                Debug.Log("Drop event fired!");
            }
        }
    }
}
