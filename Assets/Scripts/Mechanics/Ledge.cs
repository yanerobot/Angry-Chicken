using UnityEngine;
using System.Collections;

public class Ledge : MonoBehaviour
{
    [SerializeField] private float climbSpeed;
    [SerializeField] private float minDistance = 0.1f;

    [SerializeField] Transform[] transforms;

    void Awake()
    {
        if (transforms == null)
        {
            throw new System.ArgumentNullException("Transforms for teleport are not set!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMain player))
        {
            if (player.InputX == 0) return;
            player.FixPosition();
            player.Freeze();
            StartCoroutine(SmoothMoveTowards(player, 0));
        }
    }

    IEnumerator SmoothMoveTowards(PlayerMain player, int index)
    {
        var fixedUpdate = new WaitForFixedUpdate();
        var distance = Vector2.Distance(player.transform.position, transforms[index].position);
        while (distance > minDistance)
        {
            distance = Vector2.Distance(player.transform.position, transforms[index].position);
            player.transform.position = Vector3.MoveTowards(player.transform.position, transforms[index].position, climbSpeed);
            yield return fixedUpdate;
        }
        if (index ==  transforms.Length - 1)
        {
            player.FreePosition();
            player.UnFreeze();
            yield break;
        }
        StartCoroutine(SmoothMoveTowards(player, index + 1));
    }
}
