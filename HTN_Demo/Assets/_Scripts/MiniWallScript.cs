using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Enemy") {
            other.transform.GetComponentInParent<Enemy>().canCapture = false;
            int r = Random.Range(0, 3);
            if (r == 2) {
                // Disapper
             //   Debug.Log("Enemy Disapears");
                EnemyManager.GetInstance().RemoveEnemy(other.transform.GetComponentInParent<Enemy>());
            }
            else if (r == 1) {
                // Turn around
           //     Debug.Log("Enemy turns Around");
                StartCoroutine(ShiftCoroutine(other.transform.GetComponentInParent<Enemy>()));

            }
            else {
            //    Debug.Log("Nothing.");
                //nothing
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Enemy") {
            other.transform.GetComponentInParent<Enemy>().canCapture = true;
        }
    }

    IEnumerator ShiftCoroutine(Enemy pEnemy) {
        yield return new WaitForSeconds(0.5f);
        pEnemy.SwitchDirection();
    }
    

}
