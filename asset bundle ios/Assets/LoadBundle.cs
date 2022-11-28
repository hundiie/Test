using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

public class LoadBundle : MonoBehaviour
{
    public AssetBundle TextureBundle;
    public Texture[] SaveTexture = new Texture[0];
    public RawImage rawImage;

    void Start()
    {
        StartCoroutine(LoadAsset("textures", "textures"));
    }
    bool c = false;
    private void Update()
    {
        if (SaveTexture.Length != 0 && !c)
        {
            c = true;
            StartCoroutine(LoadSuccess());
        }
    }

    public IEnumerator LoadAsset(string resourceName, string odrTag)
    {
        // IOS Request
        OnDemandResourcesRequest request = OnDemandResources.PreloadAsync(new string[] { odrTag });
        Debug.Log("������Ʈ ����");
        // ������Ʈ �ɶ����� ���
        yield return request;
        Debug.Log("������Ʈ ����");
        // ������ �ִ��� üũ
        if (request.error != null)
            throw new Exception("ODR request failed: " + request.error);
        // ���� ������ �ε�
        TextureBundle = AssetBundle.LoadFromFile("res://" + resourceName);
        Debug.Log("������ �ε�");
        SaveTexture = TextureBundle.LoadAllAssets<Texture>();
        request.Dispose();
    }

    public IEnumerator LoadSuccess()
    {
        int a = 0;
        while (true)
        {
            rawImage.texture = SaveTexture[a % SaveTexture.Length];
            a++;
            yield return new WaitForSeconds(1);
        }
    }
}
