[<AutoOpen>]
module Canvas

open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging
open Base
open Wpf

/// 描画されるビットマップ
let rtb = RenderTargetBitmap(600, 600, 96., 96., PixelFormats.Pbgra32) 
let label = Label() $ sz 600. 600. $ bgco Colors.White $ tight_control
let image = Image(Source=rtb) $ sz 600. 600. $ label.add