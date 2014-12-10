@echo off

set a=%1

echo Project directory: "%a%"
echo Application data: "%AppData%"

echo copying StringSearch assembly and add-in manifest to Revit 2013 Addins folder...
copy %a%bin\Debug\ADNPlugin-StringSearch.dll "%AppData%\Autodesk\REVIT\Addins\2013"
copy %a%ADNPlugin-StringSearch.addin "%AppData%\Autodesk\REVIT\Addins\2013"

echo copying StringSearch assembly and add-in manifest to top level directory...
copy %a%bin\Debug\ADNPlugin-StringSearch.dll %a%..
copy %a%ADNPlugin-StringSearch.addin %a%..

echo copying %a%ReadMe.txt to parent directory...
copy %a%ReadMe.txt %a%..

rem cd %a%..\..
rem zip ..\zip\string_search ADNPlugin-StringSearch.addin ADNPlugin-StringSearch.dll ReadMe.txt Source\StringSearch.sln Source\StringSearch\

exit 0
