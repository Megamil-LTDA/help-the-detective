using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultModal : MonoBehaviour
{
    [Header("Modal Panel")]
    public GameObject modalPanel;
    
    [Header("Text Components")]
    public TextMeshProUGUI titleText;           // "CORRETO!" ou "ERRADO!"
    public TextMeshProUGUI explanationText;     // Explicação do caso
    public TextMeshProUGUI pointsText;          // "+X pontos!" (só aparece se acertar)
    
    [Header("Button")]
    public Button continueButton;
    
    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
        }
        HideModal();
    }
    
    public void ShowResult(bool isCorrect, string explanation, System.Action onContinue)
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(true);
        }
        
        // Define o título
        if (titleText != null)
        {
            if (isCorrect)
            {
                titleText.text = "CORRETO!";
                titleText.color = new Color(0f, 0.8f, 0f); // Verde
            }
            else
            {
                titleText.text = "ERRADO!";
                titleText.color = new Color(1f, 0.2f, 0.2f); // Vermelho
            }
        }
        
        // Define a explicação
        if (explanationText != null)
        {
            explanationText.text = explanation;
        }
        
        // Configuração do botão
        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                HideModal();
                onContinue?.Invoke();
            });
        }
    }
    
    public void SetPoints(int points)
    {
        if (points > 0)
        {
            pointsText.gameObject.SetActive(true);
            pointsText.text = $"+{points} pontos!";
            pointsText.color = new Color(1f, 0.84f, 0f); // Cor dourada
        }
        else
        {
            pointsText.gameObject.SetActive(false);
        }
    }
    
    public void HideModal()
    {
        if (modalPanel != null)
        {
            modalPanel.SetActive(false);
        }
    }
    
    private void OnContinueClicked()
    {
        HideModal();
    }
}
