[<AutoOpen>]
module WbSpray

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media
open System.Windows.Media.Imaging


            
/// RenderTargetBitmapを塗る
type WbSpray (m:NpModel, drag:IObservable<Point*Point>) as __ =
  /// ドラッグイベント
  let toggler = drag.toggled <| fun (p0,p1) ->
    let width = m.BrushWidth.V
    let color = m.BrushColor.V
    let opacity = m.BrushOpacity.V
    let wb = m.Layers.CurLayer.V.Wb
    let rect = 
      Math.rectFromPo2 p0 p1 
      |> Math.infrateRect (int width)
      |> Math.trimRect wb.rect
    if rect.length > 0 then
      let x,y,w,h = rect.XYWH
      let image = wb.rectPixels rect |> Wb.ofPixels w 

      let rtb = Rtb.ofSize w h
      rtb.drawImage image (Rect(0.,0.,float w,float h))
      let offset = Point(float x, float y)
      rtb.drawLine color width opacity (p0 -. offset) (p1 -. offset)
    
      wb.drawRect x y w rtb.pixels
  /// トグルする
  member __.Toggler = toggler

