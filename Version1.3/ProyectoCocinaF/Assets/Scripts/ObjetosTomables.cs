using UnityEngine;

public class ObjetosTomables : MonoBehaviour
{
    public enum TipoObjeto
    {
        Curativo,
        Lampara
    }

    public TipoObjeto tipo = TipoObjeto.Curativo;
    public int cantidad = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SaludP salud = other.GetComponent<SaludP>();

        switch (tipo)
        {
            case TipoObjeto.Curativo:
                if (salud != null && salud.corazonesActuales < salud.corazonesMaximos)
                {
                    salud.Curar(cantidad);
                    Destroy(gameObject);
                }
                break;

            case TipoObjeto.Lampara:
                
                Transform lamparaMano = BuscarHijo(other.transform, "LamparaMano");

                if (lamparaMano != null)
                {
                    lamparaMano.gameObject.SetActive(true);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("No se encontró 'LamparaMano' en el jugador " + other.name);
                }
                break;
        }
    }

    
    Transform BuscarHijo(Transform padre, string nombreBuscado)
    {
        foreach (Transform hijo in padre)
        {
            if (hijo.name == nombreBuscado)
                return hijo;

            Transform resultado = BuscarHijo(hijo, nombreBuscado);
            if (resultado != null)
                return resultado;
        }

        return null;
    }
}
