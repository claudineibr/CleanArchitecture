SET PROJETO_PADRAO=CleanArchitecture

cd CleanArchitecture

ren %PROJETO_PADRAO%.Application %1.Application
ren %PROJETO_PADRAO%.Domain %1.Domain
ren %PROJETO_PADRAO%.Persistence %1.Persistence
ren %PROJETO_PADRAO%.UnitTest %1.UnitTest
ren %PROJETO_PADRAO%.WebAPI %1.WebAPI

fart %PROJETO_PADRAO%.sln "%PROJETO_PADRAO%." "%1."
fart .gitignore "%PROJETO_PADRAO%." "%1."
ren %PROJETO_PADRAO%.sln %1%.sln

cd %1.Application
..\fart %PROJETO_PADRAO%.Application.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Application.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.Application.csproj "%PROJETO_PADRAO%.Domain" "%1.Domain"

..\fart Common\Behaviors\ValidatorBehavior.cs "%PROJETO_PADRAO%." "%1."
..\fart Common\Exceptions\BadRequestException.cs "%PROJETO_PADRAO%." "%1."
..\fart Common\Exceptions\NotFoundException.cs "%PROJETO_PADRAO%." "%1."

..\fart Features\UserFeatures\CreateUser\CreateUserHandler.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\CreateUser\CreateUserMapper.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\CreateUser\CreateUserRequest.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\CreateUser\CreateUserResponse.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\CreateUser\CreateUserValidator.cs "%PROJETO_PADRAO%." "%1."

..\fart Features\UserFeatures\GetAllUser\GetAllUserHandler.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\GetAllUser\GetAllUserMapper.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\GetAllUser\GetAllUserRequest.cs "%PROJETO_PADRAO%." "%1."
..\fart Features\UserFeatures\GetAllUser\GetAllUserResponse.cs "%PROJETO_PADRAO%." "%1."

..\fart Repositories\IBaseRepository.cs "%PROJETO_PADRAO%." "%1."
..\fart Repositories\IUnitOfWork.cs "%PROJETO_PADRAO%." "%1."
..\fart Repositories\IUserRepository.cs "%PROJETO_PADRAO%." "%1."

..\fart ServiceExtensions.cs "%PROJETO_PADRAO%." "%1."

ren %PROJETO_PADRAO%.Application.csproj %1%.Application.csproj
cd ..

cd %1.Domain
..\fart %PROJETO_PADRAO%.Domain.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Domain.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."

..\fart Common\BaseEntity.cs "%PROJETO_PADRAO%." "%1."
..\fart Entities\User.cs "%PROJETO_PADRAO%." "%1."
ren %PROJETO_PADRAO%.Domain.csproj %1%.Domain.csproj
cd ..

cd %1.WebAPI
..\fart %PROJETO_PADRAO%.WebAPI.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.WebAPI.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.WebAPI.csproj "%PROJETO_PADRAO%.Application" "%1.Application"
..\fart %PROJETO_PADRAO%.WebAPI.csproj "%PROJETO_PADRAO%.Persistence" "%1.Persistence"

..\fart Web.config "%PROJETO_PADRAO%." "%1."
..\fart Dockerfile "%PROJETO_PADRAO%." "%1."
..\fart Program.cs "%PROJETO_PADRAO%." "%1."
..\fart Controllers\UserController.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\ApiBehaviorExtensions.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\CorsPolicyExtensions.cs "%PROJETO_PADRAO%." "%1."
..\fart Extensions\ErrorHandlerExtensions.cs "%PROJETO_PADRAO%." "%1."

ren %PROJETO_PADRAO%.WebAPI.csproj %1%.WebAPI.csproj
cd ..

cd %1.Persistence
..\fart %PROJETO_PADRAO%.Persistence.csproj "<RootNamespace>%PROJETO_PADRAO%." "<RootNamespace>%1."
..\fart %PROJETO_PADRAO%.Persistence.csproj "<AssemblyName>%PROJETO_PADRAO%." "<AssemblyName>%1."
..\fart %PROJETO_PADRAO%.Persistence.csproj "%PROJETO_PADRAO%.Application" "%1.Application"

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
..\fart %PROJETO_PADRAO%.UnitTest.csproj "%PROJETO_PADRAO%.Domain" "%1.Domain"

ren %PROJETO_PADRAO%.UnitTest.csproj %1%.UnitTest.csproj
cd ..


del fart.exe
del clean_architecture_bootstrap.bat
del clean_architecture_bootstrap_local.bat
del criaProjeto.bat

REM Sai do diretorio ProjetoPadrao
cd ..

REM Cria um diretório com o nome do projeto
mkdir %1

REM Copia o conteúdo do diretório do projeto padrão já alterado para o diretório correto
xcopy /s ".\%PROJETO_PADRAO%" %1

REM Remove o PeD que não é mais necessário
rmdir /s /q ".\%PROJETO_PADRAO%"