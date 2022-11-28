using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class LoadBundle : MonoBehaviour
{
    public AssetBundle TextureBundle;

    void Start()
    {
        StartCoroutine(LoadAsset("textures", "textures"));
    }

    public IEnumerator LoadAsset(string resourceName, string odrTag)
    {
        // IOS Request
        OnDemandResourcesRequest request = OnDemandResources.PreloadAsync(new string[] { odrTag });

        // 리퀘스트 될때까지 대기
        yield return request;

        // 에러가 있는지 체크
        if (request.error != null)
            throw new Exception("ODR request failed: " + request.error);
        // 에셋 버들을 로드
        TextureBundle = AssetBundle.LoadFromFile("res://" + resourceName);
        
        request.Dispose();
    }
}
