using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;

namespace Infrastructure.Managers
{
    public class PixelCollisionManager : GameService, IPixelCollisionManager
    {
        public PixelCollisionManager(Game i_Game) : base(i_Game)
        {
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(IPixelCollisionManager), this);
        }

        public bool PerPixelCollision
            (ICollidable2D i_CollidableA, ICollidable2D i_CollidableB, bool i_IsPixelEatingA = false, bool i_IsPixelEatingB = false)
        {
            bool hasCollided = false;

            if (i_CollidableA is Sprite spriteA && i_CollidableB is Sprite spriteB)
            {
                // Calculate the intersecting rectangle
                Color[] bitsA = new Color[spriteA.Texture.Width * spriteA.Texture.Height];
                spriteA.Texture.GetData(bitsA);

                Color[] bitsB = new Color[spriteB.Texture.Width * spriteB.Texture.Height];
                spriteB.Texture.GetData(bitsB);

                int m_MaxX = Math.Max(spriteA.Bounds.X, spriteB.Bounds.X);
                int m_MinX = Math.Min(spriteA.Bounds.X + spriteA.Bounds.Width, spriteB.Bounds.X + spriteB.Bounds.Width);

                int m_MaxY = Math.Max(spriteA.Bounds.Y, spriteB.Bounds.Y);
                int m_MinY = Math.Min(spriteA.Bounds.Y + spriteA.Bounds.Height, spriteB.Bounds.Y + spriteB.Bounds.Height);

                // For each single pixel in the intersecting rectangle
                for (int y = m_MaxY; y < m_MinY; y++)
                {
                    for (int x = m_MaxX; x < m_MinX; x++)
                    {
                        // Get the color from each texture
                        int indexA =
                            x - spriteA.Bounds.X + spriteA.SourceRectangle.X / spriteA.SourceRectangle.Width * spriteA.SourceRectangle.Width +
                            (y - spriteA.Bounds.Y + spriteA.SourceRectangle.Y / spriteA.SourceRectangle.Height * spriteA.SourceRectangle.Height) * spriteA.Texture.Width;

                        int indexB =
                            x - spriteB.Bounds.X + spriteB.SourceRectangle.X / spriteB.SourceRectangle.Width * spriteB.SourceRectangle.Width +
                            (y - spriteB.Bounds.Y + spriteB.SourceRectangle.Y / spriteB.SourceRectangle.Height * spriteB.SourceRectangle.Height) * spriteB.Texture.Width;

                        if (bitsA[indexA].A != 0 && bitsB[indexB].A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                        {
                            hasCollided = true;

                            if (i_IsPixelEatingA)
                            {
                                bitsA[indexA].A = 0;
                            }

                            if (i_IsPixelEatingB)
                            {
                                bitsB[indexB].A = 0;
                            }

                            if (i_IsPixelEatingA == false && i_IsPixelEatingB == false)
                            {
                                break;
                            }
                        }
                    }
                }

                spriteA.Texture.SetData(bitsA);
                spriteB.Texture.SetData(bitsB);
            }

            // If no collision occurred by now, we're clear.
            return hasCollided;
        }
    }
}
