module World

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

open Input
open Entity
open Microsoft.Xna.Framework.Content

type Camera = 
    {
        Position : Vector2
        Zoom : float32
    }

type World =
    {
        Camera : Camera
        Entities : Entity list
    }

let initWorld (content : ContentManager) : World = 
    {
        Camera = { Position = Vector2.Zero; Zoom = 1.f }
        Entities =
            [("cat", Player, Vector2(20.f, 10.f), Vector2(100.f, 100.f));
             ("grass"), Background, Vector2(0.f), Vector2(1000.f)]
             |> List.map (CreateEntity content)
    }

let handleInput (keyState : Map<Keys, KeyStatus>) entity : Entity =  
    match entity.Type with
    | Player ->
        let dir = Vector2(
            (if keyState.[Keys.A].Pressed then -1.0f else 0.0f) + (if keyState.[Keys.D].Pressed then 1.0f else 0.0f),
            (if keyState.[Keys.W].Pressed then -1.0f else 0.0f) + (if keyState.[Keys.S].Pressed then 1.0f else 0.0f))
        { entity with Position = entity.Position + dir }
    | _ -> entity

let updateCamera (camera : Camera, target : Entity) : Camera =
    { camera with Position = target.Position }

let sortEntities (entity : Entity) = 
    match entity.Type with
        | Player -> 2
        | Background -> 1

let getWorld (world : World, keyState : Map<Keys, KeyStatus>) =
    let cameraTarget = world.Entities |> List.find (fun entity -> 
        match entity.Type with Player -> true | _ -> false)
    {
        
        Camera = updateCamera (world.Camera, cameraTarget);
        Entities = world.Entities   |> List.map (handleInput keyState)
                                    |> List.sortBy sortEntities
    }