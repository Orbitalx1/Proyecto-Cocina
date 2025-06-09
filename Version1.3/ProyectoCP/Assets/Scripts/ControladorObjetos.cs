using UnityEngine;
using UnityEngine.UI;

public class ControladorObjetos : MonoBehaviour
{
    private bool tieneObjetoEspecial = false;

    [Header("Vida del jugador")]
    public int corazonesMaximos = 5;               // Total de corazones
    public int corazonesActuales = 5;              // Corazones activos al inicio

    [Header("Referencias")]
    public Sprite corazonLleno;                    
    public Transform panelCorazones;              

    private Image[] corazonesUI;

    [Header("Lampara")]
    public Transform panelObjetos;         
    public Sprite iconoLampara;         
    private bool lamparaMostrada = false; 

    private float tiempoLamparaRestante;
    private Transform lamparaMano;
    private Transform llaveMano;

    [Header("Llave")]
public Sprite iconoLlave;
private bool llaveMostrada = false;
public bool TieneLlave => llaveMostrada;


    void Start()
    {
        corazonesActuales = Mathf.Clamp(corazonesActuales, 0, corazonesMaximos);

        GenerarCorazonesUI();
        ActualizarUI();
    }

    void GenerarCorazonesUI()
    {
        corazonesUI = new Image[corazonesMaximos];

        for (int i = 0; i < corazonesMaximos; i++)
        {
            GameObject corazon = new GameObject("Corazon_" + i);
            corazon.transform.SetParent(panelCorazones, false);

            Image img = corazon.AddComponent<Image>();
            img.sprite = corazonLleno;

            RectTransform rt = img.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(32, 32); // Tamaño icono
            corazonesUI[i] = img;
        }
    }

    void ActualizarUI()
    {
        for (int i = 0; i < corazonesUI.Length; i++)
        {
            corazonesUI[i].enabled = (i < corazonesActuales);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        corazonesActuales -= cantidad;
        corazonesActuales = Mathf.Clamp(corazonesActuales, 0, corazonesMaximos);
        ActualizarUI();

    }

    public void Curar(int cantidad)
    {
        corazonesActuales += cantidad;
        corazonesActuales = Mathf.Clamp(corazonesActuales, 0, corazonesMaximos);
        ActualizarUI();
    }



    public void MostrarIconoLampara()
{
    if (tieneObjetoEspecial || lamparaMostrada || iconoLampara == null || panelObjetos == null) return;

    GameObject icono = new GameObject("IconoLampara");
    icono.transform.SetParent(panelObjetos, false);

    Image img = icono.AddComponent<UnityEngine.UI.Image>();
    img.sprite = iconoLampara;

    RectTransform rt = img.GetComponent<RectTransform>();
    rt.sizeDelta = new Vector2(32, 32);

    lamparaMostrada = true;
    tieneObjetoEspecial = true;
    tiempoLamparaRestante = 10; // Reiniciar el tiempo de la lampara

    if (lamparaMano == null)
        lamparaMano = BuscarHijo(transform, "LamparaMano");

    if (lamparaMano != null)
        lamparaMano.gameObject.SetActive(true);
}

    public void OcultarIconoLampara()
    {
   Transform icono = panelObjetos.Find("IconoLampara");
    if (icono != null)
        Destroy(icono.gameObject);

    lamparaMostrada = false;
    tieneObjetoEspecial = false;
    }
    public void MostrarIconoLlave()
{
    if (tieneObjetoEspecial || llaveMostrada || iconoLlave == null || panelObjetos == null) return;

    GameObject icono = new GameObject("IconoLlave");
    icono.transform.SetParent(panelObjetos, false);

    Image img = icono.AddComponent<Image>();
    img.sprite = iconoLlave;

    RectTransform rt = img.GetComponent<RectTransform>();
    rt.sizeDelta = new Vector2(32, 32);

    llaveMostrada = true;
    tieneObjetoEspecial = true;

    if (llaveMano == null)
        llaveMano = BuscarHijo(transform, "LlaveMano");

    if (llaveMano != null)
        llaveMano.gameObject.SetActive(true);
}
    public void OcultarIconoLlave()
{
    Transform icono = panelObjetos.Find("IconoLlave");
    if (icono != null)
        Destroy(icono.gameObject);

    if (llaveMano == null)
        llaveMano = BuscarHijo(transform, "LlaveMano");

    if (llaveMano != null)
        llaveMano.gameObject.SetActive(false);

    llaveMostrada = false;
    tieneObjetoEspecial = false;
}
public bool GetTieneLampara()
{
    return lamparaMostrada;
}


    private Transform BuscarHijo(Transform padre, string nombreBuscado)
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
    void Update()
    {
        if (tiempoLamparaRestante > 0f)
        {
            tiempoLamparaRestante -= Time.deltaTime;

            if (tiempoLamparaRestante <= 0f)
            {

                if (lamparaMano == null)
                    lamparaMano = BuscarHijo(transform, "LamparaMano");

                if (lamparaMano != null)
                    lamparaMano.gameObject.SetActive(false);

                OcultarIconoLampara();
            }
        }
    }
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Daño") )
    {
        RecibirDaño(1); 
    }
}



}
