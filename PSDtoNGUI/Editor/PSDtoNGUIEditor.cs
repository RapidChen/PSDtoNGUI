using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using PSDGenerator;
using PhotoshopFile;

public class PSDGeneratorWindow:EditorWindow
{
    static string rootName;
    static Object psdObj;
    static UIAtlas uiAtlas;
    static string psdPath;

    public static PSDGeneratorWindow window;

    [MenuItem("DivaBuild/Client Tools/PSD Generator", false, 652)]
    public static void OpenPSDGeneratorWindow()
    {
        window = EditorWindow.GetWindow<PSDGeneratorWindow>();
        
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        rootName = EditorGUILayout.TextField("Root Name:",rootName);
        psdObj = EditorGUILayout.ObjectField("Choose Psd File:", psdObj,typeof(Object), false);
        uiAtlas = (UIAtlas)EditorGUILayout.ObjectField("Choose Atlas File:", uiAtlas, typeof(UIAtlas), false);
        psdPath = AssetDatabase.GetAssetPath(psdObj);
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Generate"))
        {
            PsdFile psd = PSDUtils.ImportPSD(psdPath);
            if (psd != null)
                PSDUtils.Generator(psd, uiAtlas, rootName);
            else
                Debug.LogError("Can not read the PSD Atlass File: " + psdPath);
        }
    }
}
