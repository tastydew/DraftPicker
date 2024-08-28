using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainSceneStartUp : MonoBehaviour
{
    [SerializeField] AudioSource  stadiumAudioSource;
    [SerializeField] AudioSource  whistleAudioSource;
    [SerializeField] private GameObject spaceButtonLabel;
    

    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.isGameReady)
            {
                GameManager.Instance.isGameStarted = true;
                whistleAudioSource.Play();
                stadiumAudioSource.Play();
                spaceButtonLabel.SetActive(false);
            }
        }

        if (GameManager.Instance.finalResults.Count == GameManager.Instance.numberOfPlayers)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
    }
}
