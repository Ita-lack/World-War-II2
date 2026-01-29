using UnityEngine;

public class UseButtom : Interactable, IInteractable
{
    //------------------------- Variaveis Globais privadas -------------------------------
    public void BaseAction(){
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers(1);
                }
            }
        }
        ExecuteOrder(1);
    }
}