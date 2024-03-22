using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asegúrate de que el videoPlayer no sea nulo
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Suscribir al evento de finalización del video
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        // Cambiar a la siguiente escena cuando el video termine
        SceneManager.LoadScene("MainDemo");
    }
}
