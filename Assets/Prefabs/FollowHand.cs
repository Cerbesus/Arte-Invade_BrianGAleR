using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    private TrackedHandJoint handJoint;

    private void Start()
    {
        handJoint = TrackedHandJoint.IndexTip;
    }

    private void Update()
    {
        if (HandJointUtils.TryGetJointPose(handJoint, Handedness.Right, out MixedRealityPose pose))
        {
            transform.position = pose.Position;
        }
    }
}
