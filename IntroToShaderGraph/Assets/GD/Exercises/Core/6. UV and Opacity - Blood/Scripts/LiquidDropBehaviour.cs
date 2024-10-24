using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

//starter script to update the liquid drop shader
public class LiquidDropBehaviour : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private float timeBetweenUpdatesInSecs = 1;                             //e.g. time between showing next drop in sequence

    private int rowCount = 8;
    private int columnCount = 8;
    private int rowID;
    private int columnID;

    private int row = 0;
    private int column = 0;

    private void Start()
    {
        rowID = Shader.PropertyToID("_Current_Row");
        columnID = Shader.PropertyToID("_Current_Column");

        if (material != null)
            StartCoroutine(WaitThenDoSomething());                              //start the drop coroutine
    }

    private IEnumerator WaitThenDoSomething()
    {
        //Debug.Log("Sleeping at..." + Time.time);                          //debug to show time before wait

        for (row = 0; row < rowCount; row++)
        {
            for (column = 0; column < columnCount; column++)
            {
                yield return new WaitForSeconds(timeBetweenUpdatesInSecs);          //waits for user-defined seconds

                material.SetFloat(rowID, row);
                material.SetFloat(columnID, column);

                //   Debug.Log("[" + rowIndex + "," + columnIndex + "]");
            }
        }

        if (row == rowCount && column == columnCount)
        {
            row = 0;
            column = 0;
            //    Debug.Log("[" + rowIndex + "," + columnIndex + "]");
        }

        //Debug.Log("Waking at..." + Time.time);                            //debug to show time after wait
        //perform some action e.g. update shader variables

        StartCoroutine(WaitThenDoSomething());                              //re-start the drop coroutine to repeat
    }

    private void OnDestroy()
    {
        StopAllCoroutines();                                                //stop the coroutine when the script stops
    }
}