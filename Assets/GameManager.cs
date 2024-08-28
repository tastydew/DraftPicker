using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool isGameStarted;
    public bool playerCrossedFinishLine;
    public float startTimer = 5f;
    public int numberOfPlayers = 20;

    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float offset = 0.4f;
    [SerializeField] private Vector2 playerStartingPosition;

    [SerializeField] public List<string> finalResults;
    public bool isGameReady;
    public float playerMinimumSpeed;
    public float playerMaximumSpeed;
    public int speedChangeInterval;
    public string PlayerNames { get; set; }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this) _instance = null;
    }

    public void SetupGame()
    {
        var separatedPlayerNames = PlayerNames
            .Trim()
            .Replace(" ", "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries);

        numberOfPlayers = separatedPlayerNames.Length;

        scoreBoard.SetActive(false);

        //Stop Animation for Player and JerseyColors
        var playerAnimator = playerPrefab.GetComponent<Animator>();
        var jerseyAnimator = playerPrefab.transform.Find("JerseyColor").GetComponent<Animator>();
        playerAnimator.enabled = false;
        jerseyAnimator.enabled = false;

        for (var i = 0; i < numberOfPlayers; i++)
        {
            // Instantiate the player at the current spawn position
            var newPlayer = Instantiate(playerPrefab, playerStartingPosition, Quaternion.identity);
            var jerseyColor = newPlayer.transform.Find("JerseyColor").GetComponent<SpriteRenderer>();
            var playerName = newPlayer.transform.Find("PlayerName").GetComponent<TMP_Text>();

            playerName.text = separatedPlayerNames[i];
            var randomRed = Random.Range(0f, 1f);
            var randomGreen = Random.Range(0f, 1f);
            var randomBlue = Random.Range(0f, 1f);
            jerseyColor.color = new Color(randomRed, randomGreen, randomBlue, 1);

            // Update the spawn position for the next player based on the current player's position
            playerStartingPosition = new Vector3(playerStartingPosition.x, newPlayer.transform.position.y - offset, 0);
        }
    }

    public void ShowScoreBoard()
    {
        scoreBoard.SetActive(true);
    }
}