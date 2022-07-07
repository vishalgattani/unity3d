using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleObjectLoaderAsync : MonoBehaviour
{
    // Start is called before the first frame update
    public string AssetName;
    public string BundleName;
    IEnumerator Start()
    {
        
        string filePath = Application.dataPath;
        filePath = System.IO.Path.Combine(filePath, "AssetBundles/barrierpack");
        // Debug.Log(filePath);
        AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return asyncBundleRequest;

        AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;

        if (localAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(AssetName);
        yield return assetRequest;
        GameObject prefab = assetRequest.asset as GameObject;
        Instantiate(prefab);
        localAssetBundle.Unload(false);
        // Debug.Log("AssetBundles/barrierpack");

    }
}
