module Program

open System         
open System.Windows            
open System.Windows.Controls      
open System.Windows.Input       
open System.Windows.Media
open System.Windows.Media.Imaging

open Np




/// 画面全体
let dpMain = DockPanel() $ bgco Colors.Black
/// 左側のスタックパネル
let spLeft = SpLeft() $ dpMain.addLeft
label |> dpMain.add

let win = Window(Content=dpMain)

[<EntryPoint>][<STAThread>]
let main _ = (Application()).Run win