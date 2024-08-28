using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResultListHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private TMP_Text finalResults;
    void Start()
    {
        finalResults = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerCrossedFinishLine)
        {
            StringBuilder sb = new StringBuilder();

            // Iterate over the list with index
            for (int i = 0; i < GameManager.Instance.finalResults.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {GameManager.Instance.finalResults[i]}");
            }

            finalResults.text = sb.ToString();

        }
    }
}
