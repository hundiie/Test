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

        // ������Ʈ �ɶ����� ���
        yield return request;

        // ������ �ִ��� üũ
        if (request.error != null)
            throw new Exception("ODR request failed: " + request.error);
        // ���� ������ �ε�
        TextureBundle = AssetBundle.LoadFromFile("res://" + resourceName);
        
        request.Dispose();
    }
}
