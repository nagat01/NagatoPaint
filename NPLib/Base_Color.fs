// [<AutoOpen>]
module Np.Color

open System
open System.Windows.Media

/// 16進数の文字 -> int
let hexLetterToInt (c:char) =
  let code = int c
  if   Char.IsDigit  c then code - int '0'
  elif Char.IsLetter c then code - int 'a' + 10
  else 0

/// 16進数 -> Color
let ofHex i =
  let r = i / pown 256 2 |> byte
  let g = i / 256 |> byte
  let b = i |> byte
  Color.FromRgb(r,g,b)
  
/// Color -> int
let toInt (co:Color) =
  (int co.A <<< 24) + 
  (int co.R <<< 16) + 
  (int co.G <<<  8) + 
  int co.B

// RGB値のどれかを得る
let toR (co:Color) = co.R
let toG (co:Color) = co.G
let toB (co:Color) = co.B

// RGB値のどれかを変更する
let mapR f (co:Color) = Color.FromRgb(f co.R, co.G, co.B)
let mapG f (co:Color) = Color.FromRgb(co.R, f co.G, co.B)
let mapB f (co:Color) = Color.FromRgb(co.R, co.G, f co.B)

/// float型のRGB値からColorを作る
let ofFloats r g b = 
  Color.FromScRgb(1.0f, float32 r, float32 g, float32 b)

/// float型のRGB値を得る               
let toFloats (co:Color) =
  let f byte = float byte / 255.
  f co.R , f co.G , f co.B

/// HSLからColorを作る
let fromHsl h s l : Color =
  let v = if l <= 0.5 then l * (1.0 + s) else l + s - l * s
  if v = 0.0 then ofFloats l l l else
    let m = l * 2.0 - v
    let sv = (v - m) / v
    let h = h * 6.0
    let sextant = int h
    let fract = fraction h
    let vsf = v * sv * fract
    let mid1 = m + vsf
    let mid2 = v - vsf
    match sextant with
      | 0 -> v,    mid1, m
      | 1 -> mid2, v,    m
      | 2 -> m,    v,    mid1
      | 3 -> m,    mid2, v
      | 4 -> mid1, m,    v
      | _ -> v,    m,    mid1
    |||> ofFloats

/// ColorからHSLを得る
let toHsl (color:Color) =
  let r , g , b = toFloats color
  let v = r |> max g |> max b
  let m = r |> min g |> min b
  let l = (m + v) / 2.0
  if l <= 0.0 then 0., 0., l else
    let vm = v - m
    let s = 
      if vm > 0.0 then
        if l <= 0.5 then vm / (v+m) else vm / (2.0 - v - m)
      else vm
    let r2 = (v - r) / vm
    let g2 = (v - g) / vm
    let b2 = (v - b) / vm
    let h =
      if r = v then
        if g = m then 5.0 + b2 else 1.0 - g2
      elif g = v then
        if b = m then 1.0 + r2 else 3.0 - b2
      else
        if r = m then 3.0 + g2 else 5.0 - r2
    let h = h / 6.0      
    h, s, l

/// HSL値を変更する
let mapH f co = let h, s, l = toHsl co in fromHsl (f h) s l
let mapS f co = let h, s, l = toHsl co in fromHsl h (f s) l
let mapL f co = let h, s, l = toHsl co in fromHsl h s (f l)

/// HSL値のどれかを得る
let toH co = let h,_,_ = toHsl co in h
let toS co = let _,s,_ = toHsl co in s
let toL co = let _,_,l = toHsl co in l