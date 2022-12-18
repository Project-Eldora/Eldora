@echo off
 
robocopy "../BuiltPlugins" "%Appdata%/Eldora/Plugins" /mir 

exit /B 0