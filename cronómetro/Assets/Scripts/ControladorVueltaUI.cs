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
    /// Configura la tarjeta.
    /// </summary>
    /// <param name="numero">Número de vuelta.</param>
    /// <param name="tiempoVueltaStr">Texto del tiempo de vuelta.</param>
    /// <param name="tiempoTotalStr">Texto del tiempo total.</param>
    /// <param name="colorBaseTexto">Color del texto (opcional, por defecto blanco).</param>
    /// <param name="grosorBorde">Grosor del borde (0 = sin borde, 0.2 = borde normal, 0.5 = grueso).</param>
    public void Configurar(int numero, string tiempoVueltaStr, string tiempoTotalStr, Color? colorBaseTexto = null, float grosorBorde = 0.2f)
    {
        // 1. Definir color del texto (Blanco si es null)
        Color colorFinal = colorBaseTexto ?? Color.white;

        // 2. Asignar textos
        textoNumero.text = numero.ToString("00");
        textoTiempoVuelta.text = tiempoVueltaStr;
        textoTiempoTotal.text = tiempoTotalStr;

        // 3. Asignar colores base
        textoNumero.color = colorFinal;
        textoTiempoVuelta.color = colorFinal;
        textoTiempoTotal.color = Color.gray; // El total se suele dejar gris

        // --- CAMBIO AQUÍ: FONDO NEGRO ---
        if (fondoTarjeta != null)
        {
            fondoTarjeta.color = Color.black;
        }

        // 4. Aplicar el grosor del borde (Outline)
        if (grosorBorde > 0)
        {
            EstablecerGrosorOutline(textoNumero, grosorBorde);
            EstablecerGrosorOutline(textoTiempoVuelta, grosorBorde);
        }
    }

    /// <summary>
    /// Cambia el color de la fila (para destacar vueltas rápidas).
    /// </summary>
    public void AplicarColorPuntuacion(Color color)
    {
        textoNumero.color = color;
        textoTiempoVuelta.color = color;
    }

    /// <summary>
    /// Método interno para forzar el grosor del borde (Outline) modificando el material.
    /// </summary>
    private void EstablecerGrosorOutline(TextMeshProUGUI texto, float grosor)
    {
        if (texto == null) return;

        // Creamos una copia del material para este objeto específico
        Material materialInstancia = new Material(texto.fontSharedMaterial);

        // Activamos la funcionalidad de Outline en el Shader
        materialInstancia.EnableKeyword("OUTLINE_ON");

        // --- CAMBIO AQUÍ: BORDE BLANCO ---
        materialInstancia.SetColor("_OutlineColor", Color.white);
        
        // Configuramos el grosor (Width)
        materialInstancia.SetFloat("_OutlineWidth", grosor);

        // Asignamos el nuevo material al texto
        texto.fontMaterial = materialInstancia;

        // Forzamos a TMP a recalcular los márgenes para que el borde no se corte
        texto.UpdateMeshPadding();
    }

    public void CambiarFondo(Sprite nuevoSprite)
    {
        if (fondoTarjeta != null && nuevoSprite != null)
            fondoTarjeta.sprite = nuevoSprite;
    }
}