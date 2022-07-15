using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static System.Reflection.BindingFlags;

public class GetAssetBundleNames : MonoBehaviour {

    [MenuItem ("Assets/Get Asset Bundle Info")]
    static void GetNames ()
    {

        Dictionary<string, string> dic = new Dictionary<string, string>();
        List<List<string>> listofassets = new List<List<string>>();

        string filename = Application.dataPath+"/AssetNamesAndPaths.txt";

        TextWriter tw = new StreamWriter(filename,true);

        var names = AssetDatabase.GetAllAssetBundleNames();

        // foreach (var name in names)
        //     Debug.Log ("Asset Bundle: " + name);
        // string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] {"Assets/Barrier Pack/Prefabs"});
        // foreach (string guid in guids)
        // {
        //     //Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
        //     string myObjectPath = AssetDatabase.GUIDToAssetPath(guid);
        //     Object[] myObjs = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);

        //     //Debug.Log("printing myObs now...");
        //     foreach (Object thisObject in myObjs)
        //     {

        //         if (thisObject.GetType().Name == "BoxCollider"){
        //             // Debug.Log(thisObject.name);
        //             // Debug.Log(thisObject.GetType().Name);
        //             Debug.Log(thisObject);

        //             // Debug.Log(prefab.GetComponent<BoxCollider>().size);
        //         }
        //         // string myType = thisObject.GetType().Name;
        //         // if (myType == "AudioSource")
        //         // {
        //         //     Debug.Log("Audio Source Found in...  " + thisObject.name + " at " + myObjectPath);
        //         // }
        //     }
        // }
        // var labels = typeof(AssetDatabase).GetMethod("GetAllLabels", Static | NonPublic).Invoke(null, null) as Dictionary<string, float>;

        // var labelsAndCounts = labels
        //     .Select(x => (
        //         Label: x.Key,
        //         Count: AssetDatabase.FindAssets($"l:{x.Key}").Length
        //     ))
        //     .OrderByDescending(x => x.Count)
        //     .ThenBy(x => x.Label)
        //     .Select(x => $"{x.Label}: {x.Count}");

        // string msg = string.Join(
        //     separator: "\n",
        //     values: labelsAndCounts
        // );

        // UnityEngine.Debug.Log(msg);

        foreach( var assetBundleName in AssetDatabase.GetAllAssetBundleNames() ){
            foreach( var assetPathAndName in AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName) ){
            // var prefabPath = Path.GetFileNameWithoutExtension(assetPathAndName );
            // print( assetPathAndName );

                GameObject loadedPrefab = PrefabUtility.LoadPrefabContents(assetPathAndName);
                if(loadedPrefab.GetComponent<BoxCollider>() != null){
                    BoxCollider collider = loadedPrefab.GetComponent<BoxCollider>();
                    Debug.Log(assetPathAndName);
                    Debug.Log(collider.bounds.extents.x);

                    string nameWithoutPath = assetPathAndName.Substring( assetPathAndName.LastIndexOf( "/" ) + 1 );
                    string name = nameWithoutPath.Substring( 0, nameWithoutPath.LastIndexOf( "." ) );

                    // dic.Add(name,assetPathAndName);
                    // tw.WriteLine(assetBundleName);
                    // tw.WriteLine("'"+name+"'"+":"+"'"+assetPathAndName+"',");
                    tw.WriteLine(name+","+assetPathAndName+","+assetBundleName+","+collider.bounds.extents.x+","+collider.bounds.extents.y+","+collider.bounds.extents.z);

                    // tw.WriteLine(assetPathAndName);
                }
            }
        }

        // foreach (KeyValuePair<string, string> kvp in dic){
        //         // Debug.Log (kvp.Key + "|" + kvp.Value);
        //         tw.WriteLine(kvp.Key);
        //         tw.WriteLine(kvp.Value);

        // }
        tw.Close();
    }
}