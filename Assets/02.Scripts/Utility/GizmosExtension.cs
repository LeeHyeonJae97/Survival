using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtension
{
	public enum FrameType { Inner, Center, Outer }

	public static Color color { get { return Gizmos.color; } set { Gizmos.color = value; } }

	public static float circleDetail = 30;

	public static void DrawRect(Vector2 leftBottom, Vector2 rightTop)
	{
		Vector2 leftTop = new Vector2(leftBottom.x, rightTop.y);
		Vector2 rightBottom = new Vector2(rightTop.x, leftBottom.y);

		Gizmos.DrawLine(leftBottom, leftTop);
		Gizmos.DrawLine(leftTop, rightTop);
		Gizmos.DrawLine(rightTop, rightBottom);
		Gizmos.DrawLine(rightBottom, leftBottom);
	}

	public static void DrawRectXZ(Vector2 leftBottom, Vector3 rightTop)
	{
		Vector3 leftBottom3 = new Vector3(leftBottom.x, 0, leftBottom.y);
		Vector3 rightTop3 = new Vector3(rightTop.x, 0, rightTop.y);
		Vector3 leftTop = new Vector3(leftBottom.x, 0, rightTop.y);
		Vector3 rightBottom = new Vector3(rightTop.x, 0, leftBottom.y);

		Gizmos.DrawLine(leftBottom3, leftTop);
		Gizmos.DrawLine(leftTop, rightTop3);
		Gizmos.DrawLine(rightTop3, rightBottom);
		Gizmos.DrawLine(rightBottom, leftBottom3);
	}

	public static void DrawRect(Vector2 center, float sizeX, float sizeY)
	{
		Vector2 leftBottom = new Vector2(center.x - sizeX / 2, center.y - sizeY / 2);
		Vector2 rightTop = new Vector2(center.x + sizeX / 2, center.y + sizeY / 2);

		DrawRect(leftBottom, rightTop);
	}

	public static void DrawRectXZ(Vector2 center, float sizeX, float sizeY)
	{
		Vector2 leftBottom = new Vector2(center.x - sizeX / 2, center.y - sizeY / 2);
		Vector2 rightTop = new Vector2(center.x + sizeX / 2, center.y + sizeY / 2);

		DrawRectXZ(leftBottom, rightTop);
	}

	public static void DrawRectFrame(Vector2 leftBottom, Vector2 rightTop, float thickness, FrameType type = FrameType.Center)
	{
		Vector2 offset;
		Vector2 outerLeftBottom;
		Vector2 outerRightTop;
		Vector2 innerLeftBottom;
		Vector2 innerRightTop;

		switch (type)
		{
			case FrameType.Inner:
				offset = Vector2.one * thickness;
				outerLeftBottom = leftBottom;
				outerRightTop = rightTop;
				innerLeftBottom = leftBottom + offset;
				innerRightTop = rightTop - offset;
				break;

			case FrameType.Center:
				offset = Vector2.one * thickness / 2;
				outerLeftBottom = leftBottom - offset;
				outerRightTop = rightTop + offset;
				innerLeftBottom = leftBottom + offset;
				innerRightTop = rightTop - offset;
				break;

			case FrameType.Outer:
				offset = Vector2.one * thickness;
				outerLeftBottom = leftBottom - offset;
				outerRightTop = rightTop + offset;
				innerLeftBottom = leftBottom;
				innerRightTop = rightTop;
				break;

			default:
				return;
		}

		DrawRect(outerLeftBottom, outerRightTop);
		DrawRect(innerLeftBottom, innerRightTop);
	}

	public static void DrawRectFrameXZ(Vector3 leftBottom, Vector3 rightTop, float thickness, FrameType type = FrameType.Center)
	{
		Vector3 offset;
		Vector3 outerLeftBottom;
		Vector3 outerRightTop;
		Vector3 innerLeftBottom;
		Vector3 innerRightTop;

		switch (type)
		{
			case FrameType.Inner:
				offset = Vector3.one * thickness;
				outerLeftBottom = leftBottom;
				outerRightTop = rightTop;
				innerLeftBottom = leftBottom + offset;
				innerRightTop = rightTop - offset;
				break;

			case FrameType.Center:
				offset = Vector3.one * thickness / 2;
				outerLeftBottom = leftBottom - offset;
				outerRightTop = rightTop + offset;
				innerLeftBottom = leftBottom + offset;
				innerRightTop = rightTop - offset;
				break;

			case FrameType.Outer:
				offset = Vector3.one * thickness;
				outerLeftBottom = leftBottom - offset;
				outerRightTop = rightTop + offset;
				innerLeftBottom = leftBottom;
				innerRightTop = rightTop;
				break;

			default:
				return;
		}

		DrawRect(outerLeftBottom, outerRightTop);
		DrawRect(innerLeftBottom, innerRightTop);
	}

	public static void DrawRectFrame(Vector2 center, float sizeX, float sizeY, float thickness, FrameType type = FrameType.Center)
	{
		Vector2 leftBottom = new Vector2(center.x - sizeX / 2, center.y - sizeY / 2);
		Vector2 rightTop = new Vector2(center.x + sizeX / 2, center.y + sizeY / 2);

		DrawRectFrame(leftBottom, rightTop, thickness, type);
	}

	public static void DrawRectFrameXZ(Vector3 center, float sizeX, float sizeY, float thickness, FrameType type = FrameType.Center)
	{
		Vector3 leftBottom = new Vector3(center.x - sizeX / 2, 0, center.z - sizeY / 2);
		Vector3 rightTop = new Vector3(center.x + sizeX / 2, 0, center.z + sizeY / 2);

		DrawRectFrameXZ(leftBottom, rightTop, thickness, type);
	}

	public static void DrawCircle(Vector2 center, float radius)
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

	public static void DrawCircleXZ(Vector3 center, float radius)
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
