[<AutoOpen>]
module WbEraser

open System
open System.Windows
open System.Windows.Controls   
open System.Windows.Media
open System.Windows.Media.Imaging

type WbEraser(m:NpModel, drag:IObservable<Point*Point>) =
  let toggler = drag.toggled <| fun (p0,p1) ->
    let width = m.BrushWidth.V
    let opacity = m.BrushOpacity.V
    let wb = m.Layers.CurLayer.V.Wb
    
    let rect = 
      Math.rectFromPo2 p0 p1
      |> Math.infrateRect (int width)
      |> Math.trimRect wb.rect
    let x,y,w,h = rect.XYWH

    if rect.length > 0 then
      let rtb = Rtb.ofSize w h
      let offset = Point(float x, float y)
      rtb.drawLine Colors.White width 1. (p0-.offset) (p1-.offset)
      let part = 
        (rtb.pixels , wb.rectPixels rect) 
        ||> Array.map2(fun rtb wb ->
          if rtb = 0xffffffff then
            if wb = 0 then 0 else
              let color = wb |> Co.ofInt 
              let a = int color.A 
              let a = if a = 0 then 255 else a
              let aa = a - int(opacity*256.) |> max 0
              let r = int color.R * aa / a
              let g = int color.G * aa / a
              let b = int color.B * aa / a
              Color.FromArgb(byte aa,byte r,byte g,byte b)
              |> Co.toInt
          else 
            wb
          )
      wb.drawRect x y w part
    
  member __.Toggler = toggler

  
