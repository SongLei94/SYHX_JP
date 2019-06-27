﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SYHX.Cards;
using SYHX.AbnormalStatus;

public static class LoadAllSource
{
    static string abnormalStatusPath = "Assets/_SYHX/Scripts/AbnormalStatus/ScriptableObject";
    static string cardPath = "Assets/_SYHX/Scripts/Card/CardScriptableObject";
    static string initializerPath = "Assets/_SYHX/Prefabs/Initializer.prefab";


    [MenuItem("自作/读取/读取全部source")]
    public static void LoadAll()
    {
        var initializer = AssetDatabase.LoadAssetAtPath(initializerPath, typeof(Initializer)) as Initializer;
        Debug.Log(initializer);
        {
            DirectoryInfo dir = new DirectoryInfo(cardPath);
            FileInfo[] files = dir.GetFiles("*.asset");
            foreach (var file in files)
            {
                var card = AssetDatabase.LoadAssetAtPath(cardPath + "/" + file.Name, typeof(CardSource)) as CardSource;
                if (!initializer.cSource.Contains(card))
                {
                    initializer.cSource.Add(card);
                    EditorUtility.SetDirty(card);
                }
            }
        }
        {
            DirectoryInfo dir = new DirectoryInfo(abnormalStatusPath);
            FileInfo[] files = dir.GetFiles("*.asset");
            foreach (var file in files)
            {
                var asSource = AssetDatabase.LoadAssetAtPath(abnormalStatusPath + "/" + file.Name, typeof(AbnormalStatusSource)) as AbnormalStatusSource;
                if (!initializer.asSource.Contains(asSource))
                {
                    initializer.asSource.Add(asSource);
                    EditorUtility.SetDirty(asSource);
                }
            }
        }
        EditorUtility.SetDirty(initializer);
        AssetDatabase.SaveAssets();
    }
}
