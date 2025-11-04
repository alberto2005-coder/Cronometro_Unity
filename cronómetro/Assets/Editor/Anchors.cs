using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Herramienta de editor para alinear las anclas de RectTransforms con sus esquinas actuales.
/// Permite aplicar el cambio a uno o varios elementos de UI a la vez.
/// </summary>
public static class RectTransformUtilityEditor
{
    [MenuItem("Tools/UI Tools/Anchors to Corners _%#a", false, 10)]
    public static void AnchorsToCorners()
    {
        GameObject[] seleccionados = Selection.gameObjects;

        if (seleccionados == null || seleccionados.Length == 0)
        {
            Debug.LogWarning("No hay objetos de UI seleccionados.");
            return;
        }

        int modificados = 0;
        foreach (GameObject go in seleccionados)
        {
            RectTransform rect = go.GetComponent<RectTransform>();
            if (rect == null)
            {
                Debug.LogWarning($"'{go.name}' no tiene un RectTransform. Se omitió.");
                continue;
            }

            RectTransform parentRect = rect.parent != null ? rect.parent.GetComponent<RectTransform>() : null;
            if (parentRect == null)
            {
                Debug.LogWarning($"'{go.name}' no tiene un padre con RectTransform. Se omitió.");
                continue;
            }

            // Registrar Undo
            Undo.RecordObject(rect, "Anchors to Corners");

            // Calcular tamaño del padre
            Vector2 parentSize = parentRect.rect.size;

            // Calcular nuevas anclas en base a offset y tamaño del padre
            Vector2 newAnchorMin = new Vector2(
                rect.anchorMin.x + rect.offsetMin.x / parentSize.x,
                rect.anchorMin.y + rect.offsetMin.y / parentSize.y
            );

            Vector2 newAnchorMax = new Vector2(
                rect.anchorMax.x + rect.offsetMax.x / parentSize.x,
                rect.anchorMax.y + rect.offsetMax.y / parentSize.y
            );

            // Aplicar cambios
            rect.anchorMin = newAnchorMin;
            rect.anchorMax = newAnchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            EditorUtility.SetDirty(rect);
            modificados++;
        }

        if (modificados > 0)
            Debug.Log($"✅ Anchors to Corners aplicado a {modificados} objeto(s) de UI.");
        else
            Debug.LogWarning("⚠️ Ningún objeto válido fue modificado.");
    }

    // Validación: el menú solo estará activo si hay al menos un RectTransform válido seleccionado
    [MenuItem("Tools/UI Tools/Anchors to Corners _%#a", true)]
    private static bool ValidateAnchorsToCorners()
    {
        if (Selection.gameObjects == null || Selection.gameObjects.Length == 0)
            return false;

        foreach (var go in Selection.gameObjects)
        {
            RectTransform rect = go.GetComponent<RectTransform>();
            if (rect != null && rect.parent != null && rect.parent.GetComponent<RectTransform>() != null)
                return true;
        }

        return false;
    }
}
