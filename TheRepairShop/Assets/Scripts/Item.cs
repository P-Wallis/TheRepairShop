using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string m_name;
    [SerializeField] private WorkType m_workRequired;
    [HideInInspector] public Ticket ticket;
    AnimationCurve m_smoothCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    public WorkType GetRequiredWork()
    {
        return m_workRequired;
    }

    public void WorkDone()
    {
        m_workRequired = WorkType.NONE;
    }

    public void MoveToPosition(Transform parent, Vector3 localTargetPosition)
    {
        if (m_movementAnim != null)
            StopCoroutine(m_movementAnim);
        m_movementAnim = StartCoroutine(CoMoveObject(parent, localTargetPosition));
    }

    protected Coroutine m_movementAnim;
    IEnumerator CoMoveObject(Transform parent, Vector3 targetPosition)
    {
        transform.parent = parent;
        Vector3 startPosition = transform.localPosition;
        float dt = GameManager.instance.itemSpeed / (targetPosition - startPosition).magnitude;
        float percent = 0;
        while (percent < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, m_smoothCurve.Evaluate(percent));
            percent += dt * Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
