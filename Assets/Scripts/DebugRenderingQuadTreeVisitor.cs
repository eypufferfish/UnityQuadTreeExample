using System.Collections.Generic;
using UnityEngine;

public class DebugRenderingQuadTreeVisitor : IQuadTreeVisitor
{
    public void Visit(IQuadTree aQuadTree)
    {
        DrawBoundary(aQuadTree.GetBoundary());
        DrawPoints(aQuadTree.GetRootPoints());
    }

    private void DrawBoundary(IRectangle aBoundary)
    {
        UnityEditor.Handles.color = Color.green;
        DrawRectangle(aBoundary);
    }

    private void DrawPoints(List<IPoint<IUserObject>> aPoints)
    {
        if (null != aPoints)
        {

            foreach (IPoint<IUserObject> point in aPoints)
            {
                IShape currentShape = point.GetUserObject().GetShape();
                if (typeof(ICircle).IsAssignableFrom(currentShape.GetType()))
                {
                    UnityEditor.Handles.color = Color.red;
                    DrawCircle((ICircle)currentShape);
                }
                else if (typeof(IRectangle).IsAssignableFrom(currentShape.GetType()))
                {
                    UnityEditor.Handles.color = Color.cyan;
                    DrawRectangle((IRectangle)currentShape);
                }
            }
        }
    }

    private void DrawCircle(ICircle aCircle)
    {
        UnityEditor.Handles.DrawWireDisc(new Vector3(aCircle.GetCenterX(), aCircle.GetCenterY(), 0), new Vector3(0, 0, 0.1f), aCircle.GetRadius());
    }

    private void DrawRectangle(IRectangle aRectangle)
    {
        UnityEditor.Handles.DrawPolyLine(new Vector3(aRectangle.GetX(), aRectangle.GetY()),
         new Vector3(aRectangle.GetX(), aRectangle.GetY() + aRectangle.GetHeight()),
         new Vector3(aRectangle.GetX() + aRectangle.GetWidth(), aRectangle.GetY() + aRectangle.GetHeight()),
         new Vector3(aRectangle.GetX() + aRectangle.GetWidth(), aRectangle.GetY()), new Vector3(aRectangle.GetX(), aRectangle.GetY()));
    }
}
