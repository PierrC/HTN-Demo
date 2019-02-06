using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    public static EnemyManager GetInstance() {
        return instance;
    }

    // Locations
    [SerializeField]
    private List<GameObject> doors;

    // Prefabs
    [SerializeField]
    private GameObject enemy;

    private static List<GameObject> enemies;

    private void Awake() {
        if (instance == null)
            instance = this;
    }
    // Use this for initialization
    void Start () {
        enemies = new List<GameObject>();
        StartSpawn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartSpawn() {
        int r = Random.Range(0, 4);
        int r2 = Random.Range(0, 4);
        while (r == r2)
            r2 = Random.Range(0, 4);


        SpawnEnemy(r);
        SpawnEnemy(r2);
    }

    public void SpawnEnemy(int r) {
        GameObject e = Instantiate(enemy);
        e.GetComponent<Enemy>().direction = doors[r].GetComponent<DoorScript>().direction;
        e.transform.position = doors[r].transform.position;
        enemies.Add(e);

    }

    public void RemoveEnemy(Enemy pEnemy) {
        //Destroy(pEnemy.gameObject);

        StartCoroutine(RespawnEnemy(pEnemy));
    }

    IEnumerator RespawnEnemy(Enemy e) {
        e.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        e.gameObject.SetActive(true);
        int r = Random.Range(0, 3);
        e.GetComponent<Enemy>().direction = doors[r].GetComponent<DoorScript>().direction;
        e.GetComponent<Enemy>().canCapture = true;
        e.transform.position = doors[r].transform.position;
     //   SpawnEnemy(r);
    }

    public List<GameObject> GetEnemies() {
        return (new ReadOnlyCollection<GameObject>(enemies)).ToList();
    }

    public void TeleportEnemy(Enemy enemy) {
        Assert.IsTrue(enemies.Contains(enemy.gameObject));

        StartCoroutine(RespawnEnemy(enemy));
        // RespawnEnemy(enemy);
    }

    public enum Direction {
        RIGHT,
        LEFT
    }

}
