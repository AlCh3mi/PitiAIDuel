using UnityEngine;

namespace Audio
{
    public class PlayMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private bool playAtStart;
        [SerializeField] private bool restartIfSameClip;
        [SerializeField] private bool loop;

        private bool IsSameClip => AudioManager.Instance.CurrentClip == musicClip;
        
        public void Start()
        {
            if(!playAtStart)
                return;
        
            Play();
        }
        
        public void Play()
        {
            if (restartIfSameClip || !IsSameClip)
                AudioManager.Instance.PlayMusic(musicClip, loop);
        }
    }
}