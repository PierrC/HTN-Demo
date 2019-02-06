using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {


    }

    private void FixedUpdate() {
        PlayerAvatarMovment();
        OtherControls();
    }

    [SerializeField]
    private float speed;
    private Rigidbody rb;

    void PlayerAvatarMovment() {

        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 mov = rb.position + Time.fixedDeltaTime * speed * new Vector3(_xMov, 0, _zMov);

        rb.MovePosition(mov);
    }

    void OtherControls() {
        if (Input.GetButtonDown("Trap")) {
            Debug.Log("Trap called");
            GetComponent<AvatarCharacter>().UseTrap();
        }
    }


}
