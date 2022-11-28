using UnityEngine;
using UnityEditor;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class BuildiOSAssetBundles : MonoBehaviour
{
    [InitializeOnLoadMethod]
    static void SetupResourcesBuild()
    {
        UnityEditor.iOS.BuildPipeline.collectResources += CollectResources;
    }

    static UnityEditor.iOS.Resource[] CollectResources()
    {
        return new UnityEditor.iOS.Resource[]
        {
            // textures 레이블에 태그된 파일을 가져다가 textures 이름을 가지는 에셋 번들 파일을 Assets/ODR 폴더에 생성
            new UnityEditor.iOS.Resource( "textures", "Assets/ODR/textures" ).AddOnDemandResourceTags( "textures" ),
            // 다른 프로젝트나 서드 파티 판매자가 이미 빌드한 bundle 이라는 이름을 가지는 에셋 번들을 추가하는 역할
            new UnityEditor.iOS.Resource( "bundle", "Assets/Bundles/bundle.unity3d" ).AddOnDemandResourceTags( "bundle" ),
        };
    }

    [MenuItem("Bundle/Build iOS AssetBundle")]
    static void BuildAssetBundles()
    {
        var options = BuildAssetBundleOptions.None;

        bool shouldCheckODR = EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;

#if UNITY_TVOS
            shouldCheckODR |= EditorUserBuildSettings.activeBuildTarget == BuildTarget.tvOS;
#endif

        if (shouldCheckODR)
        {
#if ENABLE_IOS_ON_DEMAND_RESOURCES
            if (PlayerSettings.iOS.useOnDemandResources)
                options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif

#if ENABLE_IOS_APP_SLICING
            options |= BuildAssetBundleOptions.UncompressedAssetBundle;
#endif
        }

        BuildPipeline.BuildAssetBundles("Assets/ODR", options, EditorUserBuildSettings.activeBuildTarget);
    }

}