using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour {

    private static WorldStateManager instance;
    [SerializeField]
    private List<Alcove> alcoves;
    

    public static WorldStateManager GetInstance() {
        return instance;
    }
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this);
    }
    // Use this for initialization
    void Start() {
        

        alcoves = new List<Alcove>();
        foreach(Alcove alcove in HolderScript.GetInstance().alcoves) {
            alcoves.Add(alcove);
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public GameObject GetItem(Alcove alcove) {
        if (alcove.transform.childCount == 2) {
            return alcove.transform.GetChild(0).gameObject;
        }
        return null;
    }

    #region Alcoves Methods
    public Alcove GetClosestAlcove() {
        double[] distance = new double[alcoves.Count];
        for (int i = 0; i < alcoves.Count; i++) {
            distance[i] = CalculateDistance(alcoves[i].transform);
        }
        int indexMin = GetIndexMin(distance);
        return alcoves[indexMin];
    }


    public Alcove GetClosestAlcoveWithObject(GameObject source) {
        double[] distance = new double[10] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        for (int i = 0; i < alcoves.Count; i++) {
            if(AlcoveHasItem(alcoves[i]))
                distance[i] = CalculateDistance(alcoves[i].transform);
        }
        int indexMin = GetIndexMin(distance);
        return alcoves[indexMin];
    }

    public Alcove GetClosestRightAlcove(GameObject gameObject) {
        double[] distance = new double[alcoves.Count];
        for (int i = 0; i < alcoves.Count; i++) {
            if (gameObject.transform.position.x < alcoves[i].transform.position.x)
                distance[i] = CalculateDistance(alcoves[i].transform);
        }
        int indexMin = GetIndexMin(distance);
        return alcoves[indexMin];
    }

    public Alcove GetClosestLeftAlcove(GameObject gameObject) {
        double[] distance = new double[alcoves.Count];
        for (int i = 0; i < alcoves.Count; i++) {
            if (gameObject.transform.position.x > alcoves[i].transform.position.x)
                distance[i] = CalculateDistance(alcoves[i].transform);
        }
        int indexMin = GetIndexMin(distance);
        return alcoves[indexMin];
    }

    public GameObject CHOSENONE;
    public bool AlcoveHasItem(Alcove alcove) {
        //    CHOSENONE = alcove;
        //  Debug.Log("worldState alcove = " + b);
        //   Debug.Log("AlcoveHasItem: transform.childCount " + alcove.transform.childCount);
        if(alcove == null)
            Debug.Log("alcove is null");
        if (alcove.transform == null)
            Debug.Log("alcove transform is null");
        if (alcove.transform.childCount == 0)
            Debug.Log("alcove transform.childCount is null");


        if (alcove.transform.childCount > 1) {
            return true;
        }
        return false;
    }

    public Alcove GetClockAlcove(Alcove alcove) {
        int i = -1;
        for (int j = 0; j < alcoves.Count; j++) {
            if (alcove == alcoves[j]) {
                i = j;
                break;
            }
        }
        switch (i) {
            case 0: return alcoves[5];
            case 1: return alcoves[0];
            case 2: return alcoves[1];
            case 3: return alcoves[2];
            case 4: return alcoves[3];
            case 5: return alcoves[6];
            case 6: return alcoves[7];
            case 7: return alcoves[8];
            case 8: return alcoves[9];
            case 9: return alcoves[4];
            default:
                return alcove;
        }
    }
    public Alcove GetCounterClockAlcove(Alcove alcove) {
        int i = -1;
        for (int j = 0; j < alcoves.Count; j++) {
            if (alcove == alcoves[j]) {
                i = j;
                break;
            }
        }
        switch (i) {
            case 0: return alcoves[1];
            case 1: return alcoves[2];
            case 2: return alcoves[3];
            case 3: return alcoves[4];
            case 4: return alcoves[9];
            case 5: return alcoves[0];
            case 6: return alcoves[5];
            case 7: return alcoves[6];
            case 8: return alcoves[7];
            case 9: return alcoves[8];
            default:
                return alcove;
        }
    }

    private Alcove GetOppositeAlcove(Alcove alcove) {
        int i = -1;
        for (int j = 0; j < alcoves.Count; j++) {
            if (alcove == alcoves[j]) {
                i = j;
                break;
            }
        }
        if (i <= 4) {
            return alcoves[i + 5];
        }
        else {
            return alcoves[i - 5];
        }
    }

    public Alcove GetCurrentAlcove() {
        return GetClosestAlcove();
    }
    #endregion

    public bool IsEnemyApproching(GameObject source, Enemy enemy) {
        if (enemy.gameObject.transform.position.x > source.transform.position.x
            && enemy.direction == EnemyManager.Direction.LEFT) {
            return true;
        }
        else if (enemy.gameObject.transform.position.x < source.transform.position.x
            && enemy.direction == EnemyManager.Direction.RIGHT) {
            return true;
        }
        return false;
    }

    public bool IsPlayerClosest(GameObject source) {
        GameObject gameObject = GetClosestAgent(source);
        AvatarCharacter a = gameObject.GetComponent<AvatarCharacter>();
        if (a == null) {
            return false;
        }
        return true;
    }

    public GameObject GetClosestAgent(GameObject source) {
        List<GameObject> gameobjects = new List<GameObject>();
        gameobjects.Add(AvatarManager.GetInstance().GetPlayer());
        gameobjects.AddRange(EnemyManager.GetInstance().GetEnemies());

        double[] distance = new double[gameobjects.Count];
        for (int i = 0; i < gameobjects.Count; i++) {
            distance[i] = CalculateDistance(gameobjects[i].transform);
        }
        int indexMin = GetIndexMin(distance);

        return gameobjects[indexMin];

    }

    public bool IsInOpen(GameObject gameObject) {
        if (gameObject.transform.position.z < -1 && gameObject.transform.position.z > -9) {
            return true;
        }
        else if (gameObject.transform.position.z > 1 && gameObject.transform.position.z < 9) {
            return true;
        }
        return false;
    }

    public bool SameSide(GameObject go1, GameObject go2) {
        if (GetSide(go1) == GetSide(go2))
            return true;

        return false;
    }

    public PlatformSide GetSide(GameObject go) {
        if (go.transform.position.z > 0) {
            return PlatformSide.UP;
        }
        else {
            return PlatformSide.DOWN;
        }
    }

    public PlatformSide GetOwnSide() {
        if (transform.position.z > 0) {
            return PlatformSide.UP;
        }
        else {
            return PlatformSide.DOWN;
        }
    }

    #region Helper methods
    private int GetIndexMin(double[] array) {
        int i = 0; double min = 100;
        for (int j = 0; j < array.Length; j++) {
            if (array[j] < min) {
                min = array[j];
                i = j;
            }
        }
        return i;
    }

    public double CalculateDistance(Transform pTransform) {
        double fx = Math.Abs(this.transform.position.x - pTransform.position.x);
        double fz = Math.Abs(this.transform.position.z - pTransform.position.z);

        double result = Math.Sqrt(Math.Pow(fx, 2) + Math.Pow(fz, 2));

        return result;
    }
    #endregion

    public enum PlatformSide {
        UP,
        DOWN
    }

    public enum CurrentLocation {
        ALCOVE,
        OPEN
    }

    public class WorldState {

        public CurrentLocation currentLocation;

        public PlatformSide platformSide;

        public Alcove currentAlcove;



        public WorldState() {

            platformSide = GetInstance().GetOwnSide();
            if (GetInstance().IsInOpen(GetInstance().gameObject)) {
                currentLocation = CurrentLocation.OPEN;
                currentAlcove = null;
            }
            else {
                currentLocation = CurrentLocation.ALCOVE;
                currentAlcove = GetInstance().GetCurrentAlcove();
            }


        }

    }
}
