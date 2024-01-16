using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text[] highscoreNames;
    public TMP_Text[] highscoreScores;

    public void Start()
    {
        inputField.text = MainManager.Instance.PlayerName;

        MainManager.HighScoreData[] highScores = MainManager.Instance.GetHighScore();

        for (int n = 0; n < highScores.Length; n++)
        {
            highscoreScores[n].text = $"{highScores[n].score}";

            if (highScores[n].playerName.Length != 0)
            {
                highscoreNames[n].text = highScores[n].playerName;
            }
            else
            {
                highscoreNames[n].text = "Empty";
                highscoreNames[n].fontStyle = FontStyles.Italic;
            }
        }
    }

    public void Play()
    {
        MainManager.Instance.PlayerName = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.PlayerName = inputField.text;
        MainManager.Instance.SaveSettings();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
