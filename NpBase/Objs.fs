[<AutoOpen>]
module Objs

open System

type SyncValue<'a 
  when 'a : struct 
  and  'a : equality> 
  (value:'a) =
  let mutable _value = value
  let changed = Event<'a>()

  member __.V = _value

  member __.Change value =
    if _value <> value then
      _value <- value
      changed.Trigger _value

  member __.Changed = changed.Publish

  member __.Init = changed.Trigger _value
    
type sv<'a 
  when 'a : struct 
  and  'a : equality> = 
  SyncValue<'a>

/// 変更を
type SyncObject<'a
  when 'a : not struct>
  (value:'a) =
  let mutable _value = value
  /// 変更通知イベント
  let changed = Event<'a>()
  let inits   = Event<unit>()
  /// 値
  member __.V = _value

  member __.Update f =
    f _value
    __.Init

  member __.Replace value =
    _value <- value
    changed.Trigger _value
  
  member __.Changed = changed.Publish
  member __.AddInit f = inits.Publish.Add f

  member __.Init = 
    _value |> changed.Trigger
    inits.Trigger ()

type so<'a when 'a : not struct> = SyncObject<'a>


/// ないかもしれない値を管理するオブジェクト
type OptionBox<'a>(?value:'a) = 
  let mutable _value = value
  /// 値をセット、ゲット
  member __.Value
    with get ()    = _value.Value 
    and  set value = _value <- Some value
  /// 値はあるか？
  member __.IsSome = _value.IsSome
  /// 値はないか？
  member __.IsNone = _value.IsNone
  /// 値を消去
  member __.Clear  = _value <- None
  /// 値があれば関数を適用
  member __.Apply f =
    match _value with
    | Some v -> f v
    | None   -> ()

type obox<'a> = OptionBox<'a>



/// トグルする
/// on'  オンにする時に実行される関数
/// off' オフにする時に実行する関数
type Toggler(on' :unit->unit, off': unit->unit) =
  let mutable _isOn = false
  member __.Toggle =
    if not _isOn then on'() else off'()
    _isOn <- not _isOn

  /// ONもしくはOFFにする
  member __.Set isOn =
    if isOn <> _isOn then __.Toggle

  member __.On  = __.Set true
  member __.Off = __.Set false
