using System.Collections;
using UnityEngine;

public class SawRotator : MonoBehaviour
{
    [SerializeField] HingeJoint2D joint;
    [SerializeField] Switcher switcher;
    [SerializeField] Rigidbody2D handleRB;
    [SerializeField] Animator sawAnimator;
    [SerializeField] Collider2D sawCollider;



    Coroutine rotation;
    bool isActivated;


    bool right, left;
    void Start()
    {
        switcher.OnActivate += Activate;
    }

    void Activate(bool isActivated)
    {
        this.isActivated = isActivated;
        handleRB.isKinematic = false;
        sawAnimator.SetBool("isActivated", true);
        sawCollider.enabled = true;
        if (rotation == null)
            rotation = StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        if (joint == null ||
            joint.attachedRigidbody.isKinematic) yield break;

        while (isActivated)
        {
            right = joint.jointAngle > 0 && !right;
            left = joint.jointAngle < -180 && !left;

            if (right || left)
            {
                var m = joint.motor;
                m.motorSpeed = -m.motorSpeed;
                joint.motor = m;

                if (right) left = false;
                if (left) right = false;
            }
            yield return new WaitForFixedUpdate();
        }
        handleRB.isKinematic = false;
        sawAnimator.SetBool("isActivated", false);
        sawCollider.enabled = false;
        rotation = null;
    }
}
