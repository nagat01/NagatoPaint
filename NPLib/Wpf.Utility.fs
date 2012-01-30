[<AutoOpen>]
module Wpf.Utility

open System.Windows
open System.Windows.Controls
open System.Windows.Media

/// キャンバス上の位置を得る
let getCanvasPos (uie:UIElement) =
  Point(Canvas.GetLeft uie , Canvas.GetTop uie)

/// ブラシを追加する
let toBrush c = SolidColorBrush c