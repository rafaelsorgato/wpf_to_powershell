# wpf_to_powershell
Convert a wpf screen to a powershell screen. An easy and free way to create screens in powershell<img src="https://i.gifer.com/origin/dc/dc9122a4c67ff1272971880b17b21ce3_w200.gif" width=45px>

<a href="https://github.com/rafaelsorgato/wpf_to_powershell/tree/master/Powershell_Screen_Converter">Click here if you want the visual studio c# project<a/>
<br><br>
<a href="https://github.com/rafaelsorgato/wpf_to_powershell/blob/master/Powershell_Screen_Converter.exe">Click here to download the converter</a>
<br><br>
<h3>Usage</h3>
<br><br>
Start a new c# WPF project, you can put buttons, calendar, text, etc, (give a name for these elements)... But don't create functions!!! These functions you will create in powershell, otherwise there would be no need to convert to powershell, right?
<img src="https://github.com/rafaelsorgato/images_videos_of_my_projects/blob/main/images/wpftopowershell1.png">
<br><br>
After you finish, just copy the XAML
<img src="https://github.com/rafaelsorgato/images_videos_of_my_projects/blob/main/images/wpftopowershell2.png">
<br><br>
Now just paste XAML it in the <a href="https://github.com/rafaelsorgato/wpf_to_powershell/blob/master/Powershell_Screen_Converter.exe">converter</a> and click "convert", you can just copy the text and paste in powershell or just click in "save"
<img src="https://github.com/rafaelsorgato/images_videos_of_my_projects/blob/main/videos/wpftopowershell.gif">
<br><br>
<bold>Consideration!</bold>
<br><br>
Remembering that you must make the functions for what each button or text must do, the generated code has a tutorial at the end, but basically you must create your element's variable and create the function, example:
<br><br>
creating the var for button<br>
$button = $xamlForm.FindName("button")
<br><br>
creating the function when you click the button<br>
$button.Add_Click({
  do something
})
