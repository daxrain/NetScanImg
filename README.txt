INSALL THE SERVICE:
Open a command prompt as an Administrator
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe + Your copied path + \your service name + .exe
If you want to uninstall your service, fire the below command.
Syntax InstallUtil.exe -u + Your copied path + \your service name + .exe
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -u C:\Users\Faisal-Pathan\source\repos\MyFirstService\MyFirstService\bin\Debug\MyFirstService.exe

References:
https://ourcodeworld.com/articles/read/382/creating-a-scanning-application-in-winforms-with-csharp
https://www.c-sharpcorner.com/article/create-windows-services-in-c-sharp/
https://www.cyotek.com/blog/an-introduction-to-using-windows-image-acquisition-wia-via-csharp
https://social.msdn.microsoft.com/Forums/windows/en-US/2d5a6fbf-f1ba-44a3-a957-e76dd2800262/interopwia-version10-unable-to-set-wiapageauto-property?forum=csharpgeneral
https://bjdejongblog.nl/scanning-an-image-wth-csharp/
https://stackoverflow.com/questions/55049546/generating-photo-quality-scans-with-wia-in-c-sharp
https://learn.microsoft.com/en-us/windows/win32/wia/-wia-wiaitempropscanneritem
https://stackoverflow.com/questions/56360036/crop-correct-part-of-image-while-the-picturebox-is-in-zoom-mode (Fantastico! Geniale!)