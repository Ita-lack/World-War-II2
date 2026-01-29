using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class KnobController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("Value Settings")]
    public float minValue = 0.54f;
    public float maxValue = 44f;
    public float currentValue = 22f;

    [Header("Rotation Settings")]
    public float minAngle = -135f;
    public float maxAngle = 135f;
    public float sensitivity = 0.25f;

    [Header("UI")]
    [SerializeField] private RectTransform knobTransform;
    //[SerializeField] private TextMeshProUGUI valueText;

    private float lastMouseY;

    void Start()
    {
        UpdateKnob();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMouseY = eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - lastMouseY;
        lastMouseY = eventData.position.y;

        currentValue += deltaY * sensitivity;
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

        UpdateKnob();
    }

    void UpdateKnob()
    {
        float normalized = Mathf.InverseLerp(minValue, maxValue, currentValue);
        float angle = Mathf.Lerp(minAngle, maxAngle, normalized);

        knobTransform.localRotation = Quaternion.Euler(0f, 0f, angle);

        /*if (valueText != null)
            valueText.text = $"{currentValue:00.0}";*/
    }

    public float GetValue()
    {
        return currentValue;
    }

    public void SetRandomValue()
    {
        currentValue = Random.Range(minValue, maxValue);
        UpdateKnob();
    }
}
