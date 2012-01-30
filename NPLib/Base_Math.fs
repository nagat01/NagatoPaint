[<AutoOpen>]
module Np.Base_Math

open System

[<AutoOpen>]
module Math =
  let normalizedByte i = i |> min 255 |> max 0 |> byte
  let fraction (f:float) = f - floor f
  