[<AutoOpen>]
module Color

open System
open System.Windows.Media

/// 16進数の文字を整数値に変換
let hexLetterToInt (c:char) =
  let code = int c
  if   Char.IsDigit  c then code - int '0'
  elif Char.IsLetter c then code - int 'a' + 10
  else 0

/// numeric literalで色を定義しようとしたが、なぜかliteralがintのまま
module NumericLiteralZ =
  let inline FromZero() = Colors.Black
  let inline FromOne()  = Color.FromRgb(0uy,0uy,1uy) 
  let inline FromString (s:string) =
    if s.Length > 6 then
      let f n digit = hexLetterToInt s.[n] * pown 16 digit |> byte
      let r = f 0 1 + f 1 0
      let g = f 2 1 + f 3 0
      let b = f 4 1 + f 5 0
      Color.FromRgb(r,g,b)
    else
      Colors.Black

  let inline FromInt32 i =
    let r = i / pown 256 2 |> byte
    let g = i / pown 256 1 |> byte
    let b = i |> byte
    Color.FromRgb(r,g,b)    

let color = 0Z    
let color2 = 1Z
let color3 = 10000Z