using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontTouchMe : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.Release();
    }
}
