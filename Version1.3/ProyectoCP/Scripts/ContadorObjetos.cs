using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ContadorObjetos : MonoBehaviour
{
    public int cantidadRecolectada = 0;
    public TextMeshProUGUI textoContador;
    public GameObject mensajeGanasteUI; 
    public string nombreEscenaLobby = "EscenaPrincipal"; 

    public void AgregarObjeto()
    {
        cantidadRecolectada++;
        ActualizarTexto();

        if (cantidadRecolectada >= 5)
        {
            GanarJuego();
        }
    }

    void ActualizarTexto()
    {
        if (textoContador != null)
            textoContador.text = "Objetos: " + cantidadRecolectada + "/5";
    }

    void GanarJuego()
    {
        if (mensajeGanasteUI != null)
            mensajeGanasteUI.SetActive(true);

       
        Time.timeScale = 0f;
    StartCoroutine(EsperarYReiniciar());
    }

    IEnumerator EsperarYReiniciar()
{
    yield return new WaitForSecondsRealtime(2f);
    Time.timeScale = 1f;
    SceneManager.LoadScene("EscenaPrincipal");

}
}
