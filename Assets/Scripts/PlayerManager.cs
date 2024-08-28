using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    // Speed variables
    public float startingSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float varianceInterval;  // Time in seconds before speed changes
    private float _currentSpeed;

    private Animator _playerAnimator;
    private Animator _jerseyColorAnimator;
    private TMP_Text _playerName;
    private bool _crossedFinishLine = false;

    void Start()
    {
        _currentSpeed = minSpeed;
        _playerAnimator = GetComponent<Animator>();
        _jerseyColorAnimator = transform.Find("JerseyColor").GetComponent<Animator>();
        _playerName = transform.Find("PlayerName").GetComponent<TMP_Text>();
        minSpeed = GameManager.Instance.playerMinimumSpeed;
        maxSpeed = GameManager.Instance.playerMaximumSpeed;
        varianceInterval = GameManager.Instance.speedChangeInterval;
        StartCoroutine(ChangeSpeedVariance());
    }

    void Update()
    {
        // If a player has crossed the finish line, stop the player and their animations.
        if (_crossedFinishLine)
        {
            _playerAnimator.enabled = false;
            _jerseyColorAnimator.enabled = false;
            return;
        }
        
        if (GameManager.Instance.isGameStarted)
        {
            _playerAnimator.enabled = true;
            _jerseyColorAnimator.enabled = true;
            transform.Translate(Vector3.right * (_currentSpeed * Time.deltaTime));
        }

    }

    IEnumerator ChangeSpeedVariance()
    {
        while (true)
        {
            yield return new WaitForSeconds(varianceInterval);
            _currentSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ScoreBoardTrigger")
        {
            GameManager.Instance.ShowScoreBoard();
        }

        if (other.gameObject.name == "EndzoneTrigger" && !_crossedFinishLine)
        {
            _crossedFinishLine = true;
            GameManager.Instance.playerCrossedFinishLine = true;
            GameManager.Instance.finalResults.Add(_playerName.text);
        }
    }
}
