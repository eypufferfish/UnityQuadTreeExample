using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeManager : MonoBehaviour
{
    private IQuadTree quadTree;

    private IUserObjectSpawner userObjectSpawner;

    private IQuadTreeVisitor insertQuadTreeVisitor;
    private IQuadTreeVisitor debugRenderingQuadTreeVisitor;

    void Start()
    {
         Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        IRectangle boundary = new Rectangle((int)-width/2, (int)-height/2, (int)width, (int)height);
        Debug.Log(height);
        quadTree = new QuadTree(4, boundary);
        userObjectSpawner = new DefaultUserObjectSpawner(boundary);
        insertQuadTreeVisitor = new InsertQuadTreeVisitor(1, userObjectSpawner);
        debugRenderingQuadTreeVisitor = new DebugRenderingQuadTreeVisitor();
        userObjectSpawner.SetUpperSpawnLimit(1000);
        userObjectSpawner.SetObjectInitialHealth(5);
    }

    // Update is called once per frame
    void Update()
    {

        IUserObject userObject = userObjectSpawner.Spawn();
        IPoint<IUserObject> point = new Point<IUserObject>(userObject.GetShape().GetCenterX(), userObject.GetShape().GetCenterY(), userObject);
        quadTree.Insert(point, insertQuadTreeVisitor);
       
    }

    public void OnDrawGizmos() {
        if (null != debugRenderingQuadTreeVisitor)
            quadTree.Accept(debugRenderingQuadTreeVisitor);
    }
}
