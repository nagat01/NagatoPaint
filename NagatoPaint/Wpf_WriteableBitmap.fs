[<AutoOpen>]
module Np.Wpf_WriteableBitmap
                           
open System.Windows
open System.Windows.Media
open System.Windows.Media.Imaging
open Np

module WriteableBitmap =
  let ofSize width height = 
    WriteableBitmap(width, height, 96., 96., PixelFormats.Pbgra32,null)
  
type WriteableBitmap with
  member __.drawRect x y width (pixels:int[]) = 
    let height = pixels.Length / width
    __.WritePixels(Int32Rect(x,y,width,height), pixels, width*4, 0) 