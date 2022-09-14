using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public bool IsMoving;
    private Vector3 OrigPos, UnitTargetPos;
    private float TimeToMove = 0.2f;

    public IEnumerator MoveSelectedUnit(Vector3 direction, GameObject selectedUnitGO)
    {
        IsMoving = true;

        float ElapsedTime = 0;

        OrigPos = selectedUnitGO.transform.position;
        UnitTargetPos = OrigPos + direction;

        while (ElapsedTime < TimeToMove)
        {
            selectedUnitGO.transform.position = Vector3.Lerp(OrigPos, UnitTargetPos, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        selectedUnitGO.transform.position = UnitTargetPos;
        IsMoving = false;
    }
}
