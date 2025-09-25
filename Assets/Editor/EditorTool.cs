using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class EditorTool
{
    [MenuItem("Assets/CustomTool/MergeSprite")]
    public static void MergeSprite()
    {
        string[] spriteGUIDs = Selection.assetGUIDs;
        if(spriteGUIDs==null||spriteGUIDs.Length<=1) return;
        List<string> spritePathList = new List<string>(spriteGUIDs.Length);
        for(int i = 0; i < spriteGUIDs.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(spriteGUIDs[i]);
            spritePathList.Add(assetPath);
        }
        spritePathList.Sort();
        Texture2D firstTex = AssetDatabase.LoadAssetAtPath<Texture2D>(spritePathList[0]);
        int unitHeight = firstTex.height;
        int unitWidth = firstTex.width;
        Texture2D outputTex = new Texture2D(unitWidth*spritePathList.Count, unitHeight);
        for(int i = 0; i < spritePathList.Count; i++)
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(spritePathList[i]);
            Color[] pixels = tex.GetPixels();
            outputTex.SetPixels(i * unitWidth, 0, unitWidth, unitHeight, pixels);
        }
        byte[] bytes = outputTex.EncodeToPNG();
        File.WriteAllBytes(spritePathList[0].Remove(spritePathList[0].LastIndexOf(firstTex.name))+"MergeSprite.png",bytes);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
