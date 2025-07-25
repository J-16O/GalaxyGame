using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour
{
    public GameObject introductionCamera;
    public GameObject internalCamera;
    public GameObject externalCamera;
    public PlayableDirector cinematicTimeline;

    private float introDuration = 5f;
    private float idleTimer = 0f;
    private float idleThreshold = 5f;
    private bool isMonitoringIdle = false;
    private bool isCinematicPlaying = false;
    private bool useInternal = true;

    void Start()
    {
        SetActiveCamera(introductionCamera);
        internalCamera.SetActive(false);
        externalCamera.SetActive(false);
        cinematicTimeline.Stop();

        Invoke(nameof(EnablePlayerView), introDuration);
    }

    void EnablePlayerView()
    {
        SetActiveCamera(useInternal ? internalCamera : externalCamera);
        isMonitoringIdle = true;
        idleTimer = 0f;
    }

    void Update()
    {
        // Kamera değiştir (R tuşuyla)
        if (Input.GetKeyDown(KeyCode.R))
        {
            useInternal = !useInternal;
            SetActiveCamera(useInternal ? internalCamera : externalCamera);
        }

        if (isCinematicPlaying)
        {
            if (PlayerInputDetected())
            {
                cinematicTimeline.Stop();
                EnablePlayerView();
                isCinematicPlaying = false;
            }
            return;
        }

        if (isMonitoringIdle)
        {
            if (PlayerInputDetected())
            {
                idleTimer = 0f;
            }
            else
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleThreshold)
                {
                    StartCinematic();
                }
            }
        }
    }

    void StartCinematic()
    {
        isMonitoringIdle = false;
        isCinematicPlaying = true;
        cinematicTimeline.Play();
    }

    bool PlayerInputDetected()
    {
        return Input.anyKey || Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0;
    }

    void SetActiveCamera(GameObject cam)
    {
        introductionCamera.SetActive(false);
        internalCamera.SetActive(false);
        externalCamera.SetActive(false);
        cam.SetActive(true);
    }
}