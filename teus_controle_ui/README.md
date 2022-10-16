# teus_controle_ui

A new Flutter project.

## Getting Started

This project is a starting point for a Flutter application.

A few resources to get you started if this is your first Flutter project:

- [Lab: Write your first Flutter app](https://flutter.dev/docs/get-started/codelab)
- [Cookbook: Useful Flutter samples](https://flutter.dev/docs/cookbook)

For help getting started with Flutter, view our
[online documentation](https://flutter.dev/docs), which offers tutorials,
samples, guidance on mobile development, and a full API reference.


Estrutura do projeto
https://femaletechentrepreneur.com/flutter-scalable-app-folder-structure/

Gerar arquivos de JsonSerializable
flutter pub run build_runner build

cria migration
add-migration NomeMigration

desfaz criação
Remove-Migration

atualiza base
update-database


publishing to heroku

nome da aplicacao
teuscontroleapi

>heroku auth:token
- pegar token e usar para logar no docker 
>docker login --password="0152bd2e-776d-4721-afdc-9fb236585a30" registry.heroku.com

add container para push
>docker tag teuscontroleapi registry.heroku.com/teuscontroleapi/web

realiza push
>docker push registry.heroku.com/teuscontroleapi/web

release a versao
>heroku container:release web -a teuscontroleapi



docker-compose build

docker build --no-cache -f Dockerfile -t teuscontroleapi:1.0.0 .. 




flutter build windows - > gera D:\Meus Docs\Desktop\4ano\TCC\TeusControle\teus_controle_ui\build\windows\runner\Release

- utilizar o app "inno setup compiler" para criar instalador
- existe o script na raiz do projeto na pasta installer para criar o exe do instalador
