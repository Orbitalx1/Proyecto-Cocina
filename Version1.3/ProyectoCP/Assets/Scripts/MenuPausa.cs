using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                Continuar();
            else
                Pausar();
        }
    }

    public void Continuar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pantalla Titulo"); 
    }

    public void Restart()
{
    Time.timeScale = 1f; 
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
}

}
