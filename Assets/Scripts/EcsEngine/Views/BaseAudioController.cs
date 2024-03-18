using UnityEngine;

namespace EcsEngine.Views
{
    public class BaseAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _damageSound;
        [SerializeField] private AudioSource _destroySound;
        [SerializeField] private AudioSource _victorySound;

        public void OnDamage()
        {
            _damageSound.Play();
        }
        
        public void WhenDestroy()
        {
            _destroySound.Play();
        }

        public void OnVictory()
        {
            _victorySound.Play();
        }
    }
}
