[<AutoOpen>]
module Np.Ui_ColorPanel

open System
open System.Windows.Controls
open System.Windows.Media
open Np

open System.Globalization
let parseHexNumber hex = 
  let success , value = Int32.TryParse(hex, NumberStyles.HexNumber, null)
  if success then Some value else None

/// 現在の色を表示
/// 現在の色を数値で指定
type ColorPanel(width, height, color:Color sh) as sp =
  inherit StackPanel()
  let label = Label() $ sz width height $ sp.add
  let textBox = TextBox() $ w width $ sp.add
  do 
    textBox.TextChanged =>~ 
      fun _ -> 
        let hex = textBox.Text
        match parseHexNumber hex with
        | Some value -> Color.ofHex value |> color.Change
        | _ -> ()

    color.Changed =>~ 
      fun co -> label |> bgco co