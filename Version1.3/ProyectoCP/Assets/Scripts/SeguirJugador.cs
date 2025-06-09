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

    [Header("Restricciones")]
    public float distanciaMaximaJugadores = 15f; // <- NUEVA VARIABLE

    void LateUpdate()
    {
        if (jugador1 == null || jugador2 == null)
            return;

        // Evita que se alejen más de la distancia permitida
        Vector3 posicion1 = jugador1.position;
        Vector3 posicion2 = jugador2.position;
        float distanciaEntreJugadores = Vector3.Distance(posicion1, posicion2);

        if (distanciaEntreJugadores > distanciaMaximaJugadores)
        {
            Vector3 direccion = (posicion2 - posicion1).normalized;
            float exceso = distanciaEntreJugadores - distanciaMaximaJugadores;

            // Mover jugador2 hacia jugador1 (puedes invertir esto o hacer un promedio)
            jugador2.position -= direccion * (exceso / 2f);
            jugador1.position += direccion * (exceso / 2f);
        }

        Vector3 puntoMedio = (jugador1.position + jugador2.position) / 2f;

        Quaternion rotacion = Quaternion.Euler(anguloVertical, 0f, 0f);
        Vector3 direccionOffset = rotacion * Vector3.back;

        Vector3 posicionDeseada = puntoMedio + direccionOffset * distancia;

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

