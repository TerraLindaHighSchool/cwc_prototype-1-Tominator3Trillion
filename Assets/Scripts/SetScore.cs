using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        //set score text to highscore player prefs
        scoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }


}
