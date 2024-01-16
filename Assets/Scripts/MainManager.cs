using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    private const string playerFile = "/settings.json";
    private const int numOfScores = 5;

    [System.Serializable]
    public struct HighScoreData
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    private class PlayerData
    {
        public string playerName;
        public HighScoreData[] highscores = new HighScoreData[numOfScores];
    }

    public static MainManager Instance;
    public string PlayerName { get; set; }
    private HighScoreData[] HighScores;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitHighscores();
        LoadSettings();
    }

    public void AddScore(int score)
    {
        int scorePos = GetHighscorePosition(score);

        if (scorePos >= numOfScores)
        {
            return;
        }

        HighScoreData newHighscore = new HighScoreData();
        newHighscore.playerName = PlayerName;
        newHighscore.score = score;

        var highscoreList = new List<HighScoreData>(HighScores);
        highscoreList.Insert(scorePos, newHighscore);
        highscoreList.RemoveAt(numOfScores);
        HighScores = highscoreList.ToArray();
    }

    private int GetHighscorePosition(int score)
    {
        for (int n = 0; n < numOfScores; n++)
        {
            if (score > HighScores[n].score)
            {
                return n;
            }
        }

        return numOfScores;
    }

    public HighScoreData[] GetHighScore()
    {
        return HighScores;
    }

    public void SaveSettings()
    {
        PlayerData data = new PlayerData();
        data.playerName = PlayerName;

        for (int n = 0; n < numOfScores; n++)
        {
            data.highscores[n] = HighScores[n];
        }

        string file = Application.persistentDataPath + playerFile;
        File.WriteAllText(file, JsonUtility.ToJson(data));
    }

    private void LoadSettings()
    {
        string file = Application.persistentDataPath + playerFile;

        if (File.Exists(file))
        {
            string json = File.ReadAllText(file);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            PlayerName = data.playerName;

            for (int n = 0; n < data.highscores.Length && n < numOfScores; n++)
            {
                HighScores[n] = data.highscores[n];
            }
        }
    }

    private void InitHighscores()
    {
        HighScores = new HighScoreData[numOfScores];
        for (int n = 0; n < numOfScores; n++)
        {
            HighScores[n] = new HighScoreData();
        }
    }
}
