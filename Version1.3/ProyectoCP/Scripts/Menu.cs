using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("EscenaPrincipal"); 
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
