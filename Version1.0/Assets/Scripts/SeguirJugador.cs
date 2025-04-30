using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform objetivo; // Jugador a seguir
    public Vector3 offset = new Vector3(0, 5, -10);
    public float velocidadSuavizado = 5f;
    public float suavizadoRotacion = 5f;

    private Vector3 posicionActualVelocidad;

    void LateUpdate()
    {
        if (objetivo == null)
            return;

        // Offset rotado basado en la dirección del jugador
        Vector3 offsetRotado = objetivo.rotation * offset;
        Vector3 posicionDeseada = objetivo.position + offsetRotado;

        // Suavizar la posición de la cámara
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado * Time.deltaTime);

        // Suavizar la rotación para mirar al jugador
        Quaternion rotacionDeseada = Quaternion.LookRotation(objetivo.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, suavizadoRotacion * Time.deltaTime);
    }
}


