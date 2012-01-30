[<AutoOpen>]
module Np.Wpf_Utility

open System.Windows
open System.Windows.Controls
open System.Windows.Media

[<AutoOpen>]
module Wpf =
  /// キャンバス上の位置を得る
  let getCanvasPos (uie:UIElement) =
    Point(Canvas.GetLeft uie , Canvas.GetTop uie)

  /// ブラシを追加する
  let toBrush c = SolidColorBrush c



open System.Runtime.InteropServices
[<DllImport("gdi32")>]
extern int GetPixel(int hdc, int x,int y)
[<DllImport("user32")>]
extern int GetWindowDC(int hwnd)
[<DllImport("user32")>]
extern int ReleaseDC(int hWnd, int hDC)

  let getPixel (p:Point) =
    let dc = GetWindowDC(0)
    let i = GetPixel(dc, int p.X, int p.Y)
    ReleaseDC(0,dc) |> ignore
    let r = i        &&& 0xff |> byte
    let g = i >>> 8  &&& 0xff |> byte
    let b = i >>> 16 &&& 0xff |> byte
    Color.FromRgb(r, g, b)