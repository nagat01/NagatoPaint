[<AutoOpen>]
module ToolUi

open System.Windows.Controls

type ToolPanel(curTool:PaintTool so) as sp =
  inherit StackPanel()
  do sp |> sp_horizontal

  let 現在ツール = Label() $ sp.add
  do curTool.Changed =>~ fun _ -> 
    現在ツール.Content <- string curTool.V
  
  let スプレー = Button(Content="スプレー") $ sp.add
  do スプレー.Click =>~ fun _ -> 
    curTool.Replace PaintTool.Spray

  let 消しゴム = Button(Content="消しゴム") $ sp.add
  do 消しゴム.Click =>~ fun _ ->
    curTool.Replace PaintTool.Eraser 