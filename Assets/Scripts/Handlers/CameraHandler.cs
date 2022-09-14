using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Camera attackCamera;

    public void EnableCamera(Camera camera)
    {
        camera.enabled = true;
    }

    public void DisableCamera(Camera camera)
    {
        camera.enabled = false;
    }

    public void SetAttackCamera(GameObject selectedUnitGO, GameObject selectedEnemyUnitGO, CoordsHandler coordsHandler)
    {
        //Debug.Log("enemy z: " + coordsHandler.GetTileZ(selectedEnemyUnitGO));
        //Debug.Log("friendly z: " + coordsHandler.GetTileZ(selectedUnitGO));
        //Debug.Log("enemy x: " + coordsHandler.GetTileX(selectedEnemyUnitGO));
        //Debug.Log("friendly x: " + coordsHandler.GetTileX(selectedUnitGO));

        //enemy is north of unit
        if (coordsHandler.GetTileZ(selectedEnemyUnitGO) > coordsHandler.GetTileZ(selectedUnitGO))
        {
            int distanceBetween = coordsHandler.GetTileZ(selectedEnemyUnitGO) - coordsHandler.GetTileZ(selectedUnitGO) - 1;
            attackCamera.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            attackCamera.transform.position = new Vector3(coordsHandler.GetTileX(selectedUnitGO) + 2 + distanceBetween, 0.75f, coordsHandler.GetTileZ(selectedUnitGO) + 0.5f);
        }

        //enemy is south of unit
        if (coordsHandler.GetTileZ(selectedEnemyUnitGO) < coordsHandler.GetTileZ(selectedUnitGO))
        {
            int distanceBetween = coordsHandler.GetTileZ(selectedUnitGO) - coordsHandler.GetTileZ(selectedEnemyUnitGO) - 1;
            attackCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            attackCamera.transform.position = new Vector3(coordsHandler.GetTileX(selectedUnitGO) - 2 - distanceBetween, 0.75f, coordsHandler.GetTileZ(selectedUnitGO) - 0.5f);
        }

        //enemy is east of unit
        if (coordsHandler.GetTileX(selectedEnemyUnitGO) > coordsHandler.GetTileX(selectedUnitGO))
        {
            int distanceBetween = coordsHandler.GetTileX(selectedEnemyUnitGO) - coordsHandler.GetTileX(selectedUnitGO) - 1;
            attackCamera.transform.position = new Vector3(coordsHandler.GetTileX(selectedUnitGO) + 0.5f, 0.75f, coordsHandler.GetTileZ(selectedUnitGO) - 2 - distanceBetween);
        }

        //enemy is west of unit
        if (coordsHandler.GetTileX(selectedEnemyUnitGO) < coordsHandler.GetTileX(selectedUnitGO))
        {
            int distanceBetween = coordsHandler.GetTileX(selectedUnitGO) - coordsHandler.GetTileX(selectedEnemyUnitGO) - 1;
            attackCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            attackCamera.transform.position = new Vector3(coordsHandler.GetTileX(selectedUnitGO) - 0.5f, 0.75f, coordsHandler.GetTileZ(selectedUnitGO) + 2 + distanceBetween);
        }
    }

    public void ResetAttackCamera()
    {
        //Quaternion currentRotation = attackCamera.transform.rotation;

        //attackCamera.transform.rotation = Quaternion.Euler(new Vector3(-currentRotation.x, -currentRotation.y, -currentRotation.z));
        attackCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
