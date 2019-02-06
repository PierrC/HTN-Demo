using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour {

    [SerializeField]
    public List<Alcove> alcoves;

    public static HolderScript instance;

    private void Start() {
        instance = this;
    }

    public static HolderScript GetInstance() {
        return instance;
    }
}
