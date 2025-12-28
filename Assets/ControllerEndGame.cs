using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ControllerEndGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI titleMessage;
    public TextMeshProUGUI pointsText;
    public Image background;
    
    [Header("Victory Assets")]
    public Sprite bannerWinner;  // Assets/Imagens/banner-winner.png
    
    [Header("Defeat Assets")]
    public Sprite bannerLoser;   // Assets/Imagens/banner-loser.png
    
    [Header("Settings")]
    [Range(0f, 1f)]
    public float winThreshold = 0.7f; // 70% para ganhar
    
    private void Start()
    {
        DisplayResults();
    }
    
    private void DisplayResults()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager não encontrado!");
            return;
        }
        
        int earnedPoints = GameManager.Instance.totalPoints;
        int maxPoints = GameManager.Instance.GetMaxPossiblePoints();
        
        // Calcula a porcentagem
        float percentage = maxPoints > 0 ? (float)earnedPoints / maxPoints : 0f;
        
        // Define se ganhou ou perdeu
        bool hasWon = percentage >= winThreshold;
        
        if (hasWon)
        {
            // VITÓRIA - Mais de 70%
            if (titleMessage != null)
            {
                titleMessage.text = "Welcome aboard, Detective.";
                titleMessage.color = new Color(0f, 0.8f, 0f); // Verde
            }
            
            if (pointsText != null)
            {
                pointsText.text = $"{earnedPoints}/{maxPoints}";
                pointsText.color = Color.white; // Cor normal
            }
            
            if (background != null && bannerWinner != null)
            {
                background.sprite = bannerWinner;
            }
        }
        else
        {
            // DERROTA - Menos de 70%
            if (titleMessage != null)
            {
                titleMessage.text = "You failed to solve the case.";
                titleMessage.color = new Color(1f, 0.2f, 0.2f); // Vermelho
            }
            
            if (pointsText != null)
            {
                pointsText.text = $"{earnedPoints}/{maxPoints}";
                pointsText.color = new Color(1f, 0.2f, 0.2f); // Vermelho
            }
            
            if (background != null && bannerLoser != null)
            {
                background.sprite = bannerLoser;
            }
        }
        
        // Log para debug
        Debug.Log($"Resultado: {(hasWon ? "VITÓRIA" : "DERROTA")} - {earnedPoints}/{maxPoints} ({percentage * 100:F1}%)");
    }
    
    private void Update()
    {
        // Verifica se a tecla Espaço foi pressionada usando o novo Input System
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Reseta o jogo e volta para Home
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResetGame();
            }
            SceneManager.LoadScene("Home");
        }
    }
}
