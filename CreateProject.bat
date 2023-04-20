SET DEFAULT_PROJECT=CleanArchitecture

cd CleanArchitecture

ren %DEFAULT_PROJECT%.Application %1.Application
ren %DEFAULT_PROJECT%.Domain %1.Domain
ren %DEFAULT_PROJECT%.Persistence %1.Persistence
ren %DEFAULT_PROJECT%.UnitTest %1.UnitTest
ren %DEFAULT_PROJECT%.WebAPI %1.WebAPI

fart %DEFAULT_PROJECT%.sln "%DEFAULT_PROJECT%." "%1."
fart .gitignore "%DEFAULT_PROJECT%." "%1."
ren %DEFAULT_PROJECT%.sln %1%.sln

cd %1.Application
..\fart %DEFAULT_PROJECT%.Application.csproj "<RootNamespace>%DEFAULT_PROJECT%." "<RootNamespace>%1."
..\fart %DEFAULT_PROJECT%.Application.csproj "<AssemblyName>%DEFAULT_PROJECT%." "<AssemblyName>%1."
..\fart %DEFAULT_PROJECT%.Application.csproj "%DEFAULT_PROJECT%.Domain" "%1.Domain"

..\fart Commands\UserCommands\CreateUserCommand\CreateUserCommand.cs "%DEFAULT_PROJECT%." "%1."
..\fart Commands\UserCommands\CreateUserCommand\CreateUserCommandHandler.cs "%DEFAULT_PROJECT%." "%1."
..\fart Commands\UserCommands\CreateUserCommand\CreateUserMapper.cs "%DEFAULT_PROJECT%." "%1."
..\fart Commands\UserCommands\CreateUserCommand\CreateUserResponse.cs "%DEFAULT_PROJECT%." "%1."
..\fart Commands\UserCommands\CreateUserCommand\CreateUserValidator.cs "%DEFAULT_PROJECT%." "%1."

..\fart Common\Behaviors\ValidatorBehavior.cs "%DEFAULT_PROJECT%." "%1."
..\fart Common\Exceptions\BadRequestException.cs "%DEFAULT_PROJECT%." "%1."
..\fart Common\Exceptions\NotFoundException.cs "%DEFAULT_PROJECT%." "%1."

..\fart Notifications\EmailNotifications\SendEmailNotification.cs "%DEFAULT_PROJECT%." "%1."
..\fart Notifications\EmailNotifications\SendEmailNotificationHandler.cs "%DEFAULT_PROJECT%." "%1."

..\fart Queries\UserQueries\GetAllUserQuery\GetAllUserQuery.cs "%DEFAULT_PROJECT%." "%1."
..\fart Queries\UserQueries\GetAllUserQuery\GetAllUserQueryHandler.cs "%DEFAULT_PROJECT%." "%1."
..\fart Queries\UserQueries\GetAllUserQuery\GetAllUserMapper.cs "%DEFAULT_PROJECT%." "%1."
..\fart Queries\UserQueries\GetAllUserQuery\GetAllUserResponse.cs "%DEFAULT_PROJECT%." "%1."

..\fart Repositories\IBaseRepository.cs "%DEFAULT_PROJECT%." "%1."
..\fart Repositories\IUnitOfWork.cs "%DEFAULT_PROJECT%." "%1."
..\fart Repositories\IUserRepository.cs "%DEFAULT_PROJECT%." "%1."

..\fart ServiceExtensions.cs "%DEFAULT_PROJECT%." "%1."

ren %DEFAULT_PROJECT%.Application.csproj %1%.Application.csproj

rmdir /s /q "obj"
rmdir /s /q "bin"

cd ..

cd %1.Domain
..\fart %DEFAULT_PROJECT%.Domain.csproj "<RootNamespace>%DEFAULT_PROJECT%." "<RootNamespace>%1."
..\fart %DEFAULT_PROJECT%.Domain.csproj "<AssemblyName>%DEFAULT_PROJECT%." "<AssemblyName>%1."

..\fart Common\BaseEntity.cs "%DEFAULT_PROJECT%." "%1."
..\fart Entities\User.cs "%DEFAULT_PROJECT%." "%1."

ren %DEFAULT_PROJECT%.Domain.csproj %1%.Domain.csproj

rmdir /s /q "obj"
rmdir /s /q "bin"

cd ..

cd %1.WebAPI
..\fart %DEFAULT_PROJECT%.WebAPI.csproj "<RootNamespace>%DEFAULT_PROJECT%." "<RootNamespace>%1."
..\fart %DEFAULT_PROJECT%.WebAPI.csproj "<AssemblyName>%DEFAULT_PROJECT%." "<AssemblyName>%1."
..\fart %DEFAULT_PROJECT%.WebAPI.csproj "%DEFAULT_PROJECT%.Application" "%1.Application"
..\fart %DEFAULT_PROJECT%.WebAPI.csproj "%DEFAULT_PROJECT%.Persistence" "%1.Persistence"

..\fart Web.config "%DEFAULT_PROJECT%." "%1."
..\fart Dockerfile "%DEFAULT_PROJECT%." "%1."
..\fart Program.cs "%DEFAULT_PROJECT%." "%1."
..\fart Controllers\UserController.cs "%DEFAULT_PROJECT%." "%1."
..\fart Extensions\ApiBehaviorExtensions.cs "%DEFAULT_PROJECT%." "%1."
..\fart Extensions\CorsPolicyExtensions.cs "%DEFAULT_PROJECT%." "%1."
..\fart Extensions\ErrorHandlerExtensions.cs "%DEFAULT_PROJECT%." "%1."
..\fart Properties\launchSettings.json "%DEFAULT_PROJECT%." "%1."

ren %DEFAULT_PROJECT%.WebAPI.csproj %1%.WebAPI.csproj

rmdir /s /q "obj"
rmdir /s /q "bin"

cd ..

cd %1.Persistence
..\fart %DEFAULT_PROJECT%.Persistence.csproj "<RootNamespace>%DEFAULT_PROJECT%." "<RootNamespace>%1."
..\fart %DEFAULT_PROJECT%.Persistence.csproj "<AssemblyName>%DEFAULT_PROJECT%." "<AssemblyName>%1."
..\fart %DEFAULT_PROJECT%.Persistence.csproj "%DEFAULT_PROJECT%.Application" "%1.Application"

..\fart Context\DataContext.cs "%DEFAULT_PROJECT%." "%1."
..\fart Repositories\BaseRepository.cs "%DEFAULT_PROJECT%." "%1."
..\fart Repositories\UnitOfWork.cs "%DEFAULT_PROJECT%." "%1."
..\fart Repositories\UserRepository.cs "%DEFAULT_PROJECT%." "%1."
..\fart ServiceExtensions.cs "%DEFAULT_PROJECT%." "%1."

ren %DEFAULT_PROJECT%.Persistence.csproj %1%.Persistence.csproj

rmdir /s /q "obj"
rmdir /s /q "bin"

cd ..

cd %1.UnitTest
..\fart %DEFAULT_PROJECT%.UnitTest.csproj "<RootNamespace>%DEFAULT_PROJECT%." "<RootNamespace>%1."
..\fart %DEFAULT_PROJECT%.UnitTest.csproj "<AssemblyName>%DEFAULT_PROJECT%." "<AssemblyName>%1."
..\fart %DEFAULT_PROJECT%.UnitTest.csproj "%DEFAULT_PROJECT%.Application" "%1.Application"
..\fart %DEFAULT_PROJECT%.UnitTest.csproj "%DEFAULT_PROJECT%.Domain" "%1.Domain"

ren %DEFAULT_PROJECT%.UnitTest.csproj %1%.UnitTest.csproj

rmdir /s /q "obj"
rmdir /s /q "bin"

cd ..


del fart.exe
del clean_architecture_bootstrap.bat
del clean_architecture_bootstrap_local.bat
del CreateProject.bat

REM Sai do diretorio ProjetoPadrao
cd ..

REM Cria um diretório com o nome do projeto
mkdir %1

REM Copia o conteúdo do diretório do projeto padrão já alterado para o diretório correto
xcopy /s ".\%DEFAULT_PROJECT%" %1

REM Remove o PeD que não é mais necessário
rmdir /s /q ".\%DEFAULT_PROJECT%"