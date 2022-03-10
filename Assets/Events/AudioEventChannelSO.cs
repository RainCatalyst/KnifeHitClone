using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "AudioEventChannel", menuName = "Events/Audio Event Channel")]
    public class AudioEventChannelSO : ScriptableObject
    {
        public UnityAction<AudioClip> OnClipPlayed;

        public void PlayClip(AudioClip clip) => OnClipPlayed?.Invoke(clip);
    }
}
