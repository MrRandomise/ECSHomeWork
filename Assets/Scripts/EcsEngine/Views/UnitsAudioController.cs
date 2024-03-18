using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Views
{
    public class UnitsAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _startSound;
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private AudioSource _deathSound;

        public void OnStart()
        {
            _startSound.Play();
        }

        public void OnAttack()
        {
            _attackSound.Play();
        }

        public void OnDeath()
        {
            _deathSound.Play();
        }
    }
}
