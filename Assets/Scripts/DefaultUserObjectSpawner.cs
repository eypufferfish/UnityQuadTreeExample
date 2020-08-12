using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUserObjectSpawner : IUserObjectSpawner
{

    private int initialHealth = 5;
    private int objectUpperLimit = 1000;
    private Queue<IUserObject> deadObjectsPool = new Queue<IUserObject>();
    private int createdObjectCount;
    private IRectangle worldBounds;
    private float radiusUpperLimit;
    private float widthUpperLimit;
    private float heightUpperLimit;

    public DefaultUserObjectSpawner(IRectangle aWorldBounds)
    {
        this.worldBounds = aWorldBounds;
        this.radiusUpperLimit = Mathf.Min(this.worldBounds.GetWidth(), this.worldBounds.GetHeight()) / 100;
        this.widthUpperLimit = this.worldBounds.GetWidth() / 100;
        this.heightUpperLimit = this.worldBounds.GetHeight() / 100;
    }


    public void SetObjectInitialHealth(int aInitialHealth)
    {
        this.initialHealth = aInitialHealth;
    }


    public void SetUpperSpawnLimit(int aUpperLimit)
    {
        this.objectUpperLimit = aUpperLimit;
    }


    public IUserObject Spawn()
    {
        IUserObject spawnObject;
        if (this.deadObjectsPool.Count > 0)
        {
            spawnObject = this.deadObjectsPool.Dequeue();
            int currentHealth = spawnObject.GetHealth();
            spawnObject.AddHealth(this.initialHealth - currentHealth);
            if (typeof(IRectangle).IsAssignableFrom(spawnObject.GetShape().GetType()))
            {
                reSpawn((IRectangle)spawnObject.GetShape());
            }
            else if (typeof(ICircle).IsAssignableFrom(spawnObject.GetShape().GetType()))
            {
                reSpawn((ICircle)spawnObject.GetShape());
            }
        }
        else if (this.createdObjectCount >= this.objectUpperLimit)
        {
            throw new OverLimitSpawnException(this.createdObjectCount, this.objectUpperLimit);
        }
        else
        {
            if (this.createdObjectCount % 2 == 0)
            {
                spawnObject = new UserObject(this.initialHealth, spawnCircle());
            }
            else
            {
                spawnObject = new UserObject(this.initialHealth, spawnRectangle());
            }
            this.createdObjectCount++;
        }
        return spawnObject;
    }

    private IRectangle spawnRectangle()
    {
        IRectangle rectangle = new Rectangle();
        reSpawn(rectangle);
        return rectangle;
    }

    private void reSpawn(IRectangle aRectangle)
    {
        aRectangle.SetX(Random.Range(0.0001f, this.worldBounds.GetWidth()) + this.worldBounds.GetX());
        aRectangle.SetY(Random.Range(0.0001f, this.worldBounds.GetHeight()) + this.worldBounds.GetY());
        aRectangle.SetWidth(Random.Range(0.001f, this.widthUpperLimit));
        aRectangle.SetHeight(Random.Range(0.001f, this.heightUpperLimit));
    }

    private ICircle spawnCircle()
    {
        ICircle circle = new Circle();
        reSpawn(circle);
        return circle;
    }

    private void reSpawn(ICircle aCircle)
    {
        aCircle.SetRadius(Random.Range(0.001f, this.radiusUpperLimit));
        aCircle.SetCenterX(Random.Range(0.0001f, this.worldBounds.GetWidth()) + this.worldBounds.GetX());
        aCircle.SetCenterY(Random.Range(0.0001f, this.worldBounds.GetHeight()) + this.worldBounds.GetY());
    }


    public void PushBackDeadObject(IUserObject aUserObject)
    {
        this.deadObjectsPool.Enqueue(aUserObject);
    }

}
