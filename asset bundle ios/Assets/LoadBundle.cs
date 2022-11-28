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
        Debug.Log("리퀘스트 시작");
        // 리퀘스트 될때까지 대기
        yield return request;
        Debug.Log("리퀘스트 끝남");
        // 에러가 있는지 체크
        if (request.error != null)
            throw new Exception("ODR request failed: " + request.error);
        // 에셋 버들을 로드
        TextureBundle = AssetBundle.LoadFromFile("res://" + resourceName);
        Debug.Log("데이터 로드");
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
