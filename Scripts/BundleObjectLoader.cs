using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BundleObjectLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string AssetName;
    public string BundleName;
    void Start()
    {
        string filePath = Application.dataPath;
        filePath = System.IO.Path.Combine(filePath, "AssetBundles/barrierpack");
        // Debug.Log(filePath);
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(filePath);
        if (localAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        GameObject asset = localAssetBundle.LoadAsset<GameObject>(AssetName);
        Instantiate(asset);
        localAssetBundle.Unload(false);
        // Debug.Log("AssetBundles/barrierpack");

    }

    
}
