set /p ProjectName="Project Name:"

echo mkdir CleanArchitecture

robocopy .\CleanArchitectureTemplate .\CleanArchitecture /E

copy .\CleanArchitecture\CreateProject.bat
cmd /C CreateProject.bat %ProjectName%
del CreateProject.bat