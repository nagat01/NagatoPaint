[<AutoOpen>]
module FileUi

open System                      
open System.IO
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Imaging
open Microsoft.Win32


/// ファイルの保存、開く       
type FilePanel(m:NpModel) as sp =
  inherit StackPanel()
  do sp |> sp_horizontal

  /// ボタン、ファイルを保存する
  let 保存する = Button(Content="保存する") $ sp.add
  do 保存する.Click =>~ fun _ ->
    let encoder = JpegBitmapEncoder()
    let frame = BitmapFrame.Create m.Layers.CurLayer.V.Wb
    encoder.Frames.Add frame
    let dialog = SaveFileDialog()
    dialog.Filter <- "JPEGファイル(*.jpeg)|*.jpeg"
    if dialog.showAndOk then
      use stream = new FileStream(dialog.FileName,FileMode.Create)
      encoder.Save stream

  /// ボタン、ファイルを開く
  let 開く = Button(Content="開く") $ sp.add
  do 開く.Click =>~ fun _ ->
    let dialog = OpenFileDialog()
    dialog.Filter <- "JPEGファイル(*.jpeg)|*.jpeg"
    if dialog.showAndOk then
      let decoder = JpegBitmapDecoder(Uri(dialog.FileName), BitmapCreateOptions.None, BitmapCacheOption.Default)
      let bitmap = decoder.Frames.[0]
      let r = bitmap.rect |> Math.trimRect (Int32Rect(0,0,600,600))
      let rtb = Rtb.ofSize r.w r.h
      rtb.drawImage bitmap (Rect(0.,0.,float r.w,float r.h))
      m.Layers.CurLayer.V.Wb.WritePixels(r, rtb.pixels, r.stride, 0)
