using UnityEngine;

public class SierraMovimiento : MonoBehaviour
{
    public Transform Inicio;  
    public Transform Fin;  
    public float velocidad = 3f;

    private Vector3 objetivoActual;

    void Start()
    {
        if (Inicio != null)
            objetivoActual = Inicio.position;
    }

    void Update()
    {
        if (Inicio == null || Fin == null) return;

        // Movimiento entre los dos puntos
        transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);

        // Cambio de dirección
        if (Vector3.Distance(transform.position, objetivoActual) < 0.1f)
        {
            objetivoActual = (objetivoActual == Inicio.position) ? Fin.position : Inicio.position;
        }

        // Rotación opcional
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);
    }
}

