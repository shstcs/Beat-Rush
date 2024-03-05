using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ResourceManager : MonoBehaviour
{
    public bool Loaded { get; private set; }
    private Dictionary<string, UnityEngine.Object> _resources = new();

    private void HandleCallback<T>(string key, AsyncOperationHandle<IList<T>> handle, Action<IList<T>> callback) where T : UnityEngine.Object
    {
        handle.Completed += operationHandle =>
        {
            IList<T> resultList = operationHandle.Result;

            for (int i = 0; i < resultList.Count; i++)
            {
                _resources.Add(resultList[i].name, resultList[i]);
            }
            callback?.Invoke(resultList);
        };
    }

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        //key로 이미 로드된 리소스인지 확인
        //이미 로드된 리소스면 콜백 호출
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += operation =>
        {
            _resources.Add(key, operation.Result);
            callback?.Invoke(operation.Result as T);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        operation.Completed += op =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (IResourceLocation result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, obj =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
        Loaded = true;
    }

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (!_resources.TryGetValue(key, out UnityEngine.Object resource))
            return null;

        return resource as T;
    }

    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        GameObject gameObject = Instantiate(prefab, parent);
        gameObject.name = prefab.name;
        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject == null) return;
        UnityEngine.Object.Destroy(gameObject);
    }
}