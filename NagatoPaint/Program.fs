module Program

open System         
open System.Windows            
open System.Windows.Controls      
open System.Windows.Input       
open System.Windows.Media
open System.Windows.Media.Imaging



let m = NpModel.Default

/// 画面全体
let dpMain = DockPanel() $ bgco Colors.LightGray
/// 左側のスタックパネル
let spLeft = SpLeft(m) $ dpMain.addLeft 
/// 右側のスタックパネル
let spRight = SpRight(m) $ dpMain.addRight
/// キャンバス
let canvas = NpCanvas(m) $ dpMain.add

/// メインウィンドウ
let win = Window(Content=dpMain,Title="NagatoPaint")

/// 状態の初期化
m.Initialize

[<EntryPoint>][<STAThread>]
let main _ = (Application()).Run win