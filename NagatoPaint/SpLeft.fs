[<AutoOpen>]
module SpLeft
                             
open System.Windows.Controls
open System.Windows.Media


/// 左側のStackPanel
type SpLeft(m:NpModel) as sp =
  inherit StackPanel()
  do sp |> bgco Colors.LightBlue

  let ファイル = FilePanel(m) $ sp.add

  let ツール = ToolPanel(m.CurTool) $ sp.add
   
  let 太さ =
    SliderPanel("太さ", m.BrushWidth, 1., 100.)
    $ sp.add

  let 透明度 =
    SliderPanel("透明度",m.BrushOpacity, 0., 1.)
    $ sp.add

  let 現在色 = 
    ColorPanel(64., 64., m.BrushColor)
    $ sp.add

  let 色スライダー =
    RgbHslSliders(m.BrushColor)
    $ sp.add

  let 色の変更の履歴 = ColorHistory(m.BrushColor) $ sp.add
