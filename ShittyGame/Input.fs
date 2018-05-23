module Input

open Microsoft.Xna.Framework.Input
open System

type KeyStatus  =
    {
        Pressed : bool
        Clicked : bool
    }

let getInput (states:Map<Keys, KeyStatus>) = 
    [ for k in Enum.GetValues(typeof<Keys>) |> unbox do 
        let pressed = Keyboard.GetState().IsKeyDown(k)
        if not ((states.Item Keys.A).Pressed) && pressed then
            yield k, { Pressed = pressed; Clicked = true }
        else
            yield k, { Pressed = pressed; Clicked = false }
    ] |> Map.ofList