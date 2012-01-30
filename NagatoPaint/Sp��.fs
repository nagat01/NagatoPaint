[<AutoOpen>]
module Sp左

open System.Windows.Controls
open Base
open Wpf

/// 左側のStackPanel
let sp左 = StackPanel()

let btnペンを選択 = 
  let d = PenDrawRTB(label, rtb)
  let btn = Button(Content="ペンを選択") $ sp左.add
  btn.Click <! fun _ -> d.Toggler.Toggle