[<AutoOpen>]
module Np.Main_SpLeft

open System.Windows.Controls
open Np


/// 左側のStackPanel
let spLeft = StackPanel()

let btnペンを選択 = 
  let d = PenDrawRTB(label, rtb, SelectedColor)
  let btn = Button(Content="ペンを選択") $ spLeft.add
  btn.Click =>~ fun _ -> d.Toggler.Toggle
  btn

let colorPanel = 
  ColorPanel(64., 64., SelectedColor)
  $ spLeft.add