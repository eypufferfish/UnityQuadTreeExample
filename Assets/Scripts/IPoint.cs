
public interface IPoint<T> where T : IUserObject
{

    float GetX();

    float GetY();

    T GetUserObject();
}
