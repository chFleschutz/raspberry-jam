using UnityEngine;

/// <summary>
/// Dedicated Class for detecting Collisions beforehand
/// </summary>
public static class CollisionForecast
{
    /// <summary>
    /// Shoots a Ray from the source objects position in the direction, with the length of the direction and returns either the nearest hit point or the end of the Ray
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector2 ForecastRay2D(GameObject sourceObject, Vector2 direction)
    {
        return ForecastRay2D(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, Physics2D.AllLayers);
    }

    /// <summary>
    /// Shoots a Ray that returns the nearest hit point or the end of the Ray
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector2 ForecastRay2D(GameObject sourceObject, Vector2 origin, Vector2 direction, float distance, LayerMask lm)
    {
        RaycastHit2D[] hits = new RaycastHit2D[20];
        int hitCount = Physics2D.RaycastNonAlloc(origin, direction, hits, distance, lm);
        hits = FilterCollider2D(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit2D closestHit = hits[0];

        foreach (RaycastHit2D hit in hits)
        {
            if ((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    /// <summary>
    /// Shoots a Box from the source objects position in the direction given with the predefined size using the directions length as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Vector2 ForecastBox2D(GameObject sourceObject, Vector2 direction, Vector2 size)
    {
        return ForecastBox2D(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, size, 0, Physics2D.AllLayers);
    }

    /// <summary>
    /// Shoots a Box that returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="size"></param>
    /// <param name="angle"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector2 ForecastBox2D(GameObject sourceObject, Vector2 origin, Vector2 direction, float distance, Vector2 size, float angle, LayerMask lm)
    {
        RaycastHit2D[] hits = new RaycastHit2D[20];
        int hitCount = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, hits, distance, lm);
        hits = FilterCollider2D(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit2D closestHit = hits[0];

        foreach (RaycastHit2D hit in hits)
        {
            if((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    /// <summary>
    /// Shoots a Circle from the source objects position using the length of the direction as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector2 ForecastCircle2D(GameObject sourceObject, Vector2 direction, float radius)
    {
        return ForecastCapsule2D(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, new Vector2(radius, radius), CapsuleDirection2D.Vertical, 0, Physics2D.AllLayers);
    }

    /// <summary>
    /// Shoots a Capsule from the source objects position using the length of the direction as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static Vector2 ForecastCapsule2D(GameObject sourceObject, Vector2 direction, Vector2 size)
    {
        return ForecastCapsule2D(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, size, CapsuleDirection2D.Vertical, 0, Physics2D.AllLayers);
    }

    /// <summary>
    /// Shoots a Capsule that returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="size"></param>
    /// <param name="extentDirection"></param>
    /// <param name="angle"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector2 ForecastCapsule2D(GameObject sourceObject, Vector2 origin, Vector2 direction, float distance, Vector2 size, CapsuleDirection2D extentDirection, float angle, LayerMask lm)
    {
        RaycastHit2D[] hits = new RaycastHit2D[20];
        int hitCount;
        if (size.x == size.y)
            hitCount = Physics2D.CircleCastNonAlloc(origin, size.x, direction, hits, distance, lm);
        else
            hitCount = Physics2D.CapsuleCastNonAlloc(origin, size, extentDirection, angle, direction, hits, distance, lm);

        hits = FilterCollider2D(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit2D closestHit = hits[0];

        foreach (RaycastHit2D hit in hits)
        {
            if ((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    /// <summary>
    /// Shoots a Ray from the source objects position in the direction, with the length of the direction and returns either the nearest hit point or the end of the Ray
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 ForecastRay(GameObject sourceObject, Vector3 direction)
    {
        return ForecastRay(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, Physics.AllLayers);
    }

    /// <summary>
    /// Shoots a Ray that returns the nearest hit point or the end of the Ray
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector3 ForecastRay(GameObject sourceObject, Vector3 origin, Vector3 direction, float distance, LayerMask lm)
    {
        RaycastHit[] hits = new RaycastHit[20];
        int hitCount = Physics.RaycastNonAlloc(origin, direction, hits, distance, lm);
        hits = FilterCollider(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit closestHit = hits[0];

        foreach (RaycastHit hit in hits)
        {
            if ((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    /// <summary>
    /// Shoots a Box from the source objects position in the direction given with the predefined size using the directions length as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="halfSize"></param>
    /// <returns></returns>
    public static Vector3 ForecastBox(GameObject sourceObject, Vector3 direction, Vector3 halfSize)
    {
        return ForecastBox(sourceObject, sourceObject.transform.position, direction.normalized, direction.magnitude, halfSize, Quaternion.identity, Physics.AllLayers);
    }

    /// <summary>
    /// Shoots a Box that returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="halfSize"></param>
    /// <param name="orientation"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector3 ForecastBox(GameObject sourceObject, Vector3 origin, Vector3 direction, float distance, Vector3 halfSize, Quaternion orientation, LayerMask lm)
    {
        RaycastHit[] hits = new RaycastHit[20];
        int hitCount = Physics.BoxCastNonAlloc(origin, halfSize, direction, hits, orientation, distance, lm);
        hits = FilterCollider(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit closestHit = hits[0];

        foreach (RaycastHit hit in hits)
        {
            if ((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    /// <summary>
    /// Shoots a Sphere from the source objects position using the length of the direction as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 ForecastSphere(GameObject sourceObject, Vector3 direction, float radius)
    {
        return ForecastCapsule(sourceObject, sourceObject.transform.position, sourceObject.transform.position, direction, direction.magnitude, radius, Physics.AllLayers);
    }

    /// <summary>
    /// Shoots a Capsule from the source objects position using the length of the direction as distance and returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="direction"></param>
    /// <param name="halfOffset">Defines the offset from the source objects position to define the two midpoints used for creating a capsule</param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 ForecastCapsule(GameObject sourceObject, Vector3 direction, Vector3 halfOffset, float radius)
    {
        return ForecastCapsule(sourceObject, sourceObject.transform.position - halfOffset, sourceObject.transform.position + halfOffset, direction.normalized, direction.magnitude, radius, Physics.AllLayers);
    }

    /// <summary>
    /// Shoots a Capsule that returns the nearest hit point or the end of the check distance
    /// </summary>
    /// <param name="sourceObject"></param>
    /// <param name="origin1"></param>
    /// <param name="origin2"></param>
    /// <param name="direction"></param>
    /// <param name="distance"></param>
    /// <param name="radius"></param>
    /// <param name="lm"></param>
    /// <returns></returns>
    public static Vector3 ForecastCapsule(GameObject sourceObject, Vector3 origin1, Vector3 origin2, Vector3 direction, float distance, float radius, LayerMask lm)
    {
        Vector3 origin = origin1 + ((origin2 - origin1) * 0.5f);
        RaycastHit[] hits = new RaycastHit[20];
        int hitCount;
        if (origin1 == origin2)
            hitCount = Physics.SphereCastNonAlloc(origin, radius, direction, hits, distance, lm);
        else
            hitCount = Physics.CapsuleCastNonAlloc(origin1, origin2, radius, direction, hits, distance, lm);

        hits = FilterCollider(hits, hitCount, out hitCount, true, true, sourceObject);

        if (hitCount <= 0)
            return origin + direction * distance;

        RaycastHit closestHit = hits[0];

        foreach (RaycastHit hit in hits)
        {
            if ((closestHit.point - origin).magnitude > (hit.point - origin).magnitude)
                continue;

            closestHit = hit;
        }

        return closestHit.point;
    }

    private static RaycastHit2D[] FilterCollider2D(RaycastHit2D[] hits, int hitCount, out int newCount, bool ignoreTrigger, bool ignoreSourceObject, GameObject sourceObject)
    {
        int newHitCount = 0;
        RaycastHit2D[] newHits = new RaycastHit2D[20];

        for (int i = hitCount - 1; i >= 0; i--)
        {
            if (ignoreTrigger && hits[i].collider.isTrigger) continue;
            if (ignoreSourceObject && hits[i].collider.gameObject == sourceObject) continue;
            newHits[newHitCount++] = hits[i];
        }

        newCount = newHitCount;

        return newHits;
    }

    private static RaycastHit[] FilterCollider(RaycastHit[] hits, int hitCount, out int newCount, bool ignoreTrigger, bool ignoreSourceObject, GameObject sourceObject)
    {
        int newHitCount = 0;
        RaycastHit[] newHits = new RaycastHit[20];

        for (int i = hitCount - 1; i >= 0; i--)
        {
            if (ignoreTrigger && hits[i].collider.isTrigger) continue;
            if (ignoreSourceObject && hits[i].collider.gameObject == sourceObject) continue;
            newHits[newHitCount++] = hits[i];
        }

        newCount = newHitCount;

        return newHits;
    }
}
