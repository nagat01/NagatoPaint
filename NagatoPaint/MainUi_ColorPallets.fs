[<AutoOpen>]
module Np.MainUi_ColorPallets

open System
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging

open Np

  
type ColorBar 
 (color :Color sv, 
  title, 
  toColor :int->Color->Color, 
  fromColor :Color->int) as sp =
  inherit StackPanel()
  do sp $ bgco Colors.White |> sphorizontal
  // タイトル
  let title = Label(Content=title) $ w 20. $ sp.add
  /// バー
  let bar = Label() $ tight_control $ sz 129. 20. $ sp.add
  let bitmap = WriteableBitmap.ofSize 129 20
  let image  = Image(Source=bitmap) $ sz 129. 20. $ bar.add

  // handling
  let renew (color: Color) =
    let value = fromColor color
    let pixels = 
      [|
        for y in 0 .. 19 do
        for x in 0 .. 128 ->
          if x = value then 
            if y % 2 = 0 then 0 else 0xff000000
          else 
            toColor x color |> Color.toInt
      |]
    bitmap.drawRect 0 0 129 pixels
  
  // initialize
  do color.Updated =>~ renew

type RgbHslPanel(color:Color sv) as sp =
  inherit StackPanel()
  do sp |> spvertical        

  let to'   map i = map (fun _ -> normalizedByte(i*2))
  let from' get co = int (get co) / 2
                                                                           
  let r = ColorBar(color, "R", to' Color.mapR, from' Color.toR) $ sp.add
  let g = ColorBar(color, "G", to' Color.mapG, from' Color.toG) $ sp.add
  let b = ColorBar(color, "B", to' Color.mapB, from' Color.toB) $ sp.add
  
  let to'   map i = map (fun _ -> float i / 128.)
  let from' get co = int (get co * 128.)

  let r = ColorBar(color, "H", to' Color.mapH, from' Color.toH) $ sp.add
  let r = ColorBar(color, "S", to' Color.mapS, from' Color.toS) $ sp.add
  let r = ColorBar(color, "L", to' Color.mapL, from' Color.toL) $ sp.add

  do sp.PreviewMouseDown =>~ fun e ->
    let po = e.GetPosition sp |> sp.PointToScreen
    let co = po |> getPixel
    color.Update co

type HslPanel(color:Color sv) as sp =
  inherit StackPanel()
  do sp |> spvertical

