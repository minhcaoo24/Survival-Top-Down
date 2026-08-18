using Unity.VisualScripting;
using UnityEngine;

namespace STD.Core.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        public static PlayerAudio Singleton;
        [SerializeField] private AudioSource audioSource;
        [Header("E Sounds")]
        [SerializeField] private AudioClip Graves_E_1;
        [SerializeField] private AudioClip Graves_E_2;

        [Header("W Sounds")]
        [SerializeField] private AudioClip Graves_W_1;
        [SerializeField] private AudioClip Graves_W_2;
        [SerializeField] private AudioClip Graves_W_3;
        [SerializeField] private AudioClip Graves_W_4;

        [Header("Other Sounds")]
        [SerializeField] private AudioClip Graves_Startgame;

        [SerializeField] private int currentE, currentW;

        void Awake()
        {
            Singleton = this;
        }

        private void Start()
        {
            PlayStartGame();
        }

        public void PlayE()
        {
            AudioClip[] clips =
            {
                Graves_E_1,
                Graves_E_2
            };

            int newIndex = GetRandomIndex(clips.Length, currentE);
            currentE = newIndex;

            audioSource.PlayOneShot(clips[currentE]);
        }

        public void PlayW()
        {
            AudioClip[] clips =
            {
                Graves_W_1,
                Graves_W_2,
                Graves_W_3,
                Graves_W_4
            };

            int newIndex = GetRandomIndex(clips.Length, currentW);
            currentW = newIndex;

            audioSource.PlayOneShot(clips[currentW]);
        }

        private int GetRandomIndex(int length, int previousIndex)
        {
            if (length <= 1)
                return 0;

            int index;

            do
            {
                index = Random.Range(0, length);
            }
            while (index == previousIndex);

            return index;
        }

        public void PlayStartGame()
        {
            audioSource.PlayOneShot(Graves_Startgame);
        }
    }
}