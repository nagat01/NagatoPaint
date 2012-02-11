[<AutoOpen>]
module NpUi_Canvas

open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

/// Rtbに描画するキャンバス
type NpCanvas(m:NpModel) as canvas =
  inherit Canvas()
  do canvas |> bgco Colors.LightGreen
  let drag = Wpf.drag canvas

  do m.Layers.Layers.Changed =>~ fun layers ->
    canvas.Children.Clear()
    layers |> Seq.iteri(fun i layer ->
      let image = Image() $ sz 600. 600. $ canvas.addZ -i
      layer.IsVisible.Changed =>~ fun _ ->
        if layer.IsVisible.V then
          image.Source <- layer.Wb
        else
          image.Source <- null
    )

  let spray = WbSpray(m, drag)
  let eraser = WbEraser(m, drag)
  do m.CurTool.Changed =>~ fun _ ->
    spray.Toggler.Off
    eraser.Toggler.Off
    match m.CurTool.V with
    | PaintTool.Pencil -> ()
    | PaintTool.Spray ->
      spray.Toggler.On
    | PaintTool.Eraser ->  
      eraser.Toggler.On
    | _ -> ()
      