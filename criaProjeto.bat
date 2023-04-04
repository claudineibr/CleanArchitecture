SET PROJETO_PADRAO=CleanArchitecture

cd CleanArchitecture

ren %PROJETO_PADRAO%.Application %1.Application
ren %PROJETO_PADRAO%.Domain %1.Domain
ren %PROJETO_PADRAO%.Persistence %1.Persistence
ren %PROJETO_PADRAO%.UnitTest %1.UnitTest
ren %PROJETO_PADRAO%.WebApi %1.WebApi

fart %PROJETO_PADRAO%.sln "%PROJETO_PADRAO%." "%1."
fart .gitignore "%PROJETO_PADRAO%." "%1."
ren %PROJETO_PADRAO%.sln %1%.sln

cd %1.Application
..\fart %PROJETO_PADRAO%.Application.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Application.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.Application.csproj "%PROJETO_PADRAO%.Domain" "%1.Domain"

..\fart Common\Behaviors\ValidationBehavior.cs "%PROJETO_PADRAO%." "%1."
..\fart Common\Exceptions\BadRequestException.cs "%PROJETO_PADRAO%." "%1."
..\fart Common\Exceptions\NotFoundException.cs "%PROJETO_PADRAO%." "%1."
ren %PROJETO_PADRAO%.Application.csproj %1%.Application.csproj
cd ..

cd %1.Domain
..\fart %PROJETO_PADRAO%.Domain.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Domain.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."

..\fart Common\BaseEntity.cs "%PROJETO_PADRAO%." "%1."
..\fart Entities\User.cs "%PROJETO_PADRAO%." "%1."
ren %PROJETO_PADRAO%.Domain.csproj %1%.Domain.csproj
cd ..

cd %1.WebApi
..\fart %PROJETO_PADRAO%.WebApi.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.WebApi.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.WebApi.csproj "%PROJETO_PADRAO%.Application" "%1.Application"
..\fart %PROJETO_PADRAO%.WebApi.csproj "%PROJETO_PADRAO%.Persistence" "%1.Persistence"

..\fart Web.config "%PROJETO_PADRAO%." "%1."
..\fart Dockerfile "%PROJETO_PADRAO%." "%1."
..\fart Program.cs "%PROJETO_PADRAO%." "%1."
..\fart Controllers\UserController.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\ApiBehaviorExtensions.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\CorsPolicyExtensions.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\ErrorHandlerExtensions.cs "%PROJETO_PADRAO%." "%1."

ren %PROJETO_PADRAO%.WebApi.csproj %1%.WebApi.csproj
cd ..

cd %1.Persistence
..\fart %PROJETO_PADRAO%.Persistence.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Persistence.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.Persistence.csproj "%PROJETO_PADRAO%.Domain" "%1.Domain"

..\fart Context\DataContext.cs "%PROJETO_PADRAO%." "%1."
..\fart Repositories\BaseRepository.cs "%PROJETO_PADRAO%." "%1."
..\fart Repositories\UnitOfWork.cs "%PROJETO_PADRAO%." "%1."
..\fart Repositories\UserRepository.cs "%PROJETO_PADRAO%." "%1."
..\fart ServiceExtensions.cs "%PROJETO_PADRAO%." "%1."

ren %PROJETO_PADRAO%.Persistence.csproj %1%.Persistence.csproj
cd ..

cd %1.UnitTest
..\fart %PROJETO_PADRAO%.UnitTest.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.UnitTest.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.UnitTest.csproj "%PROJETO_PADRAO%.Application" "%1.Application"

ren %PROJETO_PADRAO%.UnitTest.csproj %1%.UnitTest.csproj
cd ..

cd ..
..\fart compile.bat ".\%PROJETO_PADRAO%.WebApi" ".\%1.WebApi"

del fart.exe
del boilerplate.bat
del criaProjeto.bat

REM Sai do diretorio ProjetoPadrao
cd ..

REM Cria um diretório com o nome do projeto
mkdir %1

REM Copia o conteúdo do diretório do projeto padrão já alterado para o diretório correto
xcopy /s ".\%PROJETO_PADRAO%" %1

REM Remove o PeD que não é mais necessário
rmdir /s /q ".\%PROJETO_PADRAO%"