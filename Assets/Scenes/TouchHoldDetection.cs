using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchHoldDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingHeld = false;
    private float holdTime = 1.0f; // Adjust this time threshold as needed
    private float touchStartTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        isBeingHeld = true;
        touchStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingHeld = false;

        if (Time.time - touchStartTime < holdTime)
        {
            Debug.Log("Touched/Clicked");
        }
        else
        {
            Debug.Log("Hold");
        }
    }
}

