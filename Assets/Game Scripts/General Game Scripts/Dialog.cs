using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] private List<string> dLines;

    public List<string> dialogLines {
        get { return dLines; }
    }
}
