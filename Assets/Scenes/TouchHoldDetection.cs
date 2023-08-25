using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchHoldDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    public painarray pain;
    public GameObject legang;
    private bool isBeingHeld = false;
    private float holdTime = 1.0f; // Adjust this time threshold as needed
    private float touchStartTime;
    float startangle;

    public void OnPointerDown(PointerEventData eventData)
    {
        isBeingHeld = true;
        touchStartTime = Time.time;
        //startangle=2*(Mathf.Rad2Deg*legang.transform.localRotation.x);
        startangle = 2 * (Mathf.Rad2Deg * Mathf.Acos(legang.transform.localRotation.w));

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingHeld = false;

        if (Time.time - touchStartTime < holdTime)
        {
            Debug.Log("Touched/Clicked");
            pain.singledata(startangle);
        }
        else
        {   
            float lastang=2 *(Mathf.Rad2Deg*Mathf.Acos(legang.transform.localRotation.w));
            Debug.Log("Hold");
            pain.rangedata(startangle,lastang);
        }
    }
    public Transform pivot; // The central pivot point
    public float maxRadius = 100f; // The maximum allowed radius

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pivotToCursor = eventData.position - (Vector2)pivot.position;
        Vector2 clampedPosition = Vector2.ClampMagnitude(pivotToCursor, maxRadius);

        rectTransform.anchoredPosition = clampedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Perform any logic you need when dragging ends
    }
}

