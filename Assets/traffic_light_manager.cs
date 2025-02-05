using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrafficLightManager : MonoBehaviour
{
    public int greenDuration = 2;
    public int yellowDuration = 1;
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
                sortedLightList[0].isRed = false;
                sortedLightList[1].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[0].isYellow = true;
                sortedLightList[1].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[0].isYellow = false;
                sortedLightList[0].isRed = true;
                sortedLightList[1].isYellow = false;
                sortedLightList[1].isRed = true;

                sortedLightList[2].isRed = false;
                sortedLightList[3].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[2].isYellow = true;
                sortedLightList[3].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[2].isYellow = false;
                sortedLightList[2].isRed = true;
                sortedLightList[3].isYellow = false;
                sortedLightList[3].isRed = true;
                
                sortedLightList[4].isRed = false;
                sortedLightList[5].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[4].isYellow = true;
                sortedLightList[5].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[4].isYellow = false;
                sortedLightList[4].isRed = true;
                sortedLightList[5].isYellow = false;
                sortedLightList[5].isRed = true;
                                
                sortedLightList[6].isRed = false;
                sortedLightList[7].isRed = false;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[6].isYellow = true;
                sortedLightList[7].isYellow = true;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[6].isYellow = false;
                sortedLightList[6].isRed = true;
                sortedLightList[7].isYellow = false;
                sortedLightList[7].isRed = true;
        }
    }
}
