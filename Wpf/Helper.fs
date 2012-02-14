[<AutoOpen>]
module Helper

open System.Windows
open System.Windows.Controls   
open System.Windows.Input
open System.Windows.Media

/// メッセージボックス
let msg x = MessageBox.Show(sprintf "%A" x) |> ignore
/// ボタン
let btn title = Button(Content=title)

// プロパティの設定 //
    
let inline w w ctrl = 
  (^T:(member set_Width : float->unit)(ctrl,w))

let inline h h ctrl =
  (^T:(member set_Height: float->unit)(ctrl,h))

let inline sz ww hh control = 
  w ww control
  h hh control


/// 背景色を設定
let inline bgco co ui = 
  (^T:(member set_Background : Brush -> unit)(ui, Wpf.brush co))

/// 前景色を設定
let inline fgco co ui = 
  (^T:(member set_Foreground : Brush -> unit)(ui, Wpf.brush co))

/// マージンを設定
let mgn thickness (fe:FrameworkElement) =
  fe.Margin <- thickness

/// パディングを設定
let pdng thickness (c:Control) = 
  c.Padding <- thickness

/// マージン、パディングのないコントロールにする
let tight_control (c:Control) =
  mgn  (Thickness 0.) c
  pdng (Thickness 0.) c

let canvas_point (po:Point) uie =
  Canvas.SetLeft(uie , po.X)
  Canvas.SetTop (uie , po.Y)

/// スタックパネルの配置を水平にする
let sp_horizontal (sp:StackPanel) =
  sp.Orientation <- Orientation.Horizontal

/// スタックパネルの配置を垂直にする
let sp_vertical (sp:StackPanel) =
  sp.Orientation <- Orientation.Vertical

//TODO ウィンドウのドラッグ

/// ウィンドウをドラッグ可能にする
let window_draggable (window:Window) =
  let offset = obox()
  window.MouseDown.Add <| fun e ->
    if e.ChangedButton = MouseButton.Right then
      offset.Value <- e.GetPosition window
  window.MouseMove.Add <| fun e ->
    if e.RightButton.HasFlag MouseButtonState.Pressed then
      if offset.IsSome then
        let p = e.GetPosition window |> window.PointToScreen
        window.Left <- p.X - offset.Value.X
        window.Top  <- p.Y - offset.Value.Y
  window.MouseUp.Add <| fun e ->
    if e.ChangedButton = MouseButton.Right then
      offset.Clear
  window.MouseLeave.Add <| fun _ ->
    offset.Clear


/// UIElementをドラッグ可能にする
let draggable_uie (uie:UIElement) =
  let offset = obox()
  uie.MouseDown.Add <| fun e ->
    if e.ChangedButton = MouseButton.Right then
      e.Handled <- true
      offset.Value <- e.GetPosition uie
  uie.MouseMove.Add <| fun e -> 
    if e.RightButton = MouseButtonState.Pressed then
      if offset.IsSome then
        e.Handled <- true
        uie |> canvas_point (Wpf.posOnCanvas uie +. e.GetPosition uie -. offset.Value) 
  uie.MouseUp.Add <| fun _ ->
    offset.Clear
  uie.MouseLeave.Add <| fun _ ->
    offset.Clear