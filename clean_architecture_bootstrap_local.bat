set /p NomeProjeto="Informe o nome do projeto:"

echo mkdir CleanArchitecture

robocopy .\CleanArchitectureTemplate .\CleanArchitecture /E

copy .\CleanArchitecture\criaProjeto.bat
cmd /C criaProjeto.bat %NomeProjeto%
del criaProjeto.bat