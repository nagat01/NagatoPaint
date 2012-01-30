#r "WindowsBase"
#r "PresentationCore"
#r "PresentationFramework"
#r "System.Xaml"

module Scripts =
  open System
  let loadFilesToClipboard directory =
    let directory = IO.DirectoryInfo(directory)
    seq {
    for file in directory.GetFiles() do
      if file.Extension = ".fsproj" then
        let file = IO.File.ReadAllText(file.FullName)
        let matches = 
          System.Text.RegularExpressions.Regex.Matches(
            file,
            "<Compile Include=\"(\w+)\.fs\" />"
          )
        for m in matches ->
          sprintf "#load \"%s\\%s.fs\"\n" directory.FullName m.Groups.[1].Value
    } 
    |> String.Concat
    |> System.Windows.Clipboard.SetText
// 例
// Scripts.loadFilesToClipboard @"C:\NagatoPaint\Library"
