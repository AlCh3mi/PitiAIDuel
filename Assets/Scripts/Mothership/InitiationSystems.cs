using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Mothership
{
    public class InitiationSystems : MonoBehaviour
    {
        [SerializeField] private float bootUpTime;
        [SerializeField] private int threatDetectedThreshold;
        [SerializeField] private Light2D shipLights;
        [SerializeField] private TurretInitSystem[] turrets;

        private bool initialized;
        private int inVicinity;

        [ContextMenu("Boot Up")]
        public void Boot() => StartCoroutine(Initialize());

        private IEnumerator Initialize()
        {
            initialized = true;
            Debug.Log("Booting up Mother ship");
            //play ship boot up sound (something like stadium lights going on)
            
            shipLights.enabled = true;
            
            yield return new WaitForSeconds(bootUpTime);
            
            foreach (var turret in turrets)
            {
                turret.PowerUp();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(!other.CompareTag("Player") || initialized)
                return;

            inVicinity++;
            if(inVicinity >= threatDetectedThreshold)
            {
                Boot();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(initialized)
                return;
            
            if (other.CompareTag("Player"))
                inVicinity = 0;
        }
    }
}