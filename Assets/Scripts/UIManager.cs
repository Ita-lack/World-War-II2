using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI latitudeText;
    public TextMeshProUGUI longitudeText;
    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ShowCord(float lat, float lon)
    {
        latitudeText.text = $"{lat} N";
        longitudeText.text = $"{lon} E";
    }
}
