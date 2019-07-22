@ECHO OFF
TITLE Gereciar Servico
ECHO.
GOTO MENU
:PAUSA
ECHO.
PAUSE
GOTO MENU
:MENU
CLS
ECHO.
ECHO ---------------------------------------------------------------------------------------------------------
ECHO GERENCIAMENTO DO SERVICO "UNINFE"
ECHO ---------------------------------------------------------------------------------------------------------
ECHO.
ECHO OPCOES:
ECHO.
ECHO         1 - Instalar o servico
ECHO         2 - Iniciar
ECHO         3 - Parar
ECHO         4 - Remover
ECHO         5 - Status
ECHO         0 - Sair
ECHO.
ECHO ---------------------------------------------------------------------------------------------------------
SET opcao=
SET /P opcao="Digite sua opcao (0 a 5) e tecle ENTER: "
ECHO.
ECHO.
IF "%opcao%" == "1" GOTO INSTALAR
IF "%opcao%" == "2" GOTO INICIAR
IF "%opcao%" == "3" GOTO PARAR
IF "%opcao%" == "4" GOTO REMOVER
IF "%opcao%" == "5" GOTO STATUS
IF "%opcao%" == "0" GOTO FIM
GOTO MENU
:INSTALAR
SET /P usuario="Digite a nome do usuário da rede: "
IF "%usuario%" == "" GOTO MENU
SET /P senha="Digite a senha do usuario %usuario%: "
IF "%senha%" == "" GOTO MENU
SC.EXE create UniNFeServico type= "own" binPath= "c:\unimake\uninfe\uninfeservico.exe" start= "auto" displayName= "UniNfe Servico" obj= "%usuario%" password= "%senha%"
IF ERRORLEVEL 1 GOTO PAUSA
SC.EXE description UniNFeServico "Executa comunicacoes entre o ERP e a SEFAZ"
IF ERRORLEVEL 1 GOTO PAUSA
SC.EXE start UniNFeServico
GOTO Pausa
:INICIAR
SC.EXE start UniNFeServico
GOTO Pausa
:PARAR
SC.EXE stop UniNFeServico
GOTO Pausa
:REMOVER
SC.EXE stop UniNFeServico
SC.EXE delete UniNFeServico
GOTO Pausa
:STATUS
SC.EXE query UniNFeServico
GOTO Pausa
:FIM
SET usuario=
SET senha=
SET opcao=