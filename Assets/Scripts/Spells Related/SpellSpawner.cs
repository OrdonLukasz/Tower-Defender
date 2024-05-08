using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests.Spell
{
    public class SpellSpawner : MonoBehaviour
    {
        public GameObject sightPrefab;
        public void SpawnSpellPrefab(GameObject SpellPrefab)
        {
            GameObject spell = Instantiate(SpellPrefab);
            spell.GetComponent<SpellController>().sight = Instantiate(sightPrefab, spell.transform);
            spell.transform.localPosition = new Vector3(0, 1, 0);
        }
    }
}
