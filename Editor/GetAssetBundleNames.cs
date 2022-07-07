using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetAssetBundleNames : MonoBehaviour {
    
    [MenuItem ("Assets/Get Asset Bundle Info")]
    static void GetNames ()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        List<List<string>> listofassets = new List<List<string>>(); 

        string filename = Application.dataPath+"/AssetNamesAndPaths.txt";
    
        TextWriter tw = new StreamWriter(filename,true);
        
        var names = AssetDatabase.GetAllAssetBundleNames();

        foreach (var name in names)
            Debug.Log ("Asset Bundle: " + name);

        foreach( var assetBundleName in AssetDatabase.GetAllAssetBundleNames() ){
            foreach( var assetPathAndName in AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName) ){
            // var name = Path.GetFileNameWithoutExtension(assetPathAndName );
            // print( assetPathAndName );
            string nameWithoutPath = assetPathAndName.Substring( assetPathAndName.LastIndexOf( "/" ) + 1 );
            string name = nameWithoutPath.Substring( 0, nameWithoutPath.LastIndexOf( "." ) );
            dic.Add(name,assetPathAndName);
            }
        }

        foreach (KeyValuePair<string, string> kvp in dic){
                Debug.Log (kvp.Key + "|" + kvp.Value);
                tw.WriteLine("asset name: " + kvp.Key);
                tw.WriteLine("asset path: " + kvp.Value);
                  
        }
        tw.Close();        
    }
}