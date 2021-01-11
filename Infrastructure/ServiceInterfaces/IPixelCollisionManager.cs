namespace Infrastructure.ServiceInterfaces
{
    public interface IPixelCollisionManager
    {
        bool PerPixelCollision(ICollidable2D i_CollidableA, ICollidable2D i_CollidableB, bool i_PixelEatingTextureA = false, bool i_PixelEatingTextureB = false);
    }
}
