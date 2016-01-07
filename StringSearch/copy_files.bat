@echo off

set a=%1

echo Project directory: "%a%"
echo Application data: "%AppData%"

echo Copying StringSearch assembly and add-in manifest to Revit Addins folder...
copy %a%bin\Debug\ADNPlugin-StringSearch.dll "%AppData%\Autodesk\REVIT\Addins\2015"
copy %a%ADNPlugin-StringSearch.addin "%AppData%\Autodesk\REVIT\Addins\2015"

rem echo copying StringSearch assembly and add-in manifest to top level directory...
rem copy %a%bin\Debug\ADNPlugin-StringSearch.dll %a%..
rem copy %a%ADNPlugin-StringSearch.addin %a%..

rem echo copying %a%ReadMe.txt to parent directory...
rem copy %a%ReadMe.txt %a%..

rem cd %a%..\..
rem zip ..\zip\string_search ADNPlugin-StringSearch.addin ADNPlugin-StringSearch.dll ReadMe.txt Source\StringSearch.sln Source\StringSearch\

exit 0
