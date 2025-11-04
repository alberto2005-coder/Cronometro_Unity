using System;
using UnityEngine;

/// <summary>
/// Contiene la información que se muestra en el panel "Readme" del proyecto Unity.
/// Permite incluir icono, título, secciones de texto y enlaces.
/// </summary>
[CreateAssetMenu(fileName = "Readme", menuName = "Project/Readme", order = 1)]
public class Readme : ScriptableObject
{
    [Header("Información General")]
    [Tooltip("Icono que se mostrará en la parte superior del Readme.")]
    public Texture2D icon;

    [Tooltip("Título principal del documento Readme.")]
    public string title;

    [Header("Contenido")]
    [Tooltip("Lista de secciones que conforman el Readme.")]
    public Section[] sections;

    [HideInInspector]
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        [Tooltip("Encabezado de la sección.")]
        public string heading;

        [TextArea(2, 6)]
        [Tooltip("Texto descriptivo o instrucciones de la sección.")]
        public string text;

        [Tooltip("Texto del enlace (opcional).")]
        public string linkText;

        [Tooltip("URL o ruta del enlace asociado (opcional).")]
        public string url;
    }
}
