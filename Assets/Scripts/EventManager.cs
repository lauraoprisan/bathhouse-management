using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    /*

    //Declaring the events

    //public event EventHandler OnFillInBath;

    //Creating the functions to use for triggering the event

    public void FillInBath() {
        OnFillInBath?.Invoke(this, EventArgs.Empty);
    }

 

  */

}