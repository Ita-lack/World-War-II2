using UnityEngine;

public class UseTelegrafo : Interactable, IInteractable
{

    //------------------------- Variaveis Globais privadas -------------------------------
    
    private bool action = false;
    public GameObject _telegrafoUI;
    private int message = 0;
    public void BaseAction(){
        action = !action;
        message = action ? 1 : 0;
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
