#!/usr/bin/env bash
sleep 20
./opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P adminadminadmin123 -i init.sql