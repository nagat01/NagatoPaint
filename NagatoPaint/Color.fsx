#r "WindowsBase"
#r "PresentationCore"
#r "PresentationFramework"
#r "UIAutomationTypes"
#r "System.Xaml"

#r @"C:\NagatoPaint\NPLib\bin\Debug\NPLib.dll"
#r @"C:\NagatoPaint\NagatoPaint\bin\Debug\NagatoPaint.exe"

open System.Windows
open Np

let win = System.Windows.Window() $ bgco (Color.ofHex 0xff0000)
win.Show()
