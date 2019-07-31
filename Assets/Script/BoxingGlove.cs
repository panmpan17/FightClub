using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGlove : MonoBehaviour
{
    // [HideInInspector]
    public bool punching, punched, defending;
    // [HideInInspector]
    public bool retracted = true;
    [HideInInspector]
    public Fighter owner;

    public void StartPunch() {
        retracted = false;
        punching = true;
        punched = false;
    }
    public void Punched() {
        punched = true;
        owner.AddCombo();
    }
    public void RetractPunch() {
        retracted = true;
    }
    public void EndPunch() {
        punching = false;
        retracted = true;
    }
}