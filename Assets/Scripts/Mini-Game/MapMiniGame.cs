using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class MapMiniGame : Interactable, IInteractable
{
    public Slider latitudeSlider;
    public Slider longitudeSlider;
    public RectTransform mapMarking;
    public RectTransform mapArea;
    public MorseAudioPlayer mAPTelegraph;
    private float latitude;         
    private float longitude;

    private float tLat;
    private float tLon;
    private float xEixo;
    private float yEixo;


    private Vector2 coord = new Vector2();
    public void Start(){
        UpdateNewPuzzle();
    }
    public void BaseAction(){
        latitude = latitudeSlider.value;
        longitude = longitudeSlider.value;
        tLat = Mathf.InverseLerp(0, 65, latitude);
        tLon = Mathf.InverseLerp(35, 75, longitude);
        xEixo = (tLat - 0.5f) * 2f; 
        yEixo = (tLon - 0.5f) * 2f;
        mapMarking.anchoredPosition = new Vector2( xEixo *  mapArea.rect.width/2 ,  yEixo * mapArea.rect.height/2);
        UIManager.Instance.ShowCord(latitude, longitude);
        PuzzleVerified();
    }

    public void UpdateNewPuzzle(){
        latitudeSlider.value = Random.Range(latitudeSlider.minValue, latitudeSlider.maxValue);
        longitudeSlider.value = Random.Range(longitudeSlider.minValue, longitudeSlider.maxValue);
        BaseAction();
    }
    public void PuzzleVerified(){
        //
        coord.x = latitude;
        coord.y = longitude;
        Debug.Log($"[MapMiniGame] Current Coord: {coord} - Target Coord: {mAPTelegraph.GetCurrentCoords()}");
        if(coord !=  mAPTelegraph.GetCurrentCoords())return; 
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers();
                }
            }
        }
    }
}
