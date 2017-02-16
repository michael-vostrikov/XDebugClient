# XDebugClient

Copy "XDebugClient.dll" to Notepad++ plugins directory.  
Open debug panel.  
Set up XDebug as described in XDebug manual.  
Set breakpoint in script that will be executed by pressing "F9" on required line.  
Open script page in browser with GET-parameter "XDEBUG_SESSION_START=1".  
Any value can be used instead of "1".  
Execution should be stopped on selected line.  
Notepad++ window will be highlighted on connection.  

Keys:

F9 - Set breakpoint  
F10 - Step over  
F11 - Step into  
Shift+F11 - Step out  
F5 - Run  
Shift+F5 - Stop  
Insert - Add watch  
F2 - Edit watch  
Ctrl+C - Copy selected value to clipboard  
