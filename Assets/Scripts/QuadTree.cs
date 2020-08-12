using System.Collections;
using System.Collections.Generic;
public class QuadTree : IQuadTree
{

    private static readonly int LEAF_COUNT = 4;
    private int capacity;
    private IRectangle boundary;
    private List<IPoint<IUserObject>> points;
    private List<IQuadTree> subTrees;

    public QuadTree(int aCapacity, IRectangle aBoundary)
    {
        this.capacity = aCapacity;
        this.boundary = aBoundary;
    }

    public IRectangle GetBoundary()
    {
        return boundary;
    }
    public List<IPoint<IUserObject>> GetRootPoints()
    {
        return points;
    }

    public List<IQuadTree> GetSubTrees()
    {
        return subTrees;
    }

    public bool Insert(IPoint<IUserObject> aPoint, IQuadTreeVisitor aVisitor)
    {
        if (!this.boundary.Contains(aPoint))
        {
            return false;
        }

        aVisitor.Visit(this);

        if (aPoint.GetUserObject().GetHealth() < 1)
        {
            return false;
        }

        if (null == this.points)
        {
            this.points = new List<IPoint<IUserObject>>(this.capacity);
        }

        if (this.points.Count < this.capacity)
        {
            this.points.Add(aPoint);
            return true;
        }
        if (null == this.subTrees)
        {
            this.subdivide();
        }

        foreach (IQuadTree subTree in this.subTrees)
        {
            if (subTree.Insert(aPoint, aVisitor))
            {
                return true;
            }
        }
        return false;
    }

    public void subdivide()
    {
        float halfwidth = this.boundary.GetWidth() / 2;
        float halfheight = this.boundary.GetHeight() / 2;
        this.subTrees = new List<IQuadTree>(LEAF_COUNT);
        QuadTree northwest = new QuadTree(this.capacity, new Rectangle(this.boundary.GetX(), this.boundary.GetY() + halfheight, halfwidth, halfheight));
        QuadTree northeast = new QuadTree(this.capacity, new Rectangle(this.boundary.GetX() + halfwidth, this.boundary.GetY() + halfheight, halfwidth, halfheight));
        QuadTree southwest = new QuadTree(this.capacity, new Rectangle(this.boundary.GetX(), this.boundary.GetY(), halfwidth, halfheight));
        QuadTree southeast = new QuadTree(this.capacity, new Rectangle(this.boundary.GetX() + halfwidth, this.boundary.GetY(), halfwidth, halfheight));
        this.subTrees.Add(southeast);
        this.subTrees.Add(southwest);
        this.subTrees.Add(northwest);
        this.subTrees.Add(northeast);

    }

    public List<IPoint<IUserObject>> GetPoints(IRectangle aTargetBoundary, bool aIsAcceptChildren)
    {
        List<IPoint<IUserObject>> foundPoints = new List<IPoint<IUserObject>>();
        if (null == this.points || !this.boundary.IntersectsWithRectangle(aTargetBoundary))
        {
            return foundPoints;
        }

        foreach (IPoint<IUserObject> p in this.points)
        {
            if (aTargetBoundary.Contains(p))
            {
                foundPoints.Add(p);
            }
        }

        if (aIsAcceptChildren && null != this.subTrees)
        {
            foreach (IQuadTree subTree in this.subTrees)
            {
                foundPoints.AddRange(subTree.GetPoints(aTargetBoundary, aIsAcceptChildren));
            }
        }
        return foundPoints;
    }

    public void RemovePointFromRoot(IPoint<IUserObject> aPoint)
    {
        if (null != this.points)
        {
            this.points.Remove(aPoint);
        }
    }

    public void Accept(IQuadTreeVisitor aVisitor)
    {
        aVisitor.Visit(this);
        if (this.subTrees != null)
        {
            foreach (IQuadTree subTree in this.subTrees)
            {
                subTree.Accept(aVisitor);
            }
        }
    }

}
