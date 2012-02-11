/// BitmapSource
/// RenderTargetBitmap
/// WriteableBitmap
/// などの操作をする
[<AutoOpen>]
module Bitmap
                           
open System.Windows
open System.Windows.Media
open System.Windows.Media.Imaging

type BitmapSource with
  /// 矩形のpixelsを得る
  member __.rectPixels (r:Int32Rect) =
    let pixels = r.pixels
    __.CopyPixels(r, pixels, r.stride, 0)
    pixels      
  /// 全体のpixelsを得る
  member __.pixels = __.rectPixels __.rect




type RenderTargetBitmap with
  /// 直線を描画する
  member rtb.drawLine (color:Color) (width:float) (opacity:float) (p0:Point) (p1:Point) =
    let dv = DrawingVisual(Opacity=opacity)
    (use dc = dv.RenderOpen()
     let brush = Wpf.brush color
     let pen = Pen(brush,width)
     dc.DrawLine(pen, p0, p1)
     let radius = width / 2.
     dc.DrawEllipse(brush,null,p0,radius,radius)
     dc.DrawEllipse(brush,null,p1,radius,radius)
    )
    rtb.Render dv

  /// 直線を描画する
  member rtb.drawImage image rect =
    let dv = DrawingVisual()
    (use dc = dv.RenderOpen()
     dc.DrawImage(image,rect)
    )
    rtb.Render dv

module Rtb =
  let ofSize w h = 
    RenderTargetBitmap(w, h, 96., 96., PixelFormats.Pbgra32)


type WriteableBitmap with
  member __.drawRect x y width (pixels:int[]) = 
    let height = pixels.Length / width
    __.WritePixels(Int32Rect(x,y,width,height), pixels, width*4, 0) 

module Wb =
  let ofSize width height = 
    WriteableBitmap(width, height, 96., 96., PixelFormats.Pbgra32,null)
  let ofPixels w (pixels:int[]) =
    let h = pixels.Length / w
    let wb = ofSize w h
    wb.drawRect 0 0 w pixels
    wb
  