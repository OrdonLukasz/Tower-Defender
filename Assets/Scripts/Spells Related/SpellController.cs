using UnityEngine;
using Quests.Enemy;
using UnityEngine.AI;
namespace Quests.Spell
{
    public class SpellController : MonoBehaviour
    {
        public float Radius = 10f;
        public LayerMask HitLayer;
        public float ExplosiveForce;

        [HideInInspector] public GameObject sight;

        [SerializeField] private ParticleSystem spellParticles;

        private BoxCollider spellSize;
        private bool wasUsed = false;
        private Vector3 lastPoint;
        private Collider[] Hits;

        private void Start()
        {
            sight.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        }

        private void Update()
        {
            if (!wasUsed)
            {
                transform.position = GetMouseHitPoint();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                wasUsed = true;
                sight.SetActive(false);
                spellParticles.Play();
                Destroy(this.gameObject, 1.5f);

                int maxColliders = 10;
                Collider[] hitColliders = new Collider[maxColliders];

                int hits = Physics.OverlapSphereNonAlloc(transform.position, Radius, hitColliders, HitLayer);

                for (int i = 0; i < hits; i++)
                {
                    hitColliders[i].GetComponent<EnemyNavMeshController>().enabled = false;
                    hitColliders[i].GetComponent<NavMeshAgent>().enabled = false;
                    hitColliders[i].gameObject.AddComponent<Rigidbody>();
                    hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(ExplosiveForce, GetMouseHitPoint(), Radius);
                    hitColliders[i].GetComponent<EnemyController>().enemyHealth.ReactForHit(1000);
                }
            }
        }

        Vector3 GetMouseHitPoint()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                lastPoint = hit.point;
            }
            return lastPoint;
        }
    }
}