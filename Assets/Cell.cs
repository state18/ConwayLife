using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    private bool isAlive;
    public bool IsAlive {
        get {
            return isAlive;
        }
        set {
            isAlive = value;
            GameObject go = this.gameObject;
            go.GetComponent<Renderer>().enabled = value;
        }
    }
}
