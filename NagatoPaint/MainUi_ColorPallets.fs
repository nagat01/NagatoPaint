[<AutoOpen>]
module Np.MainUi_ColorPallets

open System
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

open Np


type ColorPallet(color: Color sv) as label =
  inherit Label()
  do label |> sz 128. 128.
  let bitmap = WriteableBitmap(128, 128, 96., 96., PixelFormats.Pbgra32, null)
  let image = Image(Source=bitmap) $ label.add
//  let renew(color:Color) =
//    let color = color.withA 255uy
//    let apixel = 
//      [|
//      for h in 0 .. 20 do
//        for w in 0 .. 127  -> color.mapR((+) (3*w)).toInt
//      for h in 0 .. 20 do
//        for w in 0 .. 127  -> color.mapG((+) (3*w)).toInt
//      for h in 0 .. 20 do
//        for w in 0 .. 127  -> color.mapB((+) (3*w)).toInt
//      for h in 1 .. 20 do
//        for w in 0 .. 127  -> color.mapR(fun x ->x - (3*w)).toInt
//      for h in 1 .. 21 do
//        for w in 0 .. 127  -> color.mapG(fun x ->x - (3*w)).toInt
//      for h in 1 .. 21 do
//        for w in 0 .. 127  -> color.mapB(fun x ->x - (3*w)).toInt
//      |]
//    bitmap.WriteRect 0 0 WIDTH apixel
//  do 
//    color => renew
