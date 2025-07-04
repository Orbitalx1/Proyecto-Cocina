using UnityEngine;

public class PuertaLlave : MonoBehaviour
{
    public GameObject puertaPadre; 

    public bool consumirLlave = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ControladorObjetos jugador = other.GetComponent<ControladorObjetos>();

        if (jugador != null && jugador.TieneLlave)
        {
            if (consumirLlave)
                jugador.OcultarIconoLlave();

            if (puertaPadre != null)
                Destroy(puertaPadre); // destruye el obstáculo físico
            else
                Debug.LogWarning("No asignaste la puertaPadre");
        }
    }
}

