using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

    private static AvatarManager instance;

    public static AvatarManager GetInstance() {
        return instance;
    }

    [SerializeField]
    private List<GameObject> spawnPoints;

    [SerializeField]
    private GameObject PlayerAvatarPrefab;
    [SerializeField]
    private GameObject AIAvatarPrefab;


    private GameObject playerAvatar;
    private GameObject aiAvatar;

    

    private bool PlayerPlaying = false;
    private bool AIPlaying = false;


    private void Awake() {
        instance = this;
        
    }
    // Use this for initialization
    void Start () {
        SpawnAvatars();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnAvatars() {
        int r = Random.Range(0, spawnPoints.Count);
        int r2 = Random.Range(0, spawnPoints.Count);
        while(r == r2)
            r2 = Random.Range(0, spawnPoints.Count);

        playerAvatar = Instantiate(PlayerAvatarPrefab, spawnPoints[r].transform.position, spawnPoints[r].transform.rotation);
        aiAvatar = Instantiate(AIAvatarPrefab, spawnPoints[r2].transform.position, spawnPoints[r2].transform.rotation);

        PlayerPlaying = true;
        AIPlaying = true;

    }

    public void RemoveAvatar(AvatarCharacter a) {
        if (a.characterType == AvatarCharacter.Character.AI) {
            AIPlaying = false;
            aiAvatar.SetActive(false);
        }
        else {
            PlayerPlaying = false;
            playerAvatar.SetActive(false);
        }
        //Destroy(a.gameObject);

    }

    public void Teleport(AvatarCharacter avatarCharacter) {
        int r = Random.Range(0, spawnPoints.Count);
        avatarCharacter.gameObject.GetComponent<Rigidbody>().MovePosition(spawnPoints[r].transform.position);
    }


    public int GetPlayerItems() {
        return playerAvatar.GetComponent<AvatarCharacter>().GetNumItems();
    }
    public bool IsPlayerAlive() {
        return PlayerPlaying;
    }
    public int GetAiItems() {
        return aiAvatar.GetComponent<AvatarCharacter>().GetNumItems();
    }
    public bool IsAiAlive() {
        return AIPlaying;
    }

    public GameObject GetPlayer() {
        return playerAvatar;
    }
    public GameObject GetAI() {
        return aiAvatar;
    }

    void CheckGameEnd() {
        if(!AIPlaying && !PlayerPlaying) {
            // End Game
        }
    }

}
