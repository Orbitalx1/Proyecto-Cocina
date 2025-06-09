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
    private ControladorSonidoPasos audioManager;
    private bool estabaCaminando = false;

    public Transform verificadorSuelo;
    public float distanciaSuelo = 0.4f;  
    public LayerMask capaSuelo;

    private Animator animator; 

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  
        audioManager = FindObjectOfType<ControladorSonidoPasos>();

    }

    void Update()
    {
        // Verifica si el jugador está en el suelo
        enSuelo = Physics.CheckSphere(verificadorSuelo.position, distanciaSuelo, capaSuelo);
        if (enSuelo && velocidadY.y < 0)
            velocidadY.y = -2f;  // Aseguramos que la velocidad en el eje Y se ajuste cuando esté en el suelo

        
        animator.SetBool("Ensuelo", enSuelo);

        // Leer la entrada del jugador
        float inputX = Input.GetAxisRaw("Horizontal_" + prefijoEntrada);
        float inputZ = Input.GetAxisRaw("Vertical_" + prefijoEntrada);

        Vector3 direccionEntrada = new Vector3(inputX, 0, inputZ).normalized;

        
        if (direccionEntrada.magnitude > 0.1f)
        {
            animator.SetFloat("Movement", 1f); // El jugador se está moviendo
        }
        else
        {
            animator.SetFloat("Movement", 0f); // El jugador está quieto
        }

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

        // Saltar, solo si está en el suelo
        if (Input.GetButtonDown("Jump_" + prefijoEntrada) && enSuelo)
        {
            velocidadY.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }

        // Aplicar gravedad (si no está en el suelo)
        if (!enSuelo)
        {
            velocidadY.y += gravedad * Time.deltaTime;
        }

        // Aplicar el movimiento vertical (gravedad y salto)
        controlador.Move(velocidadY * Time.deltaTime);

        bool caminandoAhora = direccionEntrada.magnitude > 0.1f;

        if (caminandoAhora && !estabaCaminando)
        {
            audioManager.EmpezarCaminar();
        }
        else if (!caminandoAhora && estabaCaminando)
        {
            audioManager.DejarDeCaminar();
        }

        estabaCaminando = caminandoAhora;


    }

    void OnControllerColliderHit(ControllerColliderHit contacto)
{
    Rigidbody rb = contacto.collider.attachedRigidbody;

    if (rb == null || rb.isKinematic)
        return;

    Vector3 fuerzaEmpuje = new Vector3(contacto.moveDirection.x, 0, contacto.moveDirection.z);
    float fuerza = 1f;
    rb.AddForce(fuerzaEmpuje * fuerza, ForceMode.Impulse);
}


    
}
