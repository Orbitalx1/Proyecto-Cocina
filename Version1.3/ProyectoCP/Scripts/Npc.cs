using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Npc : MonoBehaviour
{
    public GameObject mensajeUI; 
    public string nombreEscena = "Nivel1"; 
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacción con NPC - Cargando escena: " + nombreEscena);
            SceneManager.LoadScene(nombreEscena);
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
