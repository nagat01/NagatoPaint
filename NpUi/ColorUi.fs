[<AutoOpen>]
module ColorUi

open System
open System.Windows.Controls    
open System.Windows.Controls.Primitives
open System.Windows.Media

open System.Globalization
let parseHexNumber hex = 
  let success , value = Int32.TryParse(hex, NumberStyles.HexNumber, null)
  if success then Some value else None

/// 現在の色を表示
/// 現在の色を数値で指定
type ColorPanel(width, height, color:Color sv) as sp =
  inherit StackPanel()
  let label = Label() $ sz width height $ sp.add
  let textBox = TextBox() $ w width $ sp.add
  do 
    textBox.TextChanged =>~ fun _ -> 
      let hex = textBox.Text
      match parseHexNumber hex with
      | Some value -> Co.ofInt value |> color.Change
      | _ -> ()

    color.Changed =>~ 
      fun co -> label |> bgco co




/// 色の帯を作る
type ColorSlider
  (
  title, 
  color     : Color sv, 
  toColor   : int->Color->Color, 
  fromColor : Color->int
  ) as sp =
  inherit StackPanel()
  do sp $ bgco Colors.White |> sp_horizontal
  /// タイトル
  let title = Label(Content=title) $ w 20. $ sp.add
  /// バー
  // let bar = Label() $ tight_control $ sz 129. 20. $ sp.add
  let bitmap = Wb.ofSize 129 20
  let image  = Image(Source=bitmap) $ sz 129. 20. $ sp.add // $ bar.add

  // 色が変更されたら、表示を変更する
  let changeColor (color: Color) =
    let value = fromColor color
    let pixels = 
      [|
        for y in 0 .. 19 do
        for x in 0 .. 128 ->
          if x = value then 
            if y % 2 = 0 then 0 else 0xff000000
          else 
            toColor x color |> Co.toInt
      |]
    bitmap.drawRect 0 0 129 pixels
  
  // initialize
  do color.Changed =>~ changeColor



/// RGBとHSLの帯を作る
type RgbHslSliders(color:Color sv) as sp =
  inherit StackPanel()
  do sp |> sp_vertical        

  let to'   map i = map (fun _ -> Math.normalizedByte(i*2))
  let from' get co = int (get co) / 2
                                                                           
  let r = ColorSlider("R", color, to' Co.mapR, from' Co.toR) $ sp.add
  let g = ColorSlider("G", color, to' Co.mapG, from' Co.toG) $ sp.add
  let b = ColorSlider("B", color, to' Co.mapB, from' Co.toB) $ sp.add
  
  let to'   map i = map (fun _ -> float i / 128.)
  let from' get co = int (get co * 128.)

  let h = ColorSlider("H", color, to' Co.mapH, from' Co.toH) $ sp.add
  let s = ColorSlider("S", color, to' Co.mapS, from' Co.toS) $ sp.add
  let l = ColorSlider("L", color, to' Co.mapL, from' Co.toL) $ sp.add

  do sp.PreviewMouseDown =>~ fun e ->
    let po = e.GetPosition sp |> sp.PointToScreen
    let co = po |> Wpf.screenPointColor
    color.Change co


/// 色の変更の履歴
type ColorHistory (color:Color sv) as ugrid =
  inherit UniformGrid(Columns=8, Rows=8)
  do ugrid |> sz 128. 128.
  let colors = Array.create 64 Colors.White
  let labels = Array.init 64 <| fun i -> 
    let label = Label() $ ugrid.add
    label.MouseDown =>~ fun _ -> color.Change colors.[i]
    label
  let addColor color =    
    for i in 63 .. -1 .. 1  do
      colors.[i] <- colors.[i-1]
    colors.[0] <- color
    for i in 0 .. 63 do
      labels.[i] |> bgco colors.[i]

  do color.Changed =>~ addColor
