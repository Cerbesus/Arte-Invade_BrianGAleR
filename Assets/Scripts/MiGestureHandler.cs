using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class MiGestureHandler : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{
    public GameObject prefabTierra;

    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler<Vector3>>(this);
    }


    // public void OnGestureCompleted(InputEventData<Vector3> eventData)
    // {
    //     Debug.Log("Gesture Done");

    //     if (prefabTierra == null)
    //     {
    //         Debug.LogWarning("Prefab de Tierra no asignado en el Inspector de Unity.");
    //         return;
    //     }

    //     if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out MixedRealityPose pose))
    //     {
    //         // Instanciar el prefab de Tierra en la posición de la mano derecha
    //         GameObject tierra = Instantiate(prefabTierra, pose.Position, Quaternion.identity);
    //         // Adjuntar el script FollowHand al objeto instanciado
    //         tierra.AddComponent<FollowHand>();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No se pudo obtener la posición de la mano derecha.");
    //     }
    // }
    public void OnGestureCompleted(InputEventData<Vector3> eventData)
    {
        Debug.Log("Gesture Done");
    }

    public void OnGestureUpdated(InputEventData<Vector3> eventData) { }
    public void OnGestureStarted(InputEventData eventData) { }
    public void OnGestureUpdated(InputEventData eventData) { }
    public void OnGestureCompleted(InputEventData eventData) { }
    public void OnGestureCanceled(InputEventData eventData) { }
}
