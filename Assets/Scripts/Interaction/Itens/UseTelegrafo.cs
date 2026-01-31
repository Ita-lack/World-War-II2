using Unity.Cinemachine;
using UnityEngine;

public class UseTelegrafo : Interactable, IInteractable
{

    //------------------------- Variaveis Globais privadas -------------------------------
    
    private bool action = false;
    public GameObject _telegrafoUI;
    public CinemachineCamera cinemachineCamera;
    private int message = 0;
    public void BaseAction(){
        action = !action;
        message = action ? 1 : 0;
        _telegrafoUI.SetActive(action);
        cinemachineCamera.enabled = !action;
        Debug.Log($"[UseTelegrafo] Telegrafo UI Active: {action}");
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers(message);
                }
            }
        }
        ExecuteOrder(message);
    }
}
