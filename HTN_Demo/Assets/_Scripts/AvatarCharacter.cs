using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCharacter : MonoBehaviour {

    public Character characterType;

    [SerializeField]
    private int numOfItems;

    [SerializeField]
    private int numOfTraps;

	// Use this for initialization
	void Start () {
        numOfItems = 0;
        numOfTraps = 2;

        GameObject go = gameObject;
    }
    

    public int GetNumItems() {
        return numOfItems;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Item") {
            numOfItems++;
            Destroy(other.gameObject);
        }
    }

    public void UseTrap() {
        List<GameObject> gameobjects = new List<GameObject>();
        if (numOfTraps > 0) {
            numOfTraps--;
            if(characterType == Character.AI)
                gameobjects.Add(AvatarManager.GetInstance().GetPlayer());
            else
                gameobjects.Add(AvatarManager.GetInstance().GetAI());

            gameobjects.AddRange(EnemyManager.GetInstance().GetEnemies());

            double[] distance = new double[gameobjects.Count];
            for(int i = 0; i < gameobjects.Count; i++) {
                distance[i] = CalculateDistance(gameobjects[i].transform);
            }
            int indexMin = GetIndexMin(distance);
            if (indexMin == 0)
                AvatarManager.GetInstance().Teleport(gameobjects[indexMin].GetComponent<AvatarCharacter>());
            else {
                Debug.Log("Enemy Teloported");
                EnemyManager.GetInstance().TeleportEnemy(gameobjects[indexMin].GetComponent<Enemy>());

            }


        }
    }

    private double CalculateDistance(Transform pTransform) {
        double fx = Math.Abs(this.transform.position.x- pTransform.position.x);
        double fz = Math.Abs(this.transform.position.z - pTransform.position.z);

        double result = Math.Sqrt(Math.Pow(fx, 2) + Math.Pow(fz, 2));

        return result;
    }

    private int GetIndexMin(double[] array) {
        int i = 0; double min = 100;
        for (int j = 0; j < array.Length; j++){
            if(array[j] < min) {
                min = array[j];
                i = j;
            }
        }
        return i;
    }

    public int GetNumOfTraps() {
        return numOfTraps;
    }

    public enum Character {
        AI,
        Player
    }
}
