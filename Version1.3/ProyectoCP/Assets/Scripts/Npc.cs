using UnityEngine;
using UnityEngine.UI; // si usas Text legacy

public class Npc : MonoBehaviour
{
    public GameObject mensajeUI; 
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacción con NPC");
            // Agragar para niveles futuros
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (mensajeUI != null)
                mensajeUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (mensajeUI != null)
                mensajeUI.SetActive(false);
        }
    }
}
