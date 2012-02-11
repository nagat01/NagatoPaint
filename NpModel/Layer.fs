[<AutoOpen>]
module Layer
                            
open System                                  
open System.Windows.Media
open System.Windows.Media.Imaging

/// レイヤー         
type Layer =
  {
    Name       : string so
    Wb         : WriteableBitmap
    IsSelected : bool sv
    IsVisible  : bool sv
  }
  /// サイズから、新しいレイヤーを作る
  static member ofSize w h =
    {
      Name       = so "新しいレイヤー"
      Wb         = Wb.ofSize w h
      IsSelected = sv false
      IsVisible  = sv true 
    }
  /// 新しい同期されるレイヤーを作る
  member __.Init =
    __.Name.Init
    __.IsSelected.Init
    __.IsVisible.Init

/// レイヤーの集合    
type Layers = 
  {
    Layers   : Layer ResizeArray so
    CurLayer : Layer so
  }
  member __.AddLayer layer =
    __.Layers.Update <| fun _ ->
      __.Layers.V.Add layer
      // あるレイヤーが選択されたら、現在レイヤーを変更する
      layer.IsSelected.Changed =>~ function
      | true -> __.CurLayer.Replace <| layer
      | _ -> ()
      __.Layers.AddInit (fun _ -> layer.Init)
    __.Layers.Init

  /// 複数のレイヤーから、レイヤーの集合を作る
  static member ofSeq (layers:Layer seq) =
    let __ =
      {
        Layers = so(ResizeArray())
        CurLayer = so(Seq.head layers)
      }
    
    // 現在レイヤーが変更されたら、
    // 各レイヤーのIsSelectedを変更
    __.CurLayer.Changed =>~ fun _ ->
      for layer in __.Layers.V do
        layer.IsSelected.Change 
        <| (__.CurLayer.V = layer)
    for layer in layers do
      __.AddLayer layer
    __

  /// メンバを初期化する
  member __.Init =
    __.Layers.Init
    __.CurLayer.Init
    
  