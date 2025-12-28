using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }
    
    private const string LANGUAGE_KEY = "GameLanguage";
    private const string DEFAULT_LANGUAGE = "en";
    
    public string currentLanguage { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguagePreference();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void LoadLanguagePreference()
    {
        // Carrega o idioma salvo ou usa o padrão (en)
        currentLanguage = PlayerPrefs.GetString(LANGUAGE_KEY, DEFAULT_LANGUAGE);
        Debug.Log($"Idioma carregado: {currentLanguage}");
    }
    
    public void SetLanguage(string languageCode)
    {
        currentLanguage = languageCode;
        PlayerPrefs.SetString(LANGUAGE_KEY, languageCode);
        PlayerPrefs.Save();
        Debug.Log($"Idioma alterado para: {currentLanguage}");
    }
    
    public string GetDataFileName()
    {  
        // Retorna o nome do arquivo JSON baseado no idioma
        return $"Data-{currentLanguage}";
    }
}
