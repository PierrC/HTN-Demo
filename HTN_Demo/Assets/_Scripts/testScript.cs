using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
    //    Debug.Log("TestScript trigger:" + other.transform.tag);
        if(other.transform.tag == "Avatar") {
            AvatarManager.GetInstance().RemoveAvatar(other.GetComponent<AvatarCharacter>());
        }
    }
}
