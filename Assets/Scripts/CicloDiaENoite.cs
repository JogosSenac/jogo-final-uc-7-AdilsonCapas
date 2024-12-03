using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CicloDiaENoite : MonoBehaviour
{
    [SerializeField] private Transform diretionalLight;
    [SerializeField] [Tooltip("Duração do Dia em Segundos")] private int duracaoDoDia;

    private float segundos;
    private float multiplacador;

    void Start()
    {
        multiplacador = 86400 / duracaoDoDia;
    }

    void Update()
    {
        segundos += Time.deltaTime * multiplacador;

        if(segundos >= 86400)
        {
            segundos = 0;
        }

        ProcessarCeu(); ;
        CalcularHorario();
    }

    private void ProcessarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90, 270, segundos / 86400);
        diretionalLight.rotation = Quaternion.Euler(rotacaoX, 0, 0);
    }

    private void CalcularHorario()
    {

    }
}
