using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Interesting.Unity
{
    public class CorutineStart : MonoBehaviour
    {
        IEnumerator Start()
        {
            for(int i = 0; i < 10; i++)
            {
                Debug.Log("Elapsed time: " + i);
                yield return new WaitForSeconds(1);
            }
            Debug.Log("Finished");
        }
    }
}
