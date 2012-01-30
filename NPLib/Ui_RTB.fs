[<AutoOpen>]
module Np.Ui_RTB

open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media
open System.Windows.Media.Imaging

open Np

type RenderTargetBitmap with
  /// 直線を描画する
  member rtb.drawLine c w p0 p1 =
    let dv = DrawingVisual()
    (use dc = dv.RenderOpen()
    dc.DrawLine(Pen(toBrush c , w), p0, p1)
    )
    rtb.Render dv


/// RenderTargetBitmapを塗る
type PenDrawRTB (label:Label, rtb:RenderTargetBitmap, co:Color sv) as __ =
  /// 直前のマウスの位置
  let pPrev = obox()

  /// マウスを押したら、現在位置を保存
  let mouseDown = 
    label.MouseDown.toggled <| fun e ->
      let p = __.getMousePos e
      pPrev.Value <- p

  /// マウスを移動させたら、線を引く
  let mouseMove = 
    label.MouseMove.toggled <| fun e ->
      let p = __.getMousePos e
      if pPrev.IsSome && e.LeftButton.HasFlag MouseButtonState.Pressed then
        rtb.drawLine co.Value 1. pPrev.Value p
        pPrev.Value <- p

  /// マウスを離したら、まだ引いてない分の線を引いて、線引き終わり
  let mouseLeave = 
    label.MouseLeave.toggled <| fun e ->
      let p = __.getMousePos e
      if pPrev.IsSome then
        rtb.drawLine co.Value 1. pPrev.Value p
      pPrev.Clear

  /// マウスを上げたら、線引き終わり
  let mouseUp = label.MouseUp.toggled <| fun _ ->
    pPrev.Clear 

  let events = [mouseDown; mouseMove; mouseLeave; mouseUp]
  let toggler = 
    Toggler(
      fun _ -> for event in events do event.On
    , fun _ -> for event in events do event.Off
    )
  /// コントロール上を基準としてマウスの位置を取得する
  member __.getMousePos (e:MouseEventArgs) =
    e.GetPosition label

  /// トグルする
  member __.Toggler = toggler

