using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text playerItems;
    [SerializeField]
    private Text playerStatus;
    [SerializeField]
    private Text aiItems;
    [SerializeField]
    private Text aiStatus;
    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private AvatarManager avatarManager;
    // Use this for initialization
    void Start () {
        avatarManager = AvatarManager.GetInstance();
        while(avatarManager == null)
            avatarManager = AvatarManager.GetInstance();

    }
	
	// Update is called once per frame
	void Update () {
        CheckItems();
        CheckStatus();
        CheckGameStatus(GameManager.GetInstance().GetGameStatus());
    }

    void CheckItems() {
        playerItems.text = "Player Items: " + avatarManager.GetPlayerItems();
        aiItems.text = "AI Items: " + avatarManager.GetAiItems();
    }

    void CheckStatus() {
        if (avatarManager.IsPlayerAlive())
            playerStatus.text = "Player Status: Alive";
        else
            playerStatus.text = "Player Status: Dead";

        if (avatarManager.IsAiAlive())
            aiStatus.text = "AI Status: Alive";
        else
            aiStatus.text = "AI Status: Dead";
    }

    void CheckGameStatus(GameManager.GameStatus gameStatus) {
        if(gameStatus == GameManager.GameStatus.AIWIN) {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "AI won";
        }
        else if (gameStatus == GameManager.GameStatus.PLAYERWIN) {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Player won";
        }
        else if (gameStatus == GameManager.GameStatus.GAMEOVER) {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";
        }
        else {

        }
    }
    
}
