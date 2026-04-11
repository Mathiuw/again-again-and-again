using System;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MaiNull
{
    public class UpdateTilemapCollision : MonoBehaviour
    {
        public TilemapCollider2D TilemapCollider2D { get; private set; }

        private void Awake()
        {
            TilemapCollider2D = GetComponent<TilemapCollider2D>();
        }
    }
    
#if UNITY_EDITOR
    
    [CustomEditor(typeof(UpdateTilemapCollision))]
    public class UpdateTilemapCollisionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var instance = (UpdateTilemapCollision)target;
            
            EditorGUILayout.Space();

            if (!GUILayout.Button("Update Tilemap Collision")) return;
            
            if (instance.TilemapCollider2D && instance.TilemapCollider2D.hasTilemapChanges)
            {
                instance.TilemapCollider2D.ProcessTilemapChanges();
                Debug.Log("Processing Tilemap Changes");
            }
            Debug.LogWarning("No tilemap changes detected");
        }
    }
#endif
}