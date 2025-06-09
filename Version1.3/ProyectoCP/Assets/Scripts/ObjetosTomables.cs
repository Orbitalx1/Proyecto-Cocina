using UnityEngine;


public class ObjetosTomables : MonoBehaviour
{
    public Respawn spawner; 
    private bool tieneObjetoEspecial = false;
    public enum TipoObjeto
    {
        Curativo,
        Lampara,
        Llave
    }

    public TipoObjeto tipo = TipoObjeto.Curativo;
    public int cantidad = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ControladorObjetos salud = other.GetComponent<ControladorObjetos>();

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

                if (lamparaMano != null && salud != null)
{
    if (lamparaMano.gameObject.activeSelf || salud.TieneLlave)
        return;

    lamparaMano.gameObject.SetActive(true);
    salud.MostrarIconoLampara();
    if (spawner != null)
    spawner.IniciarRespawn();
    Destroy(gameObject);
}
                break;
                
                case TipoObjeto.Llave:
    if (salud != null && !salud.TieneLlave && !salud.GetTieneLampara())
{
    salud.MostrarIconoLlave();
    Destroy(gameObject);
}

    break;


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
}
