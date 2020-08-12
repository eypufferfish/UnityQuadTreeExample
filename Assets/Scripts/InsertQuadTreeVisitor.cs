using System.Collections;
using System.Collections.Generic;
public class InsertQuadTreeVisitor : IQuadTreeVisitor
{

    private IPoint<IUserObject> insertedPoint;
    private int damagePoint;
    private IUserObjectSpawner userObjectSpawner;

    public InsertQuadTreeVisitor(int aDamagePoint, IUserObjectSpawner aUserObjectSpawner)
    {
        this.damagePoint = aDamagePoint;
        this.userObjectSpawner = aUserObjectSpawner;
    }

    public void setInsertedPoint(IPoint<IUserObject> aInsertedPoint)
    {
        this.insertedPoint = aInsertedPoint;
    }


    public void Visit(IQuadTree aQuadTree)
    {
        if (null != this.insertedPoint)
        {
            IUserObject insertedUserObject = this.insertedPoint.GetUserObject();
            IShape insertedShape = insertedUserObject.GetShape();
            IRectangle targetBoundary = calculateTargetBoundary(insertedShape);
            ICollection<IPoint<IUserObject>> points = aQuadTree.GetPoints(targetBoundary, false);
            foreach (IPoint<IUserObject> currentPoint in points)
            {
                visit(currentPoint);
                if (currentPoint.GetUserObject().GetHealth() < 1)
                {
                    aQuadTree.RemovePointFromRoot(currentPoint);
                    this.userObjectSpawner.PushBackDeadObject(currentPoint.GetUserObject());
                }
                if (insertedUserObject.GetHealth() < 1)
                {
                    aQuadTree.RemovePointFromRoot(this.insertedPoint);
                    this.userObjectSpawner.PushBackDeadObject(insertedUserObject);
                    break;
                }
            }
        }
    }

    public void setUserObjectSpawner(IUserObjectSpawner aUserObjectSpawner)
    {
        this.userObjectSpawner = aUserObjectSpawner;
    }

    private void visit(IPoint<IUserObject> aPoint)
    {
        IUserObject currentUserObject = aPoint.GetUserObject();
        IShape currentShape = currentUserObject.GetShape();
        IUserObject insertedUserObject = this.insertedPoint.GetUserObject();
        IShape insertedShape = insertedUserObject.GetShape();
        bool collision = checkCollision(currentShape, insertedShape);
        if (collision)
        {
            insertedUserObject.TakeDamage(this.damagePoint);
            currentUserObject.TakeDamage(this.damagePoint);
        }
    }

    private bool checkCollision(IShape currentShape, IShape insertedShape)
    {
        bool collision = false;
        if (typeof(ICircle).IsAssignableFrom(currentShape.GetType()))
        {
            collision = insertedShape.IntersectsWithCircle((ICircle)currentShape);
        }
        else if (typeof(IRectangle).IsAssignableFrom(currentShape.GetType()))
        {
            collision = insertedShape.IntersectsWithRectangle((IRectangle)currentShape);
        }
        return collision;
    }



    private IRectangle calculateTargetBoundary(IShape aShape)
    {
        IRectangle targetBoundary;
        if (typeof(ICircle).IsAssignableFrom(aShape.GetType()))
        {
            ICircle circleUserObject = (ICircle)aShape;
            float width = circleUserObject.GetRadius() * 2;
            targetBoundary = new Rectangle(circleUserObject.GetCenterX() - circleUserObject.GetRadius(), circleUserObject.GetCenterY() - circleUserObject.GetRadius(), width,
                    width);
        }
        else
        {
            targetBoundary = (IRectangle)aShape;
        }
        return targetBoundary;
    }

}
