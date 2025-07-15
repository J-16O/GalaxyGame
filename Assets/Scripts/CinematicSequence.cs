using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
public class CinematicSequence : MonoBehaviour
{
   [SerializeField] private PlayableDirector timeline;
   private float idleTime = 5f;
    
    [SerializeField] private CinemachineVirtualCamera innerCamera;
    [SerializeField] private CinemachineVirtualCamera externalCamera;

    private float timer;
    private bool isCinematicPlaying = false;

    private CinemachineVirtualCamera lastActiveGameplayCamera;


    void Update()
    {
        bool playerInput =
            Input.anyKeyDown ||
            Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f ||
            Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f;

        if (playerInput)
        {
            timer = 0f;
            innerCamera.Priority = 15;
            externalCamera.Priority = 15;


            if (isCinematicPlaying)
            {
                // 🛑 Stop cinematic
                timeline.Stop();
                timeline.time = 0;
                isCinematicPlaying = false;

                // 🎯 Reactivate previously active gameplay camera
                if (lastActiveGameplayCamera != null)
                {
                    lastActiveGameplayCamera.Priority = 100;
                }
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= idleTime && !isCinematicPlaying)
            {
                // 🎥 Save which camera was active before cinematic
                if (innerCamera.Priority > externalCamera.Priority)
                    lastActiveGameplayCamera = innerCamera;
                else
                    lastActiveGameplayCamera = externalCamera;

                // Lower both to let Timeline cameras take over
                innerCamera.Priority = 5;
                externalCamera.Priority = 5;

                // 🎬 Start Timeline
                timeline.Play();
                timeline.playableGraph.GetRootPlayable(0).SetSpeed(0.1f);
                isCinematicPlaying = true;
            }
        }
    }
}