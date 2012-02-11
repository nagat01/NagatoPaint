[<AutoOpen>]
module NpModel

open System.Windows.Media
open System.Windows.Media.Imaging

type PaintTool =
  | Pencil 
  | Marker 
  | Spray  
  | Eraser 
  override __.ToString() =
    match __ with
    | Pencil -> "ペン"
    | Marker -> "マーカー"
    | Spray  -> "スプレー"
    | Eraser -> "消しゴム"

type NpModel = 
  {
    /// 色
    BrushColor : Color sv
    /// 幅
    BrushWidth : float sv
    /// 透明度
    BrushOpacity : float sv

    CurTool : PaintTool so
    /// レイヤーの集合
    Layers : Layers
  }
  with 
  /// デフォルトのオブジェクトを返す
  static member Default = 
    let layer1 = Layer.ofSize 600 600
    let layer2 = Layer.ofSize 600 600

    let layers = Layers.ofSeq [layer1; layer2]
    {
      BrushColor = sv Colors.Black
      BrushWidth = sv 1.
      BrushOpacity = sv 1.
      CurTool    = so PaintTool.Spray
      Layers     = layers
    }
  /// 全ての共有オブジェクトを初期化
  member __.Initialize = 

    __.BrushColor.Init
    __.BrushWidth.Init
    __.BrushOpacity.Init
    __.CurTool.Init
    __.Layers.Init
