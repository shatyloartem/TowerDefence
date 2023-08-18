using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Tilemaps;

namespace TD.Brushes
{
    [CreateAssetMenu(fileName = "New Brush", menuName = "Brushes/Brush")]
    [CustomGridBrush(false, true, false, "New Brush")]
    public class BrushPrefab : GameObjectBrush
    {

    }
}
#endif
