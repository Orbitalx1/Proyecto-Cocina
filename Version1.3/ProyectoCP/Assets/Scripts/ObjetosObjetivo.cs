using UnityEngine;

public class ObjetosObjetivo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ContadorObjetos contador = FindObjectOfType<ContadorObjetos>();
            if (contador != null)
            {
                contador.AgregarObjeto();
            }

            Destroy(gameObject); 
        }
    }
}

