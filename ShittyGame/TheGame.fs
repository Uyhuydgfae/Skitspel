module TheGame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open System

open Input
open World
open Graphics

type Game1 () as x =
    inherit Game()
 
    do x.Content.RootDirectory<- "Content"
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    
    let mutable keyStates = [ for i in  Enum.GetValues(typeof<Keys>) |> unbox  do yield i, { Pressed = false; Clicked = false}] |> Map.ofList
    let mutable world = Unchecked.defaultof<World>

    override x.Initialize() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice)
        let width height = x.Window.ClientBounds
        world <- World.initWorld x.Content
        ()

    override x.Update(gameTime) =
        keyStates <- getInput(keyStates)
        world <- getWorld(world, keyStates) 
        ()

    override x.Draw(gameTime) =
        do x.GraphicsDevice.Clear Color.Plum
        do spriteBatch.Begin()
        world.Entities |> List.map (drawEntity (spriteBatch, world.Camera)) |> ignore
        do spriteBatch.End()
        ()