
using UnityEngine;
using Object = UnityEngine.Object;
public interface IGetAsset
{
    T GetAsset<T>(string path) where T : Object;
    ResourceRequest GetAssetAsync<T>(string path) where T : Object;
}
