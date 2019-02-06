using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private AvatarManager avatarManager;
    private EnemyManager enemyManager;

    private GameStatus gameStatus;

    public GameStatus GetGameStatus() {
        return gameStatus;
    }

    public static GameManager GetInstance() {
        return instance;
    }

	// Use this for initialization
	void Start () {
        instance = this;
        avatarManager = GetComponent<AvatarManager>();
        enemyManager = GetComponent<EnemyManager>();
        gameStatus = GameStatus.RUNNING;
    }
	
	// Update is called once per frame
	void Update () {

        CheckGameStatus();
	}

    void CheckGameStatus() {
        if (!avatarManager.IsAiAlive() && !avatarManager.IsPlayerAlive()) {
            gameStatus = GameStatus.GAMEOVER;
            if (avatarManager.GetAiItems() > avatarManager.GetPlayerItems()) {
                gameStatus = GameStatus.AIWIN;
                StartCoroutine(BackToMenu());
            }
            else if (avatarManager.GetAiItems() < avatarManager.GetPlayerItems()) {
                gameStatus = GameStatus.PLAYERWIN;
                StartCoroutine(BackToMenu());
            }
        }
        if (avatarManager.GetAiItems() == 5) {
            gameStatus = GameStatus.AIWIN;
            StartCoroutine(BackToMenu());
        }
        else if (avatarManager.GetPlayerItems() == 5) {
            gameStatus = GameStatus.PLAYERWIN;
            StartCoroutine(BackToMenu());
        }
    }

    IEnumerator BackToMenu() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    public enum GameStatus {
        RUNNING,
        GAMEOVER,
        AIWIN,
        PLAYERWIN,
        MENU
    }
}
