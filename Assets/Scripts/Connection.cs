using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Connection : MonoBehaviour
{
    public string connectionName;
    [TextArea(4,18)]
    public string description;
    public Location location;
    public bool connectionEnabled;
}
