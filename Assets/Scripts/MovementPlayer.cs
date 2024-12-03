using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float velocidade = 5.0f;
    [SerializeField] private float forcaPulo = 5.0f;
    [SerializeField] private GameObject particulasMorte;
     [SerializeField] private float tempoAntesDestruir = 2.0f;
    private Vector3 anguloRotacao = new Vector3(0,90,0);
    private Scene cenaAtual;
    public int Chave = 0;
    private bool estaVivo = false;
    private Animator animator;
    public Transform morte;
    public GameObject jogador;


    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        Scene cenaAtual = SceneManager.GetActiveScene();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (estaVivo) return; // Se estiver morto, desabilitar controles

        // Lógica de movimento e pulo
        Andar();
        if (Input.GetKeyDown(KeyCode.Space)) Pular();
    }

    //Andar
    public void Andar()
    {
        float moveV = Input.GetAxis("Vertical");
        //transform.position += new Vector3(0, 0, moveV * velocidade * Time.deltaTime);
        Vector3 direcao = moveV * transform.forward;
        rb.MovePosition(rb.position + direcao * velocidade * Time.deltaTime);

    }

    //Pular
    public void Pular()
    {
        rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
    }

    //Virar
    public void Virar()
    {
        float moveH = Input.GetAxis("Horizontal");
        Quaternion rotacao = Quaternion.Euler(anguloRotacao * moveH * Time.deltaTime);
        rb.MoveRotation(rotacao * rb.rotation);
    }

    

    public void Morrer()
    {
        if (estaVivo) return; // Evitar múltiplas mortes

        estaVivo = true;
        animator.SetBool("EstaVivo", false); 
        rb.linearVelocity = Vector3.zero;      
        rb.isKinematic = true;

        Invoke("MostrarParticulas", 3.0f);

        Invoke("DestruirJogador", 3.1f);

        Invoke("ReiniciarFase", 5.0f);
    }
        void ReiniciarFase()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Porta"))
        {
            GameStateManager.origemTransicao = "Porta";
            SceneManager.LoadScene("Castelo");
        }
        if (other.CompareTag("Espinho"))
        {
            SceneManager.LoadScene("Game1");
        }
        if (other.CompareTag("VoltarCity"))
        {
            SceneManager.LoadScene("Mapa");
        }
        if(other.CompareTag("Lab"))
        {
            SceneManager.LoadScene("labirinto");
        }
        if (other.CompareTag("Chave1"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Castelo2");
        }
        if (other.CompareTag("Chave2"))
        {
            {
            SceneManager.LoadScene("Acabou");
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // Verifica se o objeto colidido possui uma tag específica
        if (other.gameObject.CompareTag("Espinhos"))
        {
            Destroy(other.gameObject); // Destroi o objeto colidido
            Morrer();
        }
    }

    void MostrarParticulas()
    {
        Instantiate(particulasMorte, transform.position, Quaternion.identity);
    }
    
}
