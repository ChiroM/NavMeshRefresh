using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UnityHelper : MonoBehaviour
{
    public static T CloneObject<T>(T objectToClone) where T : UnityEngine.Object
    {
        return Instantiate<T>(objectToClone);
    }

    #region Raycasting
    public static bool GetRaycastHitFromCamera(out RaycastHit hit, string tag = "", int layerMask = 0)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return GetRaycastHitFromRay(ray, out hit, tag, layerMask: layerMask);
    }

    public static bool GetRaycastHitFromPoint(Vector3 position, Vector3 direction, out RaycastHit hit, string tag = "")
    {
        Ray ray = new Ray(position, direction);

        return GetRaycastHitFromRay(ray, out hit, tag);
    }

    private static bool GetRaycastHitFromRay(Ray ray, out RaycastHit hit, string tag = "", int layerMask = 0)
    {
        if (layerMask != 0)
        {
            if (Physics.Raycast(ray, out hit, 10000, ~layerMask))
                return string.IsNullOrEmpty(tag) || hit.transform.gameObject.CompareTag(tag);
        }
        else if (Physics.Raycast(ray, out hit))
            return string.IsNullOrEmpty(tag) || hit.transform.gameObject.CompareTag(tag);

        return false;
    }
    #endregion
}
