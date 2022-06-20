using UnityEngine;

public static class GizmosExtension
{
	public static Color color { get { return Gizmos.color; } set { Gizmos.color = value; } }

	public static void DrawRect(Vector2 center, Vector2 size)
	{
		Gizmos.DrawWireCube(center, size);
	}

	public static void DrawRectXZ(Vector2 center, Vector2 size)
	{
		DrawRectXZ(new Vector3(center.x, 0, center.y), size);
	}

	public static void DrawRectXZ(Vector3 center, Vector2 size)
	{
		Gizmos.DrawWireCube(center, new Vector3(size.x, 0, size.y));
	}

	public static void DrawCircle(Vector2 center, float radius, float circleDetail = 30)
	{
		float angle = 360 / circleDetail;
		float rad1, rad2;
		Vector2 point1, point2;

		// i to i+1
		for (int i = 0; i < circleDetail - 1; i++)
		{
			rad1 = angle * Mathf.Deg2Rad * i;
			rad2 = angle * Mathf.Deg2Rad * (i + 1);

			point1 = center + new Vector2(Mathf.Cos(rad1), Mathf.Sin(rad1)) * radius;
			point2 = center + new Vector2(Mathf.Cos(rad2), Mathf.Sin(rad2)) * radius;

			Gizmos.DrawLine(point1, point2);
		}

		// last to first
		rad1 = angle * Mathf.Deg2Rad * 0;
		rad2 = angle * Mathf.Deg2Rad * (circleDetail - 1);

		point1 = center + new Vector2(Mathf.Cos(rad1), Mathf.Sin(rad1)) * radius;
		point2 = center + new Vector2(Mathf.Cos(rad2), Mathf.Sin(rad2)) * radius;

		Gizmos.DrawLine(point1, point2);
	}

	public static void DrawCircleXZ(Vector2 center, float radius, float circleDetail = 30)
	{
		DrawCircleXZ(new Vector3(center.x, 0, center.y), radius, circleDetail);
	}

	public static void DrawCircleXZ(Vector3 center, float radius, float circleDetail = 30)
	{
		float angle = 360 / circleDetail;
		float rad1, rad2;
		Vector3 point1, point2;

		// i to i+1
		for (int i = 0; i < circleDetail - 1; i++)
		{
			rad1 = angle * Mathf.Deg2Rad * i;
			rad2 = angle * Mathf.Deg2Rad * (i + 1);

			point1 = center + new Vector3(Mathf.Cos(rad1), 0, Mathf.Sin(rad1)) * radius;
			point2 = center + new Vector3(Mathf.Cos(rad2), 0, Mathf.Sin(rad2)) * radius;

			Gizmos.DrawLine(point1, point2);
		}

		// last to first
		rad1 = angle * Mathf.Deg2Rad * 0;
		rad2 = angle * Mathf.Deg2Rad * (circleDetail - 1);

		point1 = center + new Vector3(Mathf.Cos(rad1), 0, Mathf.Sin(rad1)) * radius;
		point2 = center + new Vector3(Mathf.Cos(rad2), 0, Mathf.Sin(rad2)) * radius;

		Gizmos.DrawLine(point1, point2);
	}

	public static void DrawPoint(Vector2 point, float size = 1)
	{
		Gizmos.DrawLine(new Vector2(point.x - size / 2, point.y), new Vector2(point.x + size / 2, point.y));
		Gizmos.DrawLine(new Vector2(point.x, point.y - size / 2), new Vector2(point.x, point.y + size / 2));
	}

	public static void DrawPoint(Vector3 point, float size = 1)
	{
		Gizmos.DrawLine(new Vector3(point.x - size / 2, point.y, point.z), new Vector3(point.x + size / 2, point.y, point.z));
		Gizmos.DrawLine(new Vector3(point.x, point.y - size / 2, point.z), new Vector3(point.x, point.y + size / 2, point.z));
		Gizmos.DrawLine(new Vector3(point.x, point.y, point.z - size / 2), new Vector3(point.x, point.y, point.z + size / 2));
	}
}
