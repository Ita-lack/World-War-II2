using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TelegraphMinigame : MonoBehaviour
{
    //Variaveis
    [Header("Frequency Settings")]
    [SerializeField] private float minFrequency = 0.54f;
    [SerializeField] private float maxFrequency = 44f;
    [SerializeField] private float tolerance = 0.5f;//minimo exigido da frequncia do Slider A para a resposta certa
    [SerializeField] private float toleranceB = 0.3f;//minimo exigido da frequncia do Slider B para a resposta certa
    [SerializeField] private float minNeedleAngle = -60f;
    [SerializeField] private float maxNeedleAngle = 60f;
    [SerializeField] private float needleSpeed = 5f;
    private float currentNeedleAngle;

    [Header("UI References")]
    //[SerializeField] private Slider frequencySlider;//Frequência do jogador A
    [SerializeField] private KnobController knobA;
    //[SerializeField] private Slider frequencySliderB;//Frequência do jogador B
    [SerializeField] private KnobController knobB;
    [SerializeField] private TextMeshProUGUI frequencyText;
    [SerializeField] private Image signalMeter;
    [SerializeField] private RectTransform needleTransform;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Mensagens a ser tranmitida")]
    [SerializeField] private string[] messages;
    
    //Cotroles
    private float targetFrequency;//Frequência alvo A
    private float targetFrequencyB;//Frequência alvo B
    private bool messageUnlocked;//Verifica se a mensagem foi desbloqueada
    
    private float currentA;
    private float currentB;

    private float distanceA;
    private float distanceB;

    private float signalA;
    private float signalB;

    private float combinedSignal;
    private float Normalize(float distance, float maxDistance)
    {
    float normalized = 1f - (distance / maxDistance);
    return Mathf.Clamp01(normalized);
    }
    /*float distance = Mathf.Abs(playerFrequency - targetFrequency);
    audioSource.volume = Mathf.InverseLerp(maxDistance, 0, distance);*/

    private void Start()
    {
        GenerateTargetFrequency();

        /*frequencySlider.minValue = minFrequency;
        frequencySlider.maxValue = maxFrequency;

        frequencySliderB.minValue = minFrequency;
        frequencySliderB.maxValue = maxFrequency;*/

        messagePanel.SetActive(false);
        statusText.text = "TRANSMISSION LOST";
    }

    private void Update()
    {
        if (messageUnlocked) return;

        currentA = knobA.GetValue();//frequencySlider.value;
        currentB = knobB.GetValue();//frequencySliderB.value;

        frequencyText.text = $"A:{currentA:00.0}  B:{currentB:00.0} MHz";

        distanceA = Mathf.Abs(currentA - targetFrequency);
        distanceB = Mathf.Abs(currentB - targetFrequencyB);
        
        signalA = Normalize(distanceA, maxFrequency - minFrequency);
        signalB = Normalize(distanceB, maxFrequency - minFrequency);
        
        combinedSignal = Mathf.Min(signalA, signalB);
        
        UpdateSignalMeterCombined(combinedSignal);
        
        if (distanceA <= tolerance && distanceB <= toleranceB)
        {
            UnlockMessage();
        }
        
    }

    private void GenerateTargetFrequency()//Gera a frequência alvo
    {
        targetFrequency = Random.Range(minFrequency, maxFrequency);
        targetFrequencyB = Random.Range(minFrequency, maxFrequency);
        Debug.Log("Target Frequency: " + targetFrequency);
    }

    private void UpdateSignalMeter(float distance)//Atualiza o que é utilizado como referencia para a resposta
    {
        float maxDistance = maxFrequency - minFrequency;
        float normalized = 1f - (distance / maxDistance);
        normalized = Mathf.Clamp01(normalized);
        signalMeter.fillAmount = normalized; // opcional
        UpdateNeedle(normalized);
        statusText.text = normalized > 0.95f
        ? "SIGNAL STABLE"
        : "TUNING...";
    
    }

    private void UpdateNeedle(float normalizedSignal)//Controla a agulha
    {
        float targetAngle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, normalizedSignal);
        currentNeedleAngle = Mathf.Lerp(currentNeedleAngle, targetAngle, Time.deltaTime * needleSpeed);
        needleTransform.localRotation = Quaternion.Euler(0f, 0f, currentNeedleAngle);
    }

    void UpdateSignalMeterCombined(float normalized)
    {
        signalMeter.fillAmount = normalized;
        UpdateNeedle(normalized);

        if (normalized > 0.95f) statusText.text = "SIGNAL STABLE";
        else if (normalized > 0.6f) statusText.text = "WEAK LOCK";
        else statusText.text = "TUNING...";
    }

    private void UnlockMessage()//Mensagem a ser transmitida
    {
        messageUnlocked = true;
        statusText.text = "TRANSMISSION LOCKED";
        messagePanel.SetActive(true);

        messageText.text = messages[RandomizerMansage()];
    }

    private int RandomizerMansage()
    {
        int r = 0;
        r = Random.Range(0, messages.Length);
        return r;
    }

    public void ResetMinigame()
    {
       messageUnlocked = false;
       GenerateTargetFrequency();
       
       //frequencySlider.value  = Random.Range(minFrequency, maxFrequency);
       //frequencySliderB.value = Random.Range(minFrequency, maxFrequency);
       knobA.SetRandomValue();
       knobB.SetRandomValue();
       
       currentNeedleAngle = minNeedleAngle;
       needleTransform.localRotation = Quaternion.Euler(0, 0, minNeedleAngle);
       
       signalMeter.fillAmount = 0f;
       statusText.text = "TRANSMISSION LOST";
       messagePanel.SetActive(false);
    }
}
