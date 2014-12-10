@echo off

set a=%1

echo Project directory: "%a%"
echo Application data: "%AppData%"

echo copying StringSearch assembly to Revit 2011 and 2012 Addins folders...
copy %a%bin\Debug\ADNPlugin-StringSearch.dll "%AppData%\Autodesk\REVIT\Addins\2011"
copy %a%bin\Debug\ADNPlugin-StringSearch.dll "%AppData%\Autodesk\REVIT\Addins\2012"

echo copying %a%ADNPlugin-StringSearch.addin to top level directory...
copy %a%ADNPlugin-StringSearch.addin %a%..\..

echo copying %a%ADNPlugin-StringSearch.addin to "%AppData%\Autodesk\REVIT\Addins\2011"...
copy %a%ADNPlugin-StringSearch.addin "%AppData%\Autodesk\REVIT\Addins\2011"

echo copying %a%ADNPlugin-StringSearch.addin to "%AppData%\Autodesk\REVIT\Addins\2012"...
copy %a%ADNPlugin-StringSearch.addin "%AppData%\Autodesk\REVIT\Addins\2012"

echo copying %a%ReadMe.txt to parent directories...
copy %a%ReadMe.txt %a%..

copy %a%..\ReadMe.txt %a%..\..

rem cd %a%..\..
rem zip ..\zip\string_search ADNPlugin-StringSearch.addin ADNPlugin-StringSearch.dll ReadMe.txt Source\StringSearch.sln Source\StringSearch\

exit 0
