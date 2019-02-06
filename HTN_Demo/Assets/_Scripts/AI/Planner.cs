using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Planner : MonoBehaviour {

    [SerializeField]
    HTN HTN;
    AIController aIController;

    [SerializeField]
    float distanceFromEnemies;

    private void Start() {
        HTN = new HTN();
        aIController = GetComponent<AIController>();
    }

    private void Update() {
        DoSomething();
    }

    void DoSomething() {

        WorldStateManager.WorldState worldState = new WorldStateManager.WorldState();

        // bool b = worldState.currentAlcove == null;

        string s = HTN.Traverse().GetTaskName();
        Debug.Log("Task Chosen:" + s);
        if (s == "MoveToItem") {
            if (worldState.currentAlcove == null) {
                HTN.decompose.Add(HTN.Traverse());
            }
            else if (WorldStateManager.GetInstance().AlcoveHasItem(worldState.currentAlcove)) {

                Debug.Log("Alcove has item");
                // effect
                aIController.actions.Push(AIController.Operator.MOVE);
                aIController.destinations.Push(
                    worldState.currentAlcove.transform.GetChild(0).transform.position);
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());

            }
        }
        else if (s == "IdleAvoid") {
            if ((WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) < distanceFromEnemies) ||
                (WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) < distanceFromEnemies) &&
                worldState.currentLocation == WorldStateManager.CurrentLocation.ALCOVE) {


                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());
            }
        }
        else if (s == "MoveAvoid") {
            //Debug.Log("Distance:" + WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform));
            if ((WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) < distanceFromEnemies) ||
                (WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) < distanceFromEnemies) &&
                worldState.currentLocation == WorldStateManager.CurrentLocation.OPEN &&
                Vector3.Distance(WorldStateManager.GetInstance().GetClosestAlcove().transform.position, gameObject.transform.position) > 5.5f) {

                // effect
                aIController.actions.Push(AIController.Operator.MOVE);
                aIController.destinations.Push(
                    WorldStateManager.GetInstance().GetClosestAlcove().transform.position);
                worldState.currentAlcove = WorldStateManager.GetInstance().GetClosestAlcove();
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());
            }
        }
        else if (s == "UseTrap") {
            Debug.Log("Num of traps: " + gameObject.GetComponent<AvatarCharacter>().GetNumOfTraps());
            if ((WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) < distanceFromEnemies) ||
                (WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) &&
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) < distanceFromEnemies) &&
                worldState.currentLocation == WorldStateManager.CurrentLocation.OPEN &&
                Vector3.Distance(WorldStateManager.GetInstance().GetClosestAlcove().transform.position, gameObject.transform.position) <= 3.3f &&
                gameObject.GetComponent<AvatarCharacter>().GetNumOfTraps() > 0) {

                gameObject.GetComponent<AvatarCharacter>().UseTrap();
                Debug.Log("Trap used");
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());
            }
        }
        else if (s == "MoveToNext") {
            if (WorldStateManager.GetInstance().GetClockAlcove(worldState.currentAlcove) == null) {
                Debug.Log("WorldStateManager.GetInstance().GetClockAlcove(worldState.currentAlcove) is null");
            }
            else if (WorldStateManager.GetInstance().GetCounterClockAlcove(worldState.currentAlcove) == null) {
                Debug.Log("WorldStateManager.GetInstance().GetCounterClockAlcove(worldState.currentAlcove) is null");
            }
            //Debug.Log("Distance:" + WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform));
            else if ((!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) > distanceFromEnemies) &&
                (!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) > distanceFromEnemies) &&
                WorldStateManager.GetInstance().AlcoveHasItem(WorldStateManager.GetInstance().GetClockAlcove(worldState.currentAlcove))) {

                aIController.actions.Push(AIController.Operator.MOVE);
                aIController.destinations.Push(
                    WorldStateManager.GetInstance().GetClockAlcove(worldState.currentAlcove).transform.position);
                worldState.currentAlcove = WorldStateManager.GetInstance().GetClockAlcove(worldState.currentAlcove);
                HTN.ClearDecompsedList();
            }
            else if ((!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) > 10) &&
                (!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) > 10) &&
                WorldStateManager.GetInstance().AlcoveHasItem(WorldStateManager.GetInstance().GetCounterClockAlcove(worldState.currentAlcove))) {

                aIController.actions.Push(AIController.Operator.MOVE);
                aIController.destinations.Push(
                    WorldStateManager.GetInstance().GetCounterClockAlcove(worldState.currentAlcove).transform.position);

                worldState.currentAlcove = WorldStateManager.GetInstance().GetCounterClockAlcove(worldState.currentAlcove);
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());

            }
        }
        else if (s == "MoveToClosest") {
            if ((!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[0], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[0].transform) > 10) &&
                (!WorldStateManager.GetInstance().SameSide(EnemyManager.GetInstance().GetEnemies()[1], gameObject) ||
                WorldStateManager.GetInstance().CalculateDistance(EnemyManager.GetInstance().GetEnemies()[1].transform) > 10)) {

                aIController.actions.Push(AIController.Operator.MOVE);
                aIController.destinations.Push(
                    WorldStateManager.GetInstance().GetClosestAlcoveWithObject(worldState.currentAlcove.gameObject).transform.position);
                
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());
            }
        }
        else {

            if (HTN.Traverse().GetTaskName() == "AI") {
                HTN.ClearDecompsedList();
            }
            else {
                HTN.decompose.Add(HTN.Traverse());

            }


        }
    }



}
