/*
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Upload))] 
public class UploadToServer : Editor
{
    private Upload _upload;

    private void OnEnable()
    {
        _upload = (Upload)target;
    }


    public override async void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Label("Upload Files To Server", EditorStyles.boldLabel);
        GUILayout.Space(20f);

        if (GUILayout.Button("UPLOAD"))
        {
            await _upload.UploadToServer();
        }
    }
}
*/
