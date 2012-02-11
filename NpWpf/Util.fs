[<AutoOpen>]
module Util

open System.Windows
open System.Windows.Controls   
open System.Windows.Input
open System.Windows.Media

open System.Runtime.InteropServices
[<DllImport("gdi32")>]
extern int GetPixel(int hdc, int x,int y)
[<DllImport("user32")>]
extern int GetWindowDC(int hwnd)
[<DllImport("user32")>]
extern int ReleaseDC(int hWnd, int hDC)

module Wpf =
  /// キャンバス上の位置を得る
  let posOnCanvas (uie:UIElement) =
    Point(Canvas.GetLeft uie , Canvas.GetTop uie)

  /// ブラシを追加する
  let brush c = SolidColorBrush c

  /// Win32APIである点の色を得る
  let screenPointColor (p:Point) =
    let dc = GetWindowDC(0)
    let i = GetPixel(dc, int p.X, int p.Y)
    ReleaseDC(0,dc) |> ignore
    let r = i        &&& 0xff |> byte
    let g = i >>> 8  &&& 0xff |> byte
    let b = i >>> 16 &&& 0xff |> byte
    Color.FromRgb(r, g, b)


  /// ドラッグイベント
  let drag (uie:UIElement) =
    let event = Event<Point*Point>()
    let p0 = obox()
    let getPos (e:MouseEventArgs) = 
      e.GetPosition uie
    let isPressed (e:MouseEventArgs) = 
      e.LeftButton.HasFlag MouseButtonState.Pressed
    // 押されているなら位置をセット
    uie.MouseEnter =>~ fun e ->
      if isPressed e then
        p0.Value <- getPos e
    // 位置をセット
    uie.MouseDown  =>~ fun e ->
      p0.Value <- getPos e
    // 条件が成り立てばドラッグイベント発生
    // 押されているなら位置をセット
    uie.MouseMove =>~ fun e ->
      let p1 = e.GetPosition uie
      if p0.IsSome && isPressed e then
        event.Trigger (p0.Value , p1) // ドラッグイベント
      if isPressed e then
        p0.Value <- p1
    // 条件が成り立てばドラッグイベント発生
    // 位置をクリア
    uie.MouseLeave =>~ fun e ->
      let p1 = e.GetPosition uie
      if p0.IsSome && isPressed e then
        event.Trigger (p0.Value , p1)
      p0.Clear                  
    // 位置をクリア
    uie.MouseUp =>~ fun e ->
      p0.Clear

    // イベント
    event.Publish