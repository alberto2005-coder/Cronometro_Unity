using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Controla el cronómetro principal, su estado y el registro de vueltas.
/// </summary>
public class Cronometro : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TextMeshProUGUI textoTiempo;
    public Button botonIniciarPausar;
    public Image imagenBotonIniciarPausar;
    public TextMeshProUGUI textoBotonIniciarPausar;
    public Button botonReiniciar;
    public Button botonVuelta;
    public Transform contenedorVueltas;
    public GameObject prefabTextoVuelta;

    [Header("Sprites del Atlas")]
    public Sprite spriteEmpezar;   // Lila / Morado
    public Sprite spriteDetener;   // Rojo
    public Sprite spriteSecundario; // Gris oscuro (botones y tarjetas)

    [Header("Colores de Texto y Vueltas")]
    public Color colorMejorTiempo = new(0.686f, 0.655f, 0.898f, 1f); // Lila / Morado
    public Color colorPeorTiempo = new(0.965f, 0.318f, 0.318f, 1f);  // Rojo
    public Color colorTextoSecundario = Color.white; // Blanco por defecto

    [Header("Estilo de Texto")] // <--- NUEVO
    [Range(0f, 1f)]
    public float grosorOutline = 0.2f; // <--- NUEVO: Controla el grosor desde aquí

    // ---------------------- Variables Internas ----------------------
    private float tiempoTotalTranscurrido = 0f;
    private bool estaActivo = false;

    private readonly List<float> tiemposTotalesVuelta = new();
    private readonly List<ControladorVueltaUI> controladoresVueltasUI = new();

    // ---------------------- Ciclo de Vida ----------------------

    void Start()
    {
        // Suscripción de eventos
        botonIniciarPausar.onClick.AddListener(AlternarIniciarPausar);
        botonReiniciar.onClick.AddListener(ReiniciarCronometro);
        botonVuelta.onClick.AddListener(RegistrarVuelta);

        ConfigurarEstiloBotonesSecundarios();
        ActualizarDisplay();
        ActualizarEstadoBoton(); // Muestra "Empezar" al inicio
    }

    void Update()
    {
        if (estaActivo)
        {
            tiempoTotalTranscurrido += Time.deltaTime;
            ActualizarDisplay();
        }
    }

    // ---------------------- Configuración de Estilos ----------------------

    void ConfigurarEstiloBotonesSecundarios()
    {
        if (spriteSecundario == null)
        {
            Debug.LogWarning("El Sprite Secundario no está asignado.");
            return;
        }

        ConfigurarBoton(botonReiniciar, "RESTABLECER");
        ConfigurarBoton(botonVuelta, "VUELTA");
    }

    void ConfigurarBoton(Button boton, string texto)
    {
        if (boton == null) return;

        var imagen = boton.GetComponent<Image>();
        var textoTMP = boton.GetComponentInChildren<TextMeshProUGUI>();

        if (imagen != null) 
        {
            imagen.sprite = spriteSecundario;
            imagen.color = Color.white; // Mantiene color original del sprite
        }

        if (textoTMP != null)
        {
            textoTMP.text = texto;
            textoTMP.color = colorTextoSecundario;
        }
    }

    // ---------------------- Lógica Principal ----------------------

    void AlternarIniciarPausar()
    {
        estaActivo = !estaActivo;
        ActualizarEstadoBoton();
    }

    void ReiniciarCronometro()
    {
        estaActivo = false;
        tiempoTotalTranscurrido = 0f;

        tiemposTotalesVuelta.Clear();

        // Elimina todas las tarjetas de vuelta activas
        foreach (var controlador in controladoresVueltasUI)
        {
            if (controlador != null)
                Destroy(controlador.gameObject);
        }
        controladoresVueltasUI.Clear();

        ActualizarDisplay();
        ActualizarEstadoBoton();
    }

    void RegistrarVuelta()
    {
        if (!estaActivo) return;

        float tiempoAnterior = tiemposTotalesVuelta.LastOrDefault();
        float tiempoVueltaActual = tiempoTotalTranscurrido - tiempoAnterior;

        tiemposTotalesVuelta.Add(tiempoTotalTranscurrido);

        CrearTextoVuelta(tiemposTotalesVuelta.Count, tiempoTotalTranscurrido, tiempoVueltaActual);
        ActualizarColoresVueltas();
    }

    // ---------------------- Gestión de Vueltas ----------------------

    void CrearTextoVuelta(int numeroVuelta, float tiempoTotal, float tiempoVuelta)
    {
        if (prefabTextoVuelta == null)
        {
            Debug.LogError("El prefabTextoVuelta no está asignado.");
            return;
        }

        GameObject nuevaVuelta = Instantiate(prefabTextoVuelta, contenedorVueltas);
        nuevaVuelta.transform.SetSiblingIndex(0);

        if (!nuevaVuelta.TryGetComponent(out ControladorVueltaUI controladorVuelta))
        {
            Debug.LogError("El prefab de vuelta no tiene el componente ControladorVueltaUI.");
            Destroy(nuevaVuelta);
            return;
        }

        // Asigna el fondo del prefab
        if (controladorVuelta.fondoTarjeta != null && spriteSecundario != null)
        {
            controladorVuelta.fondoTarjeta.sprite = spriteSecundario;
            controladorVuelta.fondoTarjeta.color = Color.white;
        }

        // <--- CAMBIO AQUÍ: Ahora pasamos el grosorOutline al final --->
        controladorVuelta.Configurar(
            numeroVuelta,
            FormatearTiempo(tiempoVuelta),
            FormatearTiempo(tiempoTotal),
            colorTextoSecundario,
            grosorOutline 
        );

        controladoresVueltasUI.Insert(0, controladorVuelta);
    }

    void ActualizarColoresVueltas()
    {
        if (tiemposTotalesVuelta.Count == 0) return;

        // Calcula los tiempos individuales de cada vuelta
        var tiemposIndividuales = tiemposTotalesVuelta
            .Select((t, i) => i == 0 ? t : t - tiemposTotalesVuelta[i - 1])
            .ToList();

        float mejorTiempo = tiemposIndividuales.Min();
        float peorTiempo = tiemposIndividuales.Max();

        for (int i = 0; i < tiemposIndividuales.Count; i++)
        {
            int uiIndex = controladoresVueltasUI.Count - 1 - i;
            if (uiIndex < 0 || uiIndex >= controladoresVueltasUI.Count) continue;

            var controlador = controladoresVueltasUI[uiIndex];
            float tiempoVuelta = tiemposIndividuales[i];

            if (Mathf.Approximately(tiempoVuelta, mejorTiempo))
                controlador.AplicarColorPuntuacion(colorMejorTiempo);
            else if (Mathf.Approximately(tiempoVuelta, peorTiempo))
                controlador.AplicarColorPuntuacion(colorPeorTiempo);
            else
                controlador.AplicarColorPuntuacion(colorTextoSecundario);
        }
    }

    // ---------------------- Helpers ----------------------

    string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int milisegundos = Mathf.FloorToInt((tiempo * 100f) % 100f);

        return $"{minutos:00}:{segundos:00}.{milisegundos:00}";
    }

    void ActualizarDisplay()
    {
        if (textoTiempo != null)
            textoTiempo.text = FormatearTiempo(tiempoTotalTranscurrido);
    }

    void ActualizarEstadoBoton()
    {
        if (textoBotonIniciarPausar == null || imagenBotonIniciarPausar == null) return;

        if (estaActivo)
        {
            textoBotonIniciarPausar.text = "DETENER";
            if (spriteDetener != null)
            {
                imagenBotonIniciarPausar.sprite = spriteDetener;
                imagenBotonIniciarPausar.color = Color.white;
            }
        }
        else
        {
            textoBotonIniciarPausar.text = "EMPEZAR";
            if (spriteEmpezar != null)
            {
                imagenBotonIniciarPausar.sprite = spriteEmpezar;
                imagenBotonIniciarPausar.color = Color.white;
            }
        }
    }
}