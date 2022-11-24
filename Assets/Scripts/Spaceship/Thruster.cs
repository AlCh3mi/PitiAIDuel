using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Spaceship
{
    public class Thruster : MonoBehaviour
    {
        [SerializeField] private float lightIntensity = 2.5f;
        [SerializeField] private ParticleSystem thrusterParticle;
        [SerializeField] private Light2D light2D;
        [SerializeField] private AudioSource thrusterAudio;
        [SerializeField] private float restingPitch = 1f;
        [SerializeField] private float fullSpeedPitch = 1.8f;
        
        private InputHandler inputHandler;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            if(!inputHandler.Thruster)
            {
                thrusterParticle.Stop();
                //thrusterAudio.Pause();
                thrusterAudio.pitch = Mathf.Lerp(thrusterAudio.pitch, restingPitch, Time.deltaTime);
                light2D.intensity = Mathf.Lerp(light2D.intensity, 0f, Time.deltaTime);
                return;
            }

            thrusterParticle.Play();
            //thrusterAudio.UnPause();
            thrusterAudio.pitch = Mathf.Lerp(thrusterAudio.pitch, fullSpeedPitch, Time.deltaTime);
            light2D.intensity = Mathf.Lerp(light2D.intensity, lightIntensity, Time.deltaTime);
        }
    }
}