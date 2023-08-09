using System;
using Counters;
using Orders;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [SerializeField] private AudioClipRefsSO audioClipsRef;

        public int Volume { get; private set; } = 5;

        private const string PlayerPrefsSoundEffectVolume = "SoundEffectVolume";
        
        private void Awake()
        {
            Instance = this;

            Volume = PlayerPrefs.GetInt(PlayerPrefsSoundEffectVolume, 5);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
            PlayerController.OnPickupSomething += PlayerControllerOnPickupSomething;
            BaseCounter.OnAnyObjectPlaced += BaseCounterOnAnyObjectPlaced;
            TrashCounter.OnAnyObjectTrashed += TrashCounterOnAnyObjectTrashed;
        }

        private void TrashCounterOnAnyObjectTrashed(object sender, EventArgs e)
        {
            var trash = sender as TrashCounter;
            PlaySound(audioClipsRef.trash, trash.transform.position);
        }

        private void BaseCounterOnAnyObjectPlaced(object sender, EventArgs e)
        {
            var baseCounter = sender as BaseCounter;
            PlaySound(audioClipsRef.objectDrop, baseCounter.transform.position);
        }

        private void PlayerControllerOnPickupSomething(object sender, EventArgs e)
        {
            var player = sender as PlayerController;
            PlaySound(audioClipsRef.objectPick, player.transform.position);
        }

        private void CuttingCounterOnAnyCut(object sender, EventArgs e)
        {
            var cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipsRef.chop, cuttingCounter.transform.position);
        }

        private void DeliveryManagerOnRecipeFailed(object sender, EventArgs e)
        {
            PlaySound(audioClipsRef.deliveryFail, Camera.main.transform.position);
        }

        private void DeliveryManagerOnRecipeSuccess(object sender, EventArgs e)
        {
            PlaySound(audioClipsRef.deliverySuccess, Camera.main.transform.position);
        }

        private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f)
        {
            PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier);
        }
        
        private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * (Volume * 0.1f));
        }

        public void PlayFootstepsSound(Vector3 position, float volumeMultiplier = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClipsRef.footstep[Random.Range(0, audioClipsRef.footstep.Length)], position, volumeMultiplier * (Volume * 0.1f));
        }

        public void ChangeVolume()
        {
            Volume++;

            if (Volume > 10)
            {
                Volume = 0;
            }
            
            PlayerPrefs.SetInt(PlayerPrefsSoundEffectVolume, Volume);
            PlayerPrefs.Save();
        }
    }
}
