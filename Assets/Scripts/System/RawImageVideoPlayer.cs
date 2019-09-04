using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class RawImageVideoPlayer : MonoBehaviour
{
    private RawImage targetUIImage;
    private VideoPlayer videoPlayer;
    private RenderTexture targetRT;

    public void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        targetUIImage = GetComponent<RawImage>();

        targetRT = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 0,
            RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = targetRT;

        targetUIImage.texture = targetRT;
    }
}
