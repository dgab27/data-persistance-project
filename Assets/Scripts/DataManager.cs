using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] public string currentPlayerName;
    [SerializeField] public string savedPlayerName;
    [SerializeField] public int savedBestScore;
    

    //public string userName;
    public TMP_InputField userInputField;
    public TMP_Text menuScore;

    [System.Serializable]
    class SaveData
    {
        public string savedPlayerName;
        public int savedBestScore;
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //LoadName();
        //LoadScore();
        //Debug.Log(LoadScore());
        LoadFile();
        userInputField.text = savedPlayerName;
        currentPlayerName = userInputField.text;
        menuScore.text = "Score: " + savedPlayerName + ": " + savedBestScore;

    }

    public void SaveFile()
    {
        SaveData data = new SaveData();
        data.savedPlayerName = currentPlayerName;
        data.savedBestScore = MainManager.Instance.m_Points;

        string jsonData = JsonUtility.ToJson(data);
;       File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
            savedBestScore = data.savedBestScore;
            savedPlayerName = data.savedPlayerName;
        }
    }

    /*public void SaveName()
    {
        SaveData data = new SaveData();
        data.savedPlayerName = currentPlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
    }

    public void SaveScore()
    {
        //savedBestScore = score;
        SaveData data = new SaveData();
        data.savedBestScore = MainManager.Instance.m_Points;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
    }

    public string LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedPlayerName = data.savedPlayerName;
        }
        return savedPlayerName;
    }

    public int LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedBestScore = data.savedBestScore;
        }
        return savedBestScore;
    }
    */


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        //SaveName();
        SaveFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    public void SetCurrentName()
    {
        currentPlayerName = userInputField.text;
        Debug.Log("player name is: " + currentPlayerName);
        
    }

}
