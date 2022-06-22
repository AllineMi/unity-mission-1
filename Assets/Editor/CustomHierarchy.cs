using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Editor
{
    [InitializeOnLoad]
    public class CustomHierarchy : MonoBehaviour
    {
        private static readonly Color playerColor = new Color(1f, 0.2f, 0.549f, 0.3f);
        private static readonly Color friendColor = new Color(0f, 0.5f, 1f, 0.3f);
        private static readonly Color bigBossColor = new Color(0.9f, 0.2f, 0f, 0.3f);
        private static readonly Color victoryZone = new Color(0f, 0.5f, 0f, 0.3f);
        private static readonly Color shortcutZone = new Color(1f, 0.92f, 0.016f, 0.3f);
        private static readonly Color deathZone = new Color(1f, 0f, 0f, 0.3f);
        private static readonly Color cutscenes = new Color(0f, 0f, 0f, 0.3f);
        private static readonly Color cutscenes1 = new Color(0f, 0f, 0f, 0.3f);

        private static readonly Dictionary<String, Color> _myColors = new Dictionary<string, Color>
        {
            {"Player", playerColor},
            {"BigBoss", bigBossColor},
            {"Friend", friendColor},
            {"ShortcutZone", shortcutZone},
            {"VictoryZone", victoryZone},
            {"DeathZone", deathZone},
            {"Cutscenes", cutscenes}
        };

        static CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj == null) return;

            if (!_myColors.ContainsKey(obj.name)) return;
            EditorGUI.DrawRect(selectionRect, _myColors[obj.name]);
        }
    }
}
