using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceTransformEditor : EditorWindow
{

    public List<Transform> trsCurrents = new List<Transform>();
    public Transform trsParentReplace;
    public GameObject objChild;
    public string trsName;
    [MenuItem("Tools/Replace Transform Editor")]
    public static void Open()
    {
        GetWindow<ReplaceTransformEditor>();
    }

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("trsCurrents"));
        EditorGUILayout.PropertyField(obj.FindProperty("trsParentReplace"));
        EditorGUILayout.PropertyField(obj.FindProperty("trsName"));
        EditorGUILayout.PropertyField(obj.FindProperty("objChild"));
        if (trsCurrents.Count == 0)
        {
            EditorGUILayout.HelpBox("trsCurrents is null", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();

        }
        obj.ApplyModifiedProperties();
    }

    void DrawButton() {

        if (GUILayout.Button("Copy transform"))
        {
            CopyTransform();
        }
        if (GUILayout.Button("Delete Child"))
        {
            DeleteChild();
        }
        if (GUILayout.Button("Insert Child"))
        {
            InstatiateChild();
        }
    }

    void InstatiateChild() {
        for (int i = 0; i < trsCurrents.Count; ++i)
        {
            Instantiate(objChild, trsCurrents[i]);
        }
    }

    void DeleteChild() { 
        for (int i = 0; i < trsCurrents.Count; ++i)
        {
            DestroyImmediate(trsCurrents[i].GetChild(0).gameObject);
        }
    }

    void CopyTransform() { 
        for (int i = 0; i < trsCurrents.Count; i++) {
            GameObject objReplace = new GameObject(trsName +"_"+ i);
            objReplace.transform.position = trsCurrents[i].transform.position;
            objReplace.transform.rotation = trsCurrents[i].transform.rotation;
            objReplace.transform.localScale = trsCurrents[i].transform.localScale;
            objReplace.transform.parent = trsParentReplace;
        }
    }
}
