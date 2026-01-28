//----------------------------- Bibliotecas Usadas -------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ObserverEventChannel : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    /*------------------------------------------------------------------------------
    Função:     RegisterObserver
    Descrição:  Registra qualquer Oberservador que queira receber informações sobre um interagivel especifico.
    Entrada:    IObserver - Qual Objeto quer ser adicionado na lista.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void RegisterObserver(IObserver observer){
        if (!observers.Contains(observer)) observers.Add(observer);
    }
    /*------------------------------------------------------------------------------
    Função:     UnregisterObserver
    Descrição:  Desregistra qualquer Oberservador que queira receber informações sobre um interagivel especifico.
    Entrada:    IObserver - Qual Objeto quer ser retirado da lista.
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void UnregisterObserver(IObserver observer){
        if (observers.Contains(observer)) observers.Remove(observer);
    }
    /*------------------------------------------------------------------------------
    Função:     NotifyObservers
    Descrição:  Notifica todos os Observadores que se registraram para executarem uma função.
    Entrada:    int - informação para animação do item
                object - informação generica para cada item especifico
    Saída:      -
    ------------------------------------------------------------------------------*/
    public void NotifyObservers(int message = 1, object additionalInformation = null){
        if(observers != null){
            var observersCopy = observers.ToList();
            foreach (var observer in observersCopy){
                observer.OnEventRaised(message, additionalInformation);
            }
        }
    }
    
}
  


