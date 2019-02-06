using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public EnemyManager.Direction direction;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Enemy") {
            EnemyManager.GetInstance().RemoveEnemy(other.GetComponentInParent<Enemy>());
        }
    }

}
