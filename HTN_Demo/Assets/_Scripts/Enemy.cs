using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public EnemyManager.Direction direction;
    [SerializeField]
    private float speed = 1.0f;

    private Rigidbody rb;
    private GameObject fieldVision;

    public bool canCapture;

    private void Start() {
        rb = GetComponentInChildren<Rigidbody>();
        canCapture = true;
        fieldVision = transform.GetChild(1).gameObject;
        speed = Random.Range(1.5f, 2.5f);
    }

    private void FixedUpdate() {
        Move();
        Rotation();
        if (canCapture)
            fieldVision.SetActive(true);
        else
            fieldVision.SetActive(false);
    }

    public void Move() {
        if(direction == EnemyManager.Direction.LEFT) {
            rb.MovePosition(rb.position - new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else {
            rb.MovePosition(rb.position + new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    public void Rotation() {
        if (direction == EnemyManager.Direction.LEFT) {
            rb.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else {
            rb.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    public void KillPlayer(AvatarCharacter avatar) {
        if (canCapture) {

        }
    }

    public void SwitchDirection() {
        if(direction == EnemyManager.Direction.LEFT) {
            direction = EnemyManager.Direction.RIGHT;
        }
        else {
            direction = EnemyManager.Direction.LEFT;
        }
    }
    

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Avatar")
            Debug.Log("Enemy Triger Enter: " + other.transform.tag);
    }


}
