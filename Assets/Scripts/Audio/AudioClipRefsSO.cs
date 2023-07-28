using UnityEngine;

namespace Audio
{
    //[CreateAssetMenu(menuName = "Audio Tools/Audio Clip Refs")] ONLY ONE WE NEED
    public class AudioClipRefsSO : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPick;
        public AudioClip stoveSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}
