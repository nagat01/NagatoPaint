[<AutoOpen>]
module Wpf.MainWindow

open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media

open Base

/// メインウィンドウ
type MainWindow() as win =
  inherit Window(WindowStyle = WindowStyle.None)
  let dp = DockPanel() $ win.add
  let menu = Menu() $ h 20. $ dp.addTop 
  let canvas = Canvas() $ bgco Colors.LightBlue $ dp.addBottom
  do win.Show()

  member __.Canvas = canvas
  member __.Menu   = menu