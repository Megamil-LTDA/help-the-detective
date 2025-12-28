using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerGame : MonoBehaviour
{
    [Header("Game Panel")]
    public GameObject gamePanel; // Painel principal do jogo (esconde quando mostra o modal)
    
    [Header("Suspect Photo A (Left)")]
    public GameObject photo_a;
    public TextMeshProUGUI photo_a_name;
    public Image photo_a_image;

    [Header("Suspect Photo B (Right)")]
    public GameObject photo_b;
    public TextMeshProUGUI photo_b_name;
    public Image photo_b_image;

    [Header("UI Elements")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI text_details;
    public TextMeshProUGUI text_hud;
    
    [Header("Clue Panels")]
    public GameObject textCluePanel;
    public TextMeshProUGUI textClueContent;
    public GameObject imageCluePanel;
    public Image imageClueImage;
    
    [Header("Clue Buttons")]
    public Button textClueButton;
    public Button imageClueButton;
    
    [Header("Result Modal")]
    public ResultModal resultModal;
    
    // Estado do jogo atual
    private Case currentCase;
    private Suspect suspectA; // Suspeito na posição A (esquerda)
    private Suspect suspectB; // Suspeito na posição B (direita)
    
    private int cluesUsed = 0;
    private bool textClueUsed = false;
    private bool imageClueUsed = false;
    
    private void Start()
    {
        // Cria o GameManager automaticamente se não existir
        if (GameManager.Instance == null)
        {
            GameObject gmObject = new GameObject("GameManager");
            GameManager gm = gmObject.AddComponent<GameManager>();
            
            // Carrega o JSON baseado no idioma selecionado
            gm.LoadGameDataFromLanguage();
        }
        
        resultModal.gameObject.SetActive(true);

        LoadCurrentCase();
        HideClues();
    }
    
    private void LoadCurrentCase()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager não encontrado!");
            return;
        }
        
        currentCase = GameManager.Instance.GetCurrentCase();
        
        if (currentCase == null)
        {
            Debug.LogError("Nenhum caso disponível!");
            return;
        }
        
        // Embaralha a posição dos suspeitos (50% de chance de inverter)
        bool shufflePositions = Random.value > 0.5f;
        
        if (shufflePositions)
        {
            // Inverte as posições
            suspectA = currentCase.suspects.suspect_b;
            suspectB = currentCase.suspects.suspect_a;
        }
        else
        {
            // Mantém a ordem original
            suspectA = currentCase.suspects.suspect_a;
            suspectB = currentCase.suspects.suspect_b;
        }
        
        SetupUI();
        ResetClues();
    }
    
    private void SetupUI()
    {
        // Título e descrição do caso
        title.text = currentCase.title;
        text_details.text = currentCase.description;
        
        // Informações dos suspeitos
        photo_a_name.text = suspectA.name;
        photo_b_name.text = suspectB.name;
        
        // Carrega as imagens dos suspeitos
        LoadSuspectImage(suspectA.portrait, photo_a_image);
        LoadSuspectImage(suspectB.portrait, photo_b_image);
        
        // Atualiza HUD
        UpdateHUD();
    }
    
    private void LoadSuspectImage(string imageName, Image targetImage)
    {
        // Tenta carregar a imagem da pasta Resources
        string path = "Imagens/suspects/" + imageName.Replace(".png", "");
        Sprite sprite = Resources.Load<Sprite>(path);
        
        if (sprite != null)
        {
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Imagem não encontrada: {path}");
        }
    }
    
    private void UpdateHUD()
    {
        int totalTurns = GameManager.Instance.GetTotalCases();
        text_hud.text = $"Turno: {GameManager.Instance.currentTurn}/{totalTurns}\nPontos: {GameManager.Instance.totalPoints}";
    }
    
    private void ResetClues()
    {
        cluesUsed = 0;
        textClueUsed = false;
        imageClueUsed = false;
        
        // Habilita os botões de dica
        if (textClueButton != null) textClueButton.interactable = true;
        if (imageClueButton != null) imageClueButton.interactable = true;
    }
    
    private void HideClues()
    {
        if (textCluePanel != null) textCluePanel.SetActive(false);
        if (imageCluePanel != null) imageCluePanel.SetActive(false);
    }
    
    public void SelectPhotoA()
    {
        CheckAnswer(suspectA.id);
    }

    public void SelectPhotoB()
    {
        CheckAnswer(suspectB.id);
    }
    
    private void CheckAnswer(string selectedSuspectId)
    {
        bool isCorrect = selectedSuspectId == currentCase.solution.culprit_id;
        int earnedPoints = 0;
        
        if (isCorrect)
        {
            // Calcula pontos: pontos base - penalidades por dicas
            int penalty = cluesUsed * currentCase.score.penalty_per_clue_used;
            earnedPoints = Mathf.Max(0, currentCase.score.points_on_correct - penalty);
            GameManager.Instance.AddPoints(earnedPoints);
        }
        
        // Atualiza HUD
        UpdateHUD();
        
        // Esconde o painel do jogo e mostra o modal
        ShowModal();
        
        // Mostra resultado
        if (resultModal != null)
        {
            resultModal.ShowResult(isCorrect, currentCase.solution.explanation, OnResultContinue);
            resultModal.SetPoints(earnedPoints);
        }
        else
        {
            // Fallback para Debug.Log se não houver modal
            string resultMessage = currentCase.solution.explanation;
            if (isCorrect)
            {
                resultMessage += $"\n\n+{earnedPoints} pontos!";
            }
            Debug.Log($"{(isCorrect ? "CORRETO!" : "ERRADO!")} - {resultMessage}");
            OnResultContinue();
        }
    }
    
    private void OnResultContinue()
    {
        // Fecha o modal e mostra o painel do jogo novamente
        CloseModal();
        
        if (GameManager.Instance.HasNextCase())
        {
            // Próximo caso
            GameManager.Instance.NextCase();
            LoadCurrentCase();
            HideClues();
        }
        else
        {
            // Fim do jogo
            SceneManager.LoadScene("EndGame");
        }
    }
    
    public void UseTextClue()
    {
        if (textClueUsed)
        {
            Debug.Log("Dica de texto já foi usada!");
            return;
        }
        
        textClueUsed = true;
        cluesUsed++;
        
        Debug.Log("Usando dica de texto...");
        
        // Esconde o painel do jogo e mostra a dica
        if (gamePanel != null)
        {
            gamePanel.SetActive(false);
            Debug.Log("Painel do jogo escondido");
        }
        
        if (textCluePanel != null && textClueContent != null)
        {
            textClueContent.text = currentCase.clues.text_clue.content;
            textCluePanel.SetActive(true);
            Debug.Log($"Painel de texto ativado. Ativo: {textCluePanel.activeSelf}");
        }
        else
        {
            Debug.LogWarning("textCluePanel ou textClueContent está null!");
        }
        
        // Desabilita o botão
        if (textClueButton != null) textClueButton.interactable = false;
        
        UpdateHUD();
    }
    
    private void ShowModal()
    {
        if (gamePanel != null)
        {
            gamePanel.SetActive(false);
        }
    }
    
    public void CloseModal()
    {
        if (gamePanel != null)
        {
            gamePanel.SetActive(true);
        }
    }
    
    public void UseImageClue()
    {
        if (imageClueUsed) return;
        
        imageClueUsed = true;
        cluesUsed++;
        
        Debug.Log("Usando dica de imagem...");
        
        // Esconde o painel do jogo
        if (gamePanel != null)
        {
            gamePanel.SetActive(false);
            Debug.Log("Painel do jogo escondido");
        }
        
        // Ativa o painel da dica SEMPRE, mesmo sem imagem
        if (imageCluePanel != null)
        {
            imageCluePanel.SetActive(true);
            Debug.Log($"Painel de imagem ativado. Ativo: {imageCluePanel.activeSelf}");
        }
        
        // Tenta carregar a imagem
        if (imageClueImage != null)
        {
            string path = "Imagens/clues/" + currentCase.clues.image_clue.image.Replace(".png", "");
            Sprite sprite = Resources.Load<Sprite>(path);
            
            if (sprite != null)
            {
                imageClueImage.sprite = sprite;
                Debug.Log("Imagem de dica carregada com sucesso");
            }
            else
            {
                Debug.LogWarning($"Imagem de dica não encontrada: {path}");
            }
        }
        
        // Desabilita o botão
        if (imageClueButton != null) imageClueButton.interactable = false;
        
        UpdateHUD();
    }

    public void CloseCluePanel()
    {
        // Fecha os painéis de dica e volta ao painel do jogo
        if (gamePanel != null)
        {
            gamePanel.SetActive(true);
        }
        
        if (textCluePanel != null)
        {
            textCluePanel.SetActive(false);
        }
        
        if (imageCluePanel != null)
        {
            imageCluePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Verifica se a tecla Espaço foi pressionada
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Se o modal de resultado estiver ativo, fecha ele e continua o jogo
            if (resultModal != null && resultModal.gameObject.activeSelf)
            {
                resultModal.HideModal();
                OnResultContinue();
            }
            // Senão, fecha os painéis de dica
            else
            {
                CloseCluePanel();
            }
        }
    }

}
