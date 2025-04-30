using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoJugador : MonoBehaviour
{
    public string prefijoEntrada = "P1";

    public float velocidad = 5f;
    public float gravedad = -9.81f;
    public float alturaSalto = 1f;
    public float velocidadRotacion = 10f;

    private CharacterController controlador;
    private Vector3 velocidadY;
    private bool enSuelo;

    public Transform verificadorSuelo;
    public float distanciaSuelo = 0.4f;
    public LayerMask capaSuelo;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica si el jugador está en el suelo
        enSuelo = Physics.CheckSphere(verificadorSuelo.position, distanciaSuelo, capaSuelo);
        if (enSuelo && velocidadY.y < 0)
            velocidadY.y = -2f;

        // Leer la entrada del jugador
        float inputX = Input.GetAxisRaw("Horizontal_" + prefijoEntrada);
        float inputZ = Input.GetAxisRaw("Vertical_" + prefijoEntrada);

        Vector3 direccionEntrada = new Vector3(inputX, 0, inputZ).normalized;

        // Girar hacia la dirección que se presiona
        if (direccionEntrada.magnitude >= 0.1f)
        {
            float anguloObjetivo = Mathf.Atan2(direccionEntrada.x, direccionEntrada.z) * Mathf.Rad2Deg;
            Quaternion rotacionObjetivo = Quaternion.Euler(0f, anguloObjetivo, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
        }

        // Mover en la dirección de la entrada
        Vector3 movimiento = direccionEntrada * velocidad;
        controlador.Move(movimiento * Time.deltaTime);

        // Saltar
        if (Input.GetButtonDown("Jump_" + prefijoEntrada) && enSuelo)
            velocidadY.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);

        // Aplicar gravedad
        velocidadY.y += gravedad * Time.deltaTime;
        controlador.Move(velocidadY * Time.deltaTime);
    }
}

