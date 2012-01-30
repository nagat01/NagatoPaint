module Program

open System         
open System.Windows            
open System.Windows.Controls      
open System.Windows.Input       
open System.Windows.Media
open System.Windows.Media.Imaging

open Base
open Wpf



/// 画面全体
let dp全体 = DockPanel() $ bgco Colors.Black
sp左 |> dp全体.addLeft
label |> dp全体.add

let win = Window(Content=dp全体)

[<EntryPoint>][<STAThread>]
let main _ = (Application()).Run win