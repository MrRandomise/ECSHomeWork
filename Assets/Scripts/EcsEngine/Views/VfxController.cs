using UnityEngine;

namespace EcsEngine.Views
{
    public class VfxController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _damage;
        [SerializeField] private ParticleSystem _destroy;

        public void OnDamage()
        {
            _damage.Play();
        }

        public void OnDestr()
        {
            _destroy.Play();
        }

        public void OnDamageParticleStop()
        {
            _damage.Stop();
        }
    }
}
