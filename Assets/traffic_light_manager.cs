using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrafficLightManager : MonoBehaviour
{
    public int greenDuration = 5;
    public int yellowDuration = 2;
        void Start()
    {
        StartCoroutine(ManageTrafficLights());
    }

    IEnumerator ManageTrafficLights()
    {
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();

        while (true)
        {
                Debug.Log("lights running");
                sortedLightList[0].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[0].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[0].isYellow = false;
                sortedLightList[0].isRed = true;

                sortedLightList[1].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[1].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[1].isYellow = false;
                sortedLightList[1].isRed = true;

                sortedLightList[2].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[2].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[2].isYellow = false;
                sortedLightList[2].isRed = true;
                
                sortedLightList[3].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[3].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[3].isYellow = false;
                sortedLightList[3].isRed = true;
        }
    }
}
