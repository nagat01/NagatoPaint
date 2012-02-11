[<AutoOpen>]
module BrushUi

open System
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

type SliderPanel
 (title : string, 
  value : float sv, 
  min   : float, 
  max   : float) as sp =
  inherit StackPanel()
  do sp |> sp_horizontal
  let title = Label(Content=title) $ sp.add
  let slider = Slider(Minimum=min, Maximum=max, Width=70.) $ sp.add
  let label = Label() $ sp.add
  do
    slider.ValueChanged =>~ fun _ ->
      slider.Value |> value.Change
    value.Changed =>~ fun _ ->
      slider.Value <- value.V
      label.Content <- sprintf "%0.2f" value.V

///// ブラシの幅を指定するスライダー
//type BrushWidthBar(width:float sh) as slider =
//  inherit Slider(Minimum=1.,Maximum=100.)
//  do 
//    // 値を同期させる
//    slider.ValueChanged =>~ fun _ ->
//      width.Change slider.Value
//    width.Changed =>~ fun _ ->
//      slider.Value <- float width.V
//      
//type BrushOpacityBar(opacity:float sh) as slider =
//  inherit Slider(Minimum=0., Maximum=1.)
//  do
//    slider.ValueChanged =>~ fun _ ->
//      opacity.Change slider.Value    