using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public TextAsset jsonData;
    
    private GameData gameData;
    private List<Case> shuffledCases;
    private int currentCaseIndex = 0;
    
    public int totalPoints = 0;
    public int currentTurn = 1;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Só carrega os dados se o jsonData já foi atribuído no Inspector
            if (jsonData != null)
            {
                LoadGameData();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetJsonData(TextAsset json)
    {
        jsonData = json;
        LoadGameData();
    }
    
    public void LoadGameDataFromLanguage()
    {
        // Cria o LanguageManager se não existir
        if (LanguageManager.Instance == null)
        {
            GameObject lmObject = new GameObject("LanguageManager");
            lmObject.AddComponent<LanguageManager>();
        }
        
        // Carrega o arquivo JSON baseado no idioma
        string fileName = LanguageManager.Instance.GetDataFileName();
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        
        if (jsonFile != null)
        {
            jsonData = jsonFile;
            lastLoadedLanguage = LanguageManager.Instance.currentLanguage;
            LoadGameData();
            Debug.Log($"Dados carregados do arquivo: {fileName}.json (idioma: {lastLoadedLanguage})");
        }
        else
        {
            Debug.LogError($"Arquivo {fileName}.json não encontrado em Resources!");
        }
    }
    
    private void LoadGameData()
    {
        if (jsonData != null)
        {
            gameData = JsonUtility.FromJson<GameData>(jsonData.text);
            ShuffleCases();
        }
        else
        {
            Debug.LogError("JSON Data não foi atribuído no GameManager!");
        }
    }
    
    private void ShuffleCases()
    {
        // Embaralha a ordem dos casos
        shuffledCases = gameData.cases.OrderBy(x => Random.value).ToList();
        Debug.Log($"Casos embaralhados: {shuffledCases.Count} casos carregados");
    }
    
    public Case GetCurrentCase()
    {
        if (shuffledCases != null && currentCaseIndex < shuffledCases.Count)
        {
            return shuffledCases[currentCaseIndex];
        }
        return null;
    }
    
    public int GetTotalCases()
    {
        return shuffledCases != null ? shuffledCases.Count : 0;
    }
    
    public bool HasNextCase()
    {
        return currentCaseIndex < shuffledCases.Count - 1;
    }
    
    public void NextCase()
    {
        currentCaseIndex++;
        currentTurn++;
    }
    
    public void AddPoints(int points)
    {
        totalPoints += points;
    }
    
    private string lastLoadedLanguage = "";
    
    public int GetMaxPossiblePoints()
    {
        int maxPoints = 0;
        if (shuffledCases != null)
        {
            foreach (Case c in shuffledCases)
            {
                maxPoints += c.score.points_on_correct;
            }
        }
        return maxPoints;
    }
    
    public void ResetGame()
    {
        currentCaseIndex = 0;
        currentTurn = 1;
        totalPoints = 0;
        
        // Verifica se o idioma mudou e recarrega os dados
        if (LanguageManager.Instance != null)
        {
            string currentLanguage = LanguageManager.Instance.currentLanguage;
            if (currentLanguage != lastLoadedLanguage)
            {
                Debug.Log($"Idioma mudou de '{lastLoadedLanguage}' para '{currentLanguage}'. Recarregando dados...");
                LoadGameDataFromLanguage();
            }
            else
            {
                // Mesmo idioma, apenas embaralha novamente
                ShuffleCases();
            }
        }
        else
        {
            ShuffleCases();
        }
    }
}
