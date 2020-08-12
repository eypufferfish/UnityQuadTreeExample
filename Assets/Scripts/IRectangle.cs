using System.Collections;

public interface IRectangle : IShape
{

    float GetX();

    float GetY();

    float GetWidth();

    float GetHeight();

    bool Contains(IPoint<IUserObject> aPoint);

    void SetX(float aX);

    void SetY(float aY);

    void SetWidth(float aWidth);

    void SetHeight(float aHeight);

}
