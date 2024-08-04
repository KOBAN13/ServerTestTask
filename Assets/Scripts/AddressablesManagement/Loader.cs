using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Loader
{
    public class Loader
    {
        public bool IsLoad { get; private set; } = false;

        public async Task<(T, AsyncOperationHandle<T>)> LoadResources<T>(string nameResources)
        {
            UniTaskCompletionSource<T> isTaskCompletion = new();
            AsyncOperationHandle<T> operationHandle = default;

            try
            {
                operationHandle = Addressables.LoadAssetAsync<T>(nameResources);
                await operationHandle.Task.AsUniTask();

                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    isTaskCompletion.TrySetResult(operationHandle.Result);
                    IsLoad = true;
                }
                else isTaskCompletion.TrySetException(new Exception("Failed load asset"));
            }
            catch (Exception exception)
            {
                isTaskCompletion.TrySetException(exception);
            }

            return (await isTaskCompletion.Task, operationHandle);
        }

        public async Task<(T, AsyncOperationHandle<T>)> LoadResourcesUsingReference<T>(AssetReferenceT<T> resource) where T : Object
        {
            UniTaskCompletionSource<T> isTaskComplete = new();
            AsyncOperationHandle<T> operationHandle = default;

            try
            {
                operationHandle = resource.LoadAssetAsync();
                await operationHandle;

                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    isTaskComplete.TrySetResult(operationHandle.Result);
                }

                isTaskComplete.TrySetException(new Exception("Failed load asset"));
            }
            catch (Exception e)
            {
                isTaskComplete.TrySetException(e);
            }

            return (await isTaskComplete.Task, operationHandle);
        }

        public async Task<(List<T>, AsyncOperationHandle<IList<T>>)> LoadAllResourcesUseLabel<T>(AssetLabelReference labelReference) where T : Object
        {
            UniTaskCompletionSource<List<T>> isTaskCompletionSource = new();
            AsyncOperationHandle<IList<T>> operationHandle = default;

            try
            {
                operationHandle = Addressables.LoadAssetsAsync<T>(labelReference,
                    (objectLoad) => { Debug.Log($"{objectLoad.GetType()} is load"); });

                await operationHandle;

                if (operationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    isTaskCompletionSource.TrySetResult((List<T>)operationHandle.Result);
                }

                isTaskCompletionSource.TrySetException(new Exception("Failed load asset"));
            }
            catch (Exception e)
            {
                isTaskCompletionSource.TrySetException(e);
            }

            return (await isTaskCompletionSource.Task, operationHandle);
        }

        public void ClearMemory<T>(AsyncOperationHandle<T> handle)
        {
            Addressables.Release(handle);
        }

        public void ClearMemoryInstance(GameObject objectClear)
        {
            Addressables.ReleaseInstance(objectClear);
        }
    }
}