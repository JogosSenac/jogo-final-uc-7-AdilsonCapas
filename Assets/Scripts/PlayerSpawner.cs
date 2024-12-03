using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPorta;  // Local de spawn ao entrar pela porta
    public Transform spawnChave; // Local de spawn ao entrar com a chave
    public GameObject jogador;   // Referência ao jogador

    void Start()
    {
        // Verifica a origem e posiciona o jogador
        switch (GameStateManager.origemTransicao)
        {
            case "Porta":
                jogador.transform.position = spawnPorta.position;
                break;
            case "Chave1":
                jogador.transform.position = spawnChave.position;
                break;
            default:
                Debug.LogWarning("Origem da transição não definida. Usando posição inicial.");
                break;
        }
    }
}