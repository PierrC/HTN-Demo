using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public Stack<Operator> actions;
    public Stack<Vector3> destinations;

    public bool InAction;

    private static AIController instance;

    public static AIController GetInstance() {
        return instance;
    }

	// Use this for initialization
	void Start () {
        actions = new Stack<Operator>();
        destinations = new Stack<Vector3>();
        InAction = true;
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
    //    Debug.Log("Action count:" + actions.Count);
        if (actions.Count > 0) {
            Operator op = actions.Pop();
            if (op == Operator.MOVE) {
                MoveAvatar(destinations.Pop());
            }
            else if (op == Operator.TRAP) {
                UseTrap();
            }
            else {
                Idle();
            }

        }
	}

    // primitive tasks
    public void MoveAvatar( Vector3 destination) {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    public void UseTrap() {
        GetComponent<AvatarCharacter>().UseTrap();
    }

    public void Idle() {

    }


    public enum Operator {
        MOVE,
        TRAP,
        IDLE
    }
}
