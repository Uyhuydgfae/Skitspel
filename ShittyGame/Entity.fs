module Entity

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Content

type EntityType = 
    | Player
    | Background

type Entity =
    {
        Position : Vector2
        Size : Vector2
        Sprite : Texture2D
        Type : EntityType
    }

let CreateEntity (content : ContentManager) (textureName, entityType, position, size) =
    let tex = content.Load<Texture2D> textureName
    { Sprite = tex; Position = position; Size = size; Type = entityType}
