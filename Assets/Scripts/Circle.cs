using System;
public class Circle : ICircle
{

    private float centerX;
    private float centerY;
    private float radius;

    public Circle()
    {
    }

    public Circle(float aCenterX, float aCenterY, float aRadius)
    {
        this.centerX = aCenterX;
        this.centerY = aCenterY;
        this.radius = aRadius;
    }


    public float GetCenterX()
    {
        return this.centerX;
    }


    public float GetCenterY()
    {
        return this.centerY;
    }


    public float GetRadius()
    {
        return this.radius;
    }

    public void SetCenterX(float aCenterX)
    {
        this.centerX = aCenterX;
    }

    public void SetCenterY(float aCenterY)
    {
        this.centerY = aCenterY;
    }

    public void SetRadius(float aRadius)
    {
        this.radius = aRadius;
    }

    public bool IntersectsWithRectangle(IRectangle aRectangle)
    {
        return aRectangle.IntersectsWithCircle(this);
    }

    public bool IntersectsWithCircle(ICircle aCircle)
    {
        float distanceX = this.GetCenterX() - aCircle.GetCenterX();
        float distanceY = this.GetCenterY() - aCircle.GetCenterY();
        float distance = (int)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        return distance <= this.GetRadius() + aCircle.GetRadius();
    }

}
