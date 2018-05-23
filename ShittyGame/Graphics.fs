module Graphics

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open World
open Entity

let drawEntity (spriteBatch : SpriteBatch, camera : Camera) (entity : Entity) =
    let pos = (entity.Position - camera.Position).ToPoint()
    do spriteBatch.Draw (entity.Sprite, Rectangle(pos, entity.Size.ToPoint()), Color.White)