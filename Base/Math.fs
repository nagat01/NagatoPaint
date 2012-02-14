[<AutoOpen>]
module Math

open System
open System.Windows

let normalizedByte i = i |> min 255 |> max 0 |> byte
let fraction (f:float) = f - floor f


type Int32Rect with
  member __.w = __.Width
  member __.h = __.Height
  member __.XYWH = 
    __.X , 
    __.Y ,
    __.w ,
    __.h 
  member __.r = __.X + __.w
  member __.b = __.Y + __.h
  member __.length = __.w * __.h

  member __.stride = __.w * 4
  member __.pixels = Array.zeroCreate<int> __.length 

let rectFromPo2 (p0:Point) (p1:Point) =
  let x = min p0.X p1.X |> int
  let y = min p0.Y p1.Y |> int
  let w = p0.X - p1.X |> abs |> int
  let h = p0.Y - p1.Y |> abs |> int
  Int32Rect(x,y,w,h)

let infrateRect value (rect:Int32Rect) =
  let value = value + value % 2
  Int32Rect(
    rect.X - value / 2, 
    rect.Y - value / 2, 
    rect.w + value, 
    rect.h + value
    )

let trimRect (source:Int32Rect) (target:Int32Rect) =
  let x = max target.X source.X
  let y = max target.Y source.Y
  let r = min target.r source.r
  let b = min target.b source.b
  let w = max 0 (r - x)
  let h = max 0 (b - y)
  Int32Rect(x,y,w,h)


open System.Windows.Media.Imaging
type BitmapSource with
  member __.rect = Int32Rect(0, 0, __.PixelWidth, __.PixelHeight)