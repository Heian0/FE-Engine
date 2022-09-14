using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public GameObject cursor;

    public bool IsMoving;
    private Vector3 OrigPos, UnitTargetPos;
    private float TimeToMove = 0.2f;

    public IEnumerator MoveCursor(Vector3 direction)
    {
        IsMoving = true;

        float ElapsedTime = 0;

        OrigPos = cursor.transform.position;
        UnitTargetPos = OrigPos + direction;

        while (ElapsedTime < TimeToMove)
        {
            cursor.transform.position = Vector3.Lerp(OrigPos, UnitTargetPos, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        cursor.transform.position = UnitTargetPos;
        IsMoving = false;
    }

    public void SetCursorPosition(int x, int y, int z)
    {
        cursor.transform.position = new Vector3(x, y, z);
    }
}
