
public class UserObject : IUserObject
{

    private int health;
    private IShape shape;

    public UserObject()
    {
    }

    public UserObject(int aHealth, IShape aShape)
    {
        this.health = aHealth;
        this.shape = aShape;
    }


    public void AddHealth(int aHealth)
    {
        this.health += aHealth;
    }


    public int GetHealth()
    {
        return this.health;
    }


    public void TakeDamage(int aDamage)
    {
        this.health -= aDamage;
    }


    public IShape GetShape()
    {
        return this.shape;
    }

}
