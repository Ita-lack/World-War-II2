using Unity.VisualScripting;
using UnityEngine;

public class UseButtom : Interactable, IInteractable, IObserver
{
    //------------------------- Variaveis Globais privadas -------------------------------
    bool warning = false;
    public string responceCorret;
    public TelegraphMinigame telegraphMinigame;
    private void OnEnable(){
        UIManager.Instance.ShowButtom(responceCorret, GetComponentInChildren<TMPro.TextMeshProUGUI>());
        RegisterEvent();
    }
    private void OnDisable(){
        UnregisterEvent();
    }
    public void OnEventRaised(int message, object additionalInformation){
        Debug.Log("[UseButtom] OnEventRaised");
        warning = !warning;
    }
    public void BaseAction(){
        if(!warning) return;
        Debug.Log("[UseButtom] BaseAction" + telegraphMinigame.GetMessageStatus(responceCorret));
        warning = false;
        if (_observerEventSpeak != null){
            foreach (var channel in _observerEventSpeak){
                if (channel != null){
                    channel.NotifyObservers(telegraphMinigame.GetMessageStatus(responceCorret) ? 1 : -1);
                }
            }
        }
        ExecuteOrder(1);
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEvent
    Descrição:  Desregistra o Objeto na lista de Observadores do item especifico.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    protected override void UnregisterEvent(){
        UnregisterEventPublic();
    }

    /*------------------------------------------------------------------------------
    Função:     RegisterEvent
    Descrição:  Registra este objeto como um observador em todos os canais de evento.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void RegisterEvent(){
        if (_observerEventListening != null){
            foreach (var channel in _observerEventListening){
                if (channel != null){
                    channel.RegisterObserver(this);
                }
            }
        }
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterEventPublic
    Descrição:  Desregistra este objeto de todos os canais de evento.
    Entrada:    -
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UnregisterEventPublic(){
        if (_observerEventListening  != null){
            foreach (var channel in _observerEventListening ){
                if (channel != null){
                    channel.UnregisterObserver(this);
                }
            }
        }
    }
}