using UnityEngine;
using UnityEngine.AI;

public class NPCRandomMovement : MonoBehaviour
{
    public float moveRadius = 10f; // Raio da área onde o NPC pode andar
    public float waitTime = 2f;   // Tempo que o NPC espera ao chegar no destino
    public Animator animator;

    private NavMeshAgent agent;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPosition();
    }

    void Update()
    {
        // Verifica se o NPC chegou ao destino
        animator.SetFloat("Speed", agent.velocity.magnitude);

    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
    {
        waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            MoveToRandomPosition();
            waitTimer = 0f;
        }
    }
    }

    void MoveToRandomPosition()
    {
        // Calcula uma posição aleatória dentro do raio
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Mantém a altura do NPC

        NavMeshHit hit;
        // Garante que a posição aleatória está dentro do NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
