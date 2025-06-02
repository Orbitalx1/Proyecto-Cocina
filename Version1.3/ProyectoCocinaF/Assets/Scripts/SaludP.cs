using UnityEngine;
using UnityEngine.UI;

public class SaludP : MonoBehaviour
{
    [Header("Vida del jugador")]
    public int corazonesMaximos = 5;               // Total de corazones
    public int corazonesActuales = 5;              // Corazones activos al inicio

    [Header("Referencias")]
    public Sprite corazonLleno;                    // Sprite del corazón
    public Transform panelCorazones;               // Panel en la UI para colocar los corazones

    private Image[] corazonesUI;

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
            rt.sizeDelta = new Vector2(32, 32); // Tamaño fijo
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

    public void RecibirDanio(int cantidad)
    {
        corazonesActuales -= cantidad;
        corazonesActuales = Mathf.Clamp(corazonesActuales, 0, corazonesMaximos);
        ActualizarUI();

        if (corazonesActuales <= 0)
        {
            Morir();
        }
    }

    public void Curar(int cantidad)
    {
        corazonesActuales += cantidad;
        corazonesActuales = Mathf.Clamp(corazonesActuales, 0, corazonesMaximos);
        ActualizarUI();
    }

    void Morir()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí puedes mostrar Game Over, desactivar jugador, reiniciar nivel, etc.
        gameObject.SetActive(false);
    }
}
