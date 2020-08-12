using System;
public class Rectangle : IRectangle
{
    private float x;
    private float y;
    private float width;
    private float height;

    public Rectangle()
    {
    }

    public Rectangle(float aX, float aY, float aWidth, float aHeight)
    {
        this.x = aX;
        this.y = aY;
        this.width = aWidth;
        this.height = aHeight;
    }


    public float GetX()
    {
        return this.x;
    }


    public float GetY()
    {
        return this.y;
    }


    public float GetWidth()
    {
        return this.width;
    }


    public float GetHeight()
    {
        return this.height;
    }


    public void SetX(float aX)
    {
        this.x = aX;
    }


    public void SetY(float aY)
    {
        this.y = aY;
    }


    public void SetWidth(float aWidth)
    {
        this.width = aWidth;
    }


    public void SetHeight(float aHeight)
    {
        this.height = aHeight;
    }


    public bool Contains(IPoint<IUserObject> aPoint)
    {
        return this.width > 0 && this.height > 0 && aPoint.GetX() >= this.x && aPoint.GetX() < this.x + this.width && aPoint.GetY() >= this.y
                && aPoint.GetY() < this.y + this.height;
    }


    public bool IntersectsWithRectangle(IRectangle aRectangle)
    {
        return aRectangle.GetWidth() > 0 && aRectangle.GetHeight() > 0 && this.width > 0 && this.height > 0 && aRectangle.GetX() < this.x + this.width
                && aRectangle.GetX() + aRectangle.GetWidth() > this.x && aRectangle.GetY() < this.y + this.height && aRectangle.GetY() + aRectangle.GetHeight() > this.y;
    }


    public bool IntersectsWithCircle(ICircle aCircle)
    {
        float testX = aCircle.GetCenterX();
        float testY = aCircle.GetCenterY();

        if (aCircle.GetCenterX() < this.GetX())
        {
            testX = this.GetX();
        }
        else if (aCircle.GetCenterX() > this.GetX() + this.GetWidth())
        {
            testX = this.GetX() + this.GetWidth();
        }
        if (aCircle.GetCenterY() < this.GetY())
        {
            testY = this.GetY();
        }
        else if (aCircle.GetCenterY() > this.GetY() + this.GetHeight())
        {
            testY = this.GetY() + this.GetHeight();
        }

        float distanceX = aCircle.GetCenterX() - testX;
        float distanceY = aCircle.GetCenterY() - testY;
        float distance = (int)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));

        return distance <= aCircle.GetRadius();
    }



    public float GetCenterX()
    {
        return this.x + this.width / 2;
    }


    public float GetCenterY()
    {
        return this.y + this.height / 2;
    }

}
