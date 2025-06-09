using UnityEngine;
using TMPro;

public class ContadorObjetos : MonoBehaviour
{
    public int cantidadRecolectada = 0;
    public TextMeshProUGUI textoContador;

    public void AgregarObjeto()
    {
        cantidadRecolectada++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoContador != null)
            textoContador.text = "Objetos: " + cantidadRecolectada + "/5";
    }
}

