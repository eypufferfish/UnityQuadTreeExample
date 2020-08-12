
using System.Collections;
using System.Collections.Generic;

public interface IQuadTree : IVisitable<IQuadTreeVisitor>
{


    IRectangle GetBoundary();

    List<IPoint<IUserObject>> GetRootPoints();
    bool Insert(IPoint<IUserObject> aPoint, IQuadTreeVisitor aVisitor);

    List<IPoint<IUserObject>> GetPoints(IRectangle aTargetBoundary, bool aIsAcceptChildren);

    void RemovePointFromRoot(IPoint<IUserObject> aPoint);
}
