using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour {

    public string identifier;

    [ContextMenu("Set ID")]
    public void SetID()
    {
        identifier = System.Guid.NewGuid().ToString();
    }
}

