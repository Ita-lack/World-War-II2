using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI latitudeText;
    public TextMeshProUGUI longitudeText;
    public TextMeshProUGUI scoreText;
    public InputReader _inputReader;
    public GameObject helpPanel;
    public GameObject panelTelegraph;
    private int score;
    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    /*------------------------------------------------------------------------------
    Função:     OnEnable
    Descrição:  Associa todas as funções utilizadas ao canal de comunicação para que
                qualquer script que utilize o canal possa utilizar a função.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnEnable(){
        _inputReader.HelpEvent += ShowHelpPanel;
    }
    /*------------------------------------------------------------------------------
    Função:     OnDisable
    Descrição:  Desassocia todas as funções utilizadas ao canal de comunicação.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    private void OnDisable(){
        _inputReader.HelpEvent -= ShowHelpPanel;
    }
    public void Start()
    {
        scoreText.text = "Score: 5";
    }
    public void ShowCord(float lat, float lon)
    {
        latitudeText.text = $"{lat} N";
        longitudeText.text = $"{lon} E";
    }
    public void ShowButtom (string message, TextMeshProUGUI buttomText)
    {
        buttomText.text = message;
    }
    public void UpdateScore(int points)
    {
        score += points;
        if(score < 0){
            score = 0;
            scoreText.text = $"Score: {score}";
            SceneManager.LoadScene("GameOver");
        }else if(score < 10){
            scoreText.text = $"Score: {score}";
        }else{
            SceneManager.LoadScene("Victory");
        }
    }

    private void ShowHelpPanel(){
        if (panelTelegraph.activeSelf) helpPanel.SetActive(!helpPanel.activeSelf);
    }
}
