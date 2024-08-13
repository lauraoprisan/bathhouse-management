using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BathState { 
    TurnedOff,
    Clean,
    FullCleanWater,
    Occupied,
    DirtyWater
}


public class Bath : MonoBehaviour
{
    public BathState bathState;
    private static List<Bath> readyBaths = new List<Bath>();    //this should probably be inside a BathManager
    [SerializeField] private SpriteRenderer innerBathRenderer;

    void Start()
    {
        bathState = BathState.TurnedOff;
    }

 

    public void FillBathWithWater() {
        bathState = BathState.FullCleanWater;
        SetInnerBathColor(new Color(0.4f, 0.69f, 0.95f, 1f)); //clean blue water, with opacity 1
        readyBaths.Add(this);
    }

    public void SetBathToOccupied() {
        bathState = BathState.Occupied;
        RemoveReadyBath(this);
    }

    public void SetBathToDirty() {
        bathState = BathState.DirtyWater;
        SetInnerBathColor(new Color(0.35f, 0.5f, 0.4f, 1f)); // dirty water color (greenish-blue)

    }

    public void CleanBath() {
        bathState = BathState.TurnedOff;
        SetInnerBathColor(new Color(0.4f, 0.69f, 0.95f, 0f)); //clean blue water, with opacity 1
    }

    private void SetInnerBathColor(Color color) {
        if (innerBathRenderer != null) {
            innerBathRenderer.color = color;
        } else {
            Debug.LogError("SpriteRenderer component is missing on innerBath object.");
        }
    }

    public static Bath GetFirstReadyBath() {
        if (readyBaths.Count > 0) { 
            return readyBaths[0];
        } else { return null; }
    }

    public static List<Bath> GetReadyBaths() {
        return readyBaths;
    }

    public static void RemoveReadyBath(Bath bath) { 
        readyBaths.Remove(bath);
    }
  
}
