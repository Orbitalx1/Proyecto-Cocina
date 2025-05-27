using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador1;
    public Transform jugador2;

    [Header("Posición de la cámara")]
    public float altura = 10f;
    public float distancia = 7f;
    public float suavizado = 5f;

    [Header("Ángulo de la cámara")]
    public float anguloVertical = 45f;

    [Header("Detección de colisiones")]
    public LayerMask capaObstaculos;

    void LateUpdate()
    {
        if (jugador1 == null || jugador2 == null)
            return;

        Vector3 puntoMedio = (jugador1.position + jugador2.position) / 2f;

        Quaternion rotacion = Quaternion.Euler(anguloVertical, 0f, 0f);
        Vector3 direccionOffset = rotacion * Vector3.back;

        Vector3 posicionDeseada = puntoMedio + direccionOffset * distancia;

        // Ajuste dinámico de altura basado en la altura real de los jugadores
        float alturaMedia = (jugador1.position.y + jugador2.position.y) / 2f;
        posicionDeseada.y = alturaMedia + altura;

        Vector3 direccionCamara = (posicionDeseada - puntoMedio).normalized;
        float distanciaCamara = Vector3.Distance(puntoMedio, posicionDeseada);

        if (Physics.Raycast(puntoMedio, direccionCamara, out RaycastHit hit, distanciaCamara, capaObstaculos))
        {
            posicionDeseada = hit.point;
        }

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        transform.LookAt(puntoMedio);
    }
}
