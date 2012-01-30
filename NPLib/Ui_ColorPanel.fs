[<AutoOpen>]
module Np.Ui_ColorPanel

open System
open System.Windows.Controls
open System.Windows.Media
open Np

open System.Globalization
let parseHexNumber hex = 
  Int32.TryParse(hex, NumberStyles.HexNumber, CultureInfo("en-US"))

/// 現在の色を表示
/// 現在の色を数値で指定
type ColorPanel(width, height, co:Color sv) as sp =
  inherit StackPanel()
  let label = Label() $ sz width height $ sp.add
  let textBox = TextBox() $ w width $ sp.add
  do 
    textBox.TextChanged =>~ 
      fun _ -> 
        let hex = textBox.Text
        let success , value = parseHexNumber hex
        if success then 
          Color.ofHex value |> co.Update

    co.Updated =>~ 
      fun co -> label |> bgco co