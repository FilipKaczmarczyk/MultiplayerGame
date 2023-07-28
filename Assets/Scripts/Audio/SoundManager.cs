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

        private void Awake()
        {
            Instance = this;
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

        private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
        }
        
        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }

        public void PlayFootstepsSound(Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClipsRef.footstep[Random.Range(0, audioClipsRef.footstep.Length)], position, volume);
        }
    }
}
