using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorVueltaUI : MonoBehaviour
{
    [Header("Referencias de texto (asigna en el Inspector)")]
    public TextMeshProUGUI textoNumero;
    public TextMeshProUGUI textoTiempoVuelta;
    public TextMeshProUGUI textoTiempoTotal;

    [Header("Imagen de fondo (opcional)")]
    public Image fondoTarjeta;

    /// <summary>
    /// Configura la información visual de la tarjeta de vuelta.
    /// </summary>
    /// <param name="numero">Número de la vuelta.</param>
    /// <param name="tiempoVueltaStr">Tiempo que tardó la vuelta (en string).</param>
    /// <param name="tiempoTotalStr">Tiempo total acumulado (en string).</param>
    /// <param name="colorBaseTexto">Color base del texto (normalmente blanco).</param>
    public void Configurar(int numero, string tiempoVueltaStr, string tiempoTotalStr, Color colorBaseTexto)
    {
        // Asignación de textos
        textoNumero.text = numero.ToString("00");
        textoTiempoVuelta.text = tiempoVueltaStr;
        textoTiempoTotal.text = tiempoTotalStr;

        // Colores base
        textoNumero.color = colorBaseTexto;
        textoTiempoVuelta.color = colorBaseTexto;
        textoTiempoTotal.color = Color.gray; // El tiempo total se mantiene gris
    }

    /// <summary>
    /// Aplica el color de puntuación (por ejemplo, mejor vuelta en rojo o lila).
    /// </summary>
    /// <param name="color">Color de la puntuación.</param>
    public void AplicarColorPuntuacion(Color color)
    {
        textoNumero.color = color;
        textoTiempoVuelta.color = color;
        // El tiempo total no cambia, se mantiene gris
    }

    /// <summary>
    /// Permite actualizar dinámicamente el fondo si se desea.
    /// </summary>
    /// <param name="nuevoSprite">Sprite del nuevo fondo.</param>
    public void CambiarFondo(Sprite nuevoSprite)
    {
        if (fondoTarjeta != null && nuevoSprite != null)
            fondoTarjeta.sprite = nuevoSprite;
    }
}
