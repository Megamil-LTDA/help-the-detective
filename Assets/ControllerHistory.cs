using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ControllerHistory : MonoBehaviour
{
    public TextMeshProUGUI historyText;

    void Start()
    {
        // Cria o LanguageManager se não existir
        if (LanguageManager.Instance == null)
        {
            GameObject lmObject = new GameObject("LanguageManager");
            lmObject.AddComponent<LanguageManager>();
        }
        
        // Define o texto baseado no idioma
        string currentLanguage = LanguageManager.Instance.currentLanguage;
        
        if (currentLanguage == "pt")
        {
            // Texto em Português
            historyText.text = "Você é um detetive no início de sua carreira, tentando entrar em um campo altamente restrito de investigação privada.\n\n" +
                "Antes de ser aceito, você recebe um desafio: resolver uma série de casos antigos e não resolvidos, arquivados por anos porque foram considerados inconclusivos ou enganosos. Casos onde as evidências parecem apontar para uma verdade óbvia — mas está errada.\n\n" +
                "Cada caso testa sua capacidade de olhar além das aparências, entender o contexto e questionar conclusões fáceis.\n\n" +
                "Se você tiver sucesso, provará que tem o que é preciso para se juntar à organização — e ascender para liderar a maior empresa de investigação privada do mundo.";
        }
        else
        {
            // Texto em Inglês (padrão)
            historyText.text = "You are a detective at the beginning of your career, trying to enter a highly restricted field of private investigation.\n\n" +
                "Before being accepted, you are given a challenge: solve a series of old, unsolved cases, archived for years because they were considered inconclusive or misleading. Cases where the evidence seems to point to an obvious truth — but is wrong.\n\n" +
                "Each case tests your ability to look beyond appearances, understand context, and question easy conclusions.\n\n" +
                "If you succeed, you will prove that you have what it takes to join the organization — and rise to lead the largest private investigation company in the world.";
        }
    }   

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla Espaço foi pressionada usando o novo Input System
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Carrega a cena "Game"
            SceneManager.LoadScene("Game");
        }
    }
}
